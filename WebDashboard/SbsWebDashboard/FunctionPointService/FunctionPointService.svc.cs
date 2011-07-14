using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.ServiceModel.Activation;
using System.Diagnostics;

namespace Dashboard365Service
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class FunctionPointService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
        public FunctionPointList GetRoot()
        {
            string token=HttpContext.Current.Request.Headers[AuthenticationUtil.Dashboard365TokenName];
            EventLog log = new EventLog("MyEvent");
            //  首先应判断日志来源是否存在，一个日志来源只能同时与一个事件绑定s
            if (!EventLog.SourceExists("New Application"))
                EventLog.CreateEventSource("New Application", "MyEvent");

            log.Source = "New Application";
            log.WriteEntry("token" + token, EventLogEntryType.Information);

            if (!AuthenticationUtil.Verify(token))
            {
                return null;
            }

            FunctionPointList l = new FunctionPointList();
            FunctionPoint s = new FunctionPoint();
            s.Title = "Users Management";
            s.ImgUrl = "/staticImages/users.png";
            s.AlertCount = "3";
            s.Description = "Users Management Component";
            s.NavigationUrl = "/s.htm";
            l.Add(s);

            FunctionPoint s2 = new FunctionPoint();
            s2.Title = "Users Management";
            s2.ImgUrl = "/staticImages/users.png";
            s2.AlertCount = "3";
            s2.Description = "Users Management Component";
            s2.NavigationUrl = "/s.htm";
            l.Add(s2);

            return l;
        }
        [OperationContract]
        [WebGet(UriTemplate = "/mainfpbyuser/{LogonName}", ResponseFormat = WebMessageFormat.Json)]
        public FunctionPointList GetFunctionPointsByUser(string LogonName)
        {
            FunctionPointList l = new FunctionPointList();
            FunctionPoint s = new FunctionPoint();
            s.Title = "Users Management";
            s.ImgUrl = "/staticImages/users.png";
            s.AlertCount = "3";
            s.Description = "Users Management Component";
            s.NavigationUrl = "/users.htm";
            l.Add(s);

            FunctionPoint s2 = new FunctionPoint();
            s2.Title = "Users Management";
            s2.ImgUrl = "/staticImages/users.png";
            s2.AlertCount = "3";
            s2.Description = "Users Management Component";
            s2.NavigationUrl = "/rdps.htm";
            l.Add(s2);

            return l;
        }

        [OperationContract]
        [WebGet(UriTemplate = "/fpbyuserandaddin/{LogonName}/{AddIn}", ResponseFormat = WebMessageFormat.Json)]
        public FunctionPointList GetFunctionPointsByUserAndAddIn(string LogonName, string AddIn)
        {
            string token = HttpContext.Current.Request.Headers[AuthenticationUtil.Dashboard365TokenName];
            EventLog log = new EventLog("MyEvent");
            //  首先应判断日志来源是否存在，一个日志来源只能同时与一个事件绑定s
            if (!EventLog.SourceExists("New Application"))
                EventLog.CreateEventSource("New Application", "MyEvent");

            log.Source = "New Application";
            log.WriteEntry("token" + token, EventLogEntryType.Information);

            if (!AuthenticationUtil.Verify(token))
            {
                return null;
            }
            FunctionPointList l = new FunctionPointList();
            if (AddIn.Equals("main", StringComparison.InvariantCultureIgnoreCase))
            {
                FunctionPoint s = new FunctionPoint();
                s.Title = "Users Management";
                s.ImgUrl = "/staticImages/users.png";
                s.AlertCount = "3";
                s.Description = "Users Management Component";
                s.NavigationUrl = "/users.htm";
                l.Add(s);

                FunctionPoint s2 = new FunctionPoint();
                s2.Title = "Users Management";
                s2.ImgUrl = "/staticImages/users.png";
                s2.AlertCount = "3";
                s2.Description = "Users Management Component";
                s2.NavigationUrl = "/rdps.htm";
                l.Add(s2);
            }
            else if (AddIn.Equals("users", StringComparison.InvariantCultureIgnoreCase))
            {
                FunctionPoint s = new FunctionPoint();
                s.Title = "Add User";
                s.ImgUrl = "/staticImages/AddUser.png";
                s.AlertCount = "3";
                s.Description = "Users Management Component";
                s.NavigationUrl = "/s.htm";
                l.Add(s);

                FunctionPoint s2 = new FunctionPoint();
                s2.Title = "BacktoHome";
                s2.ImgUrl = "/staticImages/back.png";
                s2.AlertCount = "3";
                s2.Description = "Users Management Component";
                s2.NavigationUrl = "/Default.html";
                l.Add(s2);
            }
            return l;
        }
    }

    [DataContract(Namespace = "")]
    public class FunctionPoint
    {
        [DataMember]
        public string Title;
        [DataMember]
        public string ImgUrl;
        [DataMember]
        public string  AlertCount;
        [DataMember]
        public string Description;
        [DataMember]
        public string NavigationUrl;
    }
    [CollectionDataContract(Name = "FunctionPoints", Namespace = "")]
    public class FunctionPointList : List<FunctionPoint>
    {
    }
}
