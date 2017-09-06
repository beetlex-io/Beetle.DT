using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Beetle.DTCore.Domains
{
	public class DomainAdapter
	{

		public DomainAdapter(string appPath, string appName, DomainArgs args)
		{
			Status = DomainStatus.Stop;
			mArgs = args;
			if (appPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) != appPath.Length - 1)
			{
				appPath += System.IO.Path.DirectorySeparatorChar;
			}
			AppPath = appPath;
			CachePath = Path.Combine(AppPath, "_tempdll" + Path.DirectorySeparatorChar);
			AppName = appName;
			if (args != null && args.UpdateWatch)
			{
				mWatcher = new FileWatcher(appPath, args.WatchFilter);
				mWatcher.Change += OnChange;
			}

		}

		private System.Collections.Hashtable mProperties = new System.Collections.Hashtable();

		public object this[string key]
		{
			get
			{
				return mProperties[key];
			}
			set
			{
				mProperties[key] = value;
			}
		}

		private DomainArgs mArgs;

		private FileWatcher mWatcher;

		private AssemblyLoader mLoader;

		private AppDomain mAppDomain;

		protected void OnChange(FileWatcher e)
		{
			try
			{
				if (Log != null)
				{
					Log.Info("<{0}> on updating!", AppName);
				}
				UnLoad();
				Load();
				if (Log != null)
				{
					Log.Info("<{0}> domain restart success!", AppName);
				}

			}
			catch (Exception e_)
			{
				if (Log != null)
				{
					Log.Error("<{0}> domain update error {1}!", AppName, e_.Message);
				}
			}
		}

		public DomainStatus Status
		{
			get;
			set;
		}

		public string AppName
		{
			get;
			set;
		}

		public string CachePath
		{
			get;
			set;
		}

		public string AppPath
		{ get; set; }

		public IEventLog Log
		{
			get;
			set;
		}

		public string GetConfigCode(string testCase)
		{
			return mLoader.GetConfigCode(testCase);
		}

		public string[] GetUnitTests()
		{
			return mLoader.GetUnitTests();
		}

		public void Load()
		{
			try
			{
				Log.Info("<{0}> domain creating ...", AppName);
				Type loadertype = typeof(AssemblyLoader);
				AppDomainSetup setup = new AppDomainSetup();
				setup.ApplicationName = AppName;
				setup.ApplicationBase = AppPath;
				setup.CachePath = CachePath;
				setup.ShadowCopyFiles = "true";
				setup.ShadowCopyDirectories = AppPath;
				setup.ConfigurationFile = AppPath + "app.config";
				mAppDomain = AppDomain.CreateDomain(
				   AppPath, null, setup);
				mLoader = (AssemblyLoader)mAppDomain.CreateInstanceAndUnwrap(
					loadertype.Assembly.GetName().Name,
					loadertype.FullName);
				if (mArgs != null)
					mLoader.CompilerFiles = mArgs.Compiler;
				mLoader.SetLog(Log);
				mLoader.AppName = AppName;
				mLoader.LoadAssembly(AppPath);
				mLoader.Load();
				Log.Info("<{0}> domain created!", AppName);
				Status = DomainStatus.Start;
			}
			catch (Exception e_)
			{
				if (Log != null)
				{
					Log.Error("<{0}> domain Creating error {1}!", AppName, e_.Message);
				}
			}
		}

		public void UnLoad()
		{

			if (mLoader != null)
			{
				try
				{
					Log.Info("<{0}> domain unloading ...", AppName);
					mLoader.UnLoad();
					System.Threading.Thread.Sleep(1000);
					AppDomain.Unload(mAppDomain);
					Status = DomainStatus.Stop;
					Log.Info("<{0}> domain unloaded!", AppName);
				}
				catch (Exception e_)
				{
					if (Log != null)
					{
						Log.Error("<{0}> domain unload error {1}!", AppName, e_.Message);
					}
				}
				mLoader = null;
			}

		}

		public object CreateProxyObject(string name)
		{
			if (mLoader != null)
				return mLoader.CreateProxyObject(name);
			return null;
		}

		public object GetValue(string key)
		{
			if (mLoader != null)
				return mLoader.GetValue(key);
			return null;
		}

		public void SetValue(string key, object value)
		{
			if (mLoader != null)
				mLoader.SetValue(key, value);
		}
	}
}
