using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Management.Instrumentation;
[assembly: Instrumented("root/sbs")]
namespace SBSWMINotifications
{
    [RunInstaller(true)]
    public class SBSNotificationInstaller : DefaultManagementProjectInstaller
    {
    }
}
