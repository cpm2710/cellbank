﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Management.Instrumentation;
using SBSBusinessObject;
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
            //string[] uninstallArgs = new string[] { "/u", @"C:\Program Files\WindowsServer2012WMIService\UserBusinessObject.dll" };
            //ManagedInstallerClass.InstallHelper(uninstallArgs);

            string[] installArgs = new string[] { @"C:\Program Files\WindowsServer2012WMIService\SBSBusinessObject.dll" };
            ManagedInstallerClass.InstallHelper(installArgs);
            /*System.Configuration.Install.AssemblyInstaller myAssemblyInstaller = new System.Configuration.Install.AssemblyInstaller();
            myAssemblyInstaller.Path = "UserBusinessObject.dll";
            myAssemblyInstaller.Install(null);
            myAssemblyInstaller.Dispose();*/
            //SBSWMIInstaller installer = new SBSWMIInstaller();
           // ManagedInstallerClass.InstallHelper(new string[]{"UserBusinessObject.dll"});
            //InstrumentationManager.RegisterType(typeof(SBS9User));
            //

            Logger.WriteLine("we started");
        }

        protected override void OnStop()
        {
            string[] installArgs = new string[] { "/u", @"C:\Program Files\WindowsServer2012WMIService\UserBusinessObject.dll" };
            ManagedInstallerClass.InstallHelper(installArgs);
            //InstrumentationManager.UnregisterType(typeof(SBS9User));
        }
    }
}
