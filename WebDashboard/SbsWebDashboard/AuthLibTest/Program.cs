using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard365Service;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System.IO;
namespace Dashboard365Service
{
    class Program
    {



        static void Main(string[] args)
        {
            string json = "{\"UserName\":\"a\",\"PassWord\":\"b@@##$?b\",\"TimeStamp\":1}";
            DataContractJsonSerializer ser =
              new DataContractJsonSerializer(typeof(AuthenticationInstance));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));

            AuthenticationInstance aii =
            ser.ReadObject(ms) as AuthenticationInstance;

            string token=AuthenticationUtil.GenCookieToken(aii);
            //AuthenticationUtil.
            //string tokdden=@"FoluB\/oYyXPTbFCRTrJMbwBodY4jR1RwVtGSXCtrxa4OjeIsovSI0nihqPXllAUdHTW9dNuOuaIk5Xb93dqDG6Ld6qnuRTeuXp0hpFrbwkwOataMcS2NwE9NCcgQZlH4IjdCjds7euP5kxzWjPVmWiP67YJz4zVQieQ6iJDLEIs=";

            AuthenticationUtil.Verify(token);
        }
    }
}
