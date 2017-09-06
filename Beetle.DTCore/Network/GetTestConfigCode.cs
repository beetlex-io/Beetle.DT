using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class GetTestConfigCode : MessageBase
	{
		public string UnitTest { get; set; }

		public string TestCase { get; set; }
	}

	public class GetTestConfigCodeResponse : MessageBase
	{
		public string Code { get; set; }

		public string ClassName { get; set; }
	}

}
