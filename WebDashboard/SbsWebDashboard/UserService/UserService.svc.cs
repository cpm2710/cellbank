using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UserService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceContract]
    public class UserService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
        public UserList GetRoot()
        {
            UserList l=new UserList();
            User s=new User();
            s.Name = "shit";
            s.Email = "email";
            s.LogonName = "logonshit";
            s.AccessLevel = "admin";
            l.Add(s);
            User s2 = new User();
            s2.Name = "shit";
            s2.Email = "email";
            s2.LogonName = "logonshit";
            s2.AccessLevel = "standard user";
            l.Add(s2);
            return l;
        }
        // TODO: Add your service operations here
    }

    [DataContract(Namespace = "")]
    public class User
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string LogonName;
        [DataMember]
        public string Email;
        [DataMember]
        public string AccessLevel;
    }
    [CollectionDataContract(Name = "Users", Namespace = "")]
    public class UserList : List<User>
    {
    }
}
