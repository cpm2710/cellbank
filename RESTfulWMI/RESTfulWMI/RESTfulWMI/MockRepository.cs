using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESTfulWMI
{
    public class MockRepository
    {
        public static List<SBS9User> sbsUsers = new List<SBS9User>() { 
            new SBS9User("andy","andypswd","andliu@microsoft.com"),
            new SBS9User("andy2","andy2pswd","andliu2@microsoft.com")
        };
        //public static List<Storage> storages = new List<Storage>();
    }
}
