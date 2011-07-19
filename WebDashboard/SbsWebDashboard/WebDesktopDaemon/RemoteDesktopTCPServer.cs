using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WebDesktopDaemon
{
    public class RemoteDesktopTCPServer
    {
        private Socket serverImageListener;
        private Socket serverControlListener;
        private int ImagePort = 3390;
        private int ControlPort = 3391;
        public RemoteDesktopTCPServer()
        {

        }
        private int ReadControllerBufferSize = 1000;
        private void ListenControllerRequest()
        {
            serverControlListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);  // Start the socket

            IPAddress localIp = this.getLocalIP();
            serverControlListener.Bind(new IPEndPoint(localIp, this.ControlPort));
            serverControlListener.Listen(20);
            while (true)
            {
                //等待客户端请求
                Socket sc = serverControlListener.Accept();
                if (sc != null)
                {
                    byte[] buffer = new byte[ReadControllerBufferSize];
                    
                    int size=sc.Receive(buffer);
                    byte[] realData = new byte[size];
                    Array.Copy(buffer, realData, size);
                    string controlstr = System.Text.UTF8Encoding.UTF8.GetString(realData);
                    byte[] trimed=System.Text.UTF8Encoding.UTF8.GetBytes(controlstr.Trim());
                    DataContractJsonSerializer ser =
                        new DataContractJsonSerializer(typeof(ControlCommand));
                    MemoryStream ms = new MemoryStream(trimed);

                    ControlCommand cc=ser.ReadObject(ms) as ControlCommand;
                    Console.Write("cc.CommandType=="+cc.CommandType);
                    sc.Close();
                }
            }
        }
        
        public void Listen()
        {
            ThreadStart imageThreadStart = new ThreadStart(this.ListenImageRequest);
            Thread imageThread = new Thread(imageThreadStart);
            imageThread.Start();

            ThreadStart controllerThreadStart = new ThreadStart(this.ListenControllerRequest);
            Thread controllerThread = new Thread(controllerThreadStart);
            controllerThread.Start();

        }
        private void ListenImageRequest()
        {
            serverImageListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);  // Start the socket

            IPAddress localIp = this.getLocalIP();
            serverImageListener.Bind(new IPEndPoint(localIp, this.ImagePort));
            serverImageListener.Listen(20);
            while (true)
            {
                //等待客户端请求
                Socket sc = serverImageListener.Accept();
                if (sc != null)
                {
                    string desktopImageBase64 = DesktopUtil.getDesktopInBase64();
                    byte[] base64Bytes = Encoding.UTF8.GetBytes(desktopImageBase64);
                    sc.Send(base64Bytes);
                    sc.Close();
                }
            }
        }

        private IPAddress getLocalIP()
        {
            String hostString = Dns.GetHostName();

            IPHostEntry hostinfo = Dns.GetHostEntry(hostString);
            System.Net.IPAddress[] addresses = hostinfo.AddressList;
            String localIpStr = null;
            foreach (IPAddress address in addresses)
            {
                if (!address.IsIPv6LinkLocal)
                {
                    localIpStr = address.ToString();
                    break;
                }
            }

            string[] ip = localIpStr.Split('.');
            IPAddress localIp = new IPAddress(new byte[] { Convert.ToByte(ip[0]), Convert.ToByte(ip[1]), Convert.ToByte(ip[2]), Convert.ToByte(ip[3]) });
            return localIp;
        }
    }
}
