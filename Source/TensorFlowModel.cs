
using System;
using Tensorflow;
using Tensorflow.Keras.Engine;
using Tensorflow.NumPy;
using text_captcha_solver.Source;
namespace captcha_solver.Source
{

	public static class TensorFlowModel
	{
		public static int ModelInputDim = 50;

		public static IModel BuildModel()
		{
			var ft = KerasApi.keras.layers;
			var inputs = KerasApi.keras.Input(shape: (ModelInputDim, ModelInputDim, 3), dtype: TF_DataType.TF_FLOAT);

			var x = ft.Conv2D(32, (3, 3), activation: "relu", padding: "VALID").Apply(inputs);
			x = ft.MaxPooling2D((2, 2), padding: "VALID").Apply(x);

			x = ft.Conv2D(64, (3, 3), activation: "relu", padding: "VALID").Apply(x);
			x = ft.MaxPooling2D((2, 2), padding: "VALID").Apply(x);

			x = ft.Flatten().Apply(x);
			x = ft.Dense(128, activation: "relu").Apply(x);
			x = ft.Dropout(0.5f).Apply(x);

			var output = ft.Dense(Captcha.Letters.Length, activation: "softmax").Apply(x);

			var model = KerasApi.keras.Model(inputs, output, name: "Single-Character-Recognizer");

			model.compile(
				optimizer: KerasApi.keras.optimizers.Adam(),
				loss: KerasApi.keras.losses.SparseCategoricalCrossentropy(),
				metrics: new[] { "accuracy" }
			);

			Console.WriteLine("Model created.");

			return model;
		}

		public static void TrainModel(
			IModel model,
			NDArray images,
			NDArray labels,
			int epochs = 32,
			int batchSize = 32,
			float validationSplit = 0.2f
			)
		{
			model.fit(
				images,
				labels,
				epochs: epochs,
				batch_size: batchSize,
				validation_split: validationSplit,
				use_multiprocessing: true
				);
			Console.WriteLine("Model trained.");
		}

		public static void LoadModel(IModel model, string modelFile)
		{
			if (string.IsNullOrEmpty(modelFile))
				return;
			model.load_weights(modelFile);
			var stats = Options.GetStatsFromFileName(modelFile);
			Console.WriteLine("Model loaded; acc: " + stats.acc + " loss: " + stats.loss + " date: " + stats.date);
		}

		public static void SaveModel(IModel model, double acc, double loss)
		{
			var modelFile = Options.GetModelFile(acc,loss);
			model.save_weights(modelFile);
			Console.WriteLine("Model saved : " + modelFile);
		}

		public static char PredictCharacter(IModel model, NDArray image)
		{
			var predictionTensor = model.predict(image);

			var predictionArray = predictionTensor.numpy();

			NDArray probabilities;
			if (predictionArray.shape.Length == 2)
			{
				probabilities = predictionArray[0];
			}
			else
			{
				probabilities = predictionArray;
			}

			int maxIndex = np.argmax(probabilities).numpy();
			return Captcha.Letters[maxIndex];
		}
	}

}

