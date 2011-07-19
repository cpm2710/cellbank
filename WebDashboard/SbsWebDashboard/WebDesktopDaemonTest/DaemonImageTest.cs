using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace WebDesktopDaemonTest
{
    class DaemonImageTest
    {
        public void Test()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("andy-PC", 3390);
            NetworkStream ns = new NetworkStream(socket);
            StreamReader sr = new StreamReader(ns);
            string base64 = sr.ReadToEnd();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
