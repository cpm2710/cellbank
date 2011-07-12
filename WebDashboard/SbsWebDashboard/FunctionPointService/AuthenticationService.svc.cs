using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace Dashboard365Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthenticationService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AuthenticationService : IAuthenticationService
    {
        public string Authenticate(AuthenticationInstance ai)
        {
            string token = AuthenticationUtil.GenCookieToken(ai);
            return token;
        }
        public string getMockToken()
        {
            AuthenticationInstance ai = new AuthenticationInstance();
            ai.UserName = "shit";
            ai.PassWord = "shitp";
            ai.TimeStamp = 1;
            return AuthenticationUtil.GenCookieToken(ai);
        }
    }
}
