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
		private int modsAddedCount;

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

		public int ModsAddedCount
		{
			get
			{
				return this.modsAddedCount;
			}
			private set
			{
				this.modsAddedCount = value;
			}
		}

		#endregion Attribute Section

		public ModsIndexer()
		{
			Ts4ModsList = new List<FileInfo>();
			Ts4TrayModsList = new List<FileInfo>();
			Ts4ScriptModsList = new List<FileInfo>();
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

						}

						ModsAddedCount++;
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
