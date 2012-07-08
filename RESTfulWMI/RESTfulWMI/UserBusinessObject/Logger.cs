using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace UserBusinessObject
{
    public static class Logger
    {
        private const String logFile = @"c:\windowsserver2012wmicomponent.log";
        public static void Log(string msg)
        {
            using (FileStream fs = new FileStream(logFile, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(msg);
                }
            }
        }
    }
}
