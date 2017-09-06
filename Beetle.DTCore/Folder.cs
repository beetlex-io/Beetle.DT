using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore
{
	public class Folder
	{
		public Folder(string path)
		{
			Path = path;
		}

		public string Path { get; set; }

		public void Clear()
		{
			foreach (string file in System.IO.Directory.GetFiles(Path))
			{
				System.IO.File.Delete(file);
			}
		}

		public static Network.UpdateFile GetFile(string filename)
		{
			Network.UpdateFile result = new Network.UpdateFile();
			using (System.IO.Stream stream = System.IO.File.OpenRead(filename))
			{
				result.Name = System.IO.Path.GetFileName(filename);
				result.Data = new byte[stream.Length];
				stream.Read(result.Data, 0, result.Data.Length);
			}
			return result;

		}

		public string CreateFolder(string name)
		{
			string folder = Path + name + System.IO.Path.DirectorySeparatorChar;
			if (!System.IO.Directory.Exists(folder))
			{
				System.IO.Directory.CreateDirectory(folder);
			}
			return folder;
		}

		public void UpdateFile(string name, byte[] data)
		{
			string file = Path + name;
			using (System.IO.Stream stream = System.IO.File.Create(file))
			{
				stream.Write(data, 0, data.Length);
				stream.Flush();
			}
		}

		public void DeleteFile(string name)
		{
			string file = Path + name;
			if (System.IO.File.Exists(file))
				System.IO.File.Delete(file);
		}

		public string GetFileMD5(string name, bool fullpath = false)
		{
			string file = name;
			if (!fullpath)
				file = Path + name;
			if (!System.IO.File.Exists(file))
				return null;
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(file))
				{
					byte[] retVal = md5.ComputeHash(stream);
					StringBuilder sb = new StringBuilder();
					for (int i = 0; i < retVal.Length; i++)
					{
						sb.Append(retVal[i].ToString("x2"));
					}
					return sb.ToString();
				}
			}
		}

		public bool VerifyMD5(List<Network.FileMD5> files)
		{
			foreach (Network.FileMD5 item in files)
			{
				if (item.MD5 != GetFileMD5(item.Name))
					return false;
			}
			return true;
		}

		public List<Network.FileMD5> GetFilesMD5()
		{
			List<Network.FileMD5> result = new List<Network.FileMD5>();
			foreach (string file in System.IO.Directory.GetFiles(Path))
			{
				result.Add(new Network.FileMD5 { MD5 = GetFileMD5(file, true), Name = System.IO.Path.GetFileName(file) });
			}
			return result;
		}

		public void Each(Action<string, byte[]> handler)
		{
			foreach (string item in System.IO.Directory.GetFiles(Path))
			{
				string name = System.IO.Path.GetFileName(item);
				byte[] data;
				using (System.IO.Stream stream = System.IO.File.OpenRead(item))
				{
					data = new byte[stream.Length];
					stream.Read(data, 0, data.Length);
				}
				handler(name, data);
			}
		}

		public List<Network.FileInfo> GetFiles()
		{
			List<Network.FileInfo> result = new List<Network.FileInfo>();

			foreach (string item in System.IO.Directory.GetFiles(Path))
			{
				Network.FileInfo info = new Network.FileInfo();
				info.Name = System.IO.Path.GetFileName(item);

				FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(item);
				if (myFileVersionInfo != null)
					info.Version = myFileVersionInfo.FileVersion;
				info.UpdateTime = System.IO.File.GetLastWriteTime(item).ToString();
				result.Add(info);
			}
			return result;
		}

	}
}
