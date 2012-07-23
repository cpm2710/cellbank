﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Management.Instrumentation;
using System.ServiceModel;
using System.Collections;
using System.Threading;
using SBSWMINotifications;
using System.Security.Principal;
using System.Runtime.Remoting.Contexts;
using System.DirectoryServices.AccountManagement;
namespace SBSBusinessObject
{
    [ManagementEntity(Name = "SBS_User", Singleton = false)]
    [DataContract]
    public class SBSUser
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
        public static SBSUser GetInstance(string UserId)
        {
            foreach (SBSUser u in MockRepository.sbsUsers)
            {
                if (string.Equals(u.UserId, UserId))
                {
                    return u;
                }
            }
            return null;
        }

        public SBSUser(string UserName, string PassWord, string Email)
        {
            this.userId = Guid.NewGuid().ToString();
            this.userName = UserName;
            this.passWord = PassWord;
            this.email = Email;
        }
       static private PrincipalContext m_context;
        static PrincipalContext Context
        {
            get
            {
                if (m_context == null)
                {
                    
                        using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                        {
                            m_context = new PrincipalContext(ContextType.Domain, context.ConnectedServer);
                        }
                   

                }

                return m_context;
            }
        }

        [ManagementCreate]
        public static SBSUser CreateUser(string UserName,string PassWord,string Email)
        {
            //System.Security.Principal.WindowsImpersonationContext impersonationContext;

            //impersonationContext = (WindowsIdentity.GetCurrent()).Impersonate();
            Logger.WriteLine("Creat user with UserName: " + UserName);
            SBSUser newUser = new SBSUser(UserName, PassWord, Email);

            
            MockRepository.sbsUsers.Add(newUser);
            try
            {
                Logger.WriteLine("publishing SBSUserAddedEvent");
                using (UserPrincipal user = new UserPrincipal(Context))
                {
                    user.Name = UserName;
                    user.SamAccountName = UserName;

                    user.Enabled = true;
                    user.Description = UserName ;

                    user.SetPassword(PassWord);
                    user.Save();
                }
                //SBSEventProvider.FireSBSUserAddedEvent(newUser.userId);
                //SBSUserAddedEvent.Publish(newUser.userId);
                Logger.WriteLine("publishedddd SBSUserAddedEvent");
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
            }
            Logger.WriteLine("Created user with UserName: " + UserName);
            //impersonationContext.Undo();
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
        public static IEnumerable<SBSUser> GetSBSUsers()
        {
            System.Security.Principal.WindowsImpersonationContext impersonationContext;

            impersonationContext = (WindowsIdentity.GetCurrent()).Impersonate();

            //Thread.CurrentPrincipal=
            //WindowsIdentity.GetCurrent().Impersonate();
            //WindowsIdentity.GetCurrent().Name;
            //Logger.WriteLine("Hello Stupid: " + Thread.CurrentPrincipal.ToString());
            foreach (SBSUser user in MockRepository.sbsUsers)
            {
                user.userName = WindowsIdentity.GetCurrent().Name;
                yield return user;
            }
            impersonationContext.Undo();
        }
    }    
}