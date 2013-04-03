using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public static class ServiceControlHelper
    {
        public static void ShutdownService(string service)
        {
            Console.WriteLine("shutting down service begins: {0}", service);
            Console.WriteLine("shutting down service ends: {0}", service);
        }
        public static void StartService(string service)
        {
            Console.WriteLine("starting up service begins: {0}", service);
            Console.WriteLine("starting up service ends: {0}", service);
        }
    }
}
