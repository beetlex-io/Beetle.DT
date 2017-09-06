using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.DTCore.Domains
{
    public interface IAppModule
    {
        string Name
        {
            get;
        }
        IEventLog Log
        {
            get;
            set;
        }
        void Load();
        void UnLoad();
    }
}
