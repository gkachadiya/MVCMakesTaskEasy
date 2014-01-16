using SpotCollege.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.BLL;
using SpotCollege.Common;
using System.Configuration;

namespace SpotCollege.Account
{
    public partial class ProfileOverviewUniversity : System.Web.UI.Page
    {
        UniversityBLL universityBll = new UniversityBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            University university = universityBll.GetAll().Where(x => x.UserName == CookieHelper.Username).SingleOrDefault();
            if (university != null)
            {

                //Basic Details

                lblUniversityName.Text = university.UniversityName;
                lblUniversityAddress.Text = university.Address;
                lblAdministratorEmail.Text = university.UserName;
                lblAdminName.Text = university.AdminName;
                lblCity.Text = university.City;
                lblCountry.Text = university.Country;
                lblEstablishedYear.Text = university.EstablishedYear.ToString();
                lblDescription.Text = university.Description;
                if (university.Image == null || university.Image == string.Empty)
                {
                    imgProfile.Src = "../Images/no_image.jpg";
                }
                else
                {
                    imgProfile.Src = "../Images/Profile/" + university.Image;
                }

                //Cost For International Students
                string [] tmp ;
                if (university.UnderGraduateFee != null)
                {
                    tmp = university.UnderGraduateFee.ToString().Split('-');
                        lblFeesForUnderGraduateStudents.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblFeesForUnderGraduateStudents.Text = "Not Available";
                if (university.GraduateFee != null)
                {
                    tmp = university.GraduateFee.ToString().Split('-');
                    lblFeesForGraduateStudents.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblFeesForGraduateStudents.Text = "Not Available";


                if (university.BookFee != null)
                {
                    tmp = university.BookFee.ToString().Split('-');
                    lblBookFees.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32( tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblBookFees.Text = "Not Available";


                if (university.BoardFee != null)
                {
                    tmp = university.BoardFee.ToString().Split('-');
                    lblBoardFees.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblBoardFees.Text = "Not Available";


                //Enrollment Number

                if (university.Graduates != null)
                    lblNoofGraduateStudent.Text = university.Graduates.ToString();
                else
                    lblNoofGraduateStudent.Text = "Not Available";
                if (university.InternationalGraduate != null)
                    lblNoofInternationalStudent.Text = university.InternationalGraduate.ToString();
                else
                    lblNoofInternationalStudent.Text = "Not Available";
                if (university.UnderGraduates != null)
                    lblNoofUnderGraduateStudent.Text = university.UnderGraduates.ToString();
                else
                    lblNoofUnderGraduateStudent.Text = "Not Available";

                //Programs and majors
                if (university.Degree != "")
                    lblProgramOffered.Text = university.Degree;
                else
                    lblProgramOffered.Text = "Not Available";
                if (university.Courses != "")
                    lblCoursesOffered.Text = university.Courses;
                else
                    lblCoursesOffered.Text = "Not Available";
            }
        }

        protected void lnkEditBasicInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfileUniversity.aspx?sec=basic");
        }

        protected void lnkCostEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfileUniversity.aspx?sec=cost");
        }

        protected void lnkEnrollmentEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfileUniversity.aspx?sec=enroll");
        }

        protected void lnkProgramAndMajorsEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfileUniversity.aspx?sec=program");
        }
    }
}