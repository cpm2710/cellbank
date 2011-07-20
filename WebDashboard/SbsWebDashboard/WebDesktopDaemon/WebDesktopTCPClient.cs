using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WebDesktopDaemon
{
    public class WebDesktopTCPClient
    {
        private string ServerIP = "";
        private int ImagePort = 3390;
        private int ControlPort = 3391;
        public WebDesktopTCPClient(string ServerIP,int ImagePort,int ControlPort)
        {
            this.ServerIP = ServerIP;
            this.ImagePort = ImagePort;
            this.ControlPort = ControlPort;
        }
        public string GetImage()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ServerIP, ImagePort);
            NetworkStream ns = new NetworkStream(socket);
            StreamReader sr = new StreamReader(ns);
            string base64 = sr.ReadToEnd();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return base64;
        }
        public string SendControl(ControlCommand cc)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ServerIP, ControlPort);

            //ControlCommand cc = new ControlCommand();
            cc.CommandType = CommandType.mouse;
            DataContractJsonSerializer ser =
                        new DataContractJsonSerializer(typeof(ControlCommand));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, cc);

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string instanceStr = sr.ReadToEnd();

            socket.Send(System.Text.UTF8Encoding.UTF8.GetBytes(instanceStr));
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return GetImage();
        }
    }
}
