using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Center
{
	public class TestInfo
	{

		public TestInfo(string path, string name)
		{
			FullPath = path;
			Name = name;
			Folder = new DTCore.Folder(path);
		}


		private Domains.DomainAdapter mDomainAdapter;

		public Folder Folder { get; set; }

		public string FullPath { get; set; }

		public string Name { get; set; }

		public ILogHandler Log { get; set; }

		public ServerCenter Center { get; set; }


		public Domains.DomainAdapter GetDomainAdapter()
		{
			lock (this)
			{
				if (mDomainAdapter == null)
				{
					mDomainAdapter = new Domains.DomainAdapter(FullPath, Name, new Domains.DomainArgs { UpdateWatch = true, WatchFilter = new string[] { "*.dll" } });
					mDomainAdapter.Log = new Domains.Log4NetEventLogImpl(this.Log);
					mDomainAdapter.Load();
				}
				return mDomainAdapter;
			}

		}

		public string GetUnitTestConfigCode(string testCase)
		{

			return GetDomainAdapter().GetConfigCode(testCase);
		}

		public string[] GetUnitTests()
		{

			return GetDomainAdapter().GetUnitTests();
		}


		public void CopyCoreFile()
		{
			string[] files = new string[] { "BeetleX.dll", "Newtonsoft.Json.dll", "Beetle.DTCore.dll", "Beetle.MR.dll" };
			foreach (string item in files)
			{
				System.IO.File.Copy(AppDomain.CurrentDomain.BaseDirectory + item, FullPath + item, true);
			}
		}



		public List<Network.FileInfo> ListFiles()
		{
			return Folder.GetFiles();
		}
	}
}
