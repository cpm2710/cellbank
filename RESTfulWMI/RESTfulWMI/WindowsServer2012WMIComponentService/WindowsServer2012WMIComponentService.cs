using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Management.Instrumentation;
using UserBusinessObject;

namespace WindowsServer2012WMIComponentService
{
    public partial class WindowsServer2012WMIComponentService : ServiceBase
    {
        public WindowsServer2012WMIComponentService()
        {
            this.ServiceName = "";
        }
        public WindowsServer2012WMIComponentService(String serviceName)
        {
            this.ServiceName = serviceName;
        }
        protected override void OnStart(string[] args)
        {
            InstrumentationManager.RegisterType(typeof(SBS9User));
            InstrumentationManager.UnregisterType(typeof(SBS9User));
        }

        protected override void OnStop()
        {
        }
    }
}
