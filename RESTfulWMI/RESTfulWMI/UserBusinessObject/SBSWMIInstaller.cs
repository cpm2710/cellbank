﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Management.Instrumentation;
[assembly: WmiConfiguration(@"root/sbs9", HostingModel = ManagementHostingModel.LocalSystem)]
namespace UserBusinessObject
{
    [System.ComponentModel.RunInstaller(true)]
    public class SBSWMIInstaller : DefaultManagementInstaller
    {
        /*public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            System.Runtime.InteropServices.RegistrationServices RS = 
                new System.Runtime.InteropServices.RegistrationServices();
            
            //This should be fixed with .NET 3.5 SP1
            RS.RegisterAssembly(System.Reflection.Assembly.LoadFile(
                Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Reference Assemblies\Microsoft\Framework\v3.5\System.Management.Instrumentation.dll")), 
                System.Runtime.InteropServices.AssemblyRegistrationFlags.SetCodeBase);
        }

        public override void Uninstall(IDictionary savedState)
        {
            
            try
            {
                ManagementClass MC = new ManagementClass(@"root\sbs8:SBS9_User");
                MC.Delete();
            }
            catch { }

            try
            {
                base.Uninstall(savedState);
            }
            catch { }
        }*/
    }
}