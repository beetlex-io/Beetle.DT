using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beetle.DTCore.UnitTest
{
	[TestClass]
	public class UnitTest1
	{

		private DTCore.Center.ServerCenter mCenter;

		public UnitTest1()
		{
			//mCenter = new Center.ServerCenter();
			//mCenter.Open("127.0.0.1", 10244, "127.0.0.1", 10245);
		}

		[TestMethod]
		public void TestCreateUnitTest()
		{
			string name = "http_test";
			mCenter.OnCreateTest(new Network.CreateTest { Name = name });
			Center.TestInfo info = mCenter.FolderManager.GetInfo(name);
			Assert.AreEqual(name, info.Name);
		}

		[TestMethod]
		public void TestUpdateFile()
		{
			Network.UpdateFile file = Folder.GetFile("c:\\offline_FtnInfo.txt");
			file.UnitTest = "http_test";
			mCenter.OnUpdateFile(file);
		}
		[TestMethod]
		public void TcpUtfTest()
		{
			Beetle.DTCase.Tcp.SocketTcpUtf socket = new DTCase.Tcp.SocketTcpUtf();
			Beetle.DTCase.Tcp.TcpConfig config = new DTCase.Tcp.TcpConfig();
			config.Host = "192.168.1.241";
			config.Port = 8088;
			config.Data = "henryfan";
			socket.Config = config;
			socket.Success = (i =>
			{
				Console.WriteLine(i);
			});
			socket.Error = (i, e) =>
			{
				Console.WriteLine(i.Message);
			};
			socket.Init();
			socket.Reset();
			socket.Execute();
			System.Threading.Thread.Sleep(2000);
		}

	}
}
