using System;
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
using System.Reflection;

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
            try
            {
                string[] installArgs = new string[] { @"C:\Program Files\WindowsServer2012WMIService\SBSBusinessObject.dll" };
                ManagedInstallerClass.InstallHelper(installArgs);

                string[] installArgs2 = new string[] { @"C:\Program Files\WindowsServer2012WMIService\SBSWMINotifications.dll" };
                ManagedInstallerClass.InstallHelper(installArgs2);
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
            }
            //Assembly a = Assembly.Load(@"C:\Program Files\WindowsServer2012WMIService\SBSBusinessObject.dll");

            //InstrumentationManager.RegisterAssembly(a);
            //InstrumentationManager.RegisterAssembly(
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
            try
            {
                string[] installArgs = new string[] { "/u", @"C:\Program Files\WindowsServer2012WMIService\SBSBusinessObject.dll" };
                ManagedInstallerClass.InstallHelper(installArgs);


                string[] installArgs2 = new string[] { "/u", @"C:\Program Files\WindowsServer2012WMIService\SBSWMINotifications.dll" };
                ManagedInstallerClass.InstallHelper(installArgs2);
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
            }
            //Assembly a = Assembly.Load(@"C:\Program Files\WindowsServer2012WMIService\SBSBusinessObject.dll");

            //InstrumentationManager.UnregisterAssembly(a);
            //InstrumentationManager.UnregisterType(typeof(SBS9User));
        }
    }
}
