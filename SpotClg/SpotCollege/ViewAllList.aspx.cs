using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.DAL;
using SpotCollege.Common;
using SpotCollege.BLL;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace SpotCollege
{
    public partial class ViewAllList : System.Web.UI.Page
    {
        StudentBLL studentBll = new StudentBLL();
        UniversityBLL universityBll = new UniversityBLL();
        UserBLL userBll = new UserBLL();
        StudentInterestBLL studentInterestBll = new StudentInterestBLL();

        public static List<Student> StaticStudentList = new List<Student>();
        public static List<StudentInterest> StaticStudentInterestList = new List<StudentInterest>();
        public static string queryData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["status"] == "Applied")
                {
                    queryData = "Applied";
                    this.BindStudentShownList();
                }
                else if (Request.QueryString["status"] == "Approved")
                {
                    queryData = "Approved";
                    this.BindStudentJoinedList();
                }
                else if (Request.QueryString["status"] == "Country")
                {
                    queryData = "Country";
                    this.BindStudentapplingCountry();
                }
                else if (Request.QueryString["status"] == "Course")
                {
                    queryData = "Course";
                    this.bindapplystudentdetail();
                }
            }
        }

        void BindStudentShownList()
        {
            lblrec.Visible = false;
            List<StudentInterest> ShownInterestList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Applied).ToList();

            StaticStudentInterestList = ShownInterestList.ToList(); //StaticStudentList will be used for displaying record
            if (ShownInterestList.Count < 5)
                StaticStudentInterestList.RemoveAll(x => 1 == 1);
            else
                StaticStudentInterestList.RemoveRange(0, 6);

            dlStudentShownInterestList2.DataSource = ShownInterestList.Take(6);
            dlStudentShownInterestList2.DataBind();

            dlStudentShownInterestList.DataSource = ShownInterestList.Take(6);
            dlStudentShownInterestList.DataBind();

            if (dlStudentShownInterestList.Items.Count == 0)
            {
                lblrec.Visible = true;
            }

        }
        void BindStudentJoinedList()
        {
            lblrec.Visible = false;
            List<StudentInterest> JoinedList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Approved).ToList();

            StaticStudentInterestList = JoinedList.ToList(); //StaticStudentList will be used for displaying record
            if (JoinedList.Count < 5)
                StaticStudentInterestList.RemoveAll(x => 1 == 1);
            else
                StaticStudentInterestList.RemoveRange(0, 6);

            dlStudentShownInterestList2.DataSource = JoinedList.Take(6);
            dlStudentShownInterestList2.DataBind();

            dlStudentShownInterestList.DataSource = JoinedList.Take(6);
            dlStudentShownInterestList.DataBind();
            if (dlStudentShownInterestList.Items.Count == 0)
            {
                lblrec.Visible = true;
            }
        }

        //Student Applying For Same Country
        void BindStudentapplingCountry()
        {
            lblrec.Visible = false;
            StudentBLL studentBll = new StudentBLL();
            UserBLL userBll = new UserBLL();
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            Student student = studentBll.GetByUserName(user.UserName);
            List<Student> studentlist = studentBll.GetAll().Where(x => x.Country == student.Country).ToList();

            StaticStudentList = studentlist.ToList(); //StaticStudentList will be used for displaying record
            if (studentlist.Count < 5)
                StaticStudentList.RemoveAll(x => 1 == 1);
            else
                StaticStudentList.RemoveRange(0, 6);

            studentlist.Remove(student);
            dlStudentShownInterestList.DataSource = studentlist.Take(6);
            dlStudentShownInterestList.DataBind();

            dlStudentShownInterestList2.DataSource = studentlist.Take(6);
            dlStudentShownInterestList2.DataBind();
            if (dlStudentShownInterestList.Items.Count == 0)
            {
                lblrec.Visible = true;
            }
        }
        //Student Applying For Cource
        void bindapplystudentdetail()
        {
            lblrec.Visible = false;
            StudentBLL studentBll = new StudentBLL();
            UserBLL userBll = new UserBLL();
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            Student student = studentBll.GetByUserName(user.UserName);
            List<Student> studentlist = studentBll.GetAll().Where(x => x.LookingFor == student.LookingFor).ToList();

            StaticStudentList = studentlist.ToList(); //StaticStudentList will be used for displaying record
            if (studentlist.Count < 5)
                StaticStudentList.RemoveAll(x => 1 == 1);
            else
                StaticStudentList.RemoveRange(0, 6);

            studentlist.Remove(student);
            dlStudentShownInterestList.DataSource = studentlist.Take(6);
            dlStudentShownInterestList.DataBind();

            dlStudentShownInterestList2.DataSource = studentlist.Take(6);
            dlStudentShownInterestList2.DataBind();
            if (dlStudentShownInterestList.Items.Count == 0)
            {
                lblrec.Visible = true;
            }
        }

        protected void dlStudentShownInterestList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName");
                LinkButton lnkStudentName = (LinkButton)e.Item.FindControl("lnkBtnViewProfile");
                Label lblAddressCityCountry = (Label)e.Item.FindControl("lblAddressCityCountry");
                Label lblLookingFor = (Label)e.Item.FindControl("lblLookingFor");
                Label lblExpectedJoining = (Label)e.Item.FindControl("lblExpectedJoining");
                Image img = (Image)e.Item.FindControl("ImgBtnPhoto");
                // HiddenField hndid = (HiddenField)e.Item.FindControl("hndId");
                HyperLink lnk = (HyperLink)e.Item.FindControl("lnkMessage");

                string field = "";
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



                        //hndid =std.StudentId.ToString();

                        lnkStudentName.Text = fName + " " + mname + " " + Lname;
                        //lblAddressCityCountry.Text = std.Address1.ToString() + " , " + std.City.ToString() + " , " + std.Country.ToString();
                        lblAddressCityCountry.Text = std.City.ToString();
                        if (std.DesiredFieldofStudy != null)
                            field = " in " + std.DesiredFieldofStudy;

                        lblLookingFor.Text = Enum.Parse(typeof(ProgramLookingFor), std.LookingFor.ToString()).ToString() + field + " in " + std.LookingForCountry + ".";
                        lblExpectedJoining.Text = Enum.Parse(typeof(expectedStart), std.StartDate.ToString()).ToString();
                        lnk.NavigateUrl = "javascript:OpenMsgPopup('" + std.UserName + "','" + std.FirstName + " " + std.LastName + "')";
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
                Label lblAddressCityCountry = (Label)e.Item.FindControl("lblAddressCityCountry");
                Label lblLookingFor = (Label)e.Item.FindControl("lblLookingFor");
                Label lblExpectedJoining = (Label)e.Item.FindControl("lblExpectedJoining");
                Image img = (Image)e.Item.FindControl("ImgBtnPhoto");
                // HiddenField hndid = (HiddenField)e.Item.FindControl("hndId");
                HyperLink lnk = (HyperLink)e.Item.FindControl("lnkMessage");

                string field = "";
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



                        //hndid =std.StudentId.ToString();

                        lnkStudentName.Text = fName + " " + mname + " " + Lname;
                        //lblAddressCityCountry.Text = std.Address1.ToString() + " , " + std.City.ToString() + " , " + std.Country.ToString();
                        lblAddressCityCountry.Text = std.City.ToString();
                        if (std.DesiredFieldofStudy != null)
                            field = " in " + std.DesiredFieldofStudy;

                        lblLookingFor.Text = Enum.Parse(typeof(ProgramLookingFor), std.LookingFor.ToString()).ToString() + field + " in " + std.LookingForCountry + ".";
                        lblExpectedJoining.Text = Enum.Parse(typeof(expectedStart), std.StartDate.ToString()).ToString();
                        lnk.NavigateUrl = "javascript:OpenMsgPopup('" + std.UserName + "','" + std.FirstName + " " + std.LastName + "')";
                    }
                }
            }
        }

        protected void ImgBtnPhoto_Click(object sender, ImageClickEventArgs e)
        {
            string username = ((ImageButton)sender).CommandArgument.ToString();
            Student student = studentBll.GetByUserName(username);
            if (student != null)
                username = student.StudentId.ToString();
            else
                username = "0";
            if (!string.IsNullOrEmpty(username))
            {
                if (Request.QueryString["status"] == "Applied")
                {
                    Response.Redirect("Account/ProfileOverView.aspx?intu=" + username);
                }
                else if (Request.QueryString["status"] == "Approved")
                {
                    Response.Redirect("Account/ProfileOverView.aspx?intu=" + username + "&dec=show");
                }
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
                if (Request.QueryString["status"] == "Applied")
                {
                    Response.Redirect("Account/ProfileOverView.aspx?intu=" + username);
                }
                else if (Request.QueryString["status"] == "Approved")
                {
                    Response.Redirect("Account/ProfileOverView.aspx?intu=" + username + "&dec=show");
                }
                else
                {
                    Response.Redirect("Account/ProfileOverView.aspx?intu=" + username);
                }
            }
        }

        // For Scrolling
        //METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST

        [WebMethod()]
        public static string AppendAndDisplayStudents()
        {
            StringBuilder sb = new StringBuilder();

            List<Student> tmpStudentList = new List<Student>();
            List<StudentInterest> tmpStudentInterestList = new List<StudentInterest>();
            StudentBLL studentBll = new StudentBLL();


            if (queryData == "Applied" || queryData == "Approved")
            {

                if (StaticStudentInterestList.Count != 0)
                {
                    if (StaticStudentInterestList.Count < 5)
                    {
                        tmpStudentInterestList = StaticStudentInterestList.ToList();
                        StaticStudentInterestList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpStudentInterestList = StaticStudentInterestList.GetRange(0, 5);
                        StaticStudentInterestList.RemoveRange(0, 5);
                    }

                    string img;
                    string tmpurl = "";
                    string field;

                    for (int i = 0; i < tmpStudentInterestList.Count; i++)
                    {

                        #region MyRegion

                        string username = "";
                        Student std = studentBll.GetByUserName(tmpStudentInterestList[i].StudentUserName);
                        if (std != null)
                            username = std.StudentId.ToString();
                        else
                            username = "0";
                        if (queryData == "Applied")
                        {
                            tmpurl = "Account/ProfileOverView.aspx?intu=" + username;
                        }
                        else if (queryData == "Approved")
                        {
                            tmpurl = "Account/ProfileOverView.aspx?intu=" + username + "&dec=show";
                        }
                        else
                        {
                            tmpurl = "Account/ProfileOverView.aspx?intu=" + username;
                        }


                        if (std.DesiredFieldofStudy != null)
                            field = " in " + std.DesiredFieldofStudy;
                        else
                            field = "";

                        if (std.Photo != null)
                            img = @"Images\Profile\" + std.Photo;
                        else
                            img = @"\Images\no_image.jpg";
                        sb.Append("<br/><span><li><div class='clg_img' style='width:8%; border:0px'> <img src='" + img + "' /> </div>");
                        sb.Append(" <div class='clg_descripition'><p><b><a href='" + tmpurl + "'>" + std.FirstName + " " + std.LastName + "</a></b><br/> Applying from <span>" + std.City + "</span> for <span>" + Enum.Parse(typeof(ProgramLookingFor), std.LookingFor.ToString()) + "</span> " + field + " in " + std.LookingForCountry + ". <br/> Desired joining in date " + Enum.Parse(typeof(expectedStart), std.StartDate.ToString()) + " </p></div>");
                        sb.Append("<div class='clg_apply'><a id='lnkMessage' class='msgbtn fright' href=\"javascript:OpenMsgPopup('" + std.UserName + "','" + std.FirstName + " " + std.LastName + "')\">Message</a> </div></li></span>");
                        #endregion

                    }

                    return sb.ToString();
                }
                else
                {
                    return "no";
                }


            }
            else if (queryData == "Country" || queryData == "Course")
            {
                if (StaticStudentList.Count != 0)
                {
                    if (StaticStudentList.Count < 5)
                    {
                        tmpStudentList = StaticStudentList.ToList();
                        StaticStudentList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpStudentList = StaticStudentList.GetRange(0, 5);
                        StaticStudentList.RemoveRange(0, 5);
                    }

                    string img;
                    string tmpurl = "";
                    string field;

                    for (int i = 0; i < tmpStudentList.Count; i++)
                    {

                        #region MyRegion

                        tmpurl = "Account/ProfileOverView.aspx?intu=" + tmpStudentList[i].StudentId;

                        if (tmpStudentList[i].DesiredFieldofStudy != null)
                            field = " in " + tmpStudentList[i].DesiredFieldofStudy;
                        else
                            field = "";

                        if (tmpStudentList[i].Photo != null)
                            img = @"Images\Profile\" + tmpStudentList[i].Photo;
                        else
                            img = @"\Images\no_image.jpg";
                        sb.Append("<br/><span><li><div class='clg_img'> <img src='" + img + "' /> </div>");
                        sb.Append(" <div class='clg_descripition'><p><b><a href='" + tmpurl + "'>" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "</a></b><br/> Applying from <span>" + tmpStudentList[i].City + "</span> for <span>" + Enum.Parse(typeof(ProgramLookingFor), tmpStudentList[i].LookingFor.ToString()) + "</span> " + field + " in " + tmpStudentList[i].LookingForCountry + ". <br/> Desired joining in date " + Enum.Parse(typeof(expectedStart), tmpStudentList[i].StartDate.ToString()) + " </p></div>");
                        sb.Append("<div class='clg_apply'><a id='lnkMessage' class='msgbtn fright' href=\"javascript:OpenMsgPopup('" + tmpStudentList[i].StudentId + "','" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "')\">Message</a> </div></li></span>");

                        #endregion
                    }

                    return sb.ToString();
                }
                else
                {
                    return "no";
                }
            }
            else
            {
                return "Error";
            }

        }

    }
}