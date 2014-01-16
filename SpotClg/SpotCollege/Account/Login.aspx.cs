using SpotCollege.BLL;
using SpotCollege.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.DAL;
using System.Web.Services;

namespace SpotCollege.Account
{
    public partial class Login : System.Web.UI.Page
    {
        UserBLL userBll = new UserBLL();
        UniversityBLL universityBll = new UniversityBLL();
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CookieHelper.Username != "")
            {
                SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
                if (user != null)
                {
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        Response.Redirect("../Admin/DashBoard.aspx");
                    }
                    else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                    {
                        University university = universityBll.GetByUserName(CookieHelper.Username);
                        if (university != null)
                        {
                            Response.Redirect("../DashboardCollege.aspx");
                        }
                        else
                        {
                            Response.Redirect("EditProfileUniversity.aspx");
                        }
                    }
                    else if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                    {
                        Student student = studentBll.GetByUserName(CookieHelper.Username);
                        StudentAcademic stdAca=new StudentAcademic();
                        if (student != null)
                        {
                            stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                        }
                        if (student != null && stdAca!=null)
                        {
                            Response.Redirect("../DashboardStudent.aspx");
                        }
                        else
                        {
                            Response.Redirect("EditProfile.aspx");
                        }
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SpotCollege.DAL.User user = userBll.Get(txtUserName.Text);
            if (user != null && user.Password==txtPassword.Text && user.IsActive == true)
            {
                CookieHelper.CreateLoginCookie(user.UserName, "", user.LoginTypeId.ToString());
                if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                {
                    //Response.Redirect("../DashboardAdmin.aspx");
                    
                    Response.Redirect("../Admin/DashBoard.aspx");
                }
                else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    University university = universityBll.GetByUserName(CookieHelper.Username);
                    if (university != null)
                    {
                        Response.Redirect("../DashboardCollege.aspx");
                    }
                    else
                    {
                        Response.Redirect("EditProfileUniversity.aspx");
                    }
                }
                else if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                {
                    Student student = studentBll.GetByUserName(CookieHelper.Username);
                    StudentAcademic stdAca = new StudentAcademic();
                    if (student != null)
                    {
                        stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                    }
                    if (student != null && stdAca != null)
                    {
                        Response.Redirect("../DashboardStudent.aspx");
                    }
                    else
                    {
                        Response.Redirect("EditProfile.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                lblErrorMsg.Text = "Username or Password is Wrong.Please Try Again..!";
                lblErrorMsg.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

                SpotCollege.DAL.User user = new SpotCollege.DAL.User();
                user.UserName = txtEmail.Text;
                user.Password = txtRegPassword.Text;

                user.LoginTypeId = (int)LoginTypes.Student;
                user.IsActive = true;
                userBll.Save(user);
                txtUserName.Text = "";

                CookieHelper.CreateLoginCookie(user.UserName, "", ((int)LoginTypes.Student).ToString());
                Response.Redirect("EditProfile.aspx");
        }

        [WebMethod()]
        public static string RegisterUser(string UserName, string Password)
        {
            string str = string.Empty;
            UserBLL userBll = new UserBLL();
            SpotCollege.DAL.User user = new SpotCollege.DAL.User();
            user.UserName = UserName;
            user.Password = Password;

            user.LoginTypeId = (int)LoginTypes.Student;
            user.IsActive = true;
            userBll.Save(user);

            CookieHelper.CreateLoginCookie(user.UserName, "", ((int)LoginTypes.Student).ToString());
            return str;
        }


        [WebMethod()]
        public static string ValidateUser(string UserName)
        {
            UserBLL userBll = new UserBLL();
            string str = string.Empty;
            SpotCollege.DAL.User user = userBll.Get(UserName);

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