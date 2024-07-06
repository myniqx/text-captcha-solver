using captcha_solver.Source;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Tensorflow;
using Tensorflow.Keras.Engine;
using Tensorflow.NumPy;
using text_captcha_solver.Source;

namespace text_captcha_solver
{
	public partial class Solver : Form
	{
		[DllImport("kernel32.dll")]
		static extern bool AllocConsole();
		public Solver()
		{
			InitializeComponent();
			RichTextBoxWriter writer = new(outPut,this);
			Console.SetOut(writer);
			options = Options.GetOptions();
		}

		public IModel currentModel;
		public FileInfo selectedFile;
		public string[] currentWords;
		public Options options;
		public double lastAccuracy,lastLoss;

		string refreshModelList(bool best)
		{
			var modelList = Options.GetModelFiles();
			var selectedModel = best  ? Options.GetBestModelFile() : Options.GetLastModelFile();
			model_list.Items.Clear();
			model_list.Items.AddRange(modelList);
			model_list.SelectedIndexChanged -= model_list_SelectedIndexChanged;
			for (int i = 0; i < modelList.Length; i++)
			{
				if (modelList[i] == selectedModel)
				{
					model_list.SelectedIndex = i;
					break;
				}
			}
			model_list.SelectedIndexChanged += model_list_SelectedIndexChanged;
			return selectedModel;
		}

		private void Solver_Load(object sender, EventArgs e)
		{
			Console.WriteLine("Starting...");

			sample_letters.Text = options.sampleLetters;
			epoch.Value = options.epoch;
			batchSize.Value = options.batch;
			validationSize.Value = (decimal)options.validationSplit;
			sample_repeat.Value = Math.Max(options.repeat, sample_repeat.Minimum);

			currentModel = TensorFlowModel.BuildModel();
			var bestModel = refreshModelList(true);
			TensorFlowModel.LoadModel(currentModel, bestModel);
		}

		void saveOptions()
		{
			options.batch = (int)batchSize.Value;
			options.epoch = (int)epoch.Value;
			options.validationSplit = (float)validationSize.Value;
			options.sampleLetters = sample_letters.Text;
			options.repeat = (int)sample_repeat.Value;
			options.SaveOptions();
		}

		private async void train_Click(object sender, EventArgs e)
		{
			saveOptions();

			train.Enabled = false;

			chart.Series["Loss"].Points.Clear();
			chart.Series["Acc"].Points.Clear();

			await Task.Run(() =>
			   {
				   var (images, labels) = GenerateDatas.LoadCaptchaData(progressBar, Options.samplePath);

				   Console.WriteLine("Training model... : " + images.size + " " + labels.size);
				   TensorFlowModel.TrainModel(
					   currentModel,
					   images,
					   labels,
					   (int)epoch.Value,
					   (int)batchSize.Value,
					   (float)validationSize.Value);

				   Console.WriteLine("Training complete...");

				   TensorFlowModel.SaveModel(currentModel, lastAccuracy, lastLoss);
			   });

			train.Enabled = true;
			refreshModelList(false);
		}

		private void refresh_Click(object sender, EventArgs e)
		{
			var captchaCode = Captcha.GenerateCaptchaCode();
			var captch = Captcha.GenerateCaptchaImage(200,50,captchaCode);
			captchaBox.Image = captch;
			var boxes = SplitBoxes(captchaBox.Image);
			var pbx = new [] {c1,c2,c3,c4};

			for (int i = 0; i < 4; i++)
				pbx[i].Image = boxes[i];

			var result = new List<char>();

			for (int boxIndex = 0; boxIndex < boxes.Count; boxIndex++)
			{
				Bitmap box = boxes[boxIndex];
				var pbx_ = pbx[boxIndex];
				var chx_ = captchaCode[boxIndex];
				var ndData = LoadImage(box);
				var predictionChar = TensorFlowModel.PredictCharacter(currentModel, ndData);
				pbx_.BackColor = predictionChar == chx_ ? Color.Blue : Color.Red;
				result.Add(predictionChar);
			}

			var predictionCaptcha = new string(result.ToArray());
			prediction.Text = predictionCaptcha;

			prediction.ForeColor = captchaCode == predictionCaptcha ? Color.Blue : Color.Red;

			List<Bitmap> SplitBoxes(Image img)
			{
				var targetDim = TensorFlowModel.ModelInputDim;
				var images = new List<Bitmap>();
				for (int i = 0; i < 4; i++)
				{
					var resizedImg = new Bitmap(targetDim, targetDim);
					using (var g = Graphics.FromImage(resizedImg))
					{
						var cropRect = new Rectangle(i * 50, 0, 50, 50);
						g.DrawImage(img, new Rectangle(0, 0, targetDim, targetDim), cropRect, GraphicsUnit.Pixel);
					}
					images.Add(resizedImg);
				}
				return images;
			}

			NDArray LoadImage(Bitmap bitmap)
			{
				var targetDim = TensorFlowModel.ModelInputDim;
				var resizedBitmap = new Bitmap(bitmap, new Size(targetDim, targetDim));
				var imageData = new float[targetDim * targetDim * 3];
				int index = 0;

				for (int y = 0; y < resizedBitmap.Height; y++)
				{
					for (int x = 0; x < resizedBitmap.Width; x++)
					{
						var pixel = resizedBitmap.GetPixel(x, y);
						imageData[index++] = pixel.R / 255.0f;
						imageData[index++] = pixel.G / 255.0f;
						imageData[index++] = pixel.B / 255.0f;
					}
				}
				return np.array(imageData).reshape(new Shape(1, targetDim, targetDim, 3));
			}
		}

		private void clear_samples_Click(object sender, EventArgs e)
		{
			Task.Run(async () => await GenerateDatas.ClearSampleFolder(progressBar, Options.samplePath));
		}

		private void generate_samples_Click(object sender, EventArgs e)
		{
			Task.Run(() => GenerateDatas.CreateSampleFolder(progressBar, (int)sample_repeat.Value, sample_letters.Text, Options.samplePath));
		}

		private void reset_letters_Click(object sender, EventArgs e)
		{
			sample_letters.Text = GenerateDatas.GenerateLettersForSample();
		}

		private void model_list_SelectedIndexChanged(object sender, EventArgs e)
		{
			var modelFile = model_list.SelectedItem.ToString();
			TensorFlowModel.LoadModel(currentModel, modelFile);
		}

		private void Solver_FormClosing(object sender, FormClosingEventArgs e)
		{
			saveOptions();
		}
	}

	public class RichTextBoxWriter : TextWriter
	{
		private RichTextBox _output = null;
		private Solver _solver = null;
		private Series _acc = null, _loss=null;

		public RichTextBoxWriter(RichTextBox output, Solver solver)
		{
			_output = output;
			_solver = solver;
			_acc = _solver.chart.Series["Acc"];
			_loss = _solver.chart.Series["Loss"];
		}

		public override void Write(char value)
		{
			MethodInvoker action = delegate
			{
				_output.AppendText(value.ToString());
			};

			_output.BeginInvoke(action);
		}

		int totalEpoch = 1;
		int currentEpoch = 0;
		bool writeTrainingData(string value)
		{
			if (value.Contains("[") == false)
				return false;

			// 0001/0003 [>............................] - 37ms/step - loss: 1,165943 - accuracy: 1,000000
			var parts = value.Split('-').Select(x => x.Trim()).ToArray();
			var progressParts = parts[0].Split(' ').Select(x => x.Trim()).ToArray();
			var progressBar = progressParts[1].Count(x => x != '.') - 2;
			var progressBarLenght = progressParts[1].Length - 2;
			var batchParts = progressParts[0].Split('/');
			var currentBatch = int.Parse(batchParts[0]);
			var totalBatch = int.Parse(batchParts[1]);
			var full = value.Contains("val_");
			var loss = Double.Parse(parts[2].Split(':')[1].Trim());
			var accuracy = Double.Parse (parts[3].Split(':')[1].Trim());

			MethodInvoker action = delegate
			{
				_solver.progressBar.Maximum = totalEpoch * progressBarLenght;
				if(full)
					_solver.progressBar.Value = currentEpoch * progressBarLenght;
				else
					_solver.progressBar.Value = (currentEpoch-1) * progressBarLenght + progressBar;
				_loss.Points.Add(loss);
				_acc.Points.Add(accuracy);
				_solver.status.Text = value;
				_solver.lastAccuracy = accuracy;
				_solver.lastLoss = loss;
			};

			_solver.progressBar.BeginInvoke(action);

			if (full)
				WriteLine(value);

			return true;
		}

		public override void Write(string value)
		{
			if (writeTrainingData(value))
				return;

			MethodInvoker action = delegate
			{
				_solver.Text = value;
				_output.AppendText(value + Environment.NewLine);
				_output.ScrollToCaret();
			};

			_solver.BeginInvoke(action);
		}

		public override void WriteLine(string value)
		{
			if (value.StartsWith("Epoch"))
			{
				var eparts = value.Split(':').Select(x => x.Trim()).ToArray();
				var epochParts = eparts[1].Split('/');
				currentEpoch = int.Parse(epochParts[0]);
				totalEpoch = int.Parse(epochParts[1]);
			}

			MethodInvoker action = delegate
			{
				_solver.Text = value;
				if(value.Contains(Environment.NewLine))
					value = value.Replace("\n", "\r\n");
				else
					value += Environment.NewLine;

				_output.AppendText(value);
				_output.ScrollToCaret();
			};

			_solver.BeginInvoke(action);

		}

		public override Encoding Encoding
		{
			get { return System.Text.Encoding.UTF8; }
		}
	}
}
