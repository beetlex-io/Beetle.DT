using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.DTCore.Domains
{
	public enum DomainStatus
	{
		None,
		Completed,
		Error,
		Uploading,
		Stop
	}
}
