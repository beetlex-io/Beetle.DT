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
				OnSuccess();
			}
			catch (Exception e)
			{
				OnError(e);
			}
		}

		protected abstract void OnExecute();

		public abstract string Name { get; }

		public Action<Exception, ITestCase> Error { get; set; }

		public Action<ITestCase> Success { get; set; }

		protected void OnError(Exception e)
		{
			if (Error != null)
				Error(e, this);
		}

		protected void OnSuccess()
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
