using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTNode.WinService
{
	public partial class Service1 : ServiceBase
	{
		public Service1()
		{
			InitializeComponent();
		}

		private Beetle.DTCore.Node.NodeApp mNodeApp;


		protected override void OnStart(string[] args)
		{
			string host = ConfigurationManager.AppSettings["server-host"];
			int port = int.Parse(ConfigurationManager.AppSettings["server-port"]);
			mNodeApp = new DTCore.Node.NodeApp(host, port);
			mNodeApp.Loger.Type = DTCore.LogType.ALL;
			mNodeApp.Loger.Handlers.Add(new Log4NetHandler());
			mNodeApp.Open();
		}

		protected override void OnStop()
		{
			mNodeApp.Close();
		}
	}
}
