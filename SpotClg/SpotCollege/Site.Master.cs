using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.Common;
using SpotCollege.BLL;
using SpotCollege.DAL;

namespace SpotCollege
{
    public partial class SiteMaster : MasterPage
    {
        UserBLL userBll = new UserBLL();
        StudentBLL studentBll = new StudentBLL();
        UniversityBLL universityBll = new UniversityBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CookieHelper.UserType == "" || CookieHelper.Username == "")
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                {
                    pnlStudent.Visible = true;
                    pnlUniversity.Visible = false;
                }
                else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    pnlStudent.Visible = false;
                    pnlUniversity.Visible = true;
                }


                if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                {
                    Student st = studentBll.GetByUserName(CookieHelper.Username);
                    if (st != null)
                    {
                        lblUsername.Text = st.FirstName + " " + st.LastName;
                        if (st.Photo != null)
                        {
                            imgUser.ImageUrl = "Images/Profile/" + st.Photo;
                        }
                        else
                        {
                            imgUser.ImageUrl = "Images/no_image.jpg";
                        }
                    }
                    else
                    {
                        imgUser.ImageUrl = "Images/no_image.jpg";
                        lblUsername.Text = "(no-name)";
                    }
                }
                else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    University un = universityBll.GetByUserName(CookieHelper.Username);
                    if (un != null)
                    {
                        lblUsername.Text = un.UniversityName;
                        if (un.Image != null)
                        {
                            imgUser.ImageUrl = "Images/Profile/" + un.Image;
                        }
                        else
                        {
                            imgUser.ImageUrl = "Images/no_image.jpg";
                        }
                    }

                    User user = userBll.Get(CookieHelper.Username);
                    if (user != null)
                    {
                        if (user.IsActive == false)
                        {
                            //lnkProfileButton.Visible = false;
                            //lnkMessages.Visible = false;
                            //lnkUniversities.Visible = false;
                            //// lnkHome.Visible = false;
                            //lnkStudentButton.Visible = false;
                        }
                    }
                }
                else if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                {
                    //lnkProfileButton.Visible = false;
                    lblUsername.Text = "Admin";
                    imgUser.ImageUrl = "Images/no_image.jpg";
                }
            }
        }

        //protected void lnkProfileButton_Click(object sender, EventArgs e)
        //{
        //    if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
        //    {
        //        Response.Redirect("~/Account/ProfileOverView.aspx");
        //    }
        //    else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
        //    {
        //        Response.Redirect("~/Account/ProfileOverviewUniversity.aspx");
        //    }
        //}

        protected void lnkLogo_Click(object sender, EventArgs e)
        {
            if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                University university = universityBll.GetByUserName(CookieHelper.Username);
                if (university != null)
                {
                    Response.Redirect("~/DashboardCollege.aspx");
                }
                else
                {
                    Response.Redirect("~/Account/EditProfileUniversity.aspx");
                }
            }
            else if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                Student student = studentBll.GetByUserName(CookieHelper.Username);
                if (student != null)
                {
                    Response.Redirect("~/DashboardStudent.aspx");
                }
                else
                {
                    Response.Redirect("~/Account/EditProfile.aspx");
                }
            }
            else if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                Response.Redirect("~/Admin/DashBoard.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        //protected void lnkHome_Click(object sender, EventArgs e)
        //{
        //    if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
        //    {
        //        Response.Redirect("~/DashboardStudent.aspx");
        //    }
        //    else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
        //    {
        //        Response.Redirect("~/DashboardCollege.aspx");
        //    }
        //}

       


    }
}