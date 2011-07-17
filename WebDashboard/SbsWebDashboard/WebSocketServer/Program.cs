using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace WebSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string[] ip = WebSocketProtocol.GetInstance.ServerId.Split('.');
            IPAddress localIp = new IPAddress(new byte[] { Convert.ToByte(ip[0]), Convert.ToByte(ip[1]), Convert.ToByte(ip[2]),Convert.ToByte(ip[3]) });
            serverListener.Bind( 
        }
    }
}
