using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESTfulWMI
{
    public class MockRepository
    {
        public static List<SBS8User> sbsUsers = new List<SBS8User>() { 
            new SBS8User("andy","andypswd","andliu@microsoft.com"),
            new SBS8User("andy2","andy2pswd","andliu2@microsoft.com")
        };
        public static List<Storage> storages = new List<Storage>();
    }
}
