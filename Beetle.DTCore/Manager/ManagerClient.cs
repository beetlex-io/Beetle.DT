using Beetle.MR;
using BeetleX.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Manager
{
	public class ManagerClient
	{

		public ManagerClient()
		{

			MessageRoute = new MR.RouteCenter();
		}

		private IClient mClient;

		public RouteCenter MessageRoute { get; private set; }

		public IClient NetClient { get { return mClient; } }

		public bool Connect(string host, int port)
		{
			mClient = BeetleX.ServerFactory.CreateTcpClient<Network.ClientPacket>(host, port);
			mClient.ConnectedServer = OnConnect;
			mClient.ClientError = OnError;
			mClient.Packet.Completed = OnPacketCompleted;
			return mClient.Connect();
		}

		private void OnConnect(IClient c)
		{

		}

		private void OnError(IClient client, Exception e, string message)
		{
			MessageRoute.Invoke(e, this);
		}

		private void OnPacketCompleted(IClient client, object message)
		{
			MessageRoute.Invoke(message, this);
		}


		public Network.GetTestConfigCode GetTestConfigCode(string unittest, string testcase)
		{
			Network.GetTestConfigCode result = new Network.GetTestConfigCode();
			result.UnitTest = unittest;
			result.TestCase = testcase;
			NetClient.Send(result);
			return result;
		}

		public void Close()
		{
			mClient.DisConnect();
			mClient = null;
		}

		public Network.CreateTest Create(string name)
		{
			Network.CreateTest create = new Network.CreateTest();
			create.Name = name;
			NetClient.Send(create);
			return create;

		}

		public void Ping()
		{
			Network.ManagerPing ping = new Network.ManagerPing();

			NetClient.Send(ping);
		}


		public Network.ListFiles ListFiles(string unittest)
		{
			Network.ListFiles list = new Network.ListFiles();
			list.UnitTest = unittest;
			this.NetClient.Send(list);
			return list;
		}


		public Network.ListTestCase ListTestCase(string unitTest)
		{
			Network.ListTestCase result = new Network.ListTestCase();
			result.UnitTest = unitTest;
			this.NetClient.Send(result);
			return result;
		}

		public Network.ListNode ListNode()
		{

			Network.ListNode node = new Network.ListNode();
			this.NetClient.Send(node);
			return node;
		}

		public Network.ListFolder List()
		{
			Network.ListFolder list = new Network.ListFolder();
			this.NetClient.Send(list);
			return list;
		}

		public Network.UpdateFile UpdateFile(string unittest, string filename)
		{
			Network.UpdateFile up = new Network.UpdateFile();
			up.UnitTest = unittest;
			up.Name = System.IO.Path.GetFileName(filename);
			using (System.IO.Stream stream = System.IO.File.OpenRead(filename))
			{
				up.Data = new byte[stream.Length];
				stream.Read(up.Data, 0, up.Data.Length);
			}
			NetClient.Send(up);
			return up;
		}


		public Network.StopTestCase StopTest(string unitTest, params string[] nodes)
		{
			Network.StopTestCase stop = new Network.StopTestCase();
			stop.UnitTest = unitTest;
			stop.Nodes = nodes;
			NetClient.Send(stop);
			return stop;
		}

		public Network.GetUnitTestReport UnitTestReport(string unitTest, params string[] nodes)
		{
			Network.GetUnitTestReport result = new Network.GetUnitTestReport();
			result.UnitTest = unitTest;
			result.Nodes = nodes;
			NetClient.Send(result);
			return result;
		}

		public Network.RunTestcase RunTest(string unitTest, string testCase, int users, object config, params string[] nodes)
		{
			Network.RunTestcase result = new Network.RunTestcase();
			result.UnitTest = unitTest;
			result.TestCase = testCase;
			result.Users = users;
			if (config != null)
				result.Config = Newtonsoft.Json.JsonConvert.SerializeObject(config);
			else
				config = "";
			result.Nodes = nodes;
			NetClient.Send(result);
			return result;
		}

		public Network.DeleteTestFolder Delete(string unittest)
		{
			Network.DeleteTestFolder del = new Network.DeleteTestFolder();
			del.Unittest = unittest;
			mClient.Send(del);
			return del;
		}

		public Network.DeleteFiles DeleteFiles(string unittest, List<string> files)
		{
			return DeleteFiles(unittest, files.ToArray());
		}

		public Network.DeleteFiles DeleteFiles(string unittest, params string[] files)
		{
			Network.DeleteFiles del = new Network.DeleteFiles();
			del.UnitTest = unittest;
			del.Files.AddRange(files);
			NetClient.Send(del);
			return del;
		}

		public Network.SyncUnitTest SyncUnitTest(string unittest, params string[] nodes)
		{
			Network.SyncUnitTest result = new Network.SyncUnitTest();
			result.UnitTest = unittest;
			result.Nodes = nodes;
			NetClient.Send(result);
			return result;
		}
	}
}
