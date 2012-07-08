using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;

namespace UserBusinessObject
{
    public class UserHelper
    {
        public static PrincipalContext m_context = null;
        public static PrincipalContext Context
        {
            get
            {
                if (m_context == null)
                {
                    m_context = new PrincipalContext(ContextType.Machine);
                    /*if (IdentityCommon.IsAD)
                    {
                        using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                        {
                            m_context = new PrincipalContext(ContextType.Domain, context.ConnectedServer);
                        }
                    }
                    else
                    {
                        m_context = new PrincipalContext(ContextType.Machine);
                    }*/

                }

                return m_context;
            }
        }
        public static void AddUser(SBS9User user){
            UserPrincipal userPrincipal = new UserPrincipal(Context);
            //if (lastName != null && lastName.Length > 0)
            userPrincipal.Surname = user.UserName;

            //if (firstName != null && firstName.Length > 0)
            userPrincipal.GivenName = user.UserName;

            //if (employeeID != null && employeeID.Length > 0)
            //    userPrincipal.EmployeeId = employeeID;

            //if (emailAddress != null && emailAddress.Length > 0)
            userPrincipal.EmailAddress = user.Email;

            //if (telephone != null && telephone.Length > 0)
            //    userPrincipal.VoiceTelephoneNumber = telephone;

            //if (userLogonName != null && userLogonName.Length > 0)
            userPrincipal.SamAccountName = user.UserName;

            string pwdOfNewlyCreatedUser = user.PassWord;
            userPrincipal.SetPassword(pwdOfNewlyCreatedUser);

            userPrincipal.Enabled = true;
            userPrincipal.ExpirePasswordNow();

            userPrincipal.Save();
        }
    }
}
