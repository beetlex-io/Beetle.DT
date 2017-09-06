using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class ErrorInfo
	{
		public ErrorInfo()
		{
		}
		public ErrorInfo(Exception e)
		{
			Message = e.Message;
			StackTrace = e.StackTrace;
		}

		public string Message { get; set; }

		public string StackTrace { get; set; }
	}
}
