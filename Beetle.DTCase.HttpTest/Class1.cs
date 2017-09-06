using Beetle.DTCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCase.HttpTest
{
	public class Test : TestCase<Config>
	{
		public override string Name
		{
			get
			{
				return "httptest";
			}


		}

		protected override void OnExecute()
		{
			System.Net.WebRequest wReq = System.Net.WebRequest.Create(Config.Url);
			System.Net.WebResponse wResp = wReq.GetResponse();
			System.IO.Stream respStream = wResp.GetResponseStream();
			using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.UTF8))
			{
				reader.ReadToEnd();
			}
		}
	}

	public class Config
	{

		public string Url { get; set; }
	}
}
