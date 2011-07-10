using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace AfterSetPrincipalPolicy
{
  
    class Program
    {
        public static bool IsAdminRole()
        {
            AppDomain domain = Thread.GetDomain();

            domain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WindowsPrincipal principle = (WindowsPrincipal)Thread.CurrentPrincipal;

            return principle.IsInRole(WindowsBuiltInRole.Administrator);
        }
        static void Main(string[] args)
        {
            //using (Impersonator imp = new Impersonator("andy2",".","andy2"))
            {
                Console.WriteLine(IsAdminRole());
            }
        }
    }
}
