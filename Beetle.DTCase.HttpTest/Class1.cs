using Beetle.DTCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCase.Http
{
	public class HttpGet : TestCase<Config>
	{
		public override string Name
		{
			get
			{
				return "HTTP_GET";
			}
		}

		private long mIndex = 0;

		private List<string> mUrls = new List<string>();

		public override void Init()
		{
			base.Init();
			mUrls.AddRange(this.Config.Urls.Split(';'));
		}

		public string GetUrl()
		{
			mIndex++;
			return mUrls[(int)(mIndex % mUrls.Count)];
		}

		protected override void OnExecute()
		{
			System.Net.WebRequest wReq = System.Net.WebRequest.Create(GetUrl());
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
		[PropertyLabel("连接地址", "进行压力测试的连接，如果是多个连接可以用;来分隔")]
		[Property(Type = PropertyType.Remark)]
		public string Urls { get; set; }
	}
}
