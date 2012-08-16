using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBSBusinessObject
{
    public class MockRepository
    {
        public static List<SBSUser> sbsUsers = new List<SBSUser>() { 
            new SBSUser("andy","andypswd","andliu@microsoft.com"),
            new SBSUser("andy2","andy2pswd","andliu2@microsoft.com")
        };

        public static List<SBSDevice> sbsDevices = new List<SBSDevice>(){
            new SBSDevice("pc1","DeviceType.ClientPC"),
            new SBSDevice("pc2","DeviceType.ClientPC"),
            new SBSDevice("server1","DeviceType.Server"),
            new SBSDevice("secondserver1","DeviceType.SecondServer")
        };
    }
}
