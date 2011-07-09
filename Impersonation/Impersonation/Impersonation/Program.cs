using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Impersonation
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Security.Principal.WindowsIdentity wid = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal printcipal = new System.Security.Principal.WindowsPrincipal(wid);
            Console.WriteLine(printcipal.Identity.AuthenticationType);

            using (Impersonator i = new Impersonator("andy2", ".", "andy2"))
            {
                Console.WriteLine(WindowsIdentity.GetCurrent().Name);
                System.Security.Principal.WindowsIdentity wid2 = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal printcipal2 = new System.Security.Principal.WindowsPrincipal(wid2);
                Console.WriteLine(printcipal2.Identity.AuthenticationType);
                bool isAdmin = (printcipal2.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator));
                Console.WriteLine(isAdmin);
            }
            //WindowsIdentity.Impersonate(IntPtr.Zero);
            //Console.WriteLine(WindowsIdentity.GetCurrent().Name);

            //System.Security.Principal.WindowsIdentity wid = System.Security.Principal.WindowsIdentity.GetCurrent();
           // System.Security.Principal.WindowsPrincipal printcipal = new System.Security.Principal.WindowsPrincipal(wid);
           // bool isAdmin = (printcipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator));
           // Console.WriteLine(isAdmin);
        }
    }
}
