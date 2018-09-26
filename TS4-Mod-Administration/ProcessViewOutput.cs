using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	class ProcessViewOutput
	{
		public string Package_Name { get; set; }
		public string Package_Type { get; set; }
		public DateTime Package_CreatedDate { get; set; }
		public DateTime Package_EditedDate { get; set; }
		public string Package_ResourceType { get; set; }
		public string Package_ResourceGroup { get; set; }
		public string Package_ResourceInstance { get; set; }
		public string Package_CanBeImported { get; set; }
	}
}
