using System;
using System.Collections.Generic;
using System.Text;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client();
            c.TestWMIServer();
        }
    }
}
