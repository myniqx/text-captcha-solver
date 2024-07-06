using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow;
using Tensorflow.NumPy;

namespace captcha_solver.Source
{
	internal class GenerateDatas
	{
		private static void updateBar(ProgressBar pb)
		{
			if (pb == null)
				return;

			pb.BeginInvoke(new Action(() => pb.Increment(1)));
		}

		private static void setupBar(ProgressBar pb, int max)
		{
			if (pb == null)
				return;

			pb.BeginInvoke(new Action(() => { pb.Minimum = 0; pb.Maximum = max; pb.Value = 0; }));
		}

		private static void maxBar(ProgressBar pb)
		{
			if (pb == null)
				return;

			pb.BeginInvoke(new Action(() => pb.Value = pb.Maximum));
		}
		public static async Task ClearSampleFolder(ProgressBar pb = null, string sampleFolder = "../sample_data")
		{
			var files = Directory.GetFiles(sampleFolder);
			setupBar(pb, files.Length);
			var tasks = files.Select(file => Task.Run(() =>
				{
					File.Delete(file);
					updateBar(pb);
				}));
			await Task.WhenAll(tasks);
			maxBar(pb);
			Console.WriteLine("Sample folder cleared successfully");
		}

		/// <summary>
		/// Creates sample captcha images for the specified letters.
		/// </summary>
		/// <param name="pb">Optional progress bar to show the generation progress.</param>
		/// <param name="repeat">Number of times each letter's captcha should be repeated.</param>
		/// <param name="sampleLetters">Letters to generate captchas for. If null, uses default letters.</param>
		/// <param name="sampleFolder">The folder path to save the generated captchas.</param>
		public static void CreateSampleFolder(ProgressBar pb = null, int repeat = 50, string sampleLetters = null, string sampleFolder = "../sample_data")
		{
			if (!Directory.Exists(sampleFolder))
			{
				Directory.CreateDirectory(sampleFolder);
			}

			var letters = (string.IsNullOrEmpty(sampleLetters) ? Captcha.Letters : sampleLetters).ToUpper();
			var targetDim = TensorFlowModel.ModelInputDim;
			setupBar(pb, letters.Length * repeat);
			Console.WriteLine("Creating sample folder...");
			Console.WriteLine("Target sample count : " + (letters.Length * repeat));

			Parallel.ForEach(letters, letter =>
			{
				Console.WriteLine("Creating sample for letter: " + letter);
				var code = $"{letter}{letter}{letter}{letter}";
				var tasks = new List<Task>();

				for (int rep = 0; rep < repeat; rep++)
				{
					tasks.Add(Task.Run(() =>
					{
						var captcha = Captcha.GenerateCaptchaImage(200, 50, code);
						using (var resizedImg = new Bitmap(targetDim, targetDim)) // Create Bitmap outside the loop
						{
							using (var g = Graphics.FromImage(resizedImg))
							{
								for (int i = 0; i < 4; i++)
								{
									var cropRect = new Rectangle(i * 50, 0, 50, 50);
									g.Clear(Color.Transparent);
									g.DrawImage(
										captcha,
										new Rectangle(0, 0, targetDim, targetDim),
										cropRect,
										GraphicsUnit.Pixel);

									var fileName = $"{code[i]}-{Guid.NewGuid():N}.png";
									var filePath = Path.Combine(sampleFolder, fileName);

									resizedImg.Save(filePath, ImageFormat.Png);
								}
							}
						}
						captcha.Dispose();
						updateBar(pb);
					}));
				}
				Task.WaitAll(tasks.ToArray());
			});

			maxBar(pb);
			Console.WriteLine("Sample folder created successfully");
		}

		public static string GenerateLettersForSample()
		{
			var letters = Captcha.Letters;
			letters += "YYYYY";
			letters += "XXXXX";
			letters += "WWWWW";
			letters += "33333";
			return letters;
		}

		/// <summary>
		/// Loads captcha data from the specified sample folder.
		/// </summary>
		/// <param name="pb">Optional progress bar to show loading progress.</param>
		/// <param name="sampleFolder">The folder containing the sample data.</param>
		/// <returns>A tuple containing the input and output NDArrays.</returns>
		public static (NDArray inputs, NDArray outputs) LoadCaptchaData(ProgressBar pb = null, string sampleFolder = "../sample_data")
		{
			Console.WriteLine("Loading sample datas...");
			var files = Directory.GetFiles(sampleFolder);
			if (files.Length == 0)
			{
				Console.WriteLine("No files found in folder: " + sampleFolder);
				return (default, default);
			}
			setupBar(pb, files.Length + 50);

			var images = new ConcurrentBag<NDArray>();
			var labels = new ConcurrentBag<NDArray>();

			Parallel.ForEach(files, file =>
			{
				var parts = file.Split('\\').Last().Split('-');
				var label = parts[0];

				var image = LoadImage(file);
				images.Add(image);
				labels.Add(ParseLabel(label));
				updateBar(pb);
			});

			Console.WriteLine($"Total images: {images.Count}");
			var inputs = np.stack(images.ToArray());
			Console.WriteLine($"Total labels: {labels.Count}");
			var outputs = np.stack(labels.ToArray());

			maxBar(pb);
			return (inputs, outputs);
		}

		public static NDArray LoadImage(string imagePath)
		{
			using (var bitmap = new Bitmap(imagePath))
			{
				return LoadImage(bitmap);
			}
		}

		/// <summary>
		/// Loads an image represented by a Bitmap into an NDArray.
		/// </summary>
		/// <param name="bitmap">The Bitmap representation of the image to load.</param>
		/// <returns>Returns the image data as an NDArray.</returns>
		public static NDArray LoadImage(Bitmap bitmap)
		{
			var targetDim = TensorFlowModel.ModelInputDim;
			var imageData = new float [targetDim * targetDim * 3];
			using (var resizedBitmap = new Bitmap(bitmap, new Size(targetDim, targetDim)))
			{
				for (int y = 0; y < resizedBitmap.Height; y++)
				{
					for (int x = 0; x < resizedBitmap.Width; x++)
					{
						var pixel = resizedBitmap.GetPixel(x, y);
						var index = (y * targetDim + x) * 3;
						imageData[index] = (pixel.R / 255.0f);
						imageData[index + 1] = (pixel.G / 255.0f);
						imageData[index + 2] = (pixel.B / 255.0f);
					}
				}
			}
			return np.array(imageData.ToArray()).reshape(new Shape(targetDim, targetDim, 3));
		}
		private static NDArray ParseLabel(string label)
		{
			var labelArray = new int[label.Length];
			for (int i = 0; i < label.Length; i++)
			{
				labelArray[i] = Captcha.Letters.IndexOf(label[i]);
			}
			return np.array(labelArray);
		}

	}
}
