using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Diagnostics;

namespace Dashboard365Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthenticationService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AuthenticationService : IAuthenticationService
    {
        public string Authenticate(AuthenticationInstance ai)
        {

            EventLog log = new EventLog("MyEvent");
            //  首先应判断日志来源是否存在，一个日志来源只能同时与一个事件绑定s
            if (!EventLog.SourceExists("New Application"))
                EventLog.CreateEventSource("New Application", "MyEvent");

            log.Source = "New Application";
            log.WriteEntry(ai.UserName, EventLogEntryType.Information);
            log.WriteEntry(ai.PassWord, EventLogEntryType.Information);
            Console.WriteLine(ai.UserName);
            Console.WriteLine(ai.PassWord);

            string token = AuthenticationUtil.GenCookieToken(ai);
            AuthenticationUtil.Verify(token);
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
