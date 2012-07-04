using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Instrumentation;
using System.Collections;

namespace RESTfulWMI
{
    [ManagementEntity(Name = "SBS9_User", Singleton = false)]
    public class SBS9User
    {
        [ManagementKey]
        public string UserId { get; set; }

        private string userName;
        [ManagementConfiguration]
        public string UserName
        {
            get
            {
                Console.WriteLine("we are getting the UserName" + userName);
                return userName;
                /*foreach (SBS9User u in MockRepository.sbsUsers)
                {
                    if (string.Equals(u.UserId, UserId))
                    {
                        return u.UserName;
                    }
                }
                return null;*/
            }
            set
            {
                Console.WriteLine("we are setting the UserName" + value);
                this.userName = value;
                //foreach (SBS9User u in MockRepository.sbsUsers)
                //{
                //    if (string.Equals(u.UserId, UserId))
                //    {
                //        u.UserName = value;
                //    }
                //}
            }
        }
        private string passWord;
        [ManagementConfiguration]
        public string PassWord
        {
            get
            {
                //foreach (SBS9User u in MockRepository.sbsUsers)
                //{
                //    if (string.Equals(u.UserId, UserId))
                //    {
                //        return u.PassWord;
                //    }
                //}
                Console.WriteLine("we are getting the passWord" + passWord);
                return passWord;
            }
            set
            {
                Console.WriteLine("we are setting the passWord" + value);
                this.passWord = value;
                //foreach (SBS9User u in MockRepository.sbsUsers)
                //{
                //    if (string.Equals(u.UserId, UserId))
                //    {
                //        u.PassWord = value;
                //    }
                //}
            }
        }

        private string email;
        [ManagementConfiguration]
        public string Email
        {
            get
            {
                //foreach (SBS9User u in MockRepository.sbsUsers)
                //{
                //    if (string.Equals(u.UserId, UserId))
                //    {
                //        return u.Email;
                //    }
                //}
                Console.WriteLine("we are getting the email" + email);
                return email;
            }
            set
            {
                //foreach (SBS9User u in MockRepository.sbsUsers)
                //{
                //    if (string.Equals(u.UserId, UserId))
                //    {
                //        u.Email = value;
                //    }
                //}
                Console.WriteLine("we are setting the email" + value);
                this.email = value;
            }
        }
        [ManagementBind]
        static public SBS9User GetInstance([ManagementName("UserId")] string UserId)
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

        /*[ManagementTask]
        static public SBS8User Create(string FullName, string PassWord, string Email)
        {
            return new SBS8User(FullName, PassWord, Email);
        }
        [ManagementTask]
        static public SBS8User Update(string FullName, string PassWord, string Email)
        {
            return new SBS8User(FullName, PassWord, Email);
        }
        [ManagementTask]
        static public SBS8User Delete(string FullName, string PassWord, string Email)
        {
            return new SBS8User(FullName, PassWord, Email);
        }*/
        /// <summary>
        /// This Function returns all members of the local Administrators group
        /// </summary>
        /// <returns></returns>
        [ManagementEnumerator]
        static public IEnumerable GetSBSUsers()
        {
            foreach (SBS9User user in MockRepository.sbsUsers)
            {
                yield return user;
            }
        }
    }
}
