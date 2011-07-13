using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard365Service;
using System.Security.Cryptography;
using AuthLib;
using System.Runtime.Serialization.Json;
using System.IO;
namespace Dashboard365Service
{
    class Program
    {



        static void Main(string[] args)
        {
            string json = "{\"PassWord\":\"shitp??\",\"TimeStamp\":1,\"UserName\":\"shit\"}";
            DataContractJsonSerializer ser =
              new DataContractJsonSerializer(typeof(AuthenticationInstance));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));

            AuthenticationInstance aii =
            ser.ReadObject(ms) as AuthenticationInstance;

            AuthenticationInstance ai = new AuthenticationInstance();
            ai.UserName = "shit";
            ai.PassWord = "shitp??";
            ai.TimeStamp = 1;
            string token=AuthenticationUtil.GenCookieToken(ai);
            //AuthenticationUtil.
            AuthenticationUtil.Verify(token);
        }
    }
}
