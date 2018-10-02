using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	static class SimsFolder
	{
		public static readonly string ModsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts\The Sims 4\Mods\";
		public static readonly string TrayFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts\The Sims 4\Tray\";

		public static bool CheckModsFolderExist()
		{
			if (Directory.Exists(ModsFolderPath))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
