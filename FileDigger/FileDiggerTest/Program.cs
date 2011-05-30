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
            EndpointAddress dynamicAddress = new EndpointAddress("http://localhost:8000/ServiceModelSamples/service");
            c.Endpoint.Address = dynamicAddress;
            c.addFolder("d:\\Extra\\FRIENDS");
            String[] abc=c.findFile("Friend");
            foreach (String a in abc)
            {
                Console.WriteLine(a);
            }
            c.fetchFile("s");
        }
    }
}
