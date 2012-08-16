using SBSBusinessObject;
using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallBinaries
{
    class Program
    {
        static void Main(string[] args)
        {

            if (string.Equals(args[0], "-install"))
            {
                try
                {
                    string[] installArgs = new string[] { @"C:\Program Files\WindowsServer2012WMIService\SBSBusinessObject.dll" };
                    ManagedInstallerClass.InstallHelper(installArgs);

                    //string[] installArgs2 = new string[] { @"C:\Program Files\WindowsServer2012WMIService\SBSWMINotifications.dll" };
                    //ManagedInstallerClass.InstallHelper(installArgs2);
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e.ToString());
                }
            }
            if (string.Equals(args[0], "-uninstall"))
            {
                try
                {
                    string[] installArgs = new string[] { "/u", @"C:\Program Files\WindowsServer2012WMIService\SBSBusinessObject.dll" };
                    ManagedInstallerClass.InstallHelper(installArgs);


                    //string[] installArgs2 = new string[] { "/u", @"C:\Program Files\WindowsServer2012WMIService\SBSWMINotifications.dll" };
                    //ManagedInstallerClass.InstallHelper(installArgs2);
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e.ToString());
                }
            }
        }
    }
}
