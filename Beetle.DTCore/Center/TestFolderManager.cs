using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Center
{
	public class TestFolderManager
	{

		public TestFolderManager(string path)
		{
			Path = path;
		}

		private Folder mFolder;

		public ILogHandler Log { get; set; }

		public ServerCenter Center { get; set; }

		public string Path { get; set; }

		private Dictionary<string, TestInfo> mFolders = new Dictionary<string, TestInfo>();


		public void Open()
		{
			mFolder = new DTCore.Folder(Path);
			LoadTestFolders();

		}

		private void LoadTestFolders()
		{
			lock (this)
			{

				foreach (string test in System.IO.Directory.GetDirectories(Path))
				{
					string name = System.IO.Path.GetFileNameWithoutExtension(test);
					TestInfo folder = new TestInfo(test + System.IO.Path.DirectorySeparatorChar, name);
					folder.Log = this.Log;
					folder.Center = Center;
					mFolders[name] = folder;
					folder.CopyCoreFile();
					folder.GetUnitTests();
					Log.Process(LogType.INFO, "find {0} test folder", name);
				}
			}
		}

		public TestInfo GetInfo(string name)
		{
			TestInfo result;
			mFolders.TryGetValue(name, out result);
			return result;
		}

		public List<FolderInfo> ListFolders()
		{
			lock (this)
			{
				List<FolderInfo> result = new List<FolderInfo>();
				foreach (TestInfo item in mFolders.Values)
				{
					result.Add(new DTCore.Center.FolderInfo { Name = item.Name, Status = item.GetDomainAdapter().Status, Cases = item.GetDomainAdapter().GetUnitTests() });
				}
				return result;
			}
		}

		public void Delete(string unittest)
		{
			lock (this)
			{
				TestInfo info = null;
				if (mFolders.TryGetValue(unittest, out info))
				{

					System.IO.Directory.Delete(info.FullPath, true);
					mFolders.Remove(unittest);
				}
			}
		}

		public void DeleteFile(string unittest, string filename)
		{
			TestInfo info = null;
			if (mFolders.TryGetValue(unittest, out info))
			{
				info.Folder.DeleteFile(filename);
			}
		}

		public string CreateFolder(string name)
		{
			lock (this)
			{
				TestInfo info = null;
				if (mFolders.TryGetValue(name, out info))
				{
					return info.FullPath;
				}
				else
				{
					string result = mFolder.CreateFolder(name);
					TestInfo folder = new TestInfo(result, name);
					folder.Log = this.Log;
					mFolders[name] = folder;
					folder.CopyCoreFile();
					folder.GetDomainAdapter();
					return result;
				}
			}
		}

		public void UpdateFile(string test, string filename, byte[] data)
		{
			TestInfo info = null;
			if (mFolders.TryGetValue(test, out info))
			{
				info.Folder.UpdateFile(filename, data);
				if (filename.IndexOf(".dll", StringComparison.CurrentCultureIgnoreCase) > 0)
					info.GetDomainAdapter().Status = Domains.DomainStatus.Uploading;
			}
		}
	}
}
