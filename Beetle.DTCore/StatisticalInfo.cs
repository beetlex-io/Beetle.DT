using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore
{
	public class StatisticalInfo
	{

		public StatisticalInfo()
		{

			Success = new DTCore.StatisticalInfo.InfoItem("Success");
			Error = new DTCore.StatisticalInfo.InfoItem("Error");

		}



		public InfoItem Success
		{
			get; set;



		}

		public InfoItem Error
		{

			get; set;

		}


		public void Reset()
		{
			Success.Result();
			Error.Result();
		}


		public override string ToString()
		{
			return string.Format("Success={0}|Error={1}", Success.Value, Error.Value);
		}




		public class InfoItem
		{

			public InfoItem()
			{

			}

			public InfoItem(string key, string name = null)
			{
				if (name == null)
					name = key;
				Key = key;
				Name = name;

			}

			private long mValue = 0;

			public string Key { get; set; }

			public string Name { get; set; }

			public long Value { get { return mValue; } set { mValue = value; } }

			public void Add(long value = 1)
			{
				System.Threading.Interlocked.Add(ref mValue, value);
			}

			public void Result()
			{
				mValue = 0;
			}
		}

	}
}
