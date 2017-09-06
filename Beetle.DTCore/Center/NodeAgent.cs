using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Beetle.DTCore.Network;

namespace Beetle.DTCore.Center
{
	public class NodeAgent
	{

		public NodeAgent(ServerCenter center, string name, System.Net.EndPoint host)
		{
			Center = center;
			Host = host;
			Log = center.Loger;
			Name = name;
			this.PerformanceInfo = new Node.PerformanceInfo();
		}

		public bool Updating
		{
			get; set;
		}


		public ConcurrentDictionary<string, StatisticalReport> mUnitTestStatistial = new ConcurrentDictionary<string, StatisticalReport>();

		public StatisticalReport GetUnitTestStatistal(string name)
		{
			StatisticalReport result = null;
			mUnitTestStatistial.TryGetValue(name, out result);
			return result;
		}
		public void SetUnitTestStatistal(string name, StatisticalReport item)
		{
			mUnitTestStatistial[name] = item;
		}

		public Node.PerformanceInfo PerformanceInfo { get; set; }

		public string Name { get; set; }

		public System.Net.EndPoint Host { get; set; }

		public LogHandlerAdapter Log { get; set; }

		public ServerCenter Center { get; set; }

		public long LastActiveTime { get; set; }

		public BeetleX.ISession Session { get; set; }

		public NodeInfo GetInfo()
		{
			NodeInfo info = new DTCore.Center.NodeInfo();
			info.Name = Name;
			info.EndPoint = Host.ToString();
			info.PerformanceInfo = this.PerformanceInfo;
			return info;
		}

		public void RunTest(Network.RunTestcase e)
		{
			Session.Send(e);
		}
		public void StopTest(Network.StopTestCase e)
		{
			Session.Send(e);
		}

		public void Sync(string unittest, string syncid)
		{
			TestInfo info = Center.FolderManager.GetInfo(unittest);
			if (info == null)
				return;
			info.CopyCoreFile();
			info.Folder.Each((n, d) =>
			{
				Network.UpdateFile update = new Network.UpdateFile();
				update.UnitTest = info.Name;
				update.Name = n;
				update.Data = d;
				update.SyncID = syncid;
				Session.Send(update);
			});
			Network.VerifyFiles verify = new Network.VerifyFiles();
			verify.SyncID = syncid;
			verify.UnitTest = info.Name;
			verify.Files = info.Folder.GetFilesMD5();
			Session.Send(verify);
		}
	}
}
