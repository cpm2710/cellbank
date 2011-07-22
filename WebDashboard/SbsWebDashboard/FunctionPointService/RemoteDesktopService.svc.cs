using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.ServiceModel.Activation;
using WebDesktopDaemon;

namespace FunctionPointService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RemoteDesktopService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class RemoteDesktopService : IRemoteDesktopService
    {
        public DesktopSnapshot getDesktopSnapshot(string MachineName)
        {
            WebDesktopTCPClient client = new WebDesktopTCPClient(MachineName);
            string base64 = client.GetImage();
            DesktopSnapshot snapshot = new DesktopSnapshot();
            snapshot.DesktopBase64 = base64;
            snapshot.MachineName = MachineName;
            return snapshot;
            //DesktopSnapshot snapshot=new DesktopSnapshot();
            //snapshot.MachineName = MachineName;
            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Connect(MachineName, 3390);
            //NetworkStream ns = new NetworkStream(socket);
            //StreamReader sr = new StreamReader(ns);
            //string base64=sr.ReadToEnd();
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();
            ////IPHostEntry hostinfo = Dns.GetHostEntry(MachineName);
            //snapshot.DesktopBase64 = base64;
            ////serverIP = IPAddress.Parse("222.18.142.171");
            //return snapshot;
        }
        public DesktopSnapshot sendControlCommand(ControlCommand ControlCommand)
        {
            WebDesktopTCPClient client = new WebDesktopTCPClient(ControlCommand.MachineName);
            string base64=client.SendControl(ControlCommand);
            DesktopSnapshot snapshot = new DesktopSnapshot();
            snapshot.DesktopBase64 = base64;
            snapshot.MachineName = ControlCommand.MachineName;
            return snapshot;
        }
    }
}
