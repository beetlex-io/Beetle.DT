using Beetle.DTCore.Network;
using Beetle.MR;
using BeetleX.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Process
{
	public class TestProcess
	{

		public TestProcess(int nodeport, string name)
		{
			this.UnitTests = new Dictionary<string, DTCore.UnitTest>();
			Name = name;
			Loger = new LogHandlerAdapter();
			mClient = BeetleX.ServerFactory.CreateTcpClient<Network.ClientPacket>("127.0.0.1", nodeport);
			mClient.ConnectedServer = OnServerConnect;
			mClient.ClientError = OnClientError;
			mClient.Packet.Completed = OnClientPacketCompleted;
			TimeWatch = new System.Diagnostics.Stopwatch();

		}

		private System.Threading.Timer mTimer;

		private UnitTest mCurrentTest;

		private RouteCenter mRouteCenter = new RouteCenter();

		private IClient mClient;

		public string Name { get; set; }

		public Dictionary<string, UnitTest> UnitTests { get; private set; }

		public LogHandlerAdapter Loger { get; private set; }

		public UnitTest CurrentTest { get { return mCurrentTest; } }

		public System.Diagnostics.Stopwatch TimeWatch
		{
			get;
			private set;
		}

		public void Open()
		{
			TimeWatch.Restart();
			mRouteCenter.Register(this);
			LoadCase();
			if (!mClient.Connect())
			{
				Loger.Process(LogType.ERROR, "connect to node error {0}", mClient.LastError.Message);
			}
			else
			{
				Loger.Process(LogType.INFO, "connect to node!");
			}
			
		}

		public bool Run(string name, int user, string config)
		{
			UnitTest ut = null;
			if (mTimer != null)
				mTimer.Dispose();
			if (UnitTests.TryGetValue(name, out ut))
			{
				mCurrentTest = ut;
				ut.Users = user;
				ut.Config = config;
				ut.Execute();
				Loger.Process(LogType.INFO, "{0} unitTest runing", name);
				mTimer = new System.Threading.Timer(OnTime, null, 1000, 1000);
				return true;
			}
			else
			{
				Loger.Process(LogType.INFO, "{0} not found!", name);
			}
			return false;
		}

		private void OnTime(object state)
		{
			mTimer.Change(-1, -1);
			try
			{
				if (mCurrentTest != null)
				{
					Loger.Process(LogType.DEBUG, mCurrentTest.StatisticalInfo.ToString());
					Network.StatisticalReport resport = new Network.StatisticalReport();
					resport.UnitTest = Name;
					resport.Info = mCurrentTest.StatisticalInfo;
					mClient.Send(resport);
					
					Network.ReportDelayTimes reporttimes = new ReportDelayTimes();
					reporttimes.UnitTest = this.Name;
					reporttimes.Times = mCurrentTest.GetDelayTimes();
					mClient.Send(reporttimes);

					Network.ReportErrors reportErrors = new ReportErrors();
					reportErrors.UnitTest = this.Name;
					reportErrors.Errors = mCurrentTest.GetErrors();
					mClient.Send(reportErrors);
				}
			}
			catch (Exception e_)
			{
			}
			finally
			{
				mTimer.Change(1000, 1000);
			}
		}

		private void LoadCase()
		{
			foreach (string dll in System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
			{
				try
				{
					System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(dll);
					foreach (Type type in assembly.GetTypes())
					{
						if (type.GetInterface("Beetle.DTCore.ITestCase") != null && !type.IsAbstract)
						{
							ITestCase tcase = (ITestCase)Activator.CreateInstance(type);
							UnitTest test = new DTCore.UnitTest(this, tcase);
							UnitTests[tcase.Name] = test;
							Loger.Process(LogType.INFO, "load {0} unit test!", tcase.Name);
						}
					}
				}
				catch (Exception e_)
				{
					Loger.Process(LogType.ERROR, "load {0} assembly error {1}!", dll, e_.Message);
				}
			}
		}

		private void OnServerConnect(IClient c)
		{
			Network.ProcessRegister register = new Network.ProcessRegister();
			register.Name = Name;
			c.Send(register);
		}

		private void OnClientError(IClient client, Exception e, string message)
		{
			Loger.Process(LogType.ERROR, "Network error {0}{1}", message, e.Message);
		}

		[Handler]
		public void OnRunTest(RunTestcase e)
		{
			Loger.Process(LogType.INFO, "{0} process runing {1} test case", e.UnitTest, e.TestCase);
			this.Run(e.TestCase, e.Users, e.Config);
		}

		private void OnClientPacketCompleted(IClient client, object message)
		{
			Loger.Process(LogType.INFO, "receive message {0}", message);
			mRouteCenter.Invoke(message, client);

		}

	}
}
