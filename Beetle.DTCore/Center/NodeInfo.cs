using Beetle.DTCore.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Center
{
	public class NodeInfo
	{
		public string Name { get; set; }

		public string EndPoint { get; set; }

		public Node.PerformanceInfo PerformanceInfo { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is NodeInfo)

			{
				return ((NodeInfo)obj).Name.Equals(this.Name);
			}
			return base.Equals(obj);
		}

		public NodeReport Report { get; set; }


		public class NodeReport
		{

			public NodeReport()
			{
				Statistical = new List<StatisticalReport>(1024 * 10);
				DelayTimes = new List<long>(1024 * 1024);
				Errors = new List<ErrorInfo>(1024 * 1024);
			}

			public string EndPoint { get; set; }

			public string Name { get; set; }

			public List<StatisticalReport> Statistical { get; set; }

			public List<long> DelayTimes { get; set; }

			public List<ErrorInfo> Errors { get; set; }

			public long GetErrors()
			{
				long result = 0;
				if (Statistical.Count > 0)
					result = Statistical[this.Statistical.Count - 1].Info.Error.Value;
				return result;
			}

			public long GetSuccess()
			{
				long result = 0;
				if (Statistical.Count > 0)
					result = Statistical[this.Statistical.Count - 1].Info.Success.Value;
				return result;
			}

			public void Reset()
			{
				Statistical.Clear();
				DelayTimes.Clear();
				Errors.Clear();
			}
		}
	}
}
