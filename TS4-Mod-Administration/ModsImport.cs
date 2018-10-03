﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	class ModsImport
    {
		private List<FileInfo> moveFileErrorList;

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
			MoveFileErrorList = new List<FileInfo>();
		}

		public void ImportMods(List<FileInfo> modFiles, List<FileInfo> trayFiles)
		{
			// Move mod files to the Mods folders...
			try
			{
				foreach (FileInfo mod in modFiles)
				{
					// Check if file already exist on destination...
					if (File.Exists(SimsFolder.ModsFolderPath + mod.Name))
					{
						File.Delete(SimsFolder.ModsFolderPath + mod.Name);
					}

					// Move file to destination path...
					mod.MoveTo(SimsFolder.ModsFolderPath + mod.Name);

					// If file not moved add the file to failed List...
					if (!File.Exists(SimsFolder.ModsFolderPath + mod.Name))
					{
						MoveFileErrorList.Add(mod);
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
