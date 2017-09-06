using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTManager.Codes
{
	public class ManagerSetting
	{

		private const string CENTER_SETTING = "centersetting.json";

		public static void SaveCenterSetting(CenterSetting e)
		{
			using (System.IO.StreamWriter writer = new System.IO.StreamWriter(CENTER_SETTING, false, Encoding.UTF8))
			{
				writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(e));
			}
		}

		public static CenterSetting GetCenterSetting()
		{
			if (System.IO.File.Exists(CENTER_SETTING))
			{
				using (System.IO.StreamReader reader = new System.IO.StreamReader(CENTER_SETTING, Encoding.UTF8))
				{
					string value = reader.ReadToEnd();
					return Newtonsoft.Json.JsonConvert.DeserializeObject<CenterSetting>(value);
				}
			}
			return new Codes.CenterSetting { Host = "127.0.0.1", Port = 9092 };
		}

		public static void SaveCaseSetting(string unittest, string testcase, CaseSetting e)
		{
			string file = string.Format("{0}_{1}.json", unittest, testcase);
			using (System.IO.StreamWriter writer = new System.IO.StreamWriter(file, false, Encoding.UTF8))
			{
				writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(e));
			}
		}


		public static CaseSetting GetCaseSetting(string unittest, string testcase)
		{
			string file = string.Format("{0}_{1}.json", unittest, testcase);
			if (System.IO.File.Exists(file))
			{
				using (System.IO.StreamReader reader = new System.IO.StreamReader(file, Encoding.UTF8))
				{
					string value = reader.ReadToEnd();
					return Newtonsoft.Json.JsonConvert.DeserializeObject<CaseSetting>(value);
				}
			}
			return new CaseSetting { Users = 10 };
		}
	}
}
