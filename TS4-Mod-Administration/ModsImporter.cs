using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	class ModsImporter
    {
		private int importedCounter;
		private int filesToImportCount;
		private int currentIndexToEdit;

		public ModsImporter()
		{
		   
		}

		public void ImportMods(List<FileInfo> modFilesToImport, IProgress<TS4ProgressReporter> progressReporter, TS4ProgressReporter progress)
		{
			// Reset import counter and set amount to import...
			importedCounter = 0;
			filesToImportCount = modFilesToImport.Count;

			// Move mod files to the Mods folders...
			try
			{
				// Run through the files and move them...
				foreach (FileInfo modFile in modFilesToImport)
				{
					// Set current index counter...
					currentIndexToEdit = modFilesToImport.IndexOf(modFile);

					// Report the progress back to GUI...
					progress.StatusMessage = "Importerer: " + modFile.Name;
					progressReporter.Report(progress);

					// Move all mod files to the mod folder...
					if (modFile.Extension == ".sims3pack" || modFile.Extension == ".package")
					{
						// Check if file already exist on destination...
						if (File.Exists(SimsFolder.ModsFolderPath + modFile.Name))
						{
							// Check if file are newer than destination file...
							if (modFile.LastWriteTime > File.GetLastWriteTime(SimsFolder.ModsFolderPath + modFile.Name))
							{
								// Delete the old file and move new file to destination path...
								File.Delete(SimsFolder.ModsFolderPath + modFile.Name);
								modFile.MoveTo(SimsFolder.ModsFolderPath + modFile.Name);
							}
							else
							{
								modFilesToImport.Remove(modFile);
							}
						}
						else
						{
							modFile.MoveTo(SimsFolder.ModsFolderPath + modFile.Name);
							modFilesToImport.Remove(modFile);
						}
					}

					// Move all Tray files to the tray folder...
					else
					{
						// Check if file already exist on destination...
						if (File.Exists(SimsFolder.ModsFolderPath + modFile.Name))
						{
							// Check if file are newer than destination file...
							if (modFile.LastWriteTime > File.GetLastWriteTime(SimsFolder.ModsFolderPath + modFile.Name))
							{
								// Delete the old file and move new file to destination path...
								File.Delete(SimsFolder.ModsFolderPath + modFile.Name);
								modFile.MoveTo(SimsFolder.ModsFolderPath + modFile.Name);
							}
							else
							{
								modFilesToImport.Remove(modFile);
							}
						}
						else
						{
							modFile.MoveTo(SimsFolder.ModsFolderPath + modFile.Name);
							modFilesToImport.Remove(modFile);
						}
					}

					// Report success progress...
					progress.ProgressPercentage = (importedCounter / filesToImportCount) * 100;
					progress.DataGridContent.ElementAt(currentIndexToEdit).Package_StatusMessage = "Importeret";
					progressReporter.Report(progress);
				}
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
