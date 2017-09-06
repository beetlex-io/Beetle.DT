using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beetle.DTCore.Process;
namespace Beetle.DTProcess
{
	class Program
	{
		static void Main(string[] args)
		{



			int port = 9090;
			string name = "HTTP_TEST";
			if (args.Length > 0)
			{
				port = int.Parse(args[0]);
				name = args[1];

			}

			TestProcess process = new TestProcess(port, name);
			process.Loger.Type = DTCore.LogType.ALL;
			process.Loger.Handlers.Add(new Beetle.DTCore.ConsoleLogHandler());
			process.Loger.Process(DTCore.LogType.INFO, "start args {0}", string.Join(",", args));
			process.Open();
			//process.Run(casename, 10, "{Url:\"http://www.baidu.com\"}");
			while (true)
			{
				System.Threading.Thread.Sleep(999);
				if (process.CurrentTest != null)
				{
					string value = Newtonsoft.Json.JsonConvert.SerializeObject(process.CurrentTest.StatisticalInfo);
					
				}
					Console.WriteLine();
			}
			Console.Read();

		}
	}
}
