using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.DAL;
using SpotCollege.Common;
using SpotCollege.BLL;
using System.Configuration;
using System.Web.Services;
using System.Text;

namespace SpotCollege.Admin
{
    public partial class UniversityIntrested : System.Web.UI.Page
    {
        UserBLL userBll = new UserBLL();
        UniversityBLL universityBll = new UniversityBLL();
        StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
        StudentBLL studentBll = new StudentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }
        void BindGrid()
        {
            List<User> user = userBll.GetAll().Where(x=>x.IsActive==false).ToList();

            List<University> universityList = new List<University>();
            foreach (var intrestuniversity in user)
            {
                University u = universityBll.GetByUserName(intrestuniversity.UserName);
                if (u!=null)
                {
                    universityList.Add(u);
                }
                else
                {
                    universityList.Remove(u);
                }
            }
            GrvUniversityInfo.DataSource = universityList;
            GrvUniversityInfo.DataBind();

        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            string universityId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(universityId))
            {
                University univ = universityBll.Get(Convert.ToInt32(universityId));
                string usernamedel = univ.UserName;
                if (universityBll.delete(Convert.ToInt32(universityId)))
                {
                    userBll.delete(usernamedel);
                    lblMsg.Text = "University Record Deleted";
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Unable to delete University Record. Some error occured..!";
                }
            }
        }
        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;

            int id = Convert.ToInt32(GrvUniversityInfo.DataKeys[row.DataItemIndex].Value);
            bool status = chkStatus.Checked;
            University univer = universityBll.Get(id);
            User us = userBll.Get(univer.UserName);
            if (us != null)
            {
                us.UserName = univer.UserName;
                us.IsActive = status;
                userBll.Save(us);
                string password = us.Password;
                try
                {
                    StringBuilder mailmsg = new StringBuilder(string.Empty);
                    mailmsg.Append("<h3>" + "Hi " + univer.UniversityName + "</h3>");
                    mailmsg.Append("<br/><b>Your account has been created.</b> ");
                    mailmsg.Append("<br/><b>University Name : </b>" + univer.UniversityName);
                    mailmsg.Append("<br/><b>University Email : </b>" + univer.UserName);
                    mailmsg.Append("<br/><b>your Password : </b>" + password);
                    mailmsg.Append("<br/><b>University City : </b>" + univer.City);
                    mailmsg.Append("<br/><b>University Country : </b>" + univer.Country);
                    mailmsg.Append("<br/><b>University Contact No : </b>" + univer.ContactNo);

                    mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                    MailHelper.sendMail(univer.UserName, "balar9426@gmail.com", "Thanks to create Account : SpotCollege", mailmsg.ToString());
                    //string msg="University Activated Successfully";
                   // this.Page.ClientScript.RegisterStartupScript(typeof(Page), "notification", "window.alert('" + msg + "');", true);
                   
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    this.Page.ClientScript.RegisterStartupScript(typeof(Page), "notification", "window.alert('" + msg + "');", true);
                }



            }
            BindGrid();
        }

    }
}