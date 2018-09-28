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
		

		public void ClearFileErrorList()
		{
			MoveFileErrorList.Clear();
		}

		public void ImportMods(List<FileInfo> filesToImport)
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
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
