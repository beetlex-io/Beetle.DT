using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore
{

	public interface ITestCase
	{

		int Interval { get; }

		long LastTime { get; set; }

		string Name { get; }

		void Init();

		void Execute();

		void Reset();

		Action<Exception, ITestCase> Error { get; set; }

		Action<ITestCase> Success { get; set; }

		ITestProcessHandler Handler { get; }


	}

	public abstract class TestCase<T> : ITestCase
	{
		public T Config { get; set; }

		public virtual void Execute()
		{
			try
			{
				OnExecute();
				Completed();
			}
			catch (Exception e)
			{
				Completed(e);
			}
		}

		protected virtual void OnExecute() { }

		public abstract string Name { get; }

		public Action<Exception, ITestCase> Error { get; set; }

		public Action<ITestCase> Success { get; set; }

		protected virtual bool Completed(Exception e = null)
		{
			if (e == null)
				OnSuccess();
			else
				OnError(e);
			return e == null;
		}

		private void OnError(Exception e)
		{
			if (Error != null)
				Error(e, this);
		}

		private void OnSuccess()
		{
			if (Success != null)
			{
				Success(this);
			}
		}

		public virtual void Init()
		{

		}

		public virtual void Reset() { }

		public virtual int Interval { get; protected set; }

		public long LastTime
		{
			get; set;
		}

		public ITestProcessHandler Handler
		{
			get
			{
				return null;
			}
		}
	}
}
