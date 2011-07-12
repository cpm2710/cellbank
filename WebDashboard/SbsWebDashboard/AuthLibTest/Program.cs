using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard365Service;
using System.Security.Cryptography;

namespace Dashboard365Service
{
    class Program
    {



        static void Main(string[] args)
        {
            AuthenticationInstance ai = new AuthenticationInstance();
            ai.UserName = "shit";
            ai.PassWord = "shitp";
            ai.TimeStamp = 1;
            string token=AuthenticationUtil.GenCookieToken(ai);
            AuthenticationUtil.Verify(token);
        }
    }
}
