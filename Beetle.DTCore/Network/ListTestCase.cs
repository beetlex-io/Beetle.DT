using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class ListTestCase : MessageBase
	{
		public string UnitTest { get; set; }
	}

	public class ListTestCaseResponse : MessageBase
	{
		public string[] Cases { get; set; }
	}

}
