using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	public sealed class ModsIndexer
	{
		#region Attribute and Constructor Section

		private static readonly ModsIndexer instance = new ModsIndexer();
		public List<FileInfo> TS4ModsList { get; private set; } = new List<FileInfo>();
		public List<FileInfo> TS4TrayModsList { get; private set; } = new List<FileInfo>();
		public List<FileInfo> TS4ScriptModsList { get; private set; } = new List<FileInfo>();
		public List<FileInfo> NotModFilesList { get; private set; } = new List<FileInfo>();
		public List<FileInfo> ModsToImportList { get; private set; } = new List<FileInfo>();
		private List<ProcessViewOutput> gridViewOutPut = new List<ProcessViewOutput>();

		public static ModsIndexer Instance
		{
			get
			{
				return instance;
			}
		}

		static ModsIndexer()
		{

		}

		private ModsIndexer()
		{

		}

		#endregion Attribute and Constructeri Section

		public void IndexModFiles(string sourcePath, IProgress<TS4ProgressReporter> progressReporter, TS4ProgressReporter progress)
		{
			// Initialize List<FileInfo> before indexing...
			TS4ModsList.Clear();
			TS4TrayModsList.Clear();
			TS4ScriptModsList.Clear();
			NotModFilesList.Clear();
			gridViewOutPut.Clear();

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
						gridViewOutPut.Add(new ProcessViewOutput(file));
						progress.DataGridContent = gridViewOutPut;
						progress.ProgressPercentage = 0;
						progressReporter.Report(progress);

						// Add Mods files...
						if (file.Extension == ".sims3pack" || file.Extension == ".package")
						{
							TS4ModsList.Add(file);
						}

						// Add TS4 Script files...
						else if (file.Extension == ".ts4script")
						{
							TS4ScriptModsList.Add(file);
						}

						// Add mod files to Tray folder...
						else if (file.Extension == ".blueprint" || file.Extension == ".bpi" || file.Extension == ".trayitem")
						{
							TS4TrayModsList.Add(file);
						}

						// Add alle non *Sims files...
						else
						{
							NotModFilesList.Add(file);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			// Progress reporting finish...
			progress.LoadingProgress = false;
			progressReporter.Report(progress);
		}

		public void IndexModsToImport(string sourcePath, IProgress<TS4ProgressReporter> progressReporter, TS4ProgressReporter progress)
		{
			// Initialize List<FileInfo> before indexing...
			NotModFilesList.Clear();
			ModsToImportList.Clear();
			gridViewOutPut.Clear();

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
						gridViewOutPut.Add(new ProcessViewOutput(file));
						progress.DataGridContent = gridViewOutPut;
						progress.LoadingProgress = true;
						progressReporter.Report(progress);
						
						// Add all accepted Mod and tray files...
						if (file.Extension == ".sims3pack" || file.Extension == ".package" || file.Extension == ".ts4script" || file.Extension == ".blueprint" || file.Extension == ".bpi" || file.Extension == ".trayitem")
						{
							TS4ModsList.Add(file);
						}

						// Add alle not accepted *Sims files...
						else
						{
							NotModFilesList.Add(file);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			// Progress reporting finish...
			progress.LoadingProgress = false;
			progressReporter.Report(progress);
		}
	}
}
