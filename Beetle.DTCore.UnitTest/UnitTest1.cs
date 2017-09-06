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
			mCenter = new Center.ServerCenter();
			mCenter.Open("127.0.0.1", 10244, "127.0.0.1", 10245);
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

	}
}
