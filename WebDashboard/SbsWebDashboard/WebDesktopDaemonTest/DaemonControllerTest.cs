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
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("andy-PC", 3391);

            ControlCommand cc=new ControlCommand();
            cc.CommandType=CommandType.mouse;
            DataContractJsonSerializer ser =
                        new DataContractJsonSerializer(typeof(ControlCommand));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, cc);

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string instanceStr=sr.ReadToEnd();

            socket.Send(System.Text.UTF8Encoding.UTF8.GetBytes(instanceStr));
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
