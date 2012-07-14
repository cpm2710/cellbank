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
    [ManagementEntity(Name = "SBS9_User", Singleton = false)]
    public class SBS9User
    {
        private string userId;
        [ManagementKey]
        public string UserId
        {
            get { return userId; }
            set { this.userId = value; }
        }

        private string userName;
        [ManagementConfiguration(Mode = ManagementConfigurationType.OnCommit)]
        public string UserName
        {
            get
            {
                Logger.WriteLine("we are getting the UserName" + userName);
                return userName;
            }
            set
            {
                Logger.WriteLine("we are setting the UserName" + value);
                this.userName = value;
            }
        }
        private string passWord;
        [ManagementConfiguration(Mode = ManagementConfigurationType.OnCommit)]
        public string PassWord
        {
            get
            {                
                Logger.WriteLine("we are getting the passWord" + passWord);
                return passWord;
            }
            set
            {
                Logger.WriteLine("we are setting the passWord" + value);
                this.passWord = value;
            }
        }

        private string email;
        [ManagementConfiguration(Mode = ManagementConfigurationType.OnCommit)]
        public string Email
        {
            get
            {
                Logger.WriteLine("we are getting the email" + email);
                return email;
            }
            set
            {
                Logger.WriteLine("we are setting the email" + value);
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
            Logger.WriteLine("we are creating user with UserName:" + UserName);
            SBS9User newUser = new SBS9User(UserName, PassWord, Email);
            return newUser;
        }
        [ManagementRemove]
        public void DeleteUser()
        {
            Logger.WriteLine("we are deleting the user with UserId:" + this.UserId);
        }
        [ManagementCommit]
        public void Commitment()
        {
            Logger.WriteLine("now we are commiting");

        }
        [ManagementEnumerator]
        static public IEnumerable GetSBSUsers()
        {
            Logger.WriteLine("hello stupid:" + Thread.CurrentPrincipal.Identity.Name);
            foreach (SBS9User user in MockRepository.sbsUsers)
            {
                yield return user;
            }
        }
    }    
}
