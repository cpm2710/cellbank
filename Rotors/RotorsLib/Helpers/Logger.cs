using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public enum LogLevel
    {
        Information = 0,
        Warning = 1,
        Error = 2
    }
    public static class Logger
    {
        public static LogLevel CurrentLogLevel
        {
            get
            {
                return LogLevel.Warning;
            }
        }
        public static void Log(string msg)
        {
            if (CurrentLogLevel <= LogLevel.Information)
            {
                Console.WriteLine(msg);
            }
        }
        public static void Log(string format, params object[] arg)
        {
            if (CurrentLogLevel <= LogLevel.Information)
            {
                Console.WriteLine(format, arg);
            }
        }

        public static void Error(string msg)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = defaultColor;
        }

        public static void Error(string format, params object[] arg)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, arg);
            Console.ForegroundColor = defaultColor;
        }
    }
}
