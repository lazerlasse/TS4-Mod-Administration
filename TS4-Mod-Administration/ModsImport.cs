using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	class ModsImport
	{
		private string modsFolderPath;
		private List<FileInfo> moveFileErrorList;

		public string ModsFolderPath
		{
			private get
			{
				return this.modsFolderPath;
			}
			set
			{
				this.modsFolderPath = value;
			}
		}

		public List<FileInfo> MoveFileErrorList
		{
			get
			{
				return this.moveFileErrorList;
			}
			private set
			{
				this.moveFileErrorList = value;
			}
		}

		public ModsImport()
		{

		}

		public void ClearFileErrorList()
		{
			MoveFileErrorList.Clear();
		}

		public string ImportMods(List<FileInfo> filesToImport)
		{
			// Remove files to the game folder...
			try
			{
				foreach (var Mod in filesToImport)
				{
					// Check if file already exist on destination...
					if (File.Exists(ModsFolderPath + Mod.Name))
					{
						File.Delete(ModsFolderPath + Mod.Name);
					}

					// Move file to destination path...
					Mod.MoveTo(ModsFolderPath + Mod.Name);

					// If file not moved add file to failed List...
					if (!File.Exists(ModsFolderPath + Mod.Name))
					{
						MoveFileErrorList.Add(Mod);
					}
				}

				return "Import af mods blev gennemført.";
			}

			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
