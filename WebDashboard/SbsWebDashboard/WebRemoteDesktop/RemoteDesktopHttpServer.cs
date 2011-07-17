using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Dashboard365Service
{
    [DataContract]
    public class DesktopSnapshot
    {
        [DataMember]
        public string MachineName;
        [DataMember]
        public string DesktopBase64;
    }
    class RemoteDesktopHttpServer
    {
        public void Listen()
        {
            using (HttpListener listener = new HttpListener())
            {
                listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证  Anonymous匿名访问
                listener.Prefixes.Add("http://localhost:3390/remotedesktops/");
                listener.Start();
                while (true)
                {
                    HttpListenerContext ctx = listener.GetContext();
                    ctx.Response.StatusCode = 200;//设置返回给客服端http状态代码
                    //Console.WriteLine(ctx.Request.Headers[0]);
                    //StreamReader sr = new StreamReader(ctx.Request.InputStream);
                    //string base64=sr.ReadToEnd();

                    string xx=ctx.Request.Headers[0];
                    string rawUrl = ctx.Request.RawUrl;
                    if(rawUrl.Equals("/remotedesktops",StringComparison.CurrentCultureIgnoreCase)){
                        DesktopSnapshot snapshot = new DesktopSnapshot();
                        snapshot.DesktopBase64 = DesktopUtil.getDesktopInBase64();
                        snapshot.MachineName = Environment.MachineName;
                        
                        MemoryStream stream1 = new MemoryStream();
                        DataContractJsonSerializer ser =
                          new DataContractJsonSerializer(typeof(DesktopSnapshot));
                        ser.WriteObject(stream1, snapshot);
                        stream1.Position = 0;
                        StreamReader sr = new StreamReader(stream1);
                        string instanceStr = sr.ReadToEnd();
                        sr.Close();
                        //DataContractJsonSerializer ser =
                        //    new DataContractJsonSerializer(typeof(AuthenticationInstance));
                        //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                        //AuthenticationInstance ai =
                        //ser.ReadObject(ms) as AuthenticationInstance;

                        using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
                        {

                            writer.Write(instanceStr);

                            writer.Close();
                            ctx.Response.Close();
                        }
                    }
                    //string[] splits=rawUrl.Split('/');
                    //string machineName = splits[1];
                    

                    
                }
            }
        }
    }
}
