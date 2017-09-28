using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeetleX;
using BeetleX.EventArgs;
using BeetleX.Clients;
using System.Configuration;
using Beetle.MR;
namespace Beetle.DTCore.Node
{
	public class NodeApp : BeetleX.IServerHandler
	{

		public NodeApp() { }

		public NodeApp(string host, int port)
		{

			ServerHost = host;
			ServerPort = port;
			Loger = new LogHandlerAdapter();
			UNIT_TESTCASE_PATH = AppDomain.CurrentDomain.BaseDirectory + @"UnitTest" + System.IO.Path.DirectorySeparatorChar;
			Name = Guid.NewGuid().ToString("N");
			AgentManager = new Process.TestProcessAgentManager(this);
			this.Performance = new Node.Performance();

		}


		private Performance Performance { get; set; }

		private RouteCenter mRouteCenter;

		public string VerifyKey { get; set; }

		public Process.TestProcessAgentManager AgentManager { get; private set; }

		private Dictionary<Type, Delegate> mMessageHandlers = new Dictionary<Type, Delegate>();

		private string UNIT_TESTCASE_PATH = "";

		private BeetleX.Clients.IClient mServerClient;

		private BeetleX.IServer mNodeServer;

		private System.Threading.Timer mPingTimer;

		private NetConfig mNetConfig = new NetConfig();

		public string ServerHost { get; set; }

		public int ServerPort { get; set; }

		public string LocalHost { get; set; }

		public string Name { get; set; }

		public NetConfig NetConfig { get { return mNetConfig; } }

		public void Open()
		{
			try
			{
				mRouteCenter = new RouteCenter();
				mRouteCenter.Register(this);
				if (!System.IO.Directory.Exists(UNIT_TESTCASE_PATH))
					System.IO.Directory.CreateDirectory(UNIT_TESTCASE_PATH);
				mServerClient = BeetleX.ServerFactory.CreateTcpClient<Network.ClientPacket>(ServerHost, ServerPort);
				mNodeServer = BeetleX.ServerFactory.CreateTcpServer<NodeApp, Network.Packet>(mNetConfig);
				mNodeServer.Handler = this;
				while (!mNodeServer.Open())
				{
					mNodeServer.Config.Port++;
				}
				Loger.Process(LogType.INFO, "Note network start!");
				LoadProcessAgent();
				Loger.Process(LogType.INFO, "Note load unittest completed!");
				mServerClient.ClientError = OnServerError;
				mServerClient.ConnectedServer = OnServerConnect;
				mServerClient.Packet.Completed = OnPacketCompleted;
				if (mServerClient.Connect())
				{
					Loger.Process(LogType.INFO, "Note connect to {0}", ServerHost);
				}
				else
				{
					Loger.Process(LogType.ERROR, "Note connect to {0} error {1}", ServerHost, mServerClient.LastError.Message);
				}

				this.Performance.Open();

			}
			catch (Exception e_)
			{
				Loger.Process(LogType.INFO, "Note network start error {0}", e_.Message);
			}
			if (mPingTimer != null)
			{
				mPingTimer.Dispose();
			}
			mPingTimer = new System.Threading.Timer(PingServer, this, 1000, 1000);
		}


		public void Close()
		{
			mServerClient.DisConnect();
			mNodeServer.Dispose();
			AgentManager.Dispose();
			AgentManager = null;

		}

		private void PingServer(object state)
		{
			mPingTimer.Change(-1, -1);
			try
			{
				Network.NodePing ping = new Network.NodePing();
				ping.Name = this.Name;
				ping.PerformanceInfo = this.Performance.GetInfo();
				mServerClient.Send(ping);

			}
			catch (Exception e_)
			{

			}
			finally
			{
				mPingTimer.Change(1000, 1000);
			}
		}

		#region node client

		private void OnServerConnect(IClient c)
		{
			LocalHost = c.Socket.LocalEndPoint.AddressFamily.ToString();
			Network.Register register = new Network.Register();
			register.Name = Name;
			register.CreateToken(this.VerifyKey);
			c.Send(register);
		}

		private void OnPacketCompleted(IClient client, object message)
		{
			mRouteCenter.Invoke(message, this);
		}

		[Handler]
		public void OnVerifyFiles(Network.VerifyFiles e)
		{

			Process.TestProcessAgent agent = AgentManager.GetAgent(e.UnitTest);
			bool status = agent.Folder.VerifyMD5(e.Files);
			Network.VerifyFilesResponse response = new Network.VerifyFilesResponse();
			response.Node = this.Name;
			response.Status = status;
			response.SyncID = e.SyncID;
			Loger.Process(LogType.INFO, "{0} verify files", e.UnitTest);
			mServerClient.Send(response);

		}
		[Handler]
		public void OnUpfiles(Network.UpdateFile e)
		{
			Process.TestProcessAgent agent = AgentManager.GetAgent(e.UnitTest);
			agent.Stop();
			agent.Folder.UpdateFile(e.Name, e.Data);
			Loger.Process(LogType.INFO, "{0} update {1} file", e.UnitTest, e.Name);
		}
		[Handler]
		public void OnNetworkResult(Network.NetWorkResult e)
		{


		}
		[Handler]
		public void OnRunTest(Network.RunTestcase e)
		{
			Process.TestProcessAgent agent = AgentManager.GetAgent(e.UnitTest);
			agent.TestCase = e;
			agent.Stop();
			Loger.Process(LogType.INFO, "{0} unit test start", e.UnitTest);
			agent.Start();
		}
		[Handler]
		public void OnStopTest(Network.StopTestCase e)
		{
			Process.TestProcessAgent agent = AgentManager.GetAgent(e.UnitTest);
			agent.TestCase = null;
			agent.Stop();
		}


		#endregion

		private void OnServerError(BeetleX.Clients.IClient client, Exception e, string message)
		{
			Loger.Process(LogType.ERROR, "Note network start error {0}", e.Message);
		}

		private void LoadProcessAgent()
		{

		}

		public void StartProcess(string test)
		{
			Process.TestProcessAgent agent = AgentManager.GetAgent(test);
			agent.Stop();
			Loger.Process(LogType.INFO, "{0} unit test start", test);
			agent.Start();

		}

		public LogHandlerAdapter Loger { get; private set; }

		public void Connecting(IServer server, ConnectingEventArgs e)
		{

		}

		public void Connected(IServer server, ConnectedEventArgs e)
		{
			Loger.Process(LogType.INFO, "process register from {0}", e.Session.RemoteEndPoint);
		}

		public void Log(IServer server, ServerLogEventArgs e)
		{
			Loger.Process(LogType.INFO, "server {0} log {1}", server.Name, e.Message);
		}

		public void Error(IServer server, ServerErrorEventArgs e)
		{
			Loger.Process(LogType.ERROR, "server {0} error {1}", server.Name, e.Error.Message);
		}

		public void SessionReceive(IServer server, SessionReceiveEventArgs e)
		{

		}



		public void SessionPacketDecodeCompleted(IServer server, PacketDecodeCompletedEventArgs e)
		{
			mRouteCenter.Invoke(e.Message, e);
		}


		public void Disconnect(IServer server, SessionEventArgs e)
		{
			Loger.Process(LogType.INFO, "process unregister from {0}", e.Session.RemoteEndPoint);
		}

		public void SessionDetection(IServer server, SessionDetectionEventArgs e)
		{

		}


		[Handler]
		public void OnProcessReport(Network.StatisticalReport report)
		{
			Process.TestProcessAgent agent = AgentManager.GetAgent(report.UnitTest);
			agent.StatisticalInfo = report.Info;
			Loger.Process(LogType.INFO, "statistical report from {0}", report.UnitTest);
			report.Node = this.Name;
			mServerClient.Send(report);
		}
		[Handler]
		public void OnProcessRegiste(Network.ProcessRegister register)
		{
			Loger.Process(LogType.INFO, "{0} process registed", register.Name);
			PacketDecodeCompletedEventArgs e = (PacketDecodeCompletedEventArgs)RouteCenter.CurrentContext.Token;
			Process.TestProcessAgent agent = AgentManager.GetAgent(register.Name);
			agent.Session = e.Session;
			if (agent.TestCase != null)
			{
				agent.Run(agent.TestCase.TestCase, agent.TestCase.Users, agent.TestCase.Config);
				Loger.Process(LogType.INFO, "{0} process runing {1} test case", register.Name, agent.TestCase.TestCase);
			}

		}

		[Handler]
		public void OnReportTimes(Network.ReportDelayTimes e)
		{
			e.Node = this.Name;
			mServerClient.Send(e);
		}

		[Handler]
		public void OnReportErrors(Network.ReportErrors e)
		{
			e.Node = this.Name;
			mServerClient.Send(e);
		}

		public void Run(string name, string casename, int users, string config)
		{
			Process.TestProcessAgent agent = AgentManager.GetAgent(name);
			agent.Run(casename, users, config);
		}
	}
}
