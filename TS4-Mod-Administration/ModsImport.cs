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
		private int modsAmountToImport;
		private int modsAmountImported;

		public int ModsAmountToImport
		{
			private get
			{
				return this.modsAmountToImport;
			}
			set
			{
				this.modsAmountToImport = value;
			}
		}

		public ModsImport()
		{

		}

		public void ImportMods()
		{
			// Remove files to the game folder...
			try
			{
				ModsIndexer
				foreach (FileInfo modFile in filesToImport)
				{
					// Check if file already exist on destination...
					if (File.Exists(ModsFolderPath + modFile.Name))
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
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
