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
        [OperationContract]
        [WebGet(UriTemplate = "/userdetails/{LogonName}", ResponseFormat = WebMessageFormat.Json)]
        public UserDetail GetUserDetail(string LogonName)
        {
            UserDetail ud= new UserDetail();
            ud.Name = "admin";
            ud.LogonName = LogonName;
            ud.AccessLevel = "admin";
            ud.Email = "email";
            ud.SharedFolders = new SharedFolderList();
            SharedFolder sf=new SharedFolder();
            sf.AccessLevel = "ReadOnly";
            sf.Name = "sharedfolder";
            ud.SharedFolders.Add(sf);
            return ud;
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
    [DataContract(Namespace = "")]
    public class UserDetail
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string LogonName;
        [DataMember]
        public string Email;
        [DataMember]
        public string AccessLevel;
        [DataMember]
        public SharedFolderList SharedFolders;
    }


    [DataContract(Namespace="")]
    public class SharedFolder
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string AccessLevel;
    }
    [CollectionDataContract(Name="SharedFolders",Namespace="")]
    public class SharedFolderList:List<SharedFolder>
    {

    }
    [CollectionDataContract(Name = "Users", Namespace = "")]
    public class UserList : List<User>
    {
    }
}
