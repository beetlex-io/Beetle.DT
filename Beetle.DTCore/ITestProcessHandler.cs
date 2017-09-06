using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore
{
	public interface ITestProcessHandler
	{
		void Execute(ITestCase test);
	}

	public class TestProcessHandler : ITestProcessHandler
	{
		public void Execute(ITestCase test)
		{
			Task.Run(() =>
			{
				try
				{
					test.Execute();
					
				}
				catch
				{
				}

			});
		}
	}

}
