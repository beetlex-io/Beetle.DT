using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class ListFiles : MessageBase
	{
		public string UnitTest { get; set; }
	}

	public class ListFilesResponse : MessageBase
	{

		public ListFilesResponse()
		{
			Files = new List<FileInfo>();
		}
		public List<FileInfo> Files { get; set; }
	}

}
