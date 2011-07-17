using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace WebSocketServer
{
    public delegate void BroadcastEvent(MessageEntity me);

    public class WebSocketServer : IDisposable
    {
        private Socket serverListener;
        //回调，用于消息传给上层应用
        ICallback callback = null;
        //广播事件
        public BroadcastEvent BroadcastMessage = null;
        //客户端连接列表
        List<ClientSocketInstance> listConnection = new List<ClientSocketInstance>();


        public WebSocketServer(ICallback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// 启动等待连接
        /// </summary>
        public void StartConnection()
        {
            serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);  // Start the socket

            string[] ip = WebSocketProtocol.GetInstance.ServerId.Split('.');
            IPAddress localIp = new IPAddress(new byte[] { Convert.ToByte(ip[0]), Convert.ToByte(ip[1]), Convert.ToByte(ip[2]), Convert.ToByte(ip[3]) });
            serverListener.Bind(new IPEndPoint(localIp, WebSocketProtocol.GetInstance.ServerPort));

            serverListener.Listen(WebSocketProtocol.GetInstance.ConnectionsCount);
            while (true)
            {
                //等待客户端请求
                Socket sc = serverListener.Accept();
                if (sc != null)
                {
                    Thread.Sleep(100);
                    ClientSocketInstance ci = new ClientSocketInstance();
                    ci.ClientSocket = sc;
                    //初始化三个事件
                    ci.NewUserConnection += new ClientSocketEvent(Ci_NewUserConnection);
                    ci.ReceiveData += new ClientSocketEvent(Ci_ReceiveData);
                    ci.DisConnection += new ClientSocketEvent(Ci_DisConnection);
                    //开始与客户端握手[握手成功，即可通讯了]
                    ci.ClientSocket.BeginReceive(ci.receivedDataBuffer, 0, ci.receivedDataBuffer.Length, 0, new AsyncCallback(ci.StartHandshake), ci.ClientSocket.Available);
                    listConnection.Add(ci);

                }

            }

        }


        /// <summary>
        /// 断开服务端Socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="me"></param>
        private void Ci_DisConnection(object sender, MessageEntity me)
        {
            callback.DisConnection(sender as ClientSocketInstance, me);
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="me"></param>
        private void Ci_ReceiveData(object sender, MessageEntity me)
        {
            callback.Read(sender as ClientSocketInstance, me);
        }

        /// <summary>
        /// 握手成功手的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="me"></param>
        private void Ci_NewUserConnection(object sender, MessageEntity me)
        {
            ClientSocketInstance ci = sender as ClientSocketInstance;
            BroadcastMessage += new BroadcastEvent(ci.SendMessage);
            callback.NewUserConnectionJoin(ci, me);

        }

        #region IDisposable 成员

        public void Dispose()
        {
            serverListener = null;
        }

        #endregion
    }
}
