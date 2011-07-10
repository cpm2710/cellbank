using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    using System;

    using System.Security.Principal;
    using System.Threading;



    class NamesAndSIDs
    {


        static void Main(string[] args)
        {
            Console.WriteLine(AuthenticationHelper.IsUserInAdminGroup());
            System.Security.Principal.WindowsIdentity wid2 = System.Security.Principal.WindowsIdentity.GetCurrent();
            WindowsPrincipal wip = new WindowsPrincipal(wid2);
           Console.WriteLine( wip.IsInRole(WindowsBuiltInRole.Administrator));
           // Console.WriteLine(IsAdminRole());
        }

    }

}
