
using captcha_solver.Source;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace text_captcha_solver.Source
{
	public class Options
	{
		public static string rootPath { get; set; }
		public static string samplePath => Path.Combine(rootPath, "samples");

		public static string modelPath => Path.Combine(rootPath, "models");
		public static string GetModelFile(double acc, double loss)
		{
			var multiplier = 1000;
			var accStr = (int)(acc * multiplier);
			var lossStr = (int)(loss * multiplier);
			var date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
			var filename = $"model_{accStr}_{lossStr}_{date}.h5";
			return Path.Combine(modelPath, filename);
		}

		public static ModelStats GetStatsFromFileName(string modelFile)
		{
			var parts = modelFile.Split('_');
			var acc = double.Parse(parts[1]) / 1000;
			var loss = double.Parse(parts[2]) / 1000;
			return new ModelStats
			{
				acc = acc,
				loss = loss,
				date = parts[3]
			};
		}

		public static string GetBestModelFile()
		{
			var modelFiles = Directory.GetFiles(modelPath);
			if (modelFiles.Length == 0)
			{
				return null;
			}
			return modelFiles.OrderBy(x =>
			{
				var parts = x.Split('_');
				var acc = int.Parse(parts[1]);
				return acc;
			}).Last();
		}

		public static string GetLastModelFile()
		{
			var modelFiles = Directory.GetFiles(modelPath);
			if (modelFiles.Length == 0)
			{
				return null;
			}
			return modelFiles.OrderBy(x =>
			{
				var parts = x.Split('_');
				var date = DateTime.Parse(parts[3]);
				return date;
			}).Last();
		}

		public static string[] GetModelFiles() => Directory.GetFiles(modelPath);

		public int epoch { get; set; }
		public int batch { get; set; }
		public int repeat { get; set; }
		public float validationSplit { get; set; }
		public string sampleLetters { get; set; }

		public void SaveOptions()
		{
			var jsonData = JsonConvert.SerializeObject(this);
			File.WriteAllText(Path.Combine(rootPath, "options.json"), jsonData);
		}

		public static Options GetOptions()
		{
			Options.rootPath = Directory.Exists("../../Data") ? "../../Data" : "";
			Directory.CreateDirectory(rootPath);
			Directory.CreateDirectory(samplePath);
			Directory.CreateDirectory(modelPath);

			try
			{
				var jsonData = File.ReadAllText(Path.Combine(rootPath, "options.json"));
				var options = JsonConvert.DeserializeObject<Options>(jsonData);
				return options;
			}
			catch
			{
				return new Options
				{
					epoch = 32,
					batch = 32,
					repeat = 25,
					validationSplit = 0.2f,
					sampleLetters = GenerateDatas.GenerateLettersForSample()
				};
			}
		}
	}

	public class ModelStats
	{
		public double acc { get; set; }
		public double loss { get; set; }
		public string date { get; set; }
	}
}
