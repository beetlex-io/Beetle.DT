using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeetleX;
using BeetleX.EventArgs;
using Beetle.MR;
using Beetle.DTCore.Network;
using System.Collections.Concurrent;

namespace Beetle.DTCore.Center
{
	public class ServerCenter : IServerHandler
	{
		public ServerCenter()
		{
			mUnitTestPath = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + @"UnitTest" + System.IO.Path.DirectorySeparatorChar;
			Loger = new LogHandlerAdapter();
			TimeWatch = new System.Diagnostics.Stopwatch();
			TimeWatch.Restart();
		}

		private TestFolderManager mTestFolderManager;

		private RouteCenter mRouteCenter;

		private NodeManager mNodeManager;

		private MessageController mController = new MessageController();

		private ConcurrentDictionary<string, SyncMonitor> mSyncMonitor = new ConcurrentDictionary<string, SyncMonitor>();

		private ConcurrentDictionary<string, ISession> mUnitTestManagers = new ConcurrentDictionary<string, ISession>();

		public LogHandlerAdapter Loger { get; private set; }

		private string mUnitTestPath = "";

		private NetConfig mCenterNetConfig = new NetConfig();

		private NetConfig mCliNetConfig = new NetConfig();

		private IServer mCenterServer;

		private IServer mCliServer;

		public System.Diagnostics.Stopwatch TimeWatch { get; set; }

		public void Open(string host, int port, string cliHost, int cliPort)
		{
			mRouteCenter = new MR.RouteCenter();
			mRouteCenter.Register(this);
			if (!System.IO.Directory.Exists(mUnitTestPath))
				System.IO.Directory.CreateDirectory(mUnitTestPath);
			mCenterNetConfig.Host = host;
			mCenterNetConfig.Port = port;
			mCliNetConfig.Host = cliHost;
			mCliNetConfig.Port = cliPort;
			CreateCenterServer();
			CreateManagerServer();
			mTestFolderManager = new TestFolderManager(mUnitTestPath);
			mNodeManager = new NodeManager(this);
			mTestFolderManager.Center = this;
			mTestFolderManager.Log = this.Loger;
			mTestFolderManager.Open();
		}

		private void CreateCenterServer()
		{
			mCenterServer = ServerFactory.CreateTcpServer<ServerCenter, Packet>(mCenterNetConfig);
			mCenterServer.Name = "CENTER-SERVER";
			mCenterServer.Handler = this;
			if (mCenterServer.Open())
			{
				Loger.Process(LogType.INFO, "center network started!");
			}
		}

		private void CreateManagerServer()
		{
			mCliServer = ServerFactory.CreateTcpServer<ServerCenter, Packet>(mCliNetConfig);
			mCliServer.Name = "Manager-SERVER";
			mCliServer.Handler = this;
			if (mCliServer.Open())
			{
				Loger.Process(LogType.INFO, "center cli network started!");
			}
		}

		public TestFolderManager FolderManager { get { return mTestFolderManager; } }

		public NodeManager NodeManager { get { return mNodeManager; } }

		public void SessionPacketDecodeCompleted(IServer server, PacketDecodeCompletedEventArgs e)
		{
			mRouteCenter.Invoke(e.Message, e);
		}

		public void Connected(IServer server, ConnectedEventArgs e)
		{
			Loger.Process(LogType.INFO, "node register from {0}", e.Session.RemoteEndPoint);
		}

		public void Log(IServer server, ServerLogEventArgs e)
		{
			Loger.Process(LogType.INFO, "server {0} {1}", server.Name, e.Message);
		}

		public void Error(IServer server, ServerErrorEventArgs e)
		{
			Loger.Process(LogType.ERROR, "server {0} error {1}", server.Name, e.Error.Message);
		}

		public void SessionReceive(IServer server, SessionReceiveEventArgs e)
		{

		}

		public void Disconnect(IServer server, SessionEventArgs e)
		{
			Loger.Process(LogType.INFO, "node unregister from {0}", e.Session.RemoteEndPoint);
		}

		public void Connecting(IServer server, ConnectingEventArgs e)
		{

		}

		public void SessionDetection(IServer server, SessionDetectionEventArgs e)
		{

		}

		private void OnSyncCompleted(SyncMonitor sync)
		{
			SyncMonitor result;
			mSyncMonitor.TryRemove(sync.Message.ID, out result);
			SyncUnitTestResponse response = new Network.SyncUnitTestResponse();
			response.NodeVerifyFiles = sync.NodeVerifyFiles;
			sync.Message.Reply(response, sync.Sessioin);

		}

		[Handler]
		[ResponseFilter]
		public void OnNodeRegiste(NodeRegister register)
		{
			PacketDecodeCompletedEventArgs e = RouteCenter.CurrentContext.GetToken<PacketDecodeCompletedEventArgs>();
			Loger.Process(LogType.INFO, "note {0} registed from {1}", register.Name, e.Session.RemoteEndPoint);
			mNodeManager.Register(register, e.Session);
		}

		[Handler]
		[ResponseFilter]
		public void OnNodePing(NodePing ping)
		{
			PacketDecodeCompletedEventArgs e = RouteCenter.CurrentContext.GetToken<PacketDecodeCompletedEventArgs>();
			mNodeManager.Ping(ping, e.Session);
		}

		[Handler]
		public void OnVerifyFilesCompleted(VerifyFilesResponse e)
		{
			SyncMonitor result;
			if (mSyncMonitor.TryGetValue(e.SyncID, out result))
			{
				result.VerifyCompleted(e);
			}
		}

		[Handler]
		public void OnSyncUnitTest(SyncUnitTest e)
		{
			PacketDecodeCompletedEventArgs token = RouteCenter.CurrentContext.GetToken<PacketDecodeCompletedEventArgs>();
			SyncMonitor monitor = new Center.SyncMonitor();
			monitor.Message = e;
			monitor.Sessioin = token.Session;
			monitor.Center = this;
			monitor.Completed = OnSyncCompleted;
			mSyncMonitor[e.ID] = monitor;
			foreach (string node in e.Nodes)
			{
				monitor.Nodes.Add(this.NodeManager.Get(node));
			}
			monitor.Execute();
		}

		[Handler]
		[ResponseFilter]
		public RunTestcaseResponse OnRunTest(RunTestcase e)
		{

			PacketDecodeCompletedEventArgs pdce = RouteCenter.CurrentContext.GetToken<PacketDecodeCompletedEventArgs>();

			Loger.Process(LogType.INFO, "runing {0}:{1} with {2}", e.UnitTest, e.TestCase, string.Join(",", e.Nodes));
			RunTestcaseResponse result = new Network.RunTestcaseResponse();
			foreach (string node in e.Nodes)
			{
				NodeAgent agent = NodeManager.Get(node);
				agent.RemoveUnitTestStatistal(e.UnitTest);
				agent.RunTest(e);
			}
			mUnitTestManagers[e.UnitTest] = pdce.Session;
			return result;
		}

		[Handler]
		[ResponseFilter]
		public StopTestCaseResponse OnStopTest(StopTestCase e)
		{
			Loger.Process(LogType.INFO, "stop {0} with {1}", e.UnitTest, string.Join(",", e.Nodes));
			StopTestCaseResponse result = new Network.StopTestCaseResponse();
			foreach (string node in e.Nodes)
			{
				NodeAgent agent = NodeManager.Get(node);
				agent.StopTest(e);
			}
			return result;
		}

		[Handler]
		[ResponseFilter]
		public GetTestConfigCodeResponse OnGetTestConfigCode(GetTestConfigCode e)
		{
			GetTestConfigCodeResponse result = new Network.GetTestConfigCodeResponse();
			TestInfo test = mTestFolderManager.GetInfo(e.UnitTest);
			result.Code = test.GetUnitTestConfigCode(e.TestCase);
			result.ClassName = e.TestCase + "_Config";
			return result;
		}

		[Handler]
		public void OnManagerPing(ManagerPing ping)
		{

		}

		[Handler]
		[ResponseFilter]
		public void OnDeleteFile(DeleteFiles e)
		{
			TestInfo test = mTestFolderManager.GetInfo(e.UnitTest);
			if (test != null && e.Files != null)
			{
				foreach (string item in e.Files)
				{
					test.Folder.DeleteFile(item);
					Loger.Process(LogType.INFO, "delete {0} file {1}", e.UnitTest, item);
				}
			}
		}

		[Handler]
		[ResponseFilter]
		public void OnCreateTest(CreateTest test)
		{
			string folder = mTestFolderManager.CreateFolder(test.Name);
			Loger.Process(LogType.INFO, "created {0} unittest", test.Name);
		}

		[Handler]
		[ResponseFilter]
		public ListFolderResponse OnListFolder(ListFolder list)
		{
			ListFolderResponse response = new ListFolderResponse();
			response.Items = mTestFolderManager.ListFolders();
			return response;
		}

		[Handler]
		[ResponseFilter]
		public void OnUpdateFile(UpdateFile file)
		{
			Loger.Process(LogType.INFO, "update {0} to {1} unittest", file.Name, file.UnitTest);
			mTestFolderManager.UpdateFile(file.UnitTest, file.Name, file.Data);
		}

		[Handler]
		[ResponseFilter]
		public ListFilesResponse OnListFile(ListFiles list)
		{
			Loger.Process(LogType.INFO, "list {0} unittest files", list.UnitTest);
			ListFilesResponse response = new ListFilesResponse();
			TestInfo test = mTestFolderManager.GetInfo(list.UnitTest);
			if (test != null)
				response.Files = test.ListFiles();
			return response;
		}

		[Handler]
		[ResponseFilter]
		public GetUnitTestReportReponse OnGetUnitTestReport(GetUnitTestReport e)
		{
			GetUnitTestReportReponse response = new Network.GetUnitTestReportReponse();
			foreach (string node in e.Nodes)
			{
				NodeAgent agent = NodeManager.Get(node);
				StatisticalReport item = agent.GetUnitTestStatistal(e.UnitTest);
				if (item != null)
				{
					response.Items.Add(item);
				}
			}
			return response;
		}

		[Handler]
		public void OnReport(StatisticalReport e)
		{
			Loger.Process(LogType.INFO, "update {0}:{1} unit test report", e.Node, e.UnitTest);
			NodeAgent agent = NodeManager.Get(e.Node);
			agent.SetUnitTestStatistal(e.UnitTest, e);

		}

		[Handler]
		public void OnReportTimes(ReportDelayTimes e)
		{
			ISession mamager = null;
			if (mUnitTestManagers.TryGetValue(e.UnitTest, out mamager))
			{
				mamager.Send(e);
			}

		}

		[Handler]
		public void OnReportErrors(ReportErrors e)
		{
			ISession mamager = null;
			if (mUnitTestManagers.TryGetValue(e.UnitTest, out mamager))
			{
				mamager.Send(e);
			}
		}

		[Handler]
		[ResponseFilter]
		public void OnDeleteFolder(DeleteTestFolder e)
		{
			Loger.Process(LogType.INFO, "delete {0} unittest", e.Unittest);
			mTestFolderManager.Delete(e.Unittest);
		}

		[Handler]
		[ResponseFilter]
		public ListTestCaseResponse ListTestCase(Network.ListTestCase e)
		{
			ListTestCaseResponse response = new Network.ListTestCaseResponse();
			TestInfo test = mTestFolderManager.GetInfo(e.UnitTest);
			if (test != null)
			{
				response.Cases = test.GetUnitTests();
			}
			return response;
		}

		[Handler]
		[ResponseFilter]
		public ListNodeResponse OnListNode(ListNode e)
		{
			ListNodeResponse response = new Network.ListNodeResponse();
			response.Nodes = mNodeManager.List();
			return response;
		}

		public class ResponseFilter : FilterAttribute
		{
			public override void Execute(MethodContext context)
			{
				PacketDecodeCompletedEventArgs e = (PacketDecodeCompletedEventArgs)context.Token;
				Network.MessageBase msg = (Network.MessageBase)context.Message;
				try
				{
					base.Execute(context);
					if (context.Result != null)
					{
						msg.Reply((Network.MessageBase)context.Result, e.Session);
					}
					else
					{
						msg.ReplySuccess(e.Session);
					}
				}
				catch (Exception e_)
				{
					msg.ReplyError(e_.Message, e.Session);
				}

			}
		}

	}
}
