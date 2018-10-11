using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	class ModsIndexer
	{
		#region Attribute Section

		private List<FileInfo> ts4ModsList;
		private List<FileInfo> ts4TrayModsList;
		private List<FileInfo> ts4SciptModsList;
		private List<FileInfo> nonModsFiles;
		private List<FileInfo> modsToImport;

		public List<FileInfo> Ts4ModsList
		{
			get
			{
				return this.ts4ModsList;
			}
			private set
			{
				this.ts4ModsList = value;
			}
		}

		public List<FileInfo> Ts4TrayModsList
		{
			get
			{
				return this.ts4TrayModsList;
			}
			private set
			{
				this.ts4TrayModsList = value;
			}
		}

		public List<FileInfo> Ts4ScriptModsList
		{
			get
			{
				return this.ts4SciptModsList;
			}
			private set
			{
				this.ts4SciptModsList = value;
			}
		}

		public List<FileInfo> NonModsFiles
		{
			get
			{
				return this.nonModsFiles;
			}
			private set
			{
				this.nonModsFiles = value;
			}
		}

		public List<FileInfo> ModsToImport
		{
			get
			{
				return this.modsToImport;
			}
			private set
			{
				this.modsToImport = value;
			}
		}

		#endregion Attribute Section

		public ModsIndexer()
		{
			
		}

		public void IndexModFiles(string sourcePath, IProgress<TS4ProgressReporter> progressReporter, TS4ProgressReporter progress)
		{
			// Initialize List<FileInfo> before indexing...
			Ts4ModsList = new List<FileInfo>();
			Ts4TrayModsList = new List<FileInfo>();
			Ts4ScriptModsList = new List<FileInfo>();
			nonModsFiles = new List<FileInfo>();

			// Index The Sims 4 mods folder for conflict scan...
			try
			{
				// Using FileInfo and DirectoryInfo for file search...
				FileInfo[] folder = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories);

				// Run through the files and add...
				foreach (FileInfo file in folder)
				{
					if ((file.Attributes & FileAttributes.Directory) != 0) continue;
					{
						// Report progress...
						progress.StatusMessage = "Indexer: " + file.Name;
						progress.ProgressPercentage = 0;
						progressReporter.Report(progress);

						// Add Mods files...
						if (file.Extension == ".sims3pack" || file.Extension == ".package")
						{
							Ts4ModsList.Add(file);
						}

						// Add TS4 Script files...
						else if (file.Extension == ".ts4script")
						{
							Ts4ScriptModsList.Add(file);
						}

						// Add mod files to Tray folder...
						else if (file.Extension == ".blueprint" || file.Extension == ".bpi" || file.Extension == ".trayitem")
						{
							Ts4TrayModsList.Add(file);
						}

						// Add alle non *Sims files...
						else
						{
							nonModsFiles.Add(file);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void IndexModsToImport(string sourcePath, IProgress<TS4ProgressReporter> progressReporter, TS4ProgressReporter progress)
		{
			// Initialize List<FileInfo> before indexing...
			NonModsFiles = new List<FileInfo>();
			ModsToImport = new List<FileInfo>();

			// Index files to import from selected path...
			try
			{
				// Using FileInfo and DirectoryInfo for file search...
				FileInfo[] folder = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories);

				// Run through the files and add...
				foreach (FileInfo file in folder)
				{
					if ((file.Attributes & FileAttributes.Directory) != 0) continue;
					{
						// Report progress...
						progress.StatusMessage = "Indexer: " + file.Name;
						progress.ProgressPercentage = 0;
						progressReporter.Report(progress);

						// Add all accepted Mod and tray files...
						if (file.Extension == ".sims3pack" || file.Extension == ".package" || file.Extension == ".ts4script" || file.Extension == ".blueprint" || file.Extension == ".bpi" || file.Extension == ".trayitem")
						{
							Ts4ModsList.Add(file);
						}

						// Add alle not accepted *Sims files...
						else
						{
							nonModsFiles.Add(file);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
