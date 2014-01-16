using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using EmailComponent;
//using SpotCollege.BLL;
using SpotCollege.DAL;
using System.IO;
using SpotCollege.Common;
using System.Net.Mail;
using System.Net;
//using SpotCollege.Common;
//using System.Web;

namespace EmailWindowsService
{
    partial class DailyEmailService : ServiceBase
    {

        //Initialize the timer
        Timer timer = new Timer();
        public DailyEmailService()
        {
            InitializeComponent();
        }
        //This method is used to raise event during start of service
        protected override void OnStart(string[] args)
        {
            //add this line to text file during start of service
            TraceService("start service");

            //handle Elapsed event
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            //This statement is used to set interval to 1 minute (= 60,000 milliseconds)
            timer.Interval = 60000;

            //enabling the timer
            timer.Enabled = true;
        }
        //This method is used to stop the service
        protected override void OnStop()
        {
            timer.Enabled = false;
            TraceService("stopping service");
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            TraceService("Another entry at " + DateTime.Now);
        }
        public void Start()
        {
            OnStart(new string[0]);
        }
        private void TraceService(string content)
        {

            //StringBuilder mailmsg = new StringBuilder(string.Empty);
            //mailmsg.Append("<h3>just test.</h3>");
            //MailHelper.sendMail("ashish@hevintechnoweb.com", "Daily Emails : SpotCollege", mailmsg.ToString());

            string FromEmailAddress = ConfigurationManager.AppSettings["Username"];
            string ToEmailAddress = "ashish@hevintechnoweb.com";
            string EmailSubject = "Daily Emails : SpotCollege";

            StringBuilder mailmsg = new StringBuilder(string.Empty);
            mailmsg.Append("<h3>just test.</h3>");

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(FromEmailAddress, ToEmailAddress, EmailSubject, mailmsg.ToString());
            msg.IsBodyHtml = true;

            var client = new SmtpClient(ConfigurationManager.AppSettings["MailServer"], Convert.ToInt32(ConfigurationManager.AppSettings["Port"]))
            {
                Credentials = new NetworkCredential(FromEmailAddress, ConfigurationManager.AppSettings["Password"]),
                EnableSsl = true
            };

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //GetEmailIdFromDB getEmails = new GetEmailIdFromDB();
            //List<User> emailList = getEmails.GetMailIds().ToList();
            //foreach (var emid in emailList)
            //{
            //    StringBuilder mailmsg = new StringBuilder(string.Empty);
            //    mailmsg.Append("<h3>just test.</h3>");
            //    MailHelper.sendMail(emid.UserName, "Daily Emails : SpotCollege", mailmsg.ToString());
            //}

            
            //set up a filestream for only testing purpose
            //FileStream fs = new FileStream(@"d:\ScheduledService.txt", FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            //StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            //sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            //sw.WriteLine(content);
            //add the text to the underlying filestream

            //sw.Flush();
            //close the writer
            //sw.Close();
        }

        #region Commented Anoder code
        //private Timer scheduleTimer = null;
        //private DateTime lastRun;
        //private bool flag;
        //public DailyEmailService()
        //{
        //    InitializeComponent();
        //    if (!System.Diagnostics.EventLog.SourceExists("EmailSource"))
        //    {
        //        System.Diagnostics.EventLog.CreateEventSource("EmailSource", "EmailLog");
        //    }
        //    eventLogEmail.Source = "EmailSource";
        //    eventLogEmail.Log = "EmailLog";
        //    scheduleTimer = new Timer();
        //    scheduleTimer.Interval = 1000; //1 * 5 * 60 * 1000;
        //    scheduleTimer.Elapsed += new ElapsedEventHandler(scheduleTimer_Elapsed);
        //}

        //protected override void OnStart(string[] args)
        //{
        //    // TODO: Add code here to start your service.
        //    flag = true;
        //    lastRun = DateTime.Now;
        //    scheduleTimer.Start();
        //    eventLogEmail.WriteEntry("Started");
        //}
        //public void Start()
        //{
        //    OnStart(new string[0]);
        //}
        //protected void scheduleTimer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    if (flag == true)
        //    {
        //        ServiceEmailMethod();
        //        lastRun = DateTime.Now;
        //        flag = false;
        //    }
        //    else if (flag == false)
        //    {
        //        if (lastRun.Date < DateTime.Now.Date)
        //        {
        //            ServiceEmailMethod();
        //        }
        //    }
        //}
        //private void ServiceEmailMethod()
        //{
        //    eventLogEmail.WriteEntry("In Sending Email Method");
        //    EmailComponent.GetEmailIdFromDB getEmails = new EmailComponent.GetEmailIdFromDB();
        //    //getEmails.connectionString = ConfigurationManager.ConnectionStrings["CustomerDBConnectionString"].ConnectionString;
        //    //getEmails.storedProcName = "GetBirthdayBuddiesEmails";
        //    List<User> emailList = getEmails.GetMailIds().ToList();
        //    EmailComponent.Email email = new EmailComponent.Email();
        //    email.fromEmail = "hevintest@gmail.com";
        //    email.fromName = "SpotCollege";
        //    email.subject = "Daily Emails";
        //    email.smtpServer = "smtp.gmail.com";
        //    email.smtpCredentials = new System.Net.NetworkCredential("hevintest@gmail.com", "hevin123");
        //    foreach (var emid in emailList)
        //    {
        //        string usn = emid.UserName;
        //        email.messageBody = "<h4>Hello User</h4>";
        //        bool result = email.SendEmailAsync(emid.UserName, emid.UserName);
        //        //if (result == true)
        //        //{
        //        //    eventLogEmail.WriteEntry("Message Sent SUCCESS to - " + dr["CustomerEmail"].ToString() + " - " + dr["CustomerName"].ToString());
        //        //}
        //        //else
        //        //{
        //        //    eventLogEmail.WriteEntry("Message Sent FAILED to - " + dr["CustomerEmail"].ToString() + " - " + dr["CustomerName"].ToString());
        //        //}
        //    }
        //}
        //protected override void OnStop()
        //{
        //    // TODO: Add code here to perform any tear-down necessary to stop your service.
        //    scheduleTimer.Stop();
        //    eventLogEmail.WriteEntry("Stopped");
        //}
        //protected override void OnPause()
        //{
        //    scheduleTimer.Stop();
        //    eventLogEmail.WriteEntry("Paused");
        //}
        //protected override void OnContinue()
        //{
        //    scheduleTimer.Start();
        //    eventLogEmail.WriteEntry("Continuing");
        //}
        //protected override void OnShutdown()
        //{
        //    scheduleTimer.Stop();
        //    eventLogEmail.WriteEntry("ShutDowned");
        //} 
        #endregion
    }
}
