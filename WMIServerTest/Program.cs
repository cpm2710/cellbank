using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBSBusinessObject;
using System.Configuration.Install;
using System.Management.Instrumentation;

namespace WMIServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] installArgs = new string[] { @"C:\WorkSpace\Microsoft\RESTfulWMI\WMIServerTest\bin\Release\SBSBusinessObject.dll" };
                ManagedInstallerClass.InstallHelper(installArgs);

                string[] installArgs2 = new string[] { @"C:\WorkSpace\Microsoft\RESTfulWMI\WMIServerTest\bin\Release\SBSWMINotifications.dll" };
                ManagedInstallerClass.InstallHelper(installArgs2);
                //InstrumentationManager.UnregisterType(typeof(SBSUser));
                InstrumentationManager.RegisterType(typeof(SBSUser));
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
            }

            Console.Read();

            try
            {
                string[] installArgs = new string[] { "/u", @"C:\WorkSpace\Microsoft\RESTfulWMI\WMIServerTest\bin\Release\SBSBusinessObject.dll" };
                ManagedInstallerClass.InstallHelper(installArgs);


                string[] installArgs2 = new string[] { "/u", @"C:\WorkSpace\Microsoft\RESTfulWMI\WMIServerTest\bin\Release\SBSWMINotifications.dll" };
                ManagedInstallerClass.InstallHelper(installArgs2);

                InstrumentationManager.UnregisterType(typeof(SBSUser));
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
            }
        }
    }
}
