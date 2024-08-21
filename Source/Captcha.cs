using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using Tensorflow.NumPy;

namespace captcha_solver.Source
{
	public static class Captcha
	{
		public  const string Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ";
		public  const int CaptchaCodeLength = 4;

		public static string GenerateCaptchaCode()
		{
			Random rand = new();
			int maxRand = Letters.Length - 1;

			StringBuilder sb = new();

			for (int i = 0; i < 4; i++)
			{
				int index = rand.Next(maxRand);
				sb.Append(Letters[index]);
			}
			return sb.ToString();
		}

		static NDArray AddRippleEffectToNDArray(Point[,] pt, Bitmap baseMap)
		{
			int nWidth = baseMap.Width;
			int nHeight = baseMap.Height;

			NDArray npImageOutput = np.zeros((nHeight, nWidth, 3), dtype: Tensorflow.TF_DataType.TF_FLOAT);

			BitmapData bitmapData = baseMap.LockBits(new Rectangle(0, 0, nWidth, nHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			int stride = bitmapData.Stride;

			unsafe
			{
				byte* pSrc = (byte*)bitmapData.Scan0;

				for (int y = 0; y < nHeight; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						var xOffset = pt[x, y].X;
						var yOffset = pt[x, y].Y;

						if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
						{
							byte* pixelSrc = pSrc + yOffset * stride + xOffset * 3;
							npImageOutput[y, x, 0] = (float)(pixelSrc[2] / 255.0); // R
							npImageOutput[y, x, 1] = (float)(pixelSrc[1] / 255.0); // G
							npImageOutput[y, x, 2] = (float)(pixelSrc[0] / 255.0); // B
						}
					}
				}
			}

			baseMap.UnlockBits(bitmapData);

			return npImageOutput;
		}

		static Bitmap AddRippleEffectToBitmap(Point[,] pt, Bitmap baseMap)
		{
			int nWidth = baseMap.Width;
			int nHeight = baseMap.Height;

			Bitmap bSrc = (Bitmap)baseMap.Clone();

			BitmapData bitmapData = baseMap.LockBits(new Rectangle(0, 0, baseMap.Width, baseMap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int scanline = bitmapData.Stride;

			IntPtr scan0 = bitmapData.Scan0;
			IntPtr srcScan0 = bmSrc.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)scan0;
				byte* pSrc = (byte*)(void*)srcScan0;

				int nOffset = bitmapData.Stride - baseMap.Width * 3;

				for (int y = 0; y < nHeight; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						var xOffset = pt[x, y].X;
						var yOffset = pt[x, y].Y;

						if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
						{
							if (pSrc != null)
							{
								p[0] = pSrc[yOffset * scanline + xOffset * 3];
								p[1] = pSrc[yOffset * scanline + xOffset * 3 + 1];
								p[2] = pSrc[yOffset * scanline + xOffset * 3 + 2];
							}
						}

						p += 3;
					}
					p += nOffset;
				}
			}

			baseMap.UnlockBits(bitmapData);
			bSrc.UnlockBits(bmSrc);
			bSrc.Dispose();

			return baseMap;
		}

		private static Point[,] GenerateCaptchaCore(Bitmap baseMap, string captchaCode, Random rand)
		{
			int width = baseMap.Width;
			int height = baseMap.Height;
			using (Graphics graph = Graphics.FromImage(baseMap))
			{
				graph.Clear(GetRandomLightColor());

				DrawCaptchaCode();
				DrawDisorderLine();
				return RippleEffect();

				int GetFontSize(int imageWidth, int captchCodeCount)
				{
					var averageSize = imageWidth / captchCodeCount;

					return Convert.ToInt32(averageSize);
				}

				Color GetRandomDeepColor()
				{
					int redlow = 160, greenLow = 100, blueLow = 160;
					return Color.FromArgb(rand.Next(redlow), rand.Next(greenLow), rand.Next(blueLow));
				}

				Color GetRandomLightColor()
				{
					int low = 180, high = 255;

					int nRend = rand.Next(high) % (high - low) + low;
					int nGreen = rand.Next(high) % (high - low) + low;
					int nBlue = rand.Next(high) % (high - low) + low;

					return Color.FromArgb(nRend, nGreen, nBlue);
				}

				void DrawCaptchaCode()
				{
					SolidBrush fontBrush = new(Color.Black);
					int fontSize = GetFontSize(width, captchaCode.Length);
					Font font = new(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
					for (int i = 0; i < captchaCode.Length; i++)
					{
						fontBrush.Color = GetRandomDeepColor();

						int shiftPx = fontSize / 6;

						float x = i * fontSize + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
						int maxY = height - fontSize;
						if (maxY < 0)
							maxY = 0;
						float y = rand.Next(0, maxY);

						graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
					}
				}

				void DrawDisorderLine()
				{
					Pen linePen = new(new SolidBrush(Color.Black), 3);
					for (int i = 0; i < rand.Next(3, 5); i++)
					{
						linePen.Color = GetRandomDeepColor();

						Point startPoint = new(rand.Next(0, width), rand.Next(0, height));
						Point endPoint = new(rand.Next(0, width), rand.Next(0, height));
						graph.DrawLine(linePen, startPoint, endPoint);

						//Point bezierPoint1 = new Point(rand.Next(0, width), rand.Next(0, height));
						//Point bezierPoint2 = new Point(rand.Next(0, width), rand.Next(0, height));

						//graph.DrawBezier(linePen, startPoint, bezierPoint1, bezierPoint2, endPoint);
					}
				}

				Point[,] RippleEffect()
				{
					short nWave = 6;
					int nWidth = baseMap.Width;
					int nHeight = baseMap.Height;

					Point[,] pt = new Point[nWidth, nHeight];

					for (int x = 0; x < nWidth; ++x)
					{
						for (int y = 0; y < nHeight; ++y)
						{
							var xo = nWave * Math.Sin(2.0 * 3.1415 * y / 128.0);
							var yo = nWave * Math.Cos(2.0 * 3.1415 * x / 128.0);

							var newX = x + xo;
							var newY = y + yo;

							if (newX > 0 && newX < nWidth)
							{
								pt[x, y].X = (int)newX;
							}
							else
							{
								pt[x, y].X = 0;
							}

							if (newY > 0 && newY < nHeight)
							{
								pt[x, y].Y = (int)newY;
							}
							else
							{
								pt[x, y].Y = 0;
							}
						}
					}

					return pt;
				}
			}

		}

		public static Bitmap GenerateCaptchaImage(int width, int height, string captchaCode)
		{
			Bitmap baseMap = new(width, height);

			var pt = GenerateCaptchaCore(baseMap,captchaCode,new Random());
			var img = AddRippleEffectToBitmap(pt,baseMap);

			return baseMap;

		}

		public static NDArray GenerateCaptchaSample(string captchaCode, int width = 200, int height = 50, int? randomSeed = null)
		{
			var random = randomSeed != null ? new Random((int)randomSeed) : new Random();
			using (Bitmap baseMap = new(width, height))
			{
				var pt = GenerateCaptchaCore(baseMap,captchaCode,random);
				return AddRippleEffectToNDArray(pt, baseMap);
			}
		}

		public static Bitmap CreateBitmapStats(Bitmap input)
		{
			var colorStats = new Dictionary<Color, ColorStats>();

			for (int y = 0; y < input.Height; y++)
			{
				for (int x = 0; x < input.Width; x++)
				{
					var pixel = input.GetPixel(x, y);
					if (!colorStats.ContainsKey(pixel))
					{
						colorStats[pixel] = new ColorStats
						{
							color = pixel
						};
					}
					colorStats[pixel].Count++;
					colorStats[pixel].widthIndex.Add(x);
				}
			}

			var maxWidth = colorStats.Values
				.Max(x => x.Width);
			var bgColor = colorStats.Values.First(x => x.Width == maxWidth).color;
			colorStats.Remove(bgColor);

			var stats = colorStats.Values.ToArray();
			Array.Sort(stats, (a, b) => b.Count.CompareTo(a.Count));

			var copy = new Bitmap(input.Width, input.Height + 30);
			//using (var g = Graphics.FromImage(copy))
			//	g.DrawImage(input, 0, 0);

			for (int i = 0; i < stats.Length; i++)
			{
				var stat = stats[i];
				if (stat.Width > copy.Width / 3)
					removeColor(stat, input, bgColor);
				else if (stat.Count <= 150)
					removeColor(stat, input, bgColor);

			}

			using (var g = Graphics.FromImage(copy))
			{
				g.DrawImage(input, 0, 0);
				int h = 2;
				int y = copy.Height - h;
				foreach (var stat in stats)
				{
					g.FillRectangle(
						new SolidBrush(stat.color),
						stat.widthIndex.Min(),
						y,
						stat.Width,
						h);
					Console.WriteLine(stat.color + " : " + stat.Width + " : " + stat.Count);
					y -= h;
				}
			}
			return copy;

		}

		static bool ColorsAreEqual(Color color1, Color color2)
		{
			return color1.A == color2.A && color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
		}

		static void removeColor(ColorStats stats, Bitmap bmp, Color newColor)
		{
			var max = stats.widthIndex.Max();
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					Color pixel = bmp.GetPixel(i, j);

					if (ColorsAreEqual(pixel, stats.color))
					{
						bmp.SetPixel(i, j, newColor);
					}
				}
			}
		}
	}

	public class ColorStats
	{
		public Color color { get; set; }
		public int Count { get; set; }
		public HashSet<int> widthIndex { get; set; } = new();

		private int _width { get; set; } = 0;

		public int Width
		{
			get
			{
				if (_width == 0)
				{
					_width = widthIndex.Max() - widthIndex.Min();
				}
				return _width;
			}
		}
	}
}
