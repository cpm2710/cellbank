using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserBusinessObject
{
    public class MockRepository
    {
        public static List<SBSUser> sbsUsers = new List<SBSUser>() { 
            new SBSUser("andy","andypswd","andliu@microsoft.com"),
            new SBSUser("andy2","andy2pswd","andliu2@microsoft.com")
        };
    }
}
