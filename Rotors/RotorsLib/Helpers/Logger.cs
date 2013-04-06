using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib.Helpers
{
    public enum LogLevel
    {
        Information = 0,
        Warning = 1,
        Error = 2
    }
    public class Logger : IReportObserver
    {
        public void ReportStatus(string statusMsg, LogLevel logLevel = LogLevel.Information)
        {
            if (logLevel > LogLevel.Warning)
            {
                this.Error(statusMsg);
            }
            this.Log(statusMsg);
        }
        public LogLevel CurrentLogLevel
        {
            get
            {
                return LogLevel.Warning;
            }
        }
        public void Log(string msg)
        {
            if (CurrentLogLevel <= LogLevel.Information)
            {
                Console.WriteLine(msg);
            }
        }
        public void Log(string format, params object[] arg)
        {
            if (CurrentLogLevel <= LogLevel.Information)
            {
                Console.WriteLine(format, arg);
            }
        }

        public void Error(string msg)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = defaultColor;
        }

        public void Error(string format, params object[] arg)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, arg);
            Console.ForegroundColor = defaultColor;
        }
    }
}
