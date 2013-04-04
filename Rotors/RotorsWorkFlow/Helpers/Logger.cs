using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public static class Logger
    {
        public static void Log(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }
    }
}
