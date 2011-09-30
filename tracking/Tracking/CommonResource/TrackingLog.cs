using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace CommonResource
{
    public class TrackingLog
    {
        static object syncLock = new object();
        public static void Log(string str)
        {
            lock (syncLock)
            {
                FileStream fs = new FileStream(@"C:\TrackingLog\Tracking.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(str);
            }
            //EventLog log = new EventLog("MyEvent");
            ////  首先应判断日志来源是否存在，一个日志来源只能同时与一个事件绑定s
            //if (!EventLog.SourceExists("New Application"))
            //    EventLog.CreateEventSource("New Application", "MyEvent");
            //log.Source = "New Application";
            //log.WriteEntry("Error:" + str, EventLogEntryType.Information);
            //log.Close();
        }
    }
}