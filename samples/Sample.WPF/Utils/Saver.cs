using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace Sample.WPF.Utils {

	internal class Saver {

		public string Folder { get; set; }

		public string Pattern { get; set; } = "scan-*";

		public ImageFormat ImageFormat { get; set; } = ImageFormat.Jpeg;

		public int JpegQuality { get; set; } = 80;

		private readonly Dictionary<ImageFormat, string> extension = new Dictionary<ImageFormat, string> {
			{ ImageFormat.Jpeg, ".jpg" },
			{ ImageFormat.Png, ".png" }
		};


		public void Save(BitmapSource img) {
			string filePath = findFile();
			using (var fileStream = new FileStream(filePath, FileMode.Create)) {

				switch (ImageFormat) {

					case ImageFormat.Png: {
						BitmapEncoder encoder = new PngBitmapEncoder();
						encoder.Frames.Add(BitmapFrame.Create(img));
						encoder.Save(fileStream);
						break;
					}

					case ImageFormat.Jpeg: {
						JpegBitmapEncoder encoder = new JpegBitmapEncoder();
						encoder.QualityLevel = JpegQuality;
						encoder.Frames.Add(BitmapFrame.Create(img));
						encoder.Save(fileStream);
						break;
					}

				}
			}
		}

		private string findFile() {
			int nr = 1;
			if (Directory.Exists(Folder)) {
				DirectoryInfo di = new DirectoryInfo(Folder);
				// NOTE: '*' must be at the end of pattern.
				foreach (FileInfo fi in di.GetFiles(Pattern)) {
					string name = fi.Name;
					string ext = fi.Extension;
					name = name.Substring(0, name.Length - fi.Extension.Length);
					string n = name.Substring(Pattern.Length - 1);
					try {
						if (int.Parse(n) >= nr)
							nr = int.Parse(n) + 1;
					} catch { }
				}
			}
			string res = nr.ToString() + extension[ImageFormat];
			return Path.Combine(Folder, Pattern.Replace("*", res));
		}

	}

	public enum ImageFormat {
		Jpeg,
		Png
	}

}
