using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Node
{
	public class PerformanceInfo
	{
		public string CpuUsage { get; set; }

		public string TotalMemory { get; set; }

		public string AvailableMemory { get; set; }
	}

	public class Performance
	{

		public Performance()
		{

		}

		private PerformanceCounter cpuCounter;

		private PerformanceCounter ramCounter;

		public void Open()
		{
			cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			ramCounter = new PerformanceCounter("Memory", "Available MBytes");
		}
		public PerformanceInfo GetInfo()
		{
			PerformanceInfo result = new PerformanceInfo();
			result.CpuUsage = cpuCounter.NextValue().ToString("##.00") + "%";
			result.AvailableMemory = ramCounter.NextValue() + "MB";
			return result;
		}

	}
}
