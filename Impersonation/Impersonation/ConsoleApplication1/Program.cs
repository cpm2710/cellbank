using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    using System;

    using System.Security.Principal;



    class NamesAndSIDs
    {

        const string userAccount = @".\SYSTEM";

        static void Main(string[] args)
        {
            Console.WriteLine(WellKnownSidType.LocalSystemSid);
            NTAccount name = new NTAccount(userAccount);

            Console.WriteLine(name);

            SecurityIdentifier sid = (SecurityIdentifier)

              name.Translate(typeof(SecurityIdentifier));

            Console.WriteLine(sid);

            name = (NTAccount)sid.Translate(typeof(NTAccount));

            Console.WriteLine(name);

        }

    }

}
