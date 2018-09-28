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
		private static readonly ModsIndexer modsIndexerInstance = new ModsIndexer();
		private int filesAddedCounter;
		private List<FileInfo> modFilesList;
		private List<FileInfo> trayModsFilesList;
		private List<FileInfo> ts4SciptModsFilesList;
		private List<FileInfo> archiveFilesList;
		private List<FileInfo> notAcceptedFilesList;

		static ModsIndexer()
		{

		}

		private ModsIndexer()
		{
			ModFilesList = new List<FileInfo>();
			TrayModsFilesList = new List<FileInfo>();
			Ts4ScriptModsFilesList = new List<FileInfo>();
			ArchiveFilesList = new List<FileInfo>();
			NotAcceptedFilesList = new List<FileInfo>();
		}

		public static ModsIndexer ModsIndexerInstance
		{
			get
			{
				return modsIndexerInstance;
			}
		}

		public int FilesAddedCounter
		{
			get
			{
				return this.filesAddedCounter;
			}
			private set
			{
				this.filesAddedCounter = value;
			}
		}

		public List<FileInfo> ModFilesList
		{
			get
			{
				return this.modFilesList;
			}
			private set
			{
				this.modFilesList = value;
			}
		}

		public List<FileInfo> TrayModsFilesList
		{
			get
			{
				return this.trayModsFilesList;
			}
			private set
			{
				this.trayModsFilesList = value;
			}
		}
        // Anders har en kæmpe tissemand!
		public List<FileInfo> Ts4ScriptModsFilesList
		{
			get
			{
				return this.ts4SciptModsFilesList;
			}
			private set
			{
				this.ts4SciptModsFilesList = value;
			}
		}

		public List<FileInfo> ArchiveFilesList
		{
			get
			{
				return this.archiveFilesList;
			}
			private set
			{
				this.archiveFilesList = value;
			}
		}

		public List<FileInfo> NotAcceptedFilesList
		{
			get
			{
				return this.notAcceptedFilesList;
			}
			private set
			{
				this.notAcceptedFilesList = value;
			}
		}



		public void IndexFilesToImport(string sourcePath)
		{
			// Index files from selected path...
			try
			{
				// Using FileInfo and DirectoryInfo for file search...
				FileInfo[] folder = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories);

				foreach (FileInfo file in folder)
				{
					if ((file.Attributes & FileAttributes.Directory) != 0) continue;
					{
						// Add Mods files...
						if (file.Extension == ".sims3pack" || file.Extension == ".package")
						{
							ModFilesList.Add(file);
						}

						// Add TS4 Script files...
						else if (file.Extension == ".ts4script")
						{
							Ts4ScriptModsFilesList.Add(file);
						}

						// Add mod files to Tray folder...
						else if (file.Extension == ".blueprint" || file.Extension == ".bpi" || file.Extension == ".trayitem")
						{
							TrayModsFilesList.Add(file);
						}

						// Add archive files...
						else if (file.Extension == ".zip" || file.Extension == ".rar")
						{
							ArchiveFilesList.Add(file);
						}

						// Add alle non *Sims files...
						else
						{
							NotAcceptedFilesList.Add(file);
						}
						
						FilesAddedCounter++;
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
