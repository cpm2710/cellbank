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
            string tokenCookie=RSAAuthenticationUtil.Instance.Encrypt("abc");
            Console.WriteLine(tokenCookie);
            string authInstance = RSAAuthenticationUtil.Instance.Decrypt(tokenCookie);
            Console.WriteLine(authInstance);
        }
    }
}
