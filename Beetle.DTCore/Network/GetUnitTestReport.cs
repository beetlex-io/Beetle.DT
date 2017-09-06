using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class GetUnitTestReport : MessageBase
	{
		public string UnitTest { get; set; }

		public string[] Nodes { get; set; }
	}

	public class GetUnitTestReportReponse : MessageBase
	{
		public GetUnitTestReportReponse()
		{
			Items = new List<StatisticalReport>();
		}
		public List<StatisticalReport> Items { get; set; }
	}

}
