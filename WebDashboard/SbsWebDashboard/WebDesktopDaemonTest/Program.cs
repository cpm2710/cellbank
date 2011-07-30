using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using WebDesktopDaemon;
using System.Threading;
using System.Runtime.Serialization.Json;
namespace WebDesktopDaemonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //DesktopUtil.Instance.GetChangedImages();
            WebDesktopTCPServer server = new WebDesktopTCPServer();
            server.Listen();
            long now = System.DateTime.Now.Ticks / 10000;
            while (true)
            {
                now = System.DateTime.Now.Ticks / 10000;
                WebDesktopTCPClient CLIENT = new WebDesktopTCPClient("andy-PC", 3390, 3391);
                string base64String = CLIENT.GetSliceImages();

                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(base64String));
                DataContractJsonSerializer ser =
                  new DataContractJsonSerializer(typeof(DesktopSnapshotList));
                DesktopSnapshotList ss = ser.ReadObject(ms) as DesktopSnapshotList;
                Console.WriteLine((System.DateTime.Now.Ticks / 10000 - now) + "ms");
                for (int i = 0; i < ss.Count; i++)
                {
                    Console.WriteLine(ss[i].Width + "  and " + ss[i].Height);
                }
                Console.WriteLine(ss.Count);

                Thread.Sleep(2000);
            }
        }
    }
}
