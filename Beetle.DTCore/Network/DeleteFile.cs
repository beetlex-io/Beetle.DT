using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class DeleteFiles : MessageBase
	{
		public DeleteFiles()
		{
			Files = new List<string>();
		}

		public string UnitTest { get; set; }

		public List<string> Files { get; set; }
	}
}
