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
    [DataContract]
    public class SBS9User
    {
        private string userId;
        [ManagementKey]
        [DataMember]
        public string UserId
        {
            get { return userId; }
            set { this.userId = value; }
        }

        private string userName;
        [ManagementConfiguration(Mode = ManagementConfigurationType.OnCommit)]
        [DataMember]
        public string UserName
        {
            get
            {
                Logger.WriteLine("Get the UserName: " + userName);
                return userName;
            }
            set
            {
                Logger.WriteLine("Set the UserName: " + value);
                this.userName = value;
            }
        }
        private string passWord;
        [ManagementConfiguration(Mode = ManagementConfigurationType.OnCommit)]
        [DataMember]
        public string PassWord
        {
            get
            {
                Logger.WriteLine("Get the passWord: " + passWord);
                return passWord;
            }
            set
            {
                Logger.WriteLine("Set the passWord: " + value);
                this.passWord = value;
            }
        }

        private string email;
        [ManagementConfiguration(Mode = ManagementConfigurationType.OnCommit)]
        [DataMember]
        public string Email
        {
            get
            {
                Logger.WriteLine("Get the email: " + email);
                return email;
            }
            set
            {
                Logger.WriteLine("Set the email: " + value);
                this.email = value;
            }
        }
        [ManagementBind]
        public static SBS9User GetInstance(string UserId)
        {
            foreach (SBS9User u in MockRepository.sbsUsers)
            {
                if (string.Equals(u.UserId, UserId))
                {
                    return u;
                }
            }
            return null;
        }
        /// <summary>
        /// The Constructor to create new instances of the LocalAdmins class...
        /// </summary>
        public SBS9User(string UserName, string PassWord, string Email)
        {
            this.userId = Guid.NewGuid().ToString();
            this.userName = UserName;
            this.passWord = PassWord;
            this.email = Email;
        }
        [ManagementCreate]
        public static SBS9User CreateUser(string UserName,string PassWord,string Email)
        {
            Logger.WriteLine("Creat user with UserName: " + UserName);
            SBS9User newUser = new SBS9User(UserName, PassWord, Email);
            MockRepository.sbsUsers.Add(newUser);
            return newUser;
        }
        [ManagementRemove]
        public void DeleteUser()
        {
            Logger.WriteLine("Delete the user with UserId: " + this.UserId);
        }
        [ManagementCommit]
        public void Commitment()
        {
            Logger.WriteLine("Commitment: ");

        }
        [ManagementEnumerator]
        public static IEnumerable<SBS9User> GetSBSUsers()
        {
            Logger.WriteLine("Hello Stupid: " + Thread.CurrentPrincipal.Identity.Name);
            foreach (SBS9User user in MockRepository.sbsUsers)
            {
                yield return user;
            }
        }
    }    
}
