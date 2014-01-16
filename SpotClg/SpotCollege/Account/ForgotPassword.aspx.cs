using SpotCollege.BLL;
using SpotCollege.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using SpotCollege.DAL;

namespace SpotCollege.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        UserBLL userBll = new UserBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnforgot_Click(object sender, EventArgs e)
        {
            try
            {
                SpotCollege.DAL.User user = userBll.Get(txtEmail.Text);
                if (user != null)
                {
                    string password = Guid.NewGuid().ToString().Substring(0, 6);
                    user.UserName = user.UserName;
                    user.Password = password;
                    userBll.Save(user);

                    // send mail to the User
                    StringBuilder mailmsg = new StringBuilder(string.Empty);
                    mailmsg.Append("<h3>" + " Your New Password is : " + password + "</h3>");
                    mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                    MailHelper.sendMail(txtEmail.Text, "apsavaliya69@gmail.com", "Changed Password : SpotCollege", mailmsg.ToString());

                    txtEmail.Text = "";
                    lblMsg.Text = "Your Password has been Successfully Changed and New Password sent on your Email Address";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    txtEmail.Focus();
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Invalid Username";
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                this.Page.ClientScript.RegisterStartupScript(typeof(Page), "notification", "window.alert('" + msg + "');", true);
            }
        }
        [WebMethod()]
        public static string CheckUserName(string UserName)
        {
            UserBLL userbl = new UserBLL();
            string str = string.Empty;
            User user = userbl.Get(UserName);
            if (user != null)
            {
                str = "true";
            }
            else
            {
                str = "false";
            }
            return str;

        }

    }
}