using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Dashboard365Service
{
    //authentication information including everything
    [DataContract(Namespace = "")]
    public class AuthenticationInstance
    {
        [DataMember]
        public string UserName;
        [DataMember]
        public string PassWord;
        [DataMember]
        public int TimeStamp;
    }
    public class AuthenticationUtil
    {
        public static string Dashboard365TokenName="Dashboard365TokenName";
        public static string GenCookieToken(AuthenticationInstance instance)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = 
              new DataContractJsonSerializer(typeof(AuthenticationInstance));
            ser.WriteObject(stream1, instance);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string instanceStr=sr.ReadToEnd();
            string tokenGenerated=RSAUtil.Instance.Encrypt(instanceStr);
            return tokenGenerated;
        }
        public static bool Verify(string token)
        {
            string json=RSAUtil.Instance.Decrypt(token);
            DataContractJsonSerializer ser =
              new DataContractJsonSerializer(typeof(AuthenticationInstance));
            MemoryStream ms=new MemoryStream(Encoding.UTF8.GetBytes(json));
            AuthenticationInstance ai =
            ser.ReadObject(ms) as AuthenticationInstance;
            return true;
        }
    }
}