using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Dashboard365Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //DesktopUtil util = new DesktopUtil();
            //string abc=DesktopUtil.getDesktopInBase64();
            //string MachineName = "andy-PC";
            //IPHostEntry hostinfo = Dns.GetHostEntry(MachineName); 

            //RemoteDesktopServer server = new RemoteDesktopServer();
            //server.StartConnection();
            RemoteDesktopHttpServer server = new RemoteDesktopHttpServer();
            server.Listen();
        }
    }
}
