﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading;
using System.Collections;
using System.Configuration.Install;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {




            /*
             * string[] installArgs = new string[] { @"C:\Program Files (x86)\WindowsServer2012WMIService\UserBusinessObject.dll" };
            ManagedInstallerClass.InstallHelper(installArgs);
             * System.Configuration.Install.AssemblyInstaller myAssemblyInstaller = new System.Configuration.Install.AssemblyInstaller();
            myAssemblyInstaller.Path = @"C:\Program Files (x86)\WindowsServer2012WMIService\UserBusinessObject.dll";
            Hashtable mySavedState = new Hashtable();
myAssemblyInstaller.Install(mySavedState); 
myAssemblyInstaller.Commit(mySavedState); 
            myAssemblyInstaller.Dispose();
            */
            ThreadStart ts = new ThreadStart(() => {

                Server s = new Server();
                //s.StartREST();
                s.StartWMI();
            });
            Thread t = new Thread(ts);
            t.Start();
            t.Join();

            ThreadStart ts2 = new ThreadStart(() =>
            {

                Server s2 = new Server();
                s2.StartREST();
                s2.StartWMI();
            });
            Thread t2 = new Thread(ts2);
            t2.Start();
            t2.Join();

            //ManagementClass mc=new ManagementClass(
            //ManagementClass mc = new ManagementClass("SBS9_USER");
           // mc.GetInstances();
        }
    }
}
