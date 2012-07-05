using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Management.Instrumentation;
using System.ServiceModel;
using System.Collections;
using System.Threading;
namespace UserBusinessObject
{
    [DataContract]
    [ManagementEntity(Name = "SBS9_User", Singleton = false)]
    public class SBS9User
    {
        private string userId;
        [ManagementKey]
        [DataMember]
        public string UserId { get { return userId; } set { this.userId = value; } }

        private string userName;
        [DataMember]
        [ManagementConfiguration]
        public string UserName
        {
            get
            {
                Console.WriteLine("we are getting the UserName" + userName);
                return userName;
            }
            set
            {
                Console.WriteLine("we are setting the UserName" + value);
                this.userName = value;
            }
        }
        private string passWord;
        [DataMember]
        [ManagementConfiguration]
        public string PassWord
        {
            get
            {                
                Console.WriteLine("we are getting the passWord" + passWord);
                return passWord;
            }
            set
            {
                Console.WriteLine("we are setting the passWord" + value);
                this.passWord = value;
            }
        }

        private string email;
        [DataMember]
        [ManagementConfiguration]
        public string Email
        {
            get
            {
                Console.WriteLine("we are getting the email" + email);
                return email;
            }
            set
            {
                Console.WriteLine("we are setting the email" + value);
                this.email = value;
            }
        }
        [ManagementBind]
        static public SBS9User GetInstance(string UserId)
        {
            foreach (SBS9User u in MockRepository.sbsUsers)
            {
                if (string.Equals(u.UserId, UserId))
                {
                    return u;
                }
            } return null;
        }
        /// <summary>
        /// The Constructor to create new instances of the LocalAdmins class...
        /// </summary>
        public SBS9User(string UserName, string PassWord, string Email)
        {
            this.UserId = Guid.NewGuid().ToString();
            this.UserName = UserName;
            this.PassWord = PassWord;
            this.Email = Email;
        }
        [ManagementCreate]
        public static SBS9User CreateUser(string UserName,string PassWord,string Email)
        {
            Console.WriteLine("we are creating user with UserName:" + UserName);
            SBS9User newUser = new SBS9User(UserName, PassWord, Email);
            return newUser;
        }
        [ManagementRemove]
        public void DeleteUser()
        {
            Console.WriteLine("we are deleting the user with UserId:" + this.UserId);
        }
        [ManagementEnumerator]
        static public IEnumerable GetSBSUsers()
        {
            Console.WriteLine("hello stupid:" + Thread.CurrentPrincipal.Identity.Name);
            foreach (SBS9User user in MockRepository.sbsUsers)
            {
                yield return user;
            }
        }
    }
    [ServiceContract]
    public class SBS9UserServiceWrapper
    {
        [OperationContract]
        public SBS9User CreateUser(SBS9User user)
        {
            return SBS9User.CreateUser(user.UserName, user.PassWord, user.Email);
        }
        [OperationContract]
        public void DeleteUser(string id)
        {
            foreach (SBS9User user in MockRepository.sbsUsers)
            {
                if (string.Equals(user.UserId, id))
                {
                    user.DeleteUser();
                }
            }
            //SBS9User foundedUser;
            //foundedUser.DeleteUser();
        }
        [OperationContract]
        public SBS9User UpdateUser(SBS9User user)
        {
            foreach (SBS9User u in MockRepository.sbsUsers)
            {
                if (string.Equals(user.UserId, u.UserId))
                {
                    u.UserName = user.UserName;
                    u.Email = user.Email;
                    u.PassWord = user.PassWord;
                    return u;
                }
            }
            return user;
        }
    }
}
