using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

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
        }
        public void TestRESTServer()
        {

        }
    }
}
