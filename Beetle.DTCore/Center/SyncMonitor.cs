using Beetle.DTCore.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Center
{
	public class SyncMonitor
	{

		public SyncMonitor()
		{
			NodeVerifyFiles = new List<Network.VerifyFilesResponse>();
			Nodes = new List<NodeAgent>();
		}

		private int mSyncCompleteds = 0;

		public List<VerifyFilesResponse> NodeVerifyFiles { get; set; }

		public Network.SyncUnitTest Message { get; set; }

		public BeetleX.ISession Sessioin { get; set; }

		public IList<NodeAgent> Nodes { get; set; }

		public ServerCenter Center { get; set; }

		public Action<SyncMonitor> Completed
		{
			get; set;
		}

		public void Execute()
		{
			TestInfo info = Center.FolderManager.GetInfo(Message.UnitTest);
			if (info != null)
			{
				foreach (NodeAgent agent in Nodes)
				{
					agent.Sync(Message.UnitTest, Message.ID);
				}
			}
		}

		public void VerifyCompleted(Network.VerifyFilesResponse response)
		{
			lock (this)
			{
				NodeVerifyFiles.Add(response);
				mSyncCompleteds++;
				if (mSyncCompleteds == Nodes.Count)
				{
					if (Completed != null)
						Completed(this);
				}
			}
		}
	}
}
