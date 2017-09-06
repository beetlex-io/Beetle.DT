using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.DTCore.Domains
{
    public class ConsoleEventLogImpl:MarshalByRefObject, IEventLog
    {
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Debug(string value)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
        }

        public void Debug(string formater, params object[] data)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(formater,data);
        }

        public void Info(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value);
        }

        public void Info(string formater, params object[] data)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(formater, data);
        }

        public void Error(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value);
        }

        public void Error(string formater, params object[] data)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(formater, data);
        }
    }
}
