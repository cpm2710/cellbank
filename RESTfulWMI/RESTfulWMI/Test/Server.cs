using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Instrumentation;
using UserBusinessObject;
using System.ServiceModel;

namespace Test
{
    public class Server
    {
        public void StartWMI()
        {
            InstrumentationManager.RegisterType(typeof(SBS9User));
            InstrumentationManager.UnregisterType(typeof(SBS9User));
        }
        public void StartREST()
        {
            /*int port = 80;
            Uri baseAddress = new Uri("http://localhost:" + port++);
            ServiceHost sh = new ServiceHost(typeof(SBS9UserServiceWrapper), baseAddress);
            sh.Open();*/
        }
    }
}
