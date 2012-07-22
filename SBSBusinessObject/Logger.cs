using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SBSBusinessObject
{
    public static class Logger
    {
        private static String logFile = null;
        private static object sync_obj = new object();
        public static void WriteLine(string msg)
        {
            lock (sync_obj)
            {
                if (logFile == null)
                {
                    //logFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\windowsserver2012wmicomponent.log";

                    logFile = @"C:\Users\AuroraUser\Desktop\windowsserver2012wmicomponent.log";
                    
                }
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
