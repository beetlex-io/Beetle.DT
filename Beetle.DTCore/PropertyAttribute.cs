using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyAttribute : Attribute
	{


		public PropertyType Type { get; set; }
	}

	public enum PropertyType
	{
		None,
		Remark
	}
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyLabelAttribute : Attribute
	{
		public PropertyLabelAttribute(string name, string remark)
		{
			Name = name;
			Remark = remark;
		}
		public string Name { get; set; }

		public string Remark { get; set; }
	}
}
