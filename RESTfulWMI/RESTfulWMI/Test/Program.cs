using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionOptions Conn = new ConnectionOptions();
            //Conn.Username = "wmitest";
            //Conn.Password = "wmitest";
            ManagementScope ms = new ManagementScope(@"\\.\root\sbs9",Conn);
            ms.Connect();

            ObjectQuery query = new ObjectQuery("select * from sbs9_user");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(ms, query);
            ManagementObjectCollection users = searcher.Get();

            foreach (ManagementObject mo in users)
            {
                Console.WriteLine(mo["UserName"]);
            }
            //ManagementClass mc=new ManagementClass(
            //ManagementClass mc = new ManagementClass("SBS9_USER");
           // mc.GetInstances();
        }
    }
}
