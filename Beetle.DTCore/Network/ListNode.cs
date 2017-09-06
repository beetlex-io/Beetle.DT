using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class ListNode : MessageBase
	{
	}

	public class ListNodeResponse : MessageBase
	{
		public List<Center.NodeInfo> Nodes { get; set; }
	}

}
