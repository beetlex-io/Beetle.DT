using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCenter
{
	class Program
	{
		static void Main(string[] args)
		{
			string host = ConfigurationManager.AppSettings["server-host"];
			int port = int.Parse(ConfigurationManager.AppSettings["server-port"]);
			string manager_host = ConfigurationManager.AppSettings["manager-host"];
			int manager_port = int.Parse(ConfigurationManager.AppSettings["manager-port"]);
			Beetle.DTCore.Center.ServerCenter server = new DTCore.Center.ServerCenter();
			server.Loger.Handlers.Add(new Beetle.DTCore.ConsoleLogHandler());
			server.Open(host, port, manager_host, manager_port);
			Console.Read();

		}
	}
}
