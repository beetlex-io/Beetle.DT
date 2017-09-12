using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beetle.DTCore;

namespace Beetle.DTCenter.WinService
{
	class Log4NetHandler : Beetle.DTCore.ILogHandler
	{
		public Log4NetHandler()
		{
			mLoger = Utils.GetLog<Beetle.DTCore.Center.ServerCenter>();
		}

		private log4net.ILog mLoger;

		public LogType Type
		{
			get; set;
		}

		public bool Enabled(LogType type)
		{
			return true;
		}

		public void Process(LogType type, string message)
		{
			switch (type)
			{
				case LogType.DEBUG:
					mLoger.Debug(message);
					break;
				case LogType.ERROR:
					mLoger.Error(message);
					break;
				case LogType.FATAL:
					mLoger.Fatal(message);
					break;
				case LogType.INFO:
					mLoger.Info(message);
					break;
				case LogType.WARN:
					mLoger.Warn(message);
					break;
				case LogType.NONE:
					break;

			}

		}

		public void Process(LogType type, string formate, params object[] parameters)
		{
			Process(type, string.Format(formate, parameters));
		}
	}
	class Utils
	{
		static Utils()
		{
			log4net.Config.XmlConfigurator.Configure();

		}
		public static log4net.ILog GetLog<T>()
		{
			return log4net.LogManager.GetLogger(typeof(T));
		}
	}
}
