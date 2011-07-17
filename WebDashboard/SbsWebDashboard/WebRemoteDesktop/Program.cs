using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard365Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //DesktopUtil util = new DesktopUtil();
            //string abc=DesktopUtil.getDesktopInBase64();
            RemoteDesktopServer server = new RemoteDesktopServer();
            server.StartConnection();
        }
    }
}
