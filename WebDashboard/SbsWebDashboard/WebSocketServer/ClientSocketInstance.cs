using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace WebSocketServer
{
    public delegate void ClientSocketEvent(object sender, MessageEntity me);

    public class ClientSocketInstance
    {
        private byte[] ServerKey1;
        private byte[] ServerKey2;

        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Socket ClientSocket;

        public byte[] receivedDataBuffer;


        public event ClientSocketEvent NewUserConnection;
        public event ClientSocketEvent ReceiveData;
        public event ClientSocketEvent DisConnection;

        public ClientSocketInstance()
        {
            receivedDataBuffer = new byte[WebSocketProtocol.GetInstance.MaxBufferSize];
            ServerKey1 = new byte[4];
            ServerKey2 = new byte[4];

        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="result"></param>
        private void Read(IAsyncResult result)
        {
            if (!ClientSocket.Connected) return;
            try
            {
                // Web Socket protocol: 0x00开头，0xFF结尾
                System.Text.UTF8Encoding decoder = new System.Text.UTF8Encoding();
                int startIndex = 0;
                int endIndex = 0;

                //查找起启位置
                while (receivedDataBuffer[startIndex] == 0x00) startIndex++;
                // 查找结束位置
                endIndex = startIndex + 1;
                while (receivedDataBuffer[endIndex] != 0xff && endIndex != WebSocketProtocol.GetInstance.MaxBufferSize - 1) endIndex++;
                if (endIndex == WebSocketProtocol.GetInstance.MaxBufferSize - 1) endIndex = WebSocketProtocol.GetInstance.MaxBufferSize;


                string messageReceived = decoder.GetString(receivedDataBuffer, startIndex, endIndex - startIndex);

                MessageEntity me = JsonConvert.DeserializeObject(messageReceived, typeof(MessageEntity)) as MessageEntity;
                if (!string.IsNullOrEmpty(this.Name))
                {
                    ReceiveData(this, me);
                }
                else if (me.MessageId.ToLower() == "login")
                {
                    if (NewUserConnection != null)
                    {

                        this.Name = (Newtonsoft.Json.JsonConvert.DeserializeObject(me.MessageContent, typeof(ChartMessage)) as ChartMessage).Message;
                        NewUserConnection(this, me);
                    }
                }

                /* MessageEntity me=new MessageEntity();
                 me.MessageContent = messageReceived;
                 ReceiveData(this, me);*/
                Array.Clear(receivedDataBuffer, 0, receivedDataBuffer.Length);
                ClientSocket.BeginReceive(receivedDataBuffer, 0, receivedDataBuffer.Length, 0, new AsyncCallback(Read), null);
            }
            catch (Exception ex)
            {
                DisConnection(this, null);
            }
        }

        /// <summary>
        /// 发送与客户端握手信息
        /// </summary>
        /// <param name="status"></param>
        public void StartHandshake(IAsyncResult status)
        {
            int ClientHandshakeLength = (int)status.AsyncState;
            byte[] last8Bytes = new byte[8];
            Array.Copy(receivedDataBuffer, ClientHandshakeLength - 8, last8Bytes, 0, 8);
            ASCIIEncoding decoder = new System.Text.ASCIIEncoding();
            string ClientHandshake = decoder.GetString(receivedDataBuffer, 0, ClientHandshakeLength - 8);
            string[] ClientHandshakeLines = ClientHandshake.Split(new string[] { Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);


            /*请求中的Sec-WebSocket-Key1中所有的数字连在一起
             * 然后除以空格的个数，得到结果1。
             * 然后从Key2同样的得到结果2，
             * 这两个结果取整后切断为32位整数，
             * 然后转成大头的网络顺序（Big-Endian），
             * 这两个结果和请求中最后的8个字节拼在一起，
             * 然后计算MD5。
              这个MD5的16字节结果就是服务器的反馈key*/

            //计算16位的服务端Key
            foreach (string Line in ClientHandshakeLines)
            {

                if (Line.Contains("Sec-WebSocket-Key1:"))
                    BuildServerSecKey(1, Line.Substring(Line.IndexOf(":") + 2));
                if (Line.Contains("Sec-WebSocket-Key2:"))
                    BuildServerSecKey(2, Line.Substring(Line.IndexOf(":") + 2));
            }

            //握手头信息
            byte[] HandshakeText = Encoding.ASCII.GetBytes(WebSocketProtocol.GetInstance.ServerHandshake);

            byte[] serverHandshakeResponse = new byte[HandshakeText.Length + 16];
            byte[] serverKey = BuildFullServerSecKey(last8Bytes);
            Array.Copy(HandshakeText, serverHandshakeResponse, HandshakeText.Length);
            Array.Copy(serverKey, 0, serverHandshakeResponse, HandshakeText.Length, 16);
            ClientSocket.BeginSend(serverHandshakeResponse, 0, HandshakeText.Length + 16, 0, HandshakeSuccess, null);
        }

        /// <summary>
        /// 根据客户端握手Key生成客户端响应给客户端的安全Key
        /// </summary>
        /// <param name="keyNum"></param>
        /// <param name="clientKey"></param>
        private void BuildServerSecKey(int keyNum, string clientKey)
        {
            string partialServerKey = "";
            byte[] currentKey;
            int spacesNum = 0;
            char[] keyChars = clientKey.ToCharArray();
            //根据客户端Key获取得其中的空格数及其中的数字
            foreach (char currentChar in keyChars)
            {
                if (char.IsDigit(currentChar)) partialServerKey += currentChar;
                if (char.IsWhiteSpace(currentChar)) spacesNum++;
            }
            try
            {
                //用获取的数字除于空格数，再转成大头网络数据
                currentKey = BitConverter.GetBytes((int)(Int64.Parse(partialServerKey) / spacesNum));
                if (BitConverter.IsLittleEndian) Array.Reverse(currentKey);

                if (keyNum == 1) ServerKey1 = currentKey;
                else ServerKey2 = currentKey;
            }
            catch
            {
                if (ServerKey1 != null) Array.Clear(ServerKey1, 0, ServerKey1.Length);
                if (ServerKey2 != null) Array.Clear(ServerKey2, 0, ServerKey2.Length);
            }
        }


        /// <summary>
        ///生成完整的16位安全Key[将Key1和Key2加在一起再加客户端握手信息的手八位] MD5后返回
        /// </summary>
        /// <returns></returns>
        private byte[] BuildFullServerSecKey(byte[] last8Bytes)
        {
            byte[] concatenatedKeys = new byte[16];
            Array.Copy(ServerKey1, 0, concatenatedKeys, 0, 4);
            Array.Copy(ServerKey2, 0, concatenatedKeys, 4, 4);
            Array.Copy(last8Bytes, 0, concatenatedKeys, 8, 8);

            // MD5 Hash
            System.Security.Cryptography.MD5 MD5Service = System.Security.Cryptography.MD5.Create();
            return MD5Service.ComputeHash(concatenatedKeys);
        }

        /// <summary>
        /// 握手成功，此时客户端与服务端建立接连，可进行通讯
        /// </summary>
        /// <param name="result"></param>
        private void HandshakeSuccess(IAsyncResult result)
        {
            ClientSocket.EndSend(result);

            ClientSocket.BeginReceive(receivedDataBuffer, 0, receivedDataBuffer.Length, 0, new AsyncCallback(Read), null);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="me"></param>
        public void SendMessage(MessageEntity me)
        {
            ClientSocket.Send(new byte[] { 0x00 });

            ClientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(me)));
            ClientSocket.Send(new byte[] { 0xff });
        }


    }
}
