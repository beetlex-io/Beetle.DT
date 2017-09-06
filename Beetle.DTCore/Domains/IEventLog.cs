using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.DTCore.Domains
{
    public interface IEventLog
    {

        void Debug(string value);

        void Debug(string formater, params object[] data);

        void Info(string value);

        void Info(string formater, params object[] data);

        void Error(string value);

        void Error(string formater, params object[] data);


    }
}
