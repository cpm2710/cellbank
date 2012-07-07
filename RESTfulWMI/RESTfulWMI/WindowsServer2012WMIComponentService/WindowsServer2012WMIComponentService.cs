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
using System.Configuration.Install;

namespace WindowsServer2012WMIComponentService
{
    public partial class WindowsServer2012WMIComponentService : ServiceBase
    {
        public WindowsServer2012WMIComponentService()
        {
            this.ServiceName = "ws2012wmisvc";
        }
        public WindowsServer2012WMIComponentService(String serviceName)
        {
            this.ServiceName = serviceName;
        }
        protected override void OnStart(string[] args)
        {
            /*System.Configuration.Install.AssemblyInstaller myAssemblyInstaller = new System.Configuration.Install.AssemblyInstaller();
            myAssemblyInstaller.Path = "UserBusinessObject.dll";
            myAssemblyInstaller.Install(null);
            myAssemblyInstaller.Dispose();*/
            //SBSWMIInstaller installer = new SBSWMIInstaller();
           // ManagedInstallerClass.InstallHelper(new string[]{"UserBusinessObject.dll"});
            InstrumentationManager.RegisterType(typeof(SBS9User));
            InstrumentationManager.UnregisterType(typeof(SBS9User));
        }

        protected override void OnStop()
        {
        }
    }
}
