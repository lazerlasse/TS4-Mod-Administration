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
		private List<ProcessViewOutput> gridViewOutput;
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

		public List<ProcessViewOutput> GridViewOutput
		{
			get
			{
				return this.gridViewOutput;
			}
			private set
			{
				this.gridViewOutput = value;
			}
		}

		public int FilesToUncompressCounter
		{
			get
			{
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
			GridViewOutput = new List<ProcessViewOutput>();
		}

		#endregion Constructer Section

		#region Method Section

		// Check selected path for files to uncompress...
		public void IndexFilesToUncompress(string sourcePath, IProgress<TS4ProgressReporter> progress, TS4ProgressReporter progressReporter)
		{
			// Clear files list and GridViewOutput before scan...
			FilesToUncompress.Clear();
			GridViewOutput.Clear();

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
					// Report progress back to GUI...
					progressReporter.DataGridContent.Add(new ProcessViewOutput(file));
					progressReporter.StatusMessage = "Indexere: " + file.FullName;
					progress.Report(progressReporter);

					if ((file.Attributes & FileAttributes.Directory) != 0) continue;
					{
						// Add files to uncompress list and count files...
						FilesToUncompress.Add(file);
						FilesToUncompressCounter++;

						System.Threading.Thread.Sleep(1000);
					}
				}
			}
		}

		// Uncompress archive files...
		public void UncompressArchiveFiles(IProgress<TS4ProgressReporter> progress, TS4ProgressReporter progressReporter)
		{
			// Reset counter before uncompressing...
			CurrentUncompressCounter = 0;

			// Run throgh the extensions and check for archive files...
			try
			{
				// Run through the files and uncompress them...
				foreach (var file in FilesToUncompress)
				{
					progressReporter.StatusMessage = "Udpakker: " + file.FullName;
					progressReporter.ProgressPercentage = (CurrentUncompressCounter * 100) / FilesToUncompressCounter;
					progressReporter.DataGridContent.ElementAt(progressReporter.DataGridContent.IndexOf(new ProcessViewOutput(file))).Package_CanBeImported = "Udpakket";
					progress.Report(progressReporter);

					// Open selected archive to extract...
					using (IArchive archive = ArchiveFactory.Open(file.FullName))
					{
						// Run throgh archive and extract files...
						foreach (var entry in archive.Entries)
						{
							if (!entry.IsDirectory)
							{
								entry.WriteToDirectory(file.DirectoryName + @"\", new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
							}
						}

						System.Threading.Thread.Sleep(1000);
					}

					// Set the current counter for progress...
					CurrentUncompressCounter++;
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