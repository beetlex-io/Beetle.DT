using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class NodePing : MessageBase
	{
		public string Name { get; set; }

		public Node.PerformanceInfo PerformanceInfo { get; set; }
	}
}
