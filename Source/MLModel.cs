using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Linq;

namespace text_captcha_solver.Source
{
	internal class MLModel
	{
		public class CaptchaData
		{
			[LoadColumn(0)]
			public string ImageBase64 { get; set; }

			[LoadColumn(1)]
			public string Label { get; set; }
		}

		public class CaptchaImage
		{
			public byte[] ImageData { get; set; }
		}

		public class ImageData
		{
			public string ImagePath { get; set; }
			public string Label { get; set; }
		}

		public class CaptchaPrediction
		{
			[ColumnName("PredictedLabel")]
			public string PredictedLabel { get; set; }
		}

		public static void TrainModel(string dataPath = "data/simpledataset.csv", string modelPath = "simplemodel.zip")
		{
			// Create MLContext
			var mlContext = new MLContext();

			// Load data
			var imagesFolder = "data/images";
			var dataView = mlContext.Data.LoadFromTextFile<CaptchaData>(dataPath, separatorChar: ',', hasHeader: false);

			// Define pipeline
			var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
						 .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: 100, imageHeight: 25))
						 .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input"))
						 //.Append(mlContext.Model.LoadTensorFlowModel("modelPath").
							//				   .ScoreTensorName("output")
							//				   .AddInput("input", 100, 25, 3))
						 .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "PredictedLabel", inputColumnName: "Label"))
						 .Append(mlContext.Transforms.CopyColumns(outputColumnName: "Features", inputColumnName: "input"))
						 .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
						 .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedLabel"));

			// Train model
			var model = pipeline.Fit(dataView);

			// Save model
			mlContext.Model.Save(model, dataView.Schema, modelPath);
		}

		public static void Predict(string imagePath, string modelPath)
		{
			// Load model
			var mlContext = new MLContext();
			ITransformer model = mlContext.Model.Load(modelPath, out var modelInputSchema);

			// Load image
			var imageData = File.ReadAllBytes(imagePath);

			// Create prediction engine
			var predictionEngine = mlContext.Model.CreatePredictionEngine<CaptchaData, CaptchaPrediction>(model);

			// Make prediction
			var prediction = predictionEngine.Predict(new CaptchaData { ImageBase64 = Convert.ToBase64String(imageData) });

			// Print prediction
			Console.WriteLine($"Predicted label: {prediction.PredictedLabel}");
		}

		private static string ConvertBase64ToTempFile(string base64String)
		{
			byte[] imageBytes = Convert.FromBase64String(base64String);
			string tempFileName = Path.GetTempFileName();
			File.WriteAllBytes(tempFileName, imageBytes);
			return tempFileName;
		}
	}
}