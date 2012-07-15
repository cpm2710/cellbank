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



            TestEvent();
            TestGet();
            TestCreate();
           

            Thread.Sleep(900000);
        }

        private void TestGet()
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
        }
        private void TestCreate()
        {
            ConnectionOptions Conn = new ConnectionOptions();
            //Conn.Username = "wmitest";
            //Conn.Password = "wmitest";
            Conn.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope ms = new ManagementScope(@"\\.\root\sbs", Conn);
            ms.Connect();
            ObjectGetOptions option=new ObjectGetOptions();
            ManagementClass mc = new ManagementClass(ms, new ManagementPath("sbs_user"), option);
            ManagementObject mo=mc.CreateInstance();
            mo["UserName"] = "aaa";
            mo.Put();

        }
        private void TestEvent()
        {
            ConnectionOptions Conn = new ConnectionOptions();
            //Conn.Username = "wmitest";
            //Conn.Password = "wmitest";
            Conn.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope ms = new ManagementScope(@"\\.\root\sbs", Conn);
            ms.Connect();


            var qCreate = new WqlEventQuery("__InstanceCreationEvent",
               TimeSpan.FromSeconds(1),
               "TargetInstance ISA 'sbs_user'");

            /*EventQuery query =

new EventQuery(

@"SELECT * FROM __InstanceCreationEvent " +

@"WHERE TargetInstance ISA 'sbs_user' ");*/

            ManagementEventWatcher watcher = new ManagementEventWatcher(ms, qCreate);
            watcher.EventArrived += (o, e) =>
            {
                Console.WriteLine(e);
            };
            watcher.Start();
        }
        public void TestRESTServer()
        {

        }
    }
}
