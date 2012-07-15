using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading;

namespace ClientTest
{
    public class Client
    {
        public void TestWMIServer()
        {
           

           

            ConnectionOptions Conn = new ConnectionOptions();
            //Conn.Username = "wmitest";
            //Conn.Password = "wmitest";
            Conn.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope ms = new ManagementScope(@"\\.\root\sbs", Conn);
            ms.Connect();

            ObjectQuery query = new ObjectQuery("select * from sbs_user where UserName='andy'");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(ms, query);
            ManagementObjectCollection users = searcher.Get();

            foreach (ManagementObject mo in users)
            {
                Console.WriteLine(mo["UserName"]);
            }

            ManagementEventWatcher watcher = new ManagementEventWatcher(@"\\.\root\sbs",
               "SELECT * FROM SBSUserAddedEvent");
            watcher.EventArrived += (o, e) =>
            {
                Console.WriteLine(e.NewEvent["UserId"]);
            };
            watcher.Start();

            Thread.Sleep(900000);
        }
        public void TestRESTServer()
        {

        }
    }
}
