using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
namespace FileDiggerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.FileDiggerClient c = new ServiceReference1.FileDiggerClient();
            c.findSharedFolders();
            c.addPeer("192.168.0.100");
            string[] xx = c.findPeers();


        }
    }
}
