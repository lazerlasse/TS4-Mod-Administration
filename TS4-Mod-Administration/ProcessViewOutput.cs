using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	public class ProcessViewOutput
	{
		public string Package_Name { get; set; }
		public string Package_Type { get; set; }
		public DateTime Package_CreatedDate { get; set; }
		public DateTime Package_EditedDate { get; set; }
		public string Package_ResourceType { get; set; }
		public string Package_ResourceGroup { get; set; }
		public string Package_ResourceInstance { get; set; }
		public string Package_CanBeImported { get; set; }

		public ProcessViewOutput()
		{

		}

		public ProcessViewOutput(FileInfo fileInfo)
		{
			this.Package_Name = fileInfo.Name.Split('.')[0];
			this.Package_Type = fileInfo.Extension;
			this.Package_CreatedDate = fileInfo.CreationTime;
			this.Package_EditedDate = fileInfo.LastWriteTime;
		}
	}
}
