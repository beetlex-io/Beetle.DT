using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class VerifyFiles : MessageBase
	{

		public VerifyFiles()
		{
			Files = new List<Network.FileMD5>();
		}

		public string UnitTest { get; set; }

		public List<FileMD5> Files { get; set; }

		public string SyncID { get; set; }
	}

	public class VerifyFilesResponse : MessageBase
	{
		public string SyncID { get; set; }

		public string Node { get; set; }

		public bool Status { get; set; }
	}

}
