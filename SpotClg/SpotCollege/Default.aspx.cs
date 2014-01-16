using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.BLL;
using SpotCollege.Common;
using SpotCollege.DAL;

namespace SpotCollege
{
    public partial class Default : System.Web.UI.Page
    {
        UserBLL userBll = new UserBLL();
        StudentBLL studentBll = new StudentBLL();
        UniversityBLL universityBll = new UniversityBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("Account/Login.aspx");
            if (CookieHelper.Username != "")
            {
                SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
                if (user != null)
                {
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        Response.Redirect("Admin/DashBoard.aspx");
                    }
                    else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                    {
                        University university = universityBll.GetByUserName(CookieHelper.Username);
                        if (university != null)
                        {
                            Response.Redirect("DashboardCollege.aspx");
                        }
                        else
                        {
                            Response.Redirect("Account/EditProfileUniversity.aspx");
                        }
                    }
                    else if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                    {
                        Student student = studentBll.GetByUserName(CookieHelper.Username);
                        if (student != null)
                        {
                            Response.Redirect("DashboardStudent.aspx");
                        }
                        else
                        {
                            Response.Redirect("Account/EditProfile.aspx");
                        }
                    }
                }
            }
        }



        //Univercity Account
        protected void lnkUnviercity_Click(object sender, EventArgs e)
        {
            
                Response.Redirect("Account/RegisterUniversityAccount.aspx");
            
        }
    }
}