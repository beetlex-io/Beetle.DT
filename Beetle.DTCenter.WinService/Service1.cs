using Beetle.DTCore.Center;
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

namespace Beetle.DTCenter.WinService
{
	public partial class Service1 : ServiceBase
	{
		public Service1()
		{
			InitializeComponent();
		}
		private ServerCenter mServer;

		protected override void OnStart(string[] args)
		{
			string host = ConfigurationManager.AppSettings["server-host"];
			int port = int.Parse(ConfigurationManager.AppSettings["server-port"]);
			string manager_host = ConfigurationManager.AppSettings["manager-host"];
			int manager_port = int.Parse(ConfigurationManager.AppSettings["manager-port"]);
			mServer = new DTCore.Center.ServerCenter();
			mServer.Loger.Handlers.Add(new Log4NetHandler());
			mServer.Open(host, port, manager_host, manager_port);
		}

		protected override void OnStop()
		{
			if (mServer != null)
				mServer.Close();
		}
	}
}
