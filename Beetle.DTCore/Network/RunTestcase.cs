using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class RunTestcase : MessageBase
	{
		public string UnitTest { get; set; }

		public string TestCase { get; set; }

		public string[] Nodes { get; set; }

		public int Users { get; set; }

		public string Config { get; set; }
	}

	public class RunTestcaseResponse : MessageBase
	{

	}

	public class StopTestCase : MessageBase
	{
		public string UnitTest { get; set; }

		public string[] Nodes { get; set; }
	}

	public class StopTestCaseResponse : MessageBase
	{

	}


}
