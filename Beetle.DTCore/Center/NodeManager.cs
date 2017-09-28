using BeetleX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Center
{
	public class NodeManager
	{

		private Dictionary<string, NodeAgent> mNodes = new Dictionary<string, NodeAgent>();

		public NodeManager(ServerCenter center)
		{
			this.Center = center;
		}

		public void Register(Network.Register register, ISession session)
		{
			GetOrCreateNode(register.Name, session);
		}

		public ServerCenter Center { get; set; }

		private NodeAgent GetOrCreateNode(string name, ISession session)
		{
			lock (mNodes)
			{
				NodeAgent result;
				if (!mNodes.TryGetValue(name, out result))
				{
					result = new NodeAgent(Center, name, session.RemoteEndPoint);
					mNodes[name] = result;
				}
				result.LastActiveTime = Center.TimeWatch.ElapsedMilliseconds;
				result.Session = session;
				return result;
			}

		}

		public NodeAgent Get(string name)
		{
			NodeAgent result = null;
			mNodes.TryGetValue(name, out result);
			return result;

		}

		public void Ping(Network.NodePing ping, ISession session)
		{
			NodeAgent agent = GetOrCreateNode(ping.Name, session);
			agent.PerformanceInfo = ping.PerformanceInfo;
		}

		public List<NodeInfo> List()
		{
			lock (this)
			{
				List<NodeInfo> result = new List<NodeInfo>();
				foreach (NodeAgent agent in mNodes.Values)
				{
					if ((Center.TimeWatch.ElapsedMilliseconds - agent.LastActiveTime) < 5000)
						result.Add(agent.GetInfo());
				}
				return result;
			}
		}
	}
}
