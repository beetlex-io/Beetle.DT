using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace Beetle.DTCore.Process
{
	public class TestProcessAgentManager
	{
		public TestProcessAgentManager(Node.NodeApp app)
		{
			Path = AppDomain.CurrentDomain.BaseDirectory + @"UnitTest" + System.IO.Path.DirectorySeparatorChar;
			App = app;
		}


		private ConcurrentDictionary<string, TestProcessAgent> ProcessAgents = new ConcurrentDictionary<string, TestProcessAgent>();

		public string Path { get; set; }

		public Node.NodeApp App { get; set; }

		public TestProcessAgent GetAgent(string unittest)
		{
			lock (this)
			{
				if (!System.IO.Directory.Exists(Path))
				{
					System.IO.Directory.CreateDirectory(Path);
				}
				Process.TestProcessAgent agent;
				if (!ProcessAgents.TryGetValue(unittest, out agent))
				{
					
					string unittestfolder = Path + unittest + System.IO.Path.DirectorySeparatorChar;
					if (!System.IO.Directory.Exists(unittestfolder))
					{
						System.IO.Directory.CreateDirectory(unittestfolder);
					}

					agent = new Process.TestProcessAgent(
					   App, unittest, unittestfolder, App.NetConfig.Port);
					ProcessAgents[unittest] = agent;
					agent.Loger = App.Loger;
					App.Loger.Process(LogType.INFO, "load {0} unit test!", unittest);
				}
				return agent;
			}
		}
	}
}
