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
using System.IO;
using System.Web.Services;
using Newtonsoft.Json;

namespace SpotCollege
{
    public partial class DashboardCollege : System.Web.UI.Page
    {
        StudentBLL studentBll = new StudentBLL();
        UniversityBLL universityBll = new UniversityBLL();
        UserBLL userBll = new UserBLL();
        StudentInterestBLL studentInterestBll = new StudentInterestBLL();
        MessageBLL messageBLL = new MessageBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindStudentShownList();
                this.BindStudentJoinedList();
            }
        }

        void BindStudentShownList()
        {
            lblrec.Visible = false;
            List<StudentInterest> ShownInterestList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Applied).OrderByDescending(x => x.StudentInterestId).Take(5).ToList();
            dlStudentShownInterestList.DataSource = ShownInterestList;
            dlStudentShownInterestList.DataBind();
            if (dlStudentShownInterestList.Items.Count == 0)
            {
                lblrec.Visible = true;
            }
            else
            {
                dlStudentShownInterestList2.DataSource = ShownInterestList;
                dlStudentShownInterestList2.DataBind();
            }


        }
        void BindStudentJoinedList()
        {
            lblrec1.Visible = false;
            List<StudentInterest> JoinedList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Approved).OrderByDescending(x => x.StudentInterestId).Take(5).ToList();
            dlStudentJoinedList.DataSource = JoinedList;
            dlStudentJoinedList.DataBind();
            if (dlStudentJoinedList.Items.Count == 0)
            {
                lblrec1.Visible = true;
            }
        }

        protected void lnkBtnViewProfile_Click(object sender, EventArgs e)
        {
            string username = ((LinkButton)sender).CommandArgument.ToString();
            Student student = studentBll.GetByUserName(username);
            if (student != null)
                username = student.StudentId.ToString();
            else
                username = "0";

            if (!string.IsNullOrEmpty(username))
            {
                Response.Redirect("Account/ProfileOverView.aspx?intu=" + username);
            }
        }
        protected void lnkBtnViewProfile1_Click(object sender, EventArgs e)
        {
            string username = ((LinkButton)sender).CommandArgument.ToString();
            Student student = studentBll.GetByUserName(username);
            if (student != null)
                username = student.StudentId.ToString();
            else
                username = "0";
            if (!string.IsNullOrEmpty(username))
            {
                Response.Redirect("Account/ProfileOverView.aspx?intu=" + username);
            }
        }

        protected void dlStudentShownInterestList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName");
                Label lblCityCountry = (Label)e.Item.FindControl("lblCityCountry");
                Label lblstdname = (Label)e.Item.FindControl("lblstdname");
                Label lblEmail = (Label)e.Item.FindControl("lblEmail");
                ImageButton img = (ImageButton)e.Item.FindControl("ImgBtnPhoto");

                if (username != null)
                {
                    Student std = studentBll.GetByUserName(username.Value);
                    if (std != null)
                    {
                        var fName = std.FirstName;
                        var mname = std.MiddleName;
                        var Lname = std.LastName;
                        if (std.Photo != null)
                        {

                            img.ImageUrl = "Images/Profile/" + std.Photo;
                        }
                        else
                        {
                            img.ImageUrl = "Images/no_image.jpg";
                        }
                        lblstdname.Text = fName + " " + Lname;
                        lblCityCountry.Text = std.City.ToString() + " , " + std.Country.ToString() + " applying for " + EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)std.LookingFor).ToString() + " in " + std.DesiredFieldofStudy + " to " + std.LookingForCountry + ". Desired date of joining " + EnumHelper.GetDescriptionFromEnumValue((expectedStart)std.StartDate).ToString();
                        //lnkMsg.PostBackUrl = "javascript:OpenMsgPopup('" + std.FirstName + " " + std.LastName + "','" + std.UserName + "');";
                    }
                }
            }
        }

        protected void dlStudentShownInterestList_ItemDataBound2(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName");
                LinkButton lnkStudentName = (LinkButton)e.Item.FindControl("lnkBtnViewProfile");
                Label lblCityCountry = (Label)e.Item.FindControl("lblCityCountry");
                Label lblEmail = (Label)e.Item.FindControl("lblEmail");
                ImageButton img = (ImageButton)e.Item.FindControl("ImgBtnPhoto");
                LinkButton lnkBtnMessage = (LinkButton)e.Item.FindControl("lnkMessage");
                LinkButton lnkBtnViewAllMessage = (LinkButton)e.Item.FindControl("lnkBtnViewAllMessage");
                LinkButton lnkMsg = (LinkButton)e.Item.FindControl("lnkMsg");

                
                if (username != null)
                {
                    Student std = studentBll.GetByUserName(username.Value);
                    if (std != null)
                    {
                        var fName = std.FirstName;
                        var mname = std.MiddleName;
                        var Lname = std.LastName;
                        if (std.Photo != null)
                            img.ImageUrl = "Images/Profile/" + std.Photo;
                        else
                            img.ImageUrl = "Images/no_image.jpg";
                        lnkStudentName.Text = fName + " " + mname + " " + Lname;
                        lblCityCountry.Text = std.City.ToString() + " , " + std.Country.ToString() + " applying for " + EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)std.LookingFor).ToString() + " in " + std.DesiredFieldofStudy + " to " + std.LookingForCountry + ". Desired date of joining " + EnumHelper.GetDescriptionFromEnumValue((expectedStart)std.StartDate).ToString();
                        lnkMsg.PostBackUrl = "javascript:OpenMsgPopup('"+std.FirstName+" "+std.LastName+"','"+std.UserName+"');";
                    }
                }
            }
        }

        protected void dlStudentJoinedList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName1");
                Label lblCityCountry = (Label)e.Item.FindControl("lblCityCountry1");
                Label lblEmail = (Label)e.Item.FindControl("lblEmail1");
                Label lblstdname = (Label)e.Item.FindControl("lblstdname1");
                ImageButton img = (ImageButton)e.Item.FindControl("ImgBtnPhoto1");
                //LinkButton lnkMsg = (LinkButton)e.Item.FindControl("lnkMsg");

                if (username != null)
                {
                    Student std = studentBll.GetByUserName(username.Value);
                    if (std != null)
                    {
                        var fName = std.FirstName;
                        var mname = std.MiddleName;
                        var Lname = std.LastName;
                        if (std.Photo != null)
                            img.ImageUrl = "Images/Profile/" + std.Photo;
                        else
                            img.ImageUrl = "Images/no_image.jpg";
                        lblstdname.Text = fName + " " + Lname;
                        lblCityCountry.Text = std.City.ToString() + " , " + std.Country.ToString() + " applying for " + EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)std.LookingFor).ToString() + " in " + std.DesiredFieldofStudy + " to " + std.LookingForCountry + ". Desired date of joining " + EnumHelper.GetDescriptionFromEnumValue((expectedStart)std.StartDate).ToString();
                        //lnkMsg.PostBackUrl = "javascript:OpenMsgPopup('" + std.FirstName + " " + std.LastName + "','" + std.UserName + "');";
                    }
                }
            }
        }

        //protected void ImgBtnPhoto_Click(object sender, ImageClickEventArgs e)
        //{
        //    string username = ((ImageButton)sender).CommandArgument.ToString();
        //    if (!string.IsNullOrEmpty(username))
        //    {
        //        Response.Redirect("Account/ProfileOverView.aspx?intu=" + username);
        //    }
        //}

        //protected void ImgBtnPhoto1_Click(object sender, ImageClickEventArgs e)
        //{
        //    string username = ((ImageButton)sender).CommandArgument.ToString();
        //    if (!string.IsNullOrEmpty(username))
        //    {
        //        Response.Redirect("Account/ProfileOverView.aspx?intu=" + username + "&dec=show");
        //    }
        //}

        protected void lnkBtnViewAllshownInt_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewAllList.aspx?status=Applied");
        }

        protected void lnkBtnViewAllJoined_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewAllList.aspx?status=Approved");
        }

        [WebMethod()]
        public static string SendMessage(string Title, string Description, string sendToUserName, string ParentId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
            msg.Title = Title;
            msg.Description = Description;
            msg.ParentId = Convert.ToInt32(ParentId);
            msg.FromUserName = CookieHelper.Username;
            msg.ToUserName = sendToUserName;
            msg.IsRead = false;
            msg.IsApproved = false;
            messageBLL.Save(msg);

            //AlertBLL alertBLL = new AlertBLL();
            //UniversityBLL universityBLL = new UniversityBLL();
            //string universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
            //Alert alert = new Alert();
            //alert.Message = universityname + " has Send Message";
            //alert.CreatedDate = DateTime.Now;
            //alert.CreatedBy = CookieHelper.Username;
            //alert.UserName = sendToUserName;
            //alertBLL.Save(alert);

            return str;
        }

        [WebMethod()]
        public static string Getstatus(string ToUserName)
        {
            Message msg = new Message();
            MessageBLL messageBLL = new MessageBLL();
            msg = messageBLL.GetAll().Where(x => x.ToUserName == ToUserName && x.FromUserName == CookieHelper.Username || x.ToUserName == CookieHelper.Username && x.FromUserName == ToUserName && x.ParentId == 0).FirstOrDefault();
            if (msg == null)
                return string.Empty;
            else
                return msg.MessageId.ToString();
        }
    }
}