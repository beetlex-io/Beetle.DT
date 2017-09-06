using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class ReportErrors : MessageBase
	{

		public string UnitTest { get; set; }

		public string Node { get; set; }

		public ErrorInfo[] Errors { get; set; }
	}
}
