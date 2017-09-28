using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTNode
{
	class Program
	{
		static void Main(string[] args)
		{
			string host = ConfigurationManager.AppSettings["server-host"];
			int port = int.Parse(ConfigurationManager.AppSettings["server-port"]);
			string serverkey = ConfigurationManager.AppSettings["server-key"];
			Beetle.DTCore.Node.NodeApp app = new DTCore.Node.NodeApp(host, port);
			app.Loger.Type = DTCore.LogType.ALL;
			app.Loger.Handlers.Add(new Beetle.DTCore.ConsoleLogHandler());
			app.VerifyKey = serverkey;
			app.Open();
			//app.StartProcess("HTTP_TEST");
			//, "httptest", "{Url:\"http://www.baidu.com\"}"
			//System.Threading.Thread.Sleep(10000);
			//app.Run("HTTP_TEST", "httptest", 10, "{Url:\"http://www.baidu.com\"}");
			Console.Read();

		}
	}
}
