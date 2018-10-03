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
		private static readonly string modsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts\The Sims 4\Mods\";
		private static readonly string trayFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts\The Sims 4\Tray\";

		public static string ModsFolderPath
		{
			get
			{
				if (Directory.Exists(modsFolderPath))
				{
					return modsFolderPath;
				}
				else
				{
					return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
		}

		public static string TrayFolderPath
		{
			get
			{
				if (Directory.Exists(trayFolderPath))
				{
					return trayFolderPath;
				}
				else
				{
					return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
		}
	}
}
