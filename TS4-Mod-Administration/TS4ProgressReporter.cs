using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
	public class TS4ProgressReporter
	{
		public int ProgressPercentage { get; set; } = 0;
		public string StatusMessage { get; set; } = "Status:";
		public List<ProcessViewOutput> DataGridContent { get; set; } = new List<ProcessViewOutput>();
	}
}
