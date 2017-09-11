using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Center
{
	public class FolderInfo
	{
		public string Name { get; set; }

		public Domains.DomainStatus Status { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is FolderInfo)
				return this.Name == ((FolderInfo)obj).Name;
			return base.Equals(obj);
		}

		public string[] Cases { get; set; }
	}
}
