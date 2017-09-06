using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class UpdateFile : MessageBase
	{

		public string UnitTest { get; set; }

		public string Name { get; set; }

		public byte[] Data { get; set; }

		public string SyncID { get; set; }
	}
}
