using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;


namespace SpotCollege.Common
{
  public class MailHelper
    {
        public static void sendMail(string to, string subject, string message)
        {
            try
            {
                MailMessage Email = null;

                Email = new MailMessage();
                Email.BodyFormat = MailFormat.Html;

                Email.From = ConfigurationManager.AppSettings["Username"];
                Email.To = to;
                Email.Subject = subject;
                Email.Body = message;
                Email.Priority = MailPriority.High;

                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", ConfigurationManager.AppSettings["Username"]);
                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ConfigurationManager.AppSettings["Password"]);
                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", ConfigurationManager.AppSettings["MailServer"]);
                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 10);
                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", ConfigurationManager.AppSettings["Port"]);
                Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", false);

                try
                {
                    System.Web.Mail.SmtpMail.Send(Email);
                }
                catch (Exception ex)
                {
                    Email.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate");
                    Email.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/sendusing");
                    Email.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpusessl");

                    Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 0);
                    Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 1);
                    Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
                    System.Web.Mail.SmtpMail.Send(Email);
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
