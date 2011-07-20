using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using WebDesktopDaemon;
namespace WebDesktopDaemonTest
{
    class DaemonImageTest
    {
        public void Test()
        {
            WebDesktopTCPClient client = new WebDesktopTCPClient("andy-PC", 3390, 3391);
            client.GetImage();
            //ControlCommand cc=new ControlCommand();
            //client.SendControl(cc);
        }
    }
}
