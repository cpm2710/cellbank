using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Threading;

namespace ClientTest
{
    public class Client
    {
        public void TestWMIServer()
        {


            TestFileNormalEvent();
            TestEvent();
            TestGet();
            TestCreate();
            

            Thread.Sleep(900000);
        }

        private void TestFileNormalEvent(){
            ConnectionOptions Conn = new ConnectionOptions();
            //Conn.Username = "wmitest";
            //Conn.Password = "wmitest";
            Conn.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope ms = new ManagementScope(@"\\.\root\sbs", Conn);
            ms.Connect();

            var qModify = new WqlEventQuery("select * from SBSUserAddedEvent");

            ManagementEventWatcher watcher = new ManagementEventWatcher(ms, qModify);
            watcher.EventArrived += (o, e) =>
            {

                foreach (var abc in e.NewEvent.Properties)
                {
                    Console.WriteLine(abc.Name+":::"+abc.Value);
                }
                Console.WriteLine(e + "SHITSHIT");
            };
            watcher.Start();
            //SBSEventProvider.FireSBSUserAddedEvent("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1"); 
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
            //SBSUserAddedEvent.Publish("userid1");
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
            Conn.EnablePrivileges = true;
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
            var qModify = new WqlEventQuery("__InstanceModificationEvent",
                TimeSpan.FromSeconds(1),
                "TargetInstance ISA 'sbs_user'");

            var qDelete = new WqlEventQuery("__InstanceDeletionEvent",
                TimeSpan.FromSeconds(1),
                "TargetInstance ISA 'sbs_user'");


            var qCreate = new WqlEventQuery("__InstanceCreationEvent",
               TimeSpan.FromSeconds(1),
               "TargetInstance ISA 'sbs_user'");

            ManagementEventWatcher watcher = new ManagementEventWatcher(ms, qCreate);
            watcher.EventArrived += (o, e) =>
            {
                ManagementBaseObject mbo =

e.NewEvent.Properties["TargetInstance"].Value

as ManagementBaseObject;
                foreach (PropertyData item in mbo.Properties)
                {

                    Console.WriteLine(item.Name + " : " + item.Value);

                }
                Console.WriteLine(e);
            };
            watcher.Start();
        }
        public void TestRESTServer()
        {

        }
    }
}
