using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Dashboard365Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthenticationService" in code, svc and config file together.
    public class AuthenticationService : IAuthenticationService
    {
        public string Authenticate(AuthenticationInstance ai)
        {
            return AuthenticationUtil.GenCookieToken(ai);
        }
    }
}
