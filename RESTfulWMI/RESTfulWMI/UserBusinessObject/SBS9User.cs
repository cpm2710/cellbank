using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserBusinessObject
{
    public class SBS9User
    {
        public string UserId { get; set; }

        private string userName;
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
        public static SBS9User CreateUser(string UserName,string PassWord,string Email)
        {
            Console.WriteLine("we are creating user with UserName:" + UserName);
            SBS9User newUser = new SBS9User(UserName, PassWord, Email);
            return newUser;
        }
        public void DeleteUser()
        {
            Console.WriteLine("we are deleting the user with UserId:" + this.UserId);
        }
    }
    public class SBS9UserManager
    {
        public SBS9User CreateUser(SBS9User user)
        {
            return SBS9User.CreateUser(user.UserName, user.PassWord, user.Email);
        }
        public void DeleteUser(string id)
        {
            SBS9User foundedUser;
            foundedUser.DeleteUser();
        }
        public SBS9User UpdateUser(SBS9User user)
        {

        }
    }
}
