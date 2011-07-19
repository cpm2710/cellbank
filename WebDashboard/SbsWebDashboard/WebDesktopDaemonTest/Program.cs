using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using WebDesktopDaemon;
namespace WebDesktopDaemonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteDesktopTCPServer server = new RemoteDesktopTCPServer();
            server.Listen();

            DaemonControllerTest dct = new DaemonControllerTest();
            dct.Test();

            DaemonImageTest img = new DaemonImageTest();
            img.Test();

            

        }
    }
}
