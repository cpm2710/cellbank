using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace FileDiggerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.FileDiggerClient c = new ServiceReference1.FileDiggerClient();
            c.findSharedFolders();
        }
    }
}
