using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Json;
using WebDesktopDaemon;

namespace WebDesktopDaemonTest
{
    class DaemonControllerTest
    {
        public void Test()
        {
            WebDesktopTCPClient client = new WebDesktopTCPClient("andy-PC", 3390, 3391);
            ControlCommand cc = new ControlCommand();
            client.SendControl(cc);
        }
    }
}
