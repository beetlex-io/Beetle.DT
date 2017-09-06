using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Beetle.DTCore.Domains
{
	class AssemblyLoader : MarshalByRefObject
	{
		private List<string> mRefAssembly = new List<string>();

		private List<IAppModule> mModules = new List<IAppModule>();

		private FileCompiler mFileCompiler = new FileCompiler();

		private List<Assembly> mCompilerAssembly = new List<Assembly>();

		private IEventLog mLog;

		private List<ITestCase> mTests = new List<ITestCase>();

		public AssemblyLoader()
		{
			AppDomain.CurrentDomain.UnhandledException += OnUnLoaderror;
			AddReference("Accessibility.dll");
			AddReference("System.Configuration.dll");
			AddReference("System.Configuration.Install.dll");
			AddReference("System.Data.dll");
			//AddReference("System.Design.dll");
			AddReference("System.DirectoryServices.dll");
			//AddReference("System.Drawing.Design.dll");
			//AddReference("System.Drawing.dll");
			AddReference("System.EnterpriseServices.dll");
			AddReference("System.Management.dll");
			AddReference("System.Runtime.Remoting.dll");
			AddReference("System.Runtime.Serialization.Formatters.Soap.dll");
			AddReference("System.Security.dll");
			AddReference("System.ServiceProcess.dll");
			AddReference("System.Web.dll");
			AddReference("System.Web.RegularExpressions.dll");
			AddReference("System.Web.Services.dll");
			//AddReference("System.Windows.Forms.Dll");
			AddReference("System.Xml.Linq.dll");
			AddReference("System.XML.dll");
			AddReference("System.Core.dll");
		}

		public string AppName
		{
			get;
			set;
		}

		public IEventLog Log
		{
			get
			{
				return mLog;
			}
		}

		public void SetLog(IEventLog log)
		{
			mLog = log;
			Context.Current.Log = log;
		}

		public bool CompilerFiles
		{
			get;
			set;
		}

		private void AddReference(string referenceToDll)
		{
			if (!mRefAssembly.Contains(referenceToDll))
			{
				mRefAssembly.Add(referenceToDll);
			}
		}

		public void LoadAssembly(string path)
		{

			DirectoryInfo directory = new DirectoryInfo(path);
			foreach (FileInfo item in directory.GetFiles("*.dll"))
			{
				try
				{
					string filename = Path.GetFileNameWithoutExtension(item.FullName);
					Assembly assembly = Assembly.Load(filename);
					foreach (Type type in assembly.GetTypes())
					{
						if (type.GetInterface("Beetle.DTCore.ITestCase") != null && !type.IsAbstract)
						{
							ITestCase tcase = (ITestCase)Activator.CreateInstance(type);
							mTests.Add(tcase);
						}
					}
					AddReference(item.FullName);
					//foreach (Type type in assembly.GetTypes())
					//{
					//    if (type.GetInterface("Glue4Net.IAppModule") != null)
					//    {
					//        mModules.Add((IAppModule)Activator.CreateInstance(type));
					//    }
					//}
				}

				catch (Exception e_)
				{
					if (Log != null)
						Log.Error("<{0}> domain load {1} assembly error:{3}.", AppName, item.Name, e_.Message);
				}
			}
			if (CompilerFiles)
			{
				LoadVB(path);
				LoadCS(path);
			}
		}

		public string GetConfigCode(string test)
		{
			foreach (ITestCase item in mTests)
			{
				if (item.Name == test)
				{
					return CodeBuilder(item.GetType(), test);
				}
			}
			return null;
		}

		private string CodeBuilder(Type type, string test)
		{
			StringBuilder sb = new StringBuilder();
			PropertyInfo property = type.GetProperty("Config");
			if (property != null)
			{
				Type ptype = property.PropertyType;
				sb.AppendLine("public class " + test + "_Config");
				sb.AppendLine("{");
				foreach (PropertyInfo pp in ptype.GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					sb.AppendLine("[System.ComponentModel.Category(\"Case Config\")]");
					sb.AppendLine(string.Format("public {0} {1} {{get;set;}}", pp.PropertyType.FullName, pp.Name));
				}
				sb.AppendLine("}");
			}
			return sb.ToString();
		}

		public string[] GetUnitTests()
		{
			List<string> result = new List<string>();
			foreach (ITestCase tc in mTests)
			{
				result.Add(tc.Name);
			}
			return result.ToArray();

		}

		public void Load()
		{

			foreach (IAppModule module in mModules)
			{
				try
				{
					module.Log = Log;
					module.Load();
					if (Log != null)
						Log.Info("<{0}> domain load <{1}> module success!", AppName, module.Name);
				}
				catch (Exception e_)
				{
					if (Log != null)
						Log.Error("<{0}> domain load <{1}> module error {2}!", AppName, module.Name, e_.Message);
				}
			}
		}

		public void UnLoad()
		{

			foreach (IAppModule module in mModules)
			{
				try
				{
					module.UnLoad();
					if (Log != null)
						Log.Info("<{0}> domain unload <{1}> module success!", AppName, module.Name);
				}
				catch (Exception e_)
				{
					if (Log != null)
						Log.Error("<{0}> domain unload <{1}>module error {2}!", AppName, module.Name, e_.Message);
				}
			}
		}



		private void OnUnLoaderror(object sender, UnhandledExceptionEventArgs e)
		{

			if (Log != null)
			{
				Exception error = (Exception)e.ExceptionObject;
				Log.Error("<{0}> domain UnhandledException error {1} {2}", AppName, error.Message, error.StackTrace);
			}

		}

		public object CreateProxyObject(string name)
		{
			return Context.Current.CreateProxyObject(name);
		}

		public object GetValue(string key)
		{
			return Context.Current[key];
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public void SetValue(string key, object value)
		{
			Context.Current[key] = value;
		}

		private void LoadVB(string path)
		{
			string[] files = System.IO.Directory.GetFiles(path, "*.vb");
			if (files.Length > 0)
			{
				try
				{
					Log.Info("<{0}> domain compiling .vb files ...", AppName);
					Assembly assembly = mFileCompiler.CreateAssembly(files, mRefAssembly);
					mCompilerAssembly.Add(assembly);
					foreach (Type type in assembly.GetTypes())
					{
						if (type.GetInterface("Glue4Net.IAppModule") != null)
						{
							mModules.Add((IAppModule)Activator.CreateInstance(type));
						}
					}
					if (Log != null)
						Log.Info("<{0}> domain compiler .vb files success", AppName);
				}
				catch (Exception e_)
				{
					if (Log != null)
						Log.Error("<{1}> compiler .vb files error {0}", e_.Message, AppName);
				}
			}
		}

		private void LoadCS(string path)
		{
			string[] files = System.IO.Directory.GetFiles(path, "*.cs");
			if (files.Length > 0)
			{
				try
				{
					Log.Info("<{0}> domain compiling .cs files ...", AppName);
					Assembly assembly = mFileCompiler.CreateAssembly(files, mRefAssembly);
					foreach (Type type in assembly.GetTypes())
					{
						if (type.GetInterface("Glue4Net.IAppModule") != null)
						{
							mModules.Add((IAppModule)Activator.CreateInstance(type));
						}
					}
					mCompilerAssembly.Add(assembly);
					if (Log != null)
						Log.Info("<{0}> domain compiler .cs files success", AppName);
				}
				catch (Exception e_)
				{
					if (Log != null)
						Log.Error("<{1}> compiler .cs files error {0}", e_.Message, AppName);
				}
			}
		}

	}
}
