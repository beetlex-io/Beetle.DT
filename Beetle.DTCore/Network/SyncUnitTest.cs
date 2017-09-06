using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class SyncUnitTest : MessageBase
	{
		public string UnitTest { get; set; }

		public string[] Nodes { get; set; }
	}

	public class SyncUnitTestResponse : MessageBase
	{

		public List<VerifyFilesResponse> NodeVerifyFiles { get; set; }
	}


}
