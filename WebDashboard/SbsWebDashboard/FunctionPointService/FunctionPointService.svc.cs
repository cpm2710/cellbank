﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.ServiceModel.Activation;

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
            string token=HttpContext.Current.Request.Cookies[AuthenticationUtil.Dashboard365TokenName].Value;

            AuthenticationUtil.Verify(token);

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
