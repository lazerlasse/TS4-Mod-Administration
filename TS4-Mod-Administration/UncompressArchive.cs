using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SharpCompress.Archives;
using SharpCompress.Readers;

namespace TS4_Mod_Administration
{
	class UncompressArchive
	{
		#region Atributes Section
		private List<FileInfo> filesToUncompress;
		private int filesToUncompressCounter;
		private int currentUncompressCounter;

		public List<FileInfo> FilesToUncompress
		{
			get
			{
				return this.filesToUncompress;
			}
			private set
			{
				this.filesToUncompress = value;
			}
		}

		public int FilesToUncompressCounter
		{
			get
			{
				this.filesToUncompressCounter = FilesToUncompress.Count;
				return this.filesToUncompressCounter;
			}
			private set
			{
				this.filesToUncompressCounter = value;
			}
		}

		public int CurrentUncompressCounter
		{
			get
			{
				return this.currentUncompressCounter;
			}
			private set
			{
				this.currentUncompressCounter = value;
			}
		}
		#endregion Atributes Section

		#region Constructer Section

		public UncompressArchive()
		{
			FilesToUncompress = new List<FileInfo>();
		}

		#endregion Constructer Section

		#region Method Section
		
		// Uncompress files in selected path...
		public void UncompressFiles(string path)
		{
			IndexFilesToUncompress(path);

			if (FilesToUncompressCounter != 0)
			{
				UncompressArchiveFiles();
			}
		}

		// Check selected path for files to uncompress...
		private void IndexFilesToUncompress(string sourcePath)
		{
			// Clear files list before scan...
			FilesToUncompress.Clear();

			// Set the file extensions to search for...
			string[] ArchiveExtensions = { "*.zip", "*.rar", "*.gzip", "*.7z" };

			// Search foreach accepted file extensions...
			foreach (string ext in ArchiveExtensions)
			{
				// Use DirectoryInfo to search directory and sub directories for files...
				FileInfo[] folder = new DirectoryInfo(sourcePath).GetFiles(ext, SearchOption.AllDirectories);

				// Add found archive files to list...
				foreach (FileInfo file in folder)
				{
					if ((file.Attributes & FileAttributes.Directory) != 0) continue;
					{
						FilesToUncompress.Add(file);
					}
				}
			}
		}

		// Uncompress archive files...
		private void UncompressArchiveFiles()
		{
			// Reset counter before uncompressing...
			CurrentUncompressCounter = 0;

			// Run throgh the extensions and check for archive files...
			try
			{
				foreach (var file in FilesToUncompress)
				{
					CurrentUncompressCounter++;

					// Open selected archive to extract...
					using (var archive = ArchiveFactory.Open(file.FullName))
					{
						// Run throgh archive and extract files...
						foreach (var entry in archive.Entries)
						{
							if (!entry.IsDirectory)
							{
								entry.WriteToDirectory(file.DirectoryName + @"\", new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
							}
						}
					}
				}
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion Method Section
	}
}
