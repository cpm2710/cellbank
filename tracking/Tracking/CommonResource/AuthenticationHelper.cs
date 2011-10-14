using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;

namespace CommonResource
{
    public class AuthenticationHelper
    {
        public static string GetCurrentUser()
        {
            WindowsPrincipal principal = (WindowsPrincipal)Thread.CurrentPrincipal;

            WindowsIdentity identity = (WindowsIdentity)principal.Identity;

            return identity.Name;
        }
    }
}
