using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.DTCore.Domains
{
	public class Log4NetEventLogImpl : MarshalByRefObject, IEventLog
	{
		public override object InitializeLifetimeService()
		{
			return null;
		}

		public Log4NetEventLogImpl(ILogHandler handler)
		{
			Log = handler;
		}

		private ILogHandler Log;


		public void Debug(string value)
		{

			Log.Process(LogType.DEBUG, value);
		}

		public void Debug(string formater, params object[] data)
		{
			Log.Process(LogType.DEBUG, formater, data);
			
		}

		public void Info(string value)
		{
			Log.Process(LogType.INFO, value);

		}

		public void Info(string formater, params object[] data)
		{
			Log.Process(LogType.INFO, formater, data);
		}

		public void Error(string value)
		{
			Log.Process(LogType.ERROR, value);
		}

		public void Error(string formater, params object[] data)
		{
			Log.Process(LogType.ERROR, formater, data);
		}
	}
}
