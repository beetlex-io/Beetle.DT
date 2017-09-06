using Beetle.DTCore.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore
{
	public class UnitTest
	{

		public UnitTest(Process.TestProcess process, ITestCase testCase)
		{
			Process = process;
			Case = testCase;
			Errors = new Exception[20];
			this.StatisticalInfo = new DTCore.StatisticalInfo();


		}

		public ITestCase Case { get; set; }

		public string Config { get; set; }

		public int Users { get; set; }

		public Process.TestProcess Process { get; set; }

		private StatisticalInfo mStatisticalInfo = new StatisticalInfo();

		private List<long> mdelayTimes = new List<long>(1024 * 1024);

		private List<Exception> mErrors = new List<Exception>(1024 * 10);

		public void AddDelayTime(long time)
		{
			lock (mdelayTimes)
			{
				mdelayTimes.Add(time);
			}
		}

		public long[] GetDelayTimes()
		{
			lock (mdelayTimes)
			{
				long[] result = mdelayTimes.ToArray();
				mdelayTimes.Clear();
				return result;

			}
		}

		public void AddError(Exception e)
		{
			lock (mErrors)
			{
				if (mErrors.Count > 0)
				{
					Exception last = mErrors[mErrors.Count - 1];
					if (last.Message != e.Message)
						mErrors.Add(e);
				}
				else
					mErrors.Add(e);
			}
		}

		public ErrorInfo[] GetErrors()
		{
			lock (mErrors)
			{
				List<ErrorInfo> result = new List<Network.ErrorInfo>();
				foreach (Exception err in mErrors)
				{
					result.Add(new Network.ErrorInfo(err));
				}
				mErrors.Clear();
				return result.ToArray();
			}
		}

		public StatisticalInfo StatisticalInfo
		{
			get; set;
		}

		private ConcurrentQueue<ITestCase> mCaseQueue;

		public Exception[] Errors { get; private set; }

		public bool Started { get; set; }

		public ITestProcessHandler Handler { get; set; }

		public void Execute()
		{
			mCaseQueue = new ConcurrentQueue<ITestCase>();
			Handler = Case.Handler;
			if (Handler == null)
				Handler = new TestProcessHandler();
			Started = true;
			this.StatisticalInfo.Reset();
			this.StatisticalInfo.Success.Result();
			this.StatisticalInfo.Error.Result();
			GetDelayTimes();
			Type caseType = Case.GetType();
			object config = null;
			System.Reflection.PropertyInfo property = caseType.GetProperty("Config", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			if (property != null)
			{
				config = Newtonsoft.Json.JsonConvert.DeserializeObject(Config, property.PropertyType);
			}
			for (int i = 0; i < Users; i++)
			{
				ITestCase item = (ITestCase)Activator.CreateInstance(caseType);
				if (property != null)
				{
					property.SetValue(item, config);
				}
				item.Init();
				item.Error = OnError;
				item.Success = OnSuccess;
				mCaseQueue.Enqueue(item);
			}

			System.Threading.ThreadPool.QueueUserWorkItem(OnExecute);
		}

		public void Stop()
		{
			Started = false;
		}

		private void OnExecute(object state)
		{
			while (true)
			{
				if (Started)
				{
					ITestCase tcase = null;
					mCaseQueue.TryPeek(out tcase);
					if (tcase == null || (Process.TimeWatch.ElapsedMilliseconds - tcase.LastTime) >= tcase.Interval)
					{
						if (mCaseQueue.TryDequeue(out tcase))
						{
							tcase.Reset();
							tcase.LastTime = Process.TimeWatch.ElapsedMilliseconds;
							Handler.Execute(tcase);
						}

					}
					else
					{
						System.Threading.Thread.Sleep(10);
					}
				}
				else
				{
					System.Threading.Thread.Sleep(10);
				}

			}
		}

		private void OnCaseRun(object state)
		{
			ITestCase item = (ITestCase)state;
			try
			{
				item.Execute();
			}
			catch (Exception e_)
			{

			}
		}

		private void OnError(Exception e, ITestCase c)
		{


			AddDelayTime(Process.TimeWatch.ElapsedMilliseconds - c.LastTime);
			this.StatisticalInfo.Error.Add();
			Errors[this.StatisticalInfo.Error.Value % 20] = e;
			mCaseQueue.Enqueue(c);
		}

		private void OnSuccess(ITestCase e)
		{
			AddDelayTime(Process.TimeWatch.ElapsedMilliseconds - e.LastTime);
			this.StatisticalInfo.Success.Add();
			mCaseQueue.Enqueue(e);

		}
	}
}
