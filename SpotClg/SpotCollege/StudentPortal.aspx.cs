using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.Common;
using SpotCollege.DAL;
using SpotCollege.BLL;
using System.Web.Services;
using System.Text;

namespace SpotCollege
{
    public partial class StudentPortal : System.Web.UI.Page
    {
        StudentBLL studentBll = new StudentBLL();
        UniversityBLL universityBLL = new UniversityBLL();
        static List<Student> staticStudentList = new List<Student>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindStudentList();
                if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                {
                    StudentBLL studentBll = new StudentBLL();
                    Student student = studentBll.GetByUserName(CookieHelper.Username);
                    StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
                    StudentTestBLL studentTestBll = new StudentTestBLL();
                    if (student != null)
                    {
                        StudentAcademic stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                        if (string.IsNullOrEmpty(student.LookingForCountry) || string.IsNullOrEmpty(student.Country) || student.LookingFor == 0)
                        {
                            Response.Redirect("Account/EditProfile.aspx?sec=EducationPreferences");
                        }
                        else if (stdAca == null)
                        {
                            Response.Redirect("Account/EditProfile.aspx?sec=Academics");
                        }

                        List<StudentTest> stdTest = studentTestBll.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
                        if (stdTest == null || stdTest.Count == 0)
                        {
                            Response.Redirect("Account/EditProfile.aspx?sec=IntTest");
                        }
                        if (string.IsNullOrEmpty(student.Photo))
                        {
                            Response.Redirect("Account/EditProfile.aspx?sec=Photo");
                        }

                        BindStudentList(student.LookingForCountry, student.LookingFor, student.Country);
                        BindDropdown(student.Country, student.LookingForCountry);
                        //ddlSearchByLookingFor.SelectedValue = std.LookingFor.ToString();
                        ddlSearchByLookingFor.SelectedIndex = (int)student.LookingFor;
                    }
                    else
                    {
                        Response.Redirect("Account/EditProfile.aspx?sec=basic");
                    }
                }
                if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    University uni = universityBLL.GetByUserName(CookieHelper.Username);
                    if (uni != null)
                    {
                        BindStudentList(uni.Country);
                        BindDropdown(uni.Country, uni.Country);
                        ddlSearchByLookingFor.SelectedIndex = 0;
                    }
                }

            }
        }

        void BindDropdown(string current, string looking)
        {
            //Bind Country Dropdown
            ddlSearchByCountry.Items.Add(new ListItem("Select Country", "0"));
            //ddlSearchByCountry.SelectedValue = "0";
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));

            string tmp = "";
            int[] CountryValues = (int[])Countryvalue;
            for (int i = 0; i < Countrynames.Length; i++)
            {
                if (EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]) == looking)
                    tmp = CountryValues[i].ToString();
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]), CountryValues[i].ToString());
                ddlSearchByCountry.Items.Add(item);
            }
            ddlSearchByCountry.SelectedValue = tmp;


            dllStudentCountry.Items.Add(new ListItem("Select Country", "0"));
            //ddlSearchByCountry.SelectedValue = "0";
            for (int i = 0; i < Countrynames.Length; i++)
            {
                if (EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]) == current)
                    tmp = CountryValues[i].ToString();
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]), CountryValues[i].ToString());
                dllStudentCountry.Items.Add(item);
            }
            dllStudentCountry.SelectedValue = tmp;
            //Bind LookingFor In Dropdown
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(ProgramLookingFor));
            value = Enum.GetValues(typeof(ProgramLookingFor));
            Values = (int[])value;

            ddlSearchByLookingFor.Items.Add(new ListItem("Select Any", "0"));
            ddlSearchByLookingFor.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)Values[i]), Values[i].ToString());
                ddlSearchByLookingFor.Items.Add(item);
            }
        }

        void BindStudentList(string lookingforcoutry, int lkngfor, string country)
        {
            string looking_for = lkngfor.ToString();
            List<Student> studentlist;
            if (looking_for == "0")
            {
                //studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username && x.LookingForCountry.Contains(lookingforcoutry) && x.Country.Contains(country)).ToList();
                studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username).OrderBy(y => y.Country == country).ToList();
            }
            else
                //studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username && x.LookingForCountry.Contains(lookingforcoutry) && x.LookingFor.ToString().Contains(looking_for) && x.Country.Contains(country)).ToList();
                studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username).OrderBy(y => y.Country == country).ToList();


            staticStudentList = studentlist.ToList();
            if (studentlist.Count < 8)
                staticStudentList.RemoveAll(x => 1 == 1);
            else
                staticStudentList.RemoveRange(0, 8);

            dlStudentList2.DataSource = studentlist.Take(8);
            dlStudentList2.DataBind();

            dlStudentList.DataSource = studentlist.Take(8);
            dlStudentList.DataBind();

            if (studentlist.Count == 0)
                errorMsgDiv.InnerHtml = "No Student Found";
            else
                errorMsgDiv.InnerHtml = "";
        }
        void BindStudentList(string unicnt)
        {
            List<Student> studentlist = studentBll.GetAll().OrderBy(x => x.LookingForCountry == unicnt).ThenBy(y => y.LookingFor == 1).ToList();

            staticStudentList = studentlist.ToList();
            if (studentlist.Count < 8)
                staticStudentList.RemoveAll(x => 1 == 1);
            else
                staticStudentList.RemoveRange(0, 8);
            dlStudentList2.DataSource = studentlist.Take(8);
            dlStudentList2.DataBind();

            dlStudentList.DataSource = studentlist.Take(8);
            dlStudentList.DataBind();

            if (studentlist.Count == 0)
                errorMsgDiv.InnerHtml = "No Student Found";
            else
                errorMsgDiv.InnerHtml = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            errorMsgDiv.InnerHtml = "";
            staticStudentList.RemoveAll(x => 1 == 1);
            List<Student> studentlist = new List<Student>();
            studentlist = studentBll.GetAll().ToList();



            if (dllStudentCountry.SelectedIndex == 0 && ddlSearchByLookingFor.SelectedIndex != 0 && ddlSearchByCountry.SelectedIndex != 0)
                studentlist = studentlist.Where(x => x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor.SelectedValue) && x.LookingForCountry == ddlSearchByCountry.SelectedItem.Text && x.UserName != CookieHelper.Username).ToList();

            else if (dllStudentCountry.SelectedIndex != 0 && ddlSearchByLookingFor.SelectedIndex == 0 && ddlSearchByCountry.SelectedIndex != 0)
                studentlist = studentlist.Where(x => x.LookingForCountry == ddlSearchByCountry.SelectedItem.Text && x.Country == dllStudentCountry.SelectedItem.Text && x.UserName != CookieHelper.Username).ToList();

            else if (dllStudentCountry.SelectedIndex != 0 && ddlSearchByLookingFor.SelectedIndex != 0 && ddlSearchByCountry.SelectedIndex == 0)
                studentlist = studentlist.Where(x => x.Country == dllStudentCountry.SelectedItem.Text && x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor.SelectedValue) && x.UserName != CookieHelper.Username).ToList();



            else if (dllStudentCountry.SelectedIndex == 0 && ddlSearchByLookingFor.SelectedIndex == 0 && ddlSearchByCountry.SelectedIndex != 0)
                studentlist = studentlist.Where(x => x.LookingForCountry == ddlSearchByCountry.SelectedItem.Text && x.UserName != CookieHelper.Username).ToList();

            else if (dllStudentCountry.SelectedIndex != 0 && ddlSearchByLookingFor.SelectedIndex == 0 && ddlSearchByCountry.SelectedIndex == 0)
                studentlist = studentlist.Where(x => x.Country == dllStudentCountry.SelectedItem.Text && x.UserName != CookieHelper.Username).ToList();

            else if (dllStudentCountry.SelectedIndex == 0 && ddlSearchByLookingFor.SelectedIndex != 0 && ddlSearchByCountry.SelectedIndex == 0)
                studentlist = studentlist.Where(x => x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor.SelectedValue) && x.UserName != CookieHelper.Username).ToList();



            else if (dllStudentCountry.SelectedIndex != 0 && ddlSearchByLookingFor.SelectedIndex != 0 && ddlSearchByCountry.SelectedIndex != 0)
                studentlist = studentlist.Where(x => x.LookingForCountry == ddlSearchByCountry.SelectedItem.Text && x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor.SelectedValue) && x.Country == dllStudentCountry.SelectedItem.Text && x.UserName != CookieHelper.Username).ToList();
            else { studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username).OrderBy(y => y.Country).ToList(); }


            staticStudentList = studentlist.ToList();
            if (studentlist.Count < 8)
                staticStudentList.RemoveAll(x => 1 == 1);
            else
                staticStudentList.RemoveRange(0, 8);

            dlStudentList.DataSource = studentlist.Take(8);
            dlStudentList.DataBind();
            dlStudentList2.DataSource = studentlist.Take(5);
            dlStudentList2.DataBind();

            if (studentlist.Count == 0)
                errorMsgDiv.InnerHtml = "No Student Found";
            else
                errorMsgDiv.InnerHtml = "";
        }

        protected void lnkBtnViewMore_Click(object sender, EventArgs e)
        {
            string username = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                Response.Redirect("Account/ProfileOverView.aspx?intu=" + username);
            }
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST
        [WebMethod()]
        public static string AppendAndDisplayStudent()
        {
            StringBuilder sb = new StringBuilder();

            List<Student> tmpStudentList = new List<Student>();
            if (staticStudentList != null)
            {
                if (staticStudentList.Count != 0)
                {
                    if (staticStudentList.Count < 8)
                    {
                        tmpStudentList = staticStudentList.ToList();
                        staticStudentList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpStudentList = staticStudentList.GetRange(0, 8);
                        staticStudentList.RemoveRange(0, 8);
                    }
                    string img;
                    for (int i = 0; i < tmpStudentList.Count; i++)
                    {
                        if (tmpStudentList[i].Photo != null)
                            img = @"Images\Profile\" + tmpStudentList[i].Photo;
                        else
                            img = @"\Images\no_image.jpg";

                        sb.Append("<span><li><div class='name'>" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "<div class='country'>" + tmpStudentList[i].Country + "</div></div>");
                        sb.Append("<div class='img'><img src=" + img + " /></a></div>");
                        sb.Append("<div class='detail'><ul class='list_4'><li><label>Location :</label><span>" + tmpStudentList[i].City + ", " + tmpStudentList[i].Country + "</span></li>");
                        if (tmpStudentList[i].StudyingIn != 0)
                        {
                            sb.Append("<li><label>Current Status :</label><span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CurrentlyIn)(tmpStudentList[i].StudyingIn))).ToString() + "</span></li>");
                            sb.Append("<li><label>Looking for a :</label><span>" + ((SpotCollege.Common.ProgramLookingFor)(tmpStudentList[i].LookingFor)).ToString() + " in " + tmpStudentList[i].DesiredFieldofStudy + "</span></li>");
                            sb.Append("<li><label>Desired Start Date :</label><span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.expectedStart)(tmpStudentList[i].StartDate))).ToString() + "</span></li>");
                        }
                        else
                        {
                            sb.Append("<li><label>Current Status :</label><span></span></li>");
                            sb.Append("<li><label>Looking for a :</label><span></span></li>");
                            sb.Append("<li><label>Desired Start Date :</label><span></span></li>");
                        }
                        sb.Append(" <li><label>Desired Location :</label><span>" + tmpStudentList[i].LookingForCountry + "</span>");
                        sb.Append(" <li><label>Profile Created On :</label><span>" + tmpStudentList[i].CreatedDate.Value.ToShortDateString() + "</span></li></ul></div>");
                        sb.Append(" <div class='morebtn_'><input type='hidden' id='" + tmpStudentList[i].StudentId + "' value='" + tmpStudentList[i].UserName + "' />");
                        sb.Append("<a href='#' class='msgbtn' id='" + tmpStudentList[i].UserName + "' onclick=\"javascript:OpenMsgPopup( '" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "','" + tmpStudentList[i].StudentId + "' )\">Message</a></div></li></span>");

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

        protected void dlStudentList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("hndUsername");
                Label lblCurrentStatus = (Label)e.Item.FindControl("lblCurrentStatus");
                Label lblLookingFor = (Label)e.Item.FindControl("lblLookingFor");
                Label lblStartDate = (Label)e.Item.FindControl("lblStartDate");
                Label lblDate = (Label)e.Item.FindControl("lblDate");
                Image img = (Image)e.Item.FindControl("imgStudent");
                Student std = studentBll.GetByUserName(username.Value);
                if (std != null)
                {
                    if (std.StudyingIn == 0)
                    {
                        lblCurrentStatus.Text = "";
                    }

                    if (std.LookingFor == 0)
                    {
                        lblLookingFor.Text = "";
                    }
                    else if (std.DesiredFieldofStudy == "")
                    {
                        lblLookingFor.Text = ((ProgramLookingFor)std.LookingFor).ToString();
                    }

                    if (std.StartDate == 0)
                    {
                        lblStartDate.Text = "";
                    }

                    if (std.CreatedDate != null)
                    {
                        DateTime dt = (DateTime)std.CreatedDate;
                        lblDate.Text = dt.ToShortDateString();
                    }
                    if (std.Photo != null)
                    {
                        img.ImageUrl = "Images/Profile/" + std.Photo;
                    }
                    else
                    {
                        img.ImageUrl = "Images/no_image.jpg";
                    }
                }

            }
        }
    }
}