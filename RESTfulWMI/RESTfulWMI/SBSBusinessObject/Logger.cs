using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SBSBusinessObject
{
    public static class Logger
    {
        private const String logFile = @"C:\Users\andy\Desktop\windowsserver2012wmicomponent.log";
        private static object sync_obj = new object();
        public static void WriteLine(string msg)
        {
            lock (sync_obj)
            {
                using (FileStream fs = new FileStream(logFile, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(msg);
                        //sw.Flush();
                    }
                    //fs.Flush();
                }
            }
        }
    }
}
