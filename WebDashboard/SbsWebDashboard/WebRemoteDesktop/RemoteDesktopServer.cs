using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Dashboard365Service
{
    class RemoteDesktopServer
    {
        private Socket serverListener;

        public RemoteDesktopServer()
        {

        }
        public void StartControllerConnection()
        {

        }
        public void StartConnection()
        {
            serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);  // Start the socket

            String hostString = Dns.GetHostName();

            IPHostEntry hostinfo = Dns.GetHostEntry(hostString);
            System.Net.IPAddress[] addresses = hostinfo.AddressList;
            String localIpStr = null;
            foreach (IPAddress address in addresses)
            {
                if (!address.IsIPv6LinkLocal)
                {
                    localIpStr = address.ToString();
                   // break;
                }
            }

            string[] ip = localIpStr.Split('.');
            IPAddress localIp = new IPAddress(new byte[] { Convert.ToByte(ip[0]), Convert.ToByte(ip[1]), Convert.ToByte(ip[2]), Convert.ToByte(ip[3]) });
            serverListener.Bind(new IPEndPoint(localIp, 3390));

            serverListener.Listen(20);
            while (true)
            {
                //等待客户端请求
                Socket sc = serverListener.Accept();
                if (sc != null)
                {
                    string desktopImageBase64=DesktopUtil.getDesktopInBase64();
                    byte[] base64Bytes = Encoding.UTF8.GetBytes(desktopImageBase64);
                    sc.Send(base64Bytes);
                    sc.Close();
                }
            }
        }
    }
}
