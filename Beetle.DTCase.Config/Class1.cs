using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCase.Config
{

	public class GetAppSetting : Beetle.DTCore.TestCase<string>
	{
		protected override void OnExecute()
		{
			base.OnExecute();
			System.Configuration.Configuration conf = GetConfig();
			if (conf != null)
			{
				string value = conf.AppSettings.Settings["bbq"].Value;
				Console.WriteLine(value);
			}
			System.Threading.Thread.Sleep(1000);
		}
		public override string Name
		{
			get
			{
				return "GET_APP_SETTING";
			}
		}
	}


	public class StringProperty : Beetle.DTCore.TestCase<string>
	{
		public override string Name
		{
			get
			{
				return "TEST_STRING_PROPERTY";
			}
		}
		protected override void OnExecute()
		{
			base.OnExecute();
			Console.WriteLine(this.Config);
			System.Threading.Thread.Sleep(1000);
		}
	}

	public class EmunProperty : Beetle.DTCore.TestCase<TestType>
	{
		public override string Name
		{
			get
			{
				return "TEST_ENUM_PROPERTY";
			}
		}
		protected override void OnExecute()
		{
			base.OnExecute();
			Console.WriteLine(this.Config);
			System.Threading.Thread.Sleep(1000);
		}
	}

	public enum TestType
	{
		None = 1,
		Yes = 2,
		No = 3
	}


}
