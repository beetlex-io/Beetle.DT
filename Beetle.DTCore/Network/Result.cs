using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class NetWorkResult : MessageBase
	{
		public bool Success { get; set; }

		public string Message { get; set; }
	}
}
