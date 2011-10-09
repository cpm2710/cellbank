using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Security.Principal;

namespace SEActivities.Mail
{
    public class EmailSender
    {
        #region Private Properties

        private string mailDomain;
        private string mailHost;
        private string from;

        #endregion Private Properties

        #region Constructor
        /// <summary>
        /// Constructor to initialize the SendMail
        /// </summary>
        /// <param name="mailDomain"></param>
        /// <param name="mailHost"></param>
        /// <param name="from"></param>
        public EmailSender(string mailDomain = "@microsoft.com", string mailHost = "smtphost.redmond.corp.microsoft.com", string from = "seportal@microsoft.com")
        {
            this.mailDomain = mailDomain;
            this.mailHost = mailHost;
            this.from = from;
        }

        #endregion Constructor

        #region Public Functions

        public void SendEmail(string subject, string toList, string ccList, string body, string alias, bool isHighPriority = false)
        {
            try
            {
                if (string.IsNullOrEmpty(subject) == false && string.IsNullOrEmpty(body) == false)
                {
                    // Create the mail message, subject and body
                    MailMessage mailMessage = this.CreateMessage(subject,
                        body, toList.Split(';').ToList(),
                        ccList.Split(';').ToList(),
                        alias, isHighPriority);

                    // Send the e-mail using SMTP
                    this.SmtpSend(mailMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Exception in SendEmail to {0}", toList), e);
            }
        }

        #endregion Public Functions

        #region Private Functions

        /// <summary>
        /// Create the mail message with subject, body, to, cc and bcc
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="toList"></param>
        /// <param name="ccList"></param>
        /// <param name="bccList"></param>
        private MailMessage CreateMessage(string subject, string body, List<string> toList, List<string> ccList, string replyTo, bool isHighPriority)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(this.from);
            mailMessage.ReplyToList.Add(replyTo.Trim() + this.mailDomain);
            // Set the subject of the mail
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Subject = subject;

            if (isHighPriority == true)
            {
                mailMessage.Priority = MailPriority.High;
            }
            else
            {
                mailMessage.Priority = MailPriority.Normal;
            }

            // Set the body of the mail
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            // Add all the people in the to line
            foreach (string to in toList)
            {
                if (to.Trim().Length > 0)
                {
                    mailMessage.To.Add(new MailAddress(to.Trim() + this.mailDomain));
                }
            }

            // Add all the people in the cc line
            foreach (string cc in ccList)
            {
                if (cc.Trim().Length > 0)
                {
                    mailMessage.CC.Add(new MailAddress(cc.Trim() + this.mailDomain));
                }
            }

            return mailMessage;
        }

        /// <summary>
        /// Use the SmtpClient to send the e-mail
        /// </summary>
        private void SmtpSend(MailMessage mailMessage)
        {
            SmtpClient client = new SmtpClient(this.mailHost);
            // Impersonate the user represented by IIS pool account
            WindowsImpersonationContext windowsImpersonationContext = WindowsIdentity.GetCurrent().Impersonate();
            // Get the credential
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            // Reverts the user context
            windowsImpersonationContext.Undo();

            // Send the message
            client.Send(mailMessage);
        }

        #endregion Private Functions
    }
}
