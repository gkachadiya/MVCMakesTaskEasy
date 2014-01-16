using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.Common;
using SpotCollege.DAL;
using SpotCollege.BLL;
using System.Configuration;
using System.IO;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace SpotCollege
{
    public partial class Collegelist : System.Web.UI.Page
    {
        public static List<University> StaticUniversityList = new List<University>();


        protected void Page_Load(object sender, EventArgs e)
        {
            UniversityBLL uinversityBLL = new UniversityBLL();
            StudentBLL studentBLL = new StudentBLL();


            if (!Page.IsPostBack)
            {

                if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    University uni = uinversityBLL.GetByUserName(CookieHelper.Username);
                    BindDropDown(uni.Country);
                    bindUniversityDetail();
                    //if (uni.Courses != null)
                    //    ddlCourses.SelectedItem.Text = uni.Courses;
                    //if (uni.Degree != null)
                    //    ddlLookingFor.SelectedValue = uni.Degree;
                }
                if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                {

                    Student student = studentBLL.GetByUserName(CookieHelper.Username);
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
                        BindDropDown(student.Country);
                        ddlLookingFor.SelectedValue = student.LookingFor.ToString();
                        bindUniversityDetail();
                    }
                    else
                    {
                        Response.Redirect("Account/EditProfile.aspx?sec=basic");
                    }

                }

            }
        }

        void BindDropDown(string cont)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            ddlCountryList.Items.Add(new ListItem("Select Country", "0"));
            // ddlCountryList.SelectedValue = "0";
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));
            string tmp = "";
            int[] CountryValues = (int[])Countryvalue;
            for (int i = 0; i < Countrynames.Length; i++)
            {
                if (EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]) == cont)
                    tmp = CountryValues[i].ToString();
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]), CountryValues[i].ToString());
                ddlCountryList.Items.Add(item);
            }
            ddlCountryList.SelectedValue = tmp;

            names = Enum.GetNames(typeof(ProgramLookingFor));
            value = Enum.GetValues(typeof(ProgramLookingFor));
            Values = (int[])value;

            ddlLookingFor.Items.Add(new ListItem("Select Any Degree", "0"));
            ddlLookingFor.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)Values[i]), Values[i].ToString());
                ddlLookingFor.Items.Add(item);
            }

            names = Enum.GetNames(typeof(CoursesOffered));
            value = Enum.GetValues(typeof(CoursesOffered));
            Values = (int[])value;

            ddlCourses.Items.Add(new ListItem("Select Any Course", "0"));
            ddlCourses.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Values[i]), Values[i].ToString());
                ddlCourses.Items.Add(item);
            }
        }

        private void bindUniversityDetail()
        {

            UniversityBLL universityBll = new UniversityBLL();
            StudentBLL studentBll = new StudentBLL();
            List<University> universityList = new List<University>();
            List<University> universityList2 = new List<University>();
            List<University> universityList3 = new List<University>();
            List<University> result = new List<University>();



            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                List<Student> studentList = new List<Student>();
                studentList = studentBll.GetAll().Where(x => x.UserName == CookieHelper.Username).ToList();
                if (studentList.Count != 0 && studentList != null)
                {
                    string lookingFr = EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)studentList[0].LookingFor);
                    if (lookingFr == "Bachelor's program")
                    {
                        lookingFr = "Bachelors";
                    }
                    else if (lookingFr == "Master's program")
                    {
                        lookingFr = "Masters";
                    }
                    else if (lookingFr == "Phd Program")
                    {
                        lookingFr = "Phd";
                    }
                    else if (lookingFr == "Other")
                    {
                        lookingFr = "Associates";
                    }
                    else
                    {
                        lookingFr = "";
                    }

                    universityList = universityBll.GetAll().Where(x => x.Degree != null && x.Courses != null && x.Country == studentList[0].LookingForCountry && x.Degree.Contains(lookingFr) && x.Courses.Contains(studentList[0].DesiredFieldofStudy)).ToList();
                    universityList2 = universityBll.GetAll().Where(x => !(string.IsNullOrEmpty(x.Degree)) & x.Country == studentList[0].LookingForCountry && x.Degree.Contains(lookingFr)).ToList();
                    universityList3 = universityBll.GetAll().Where(x => x.Country == studentList[0].LookingForCountry).ToList();

                    universityList2 = universityList2.Except(universityList).ToList();
                    universityList3 = universityList3.Except(universityList2).Except(universityList).ToList();

                    universityList.AddRange(universityList2);
                    universityList.AddRange(universityList3);
                    universityList.AddRange(universityBll.GetAll().ToList().Except(universityList));

                    UserBLL user = new UserBLL();
                    List<string> userlist = user.GetAll().Where(x => x.LoginTypeId == 2 && x.IsActive == true).Select(y => y.UserName).ToList();
                    string[] strarr = (string[])userlist.ToArray();

                    universityList = universityList.Where(x => strarr.Contains(x.UserName)).ToList();



                    StaticUniversityList = universityList.ToList(); //StaticUniversityList will be used for displaying record
                    if (universityList.Count < 6)
                        StaticUniversityList.RemoveAll(x => 1 == 1);
                    else
                        StaticUniversityList.RemoveRange(0, 6);

                    ddlunivercitylist.DataSource = universityList.Take(6);
                    ddlunivercitylist.DataBind();
                }
            }
            else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                University un = universityBll.GetByUserName(CookieHelper.Username);
                universityList = universityBll.GetAll().Where(x => x.UserName != CookieHelper.Username).OrderBy(x => x.Country == un.Country).ThenBy(x => x.Courses == un.Courses).ToList();


                UserBLL user = new UserBLL();
                List<string> userlist = user.GetAll().Where(x => x.LoginTypeId == 2 && x.IsActive == true).Select(y => y.UserName).ToList();
                string[] strarr = (string[])userlist.ToArray();

                universityList = universityList.Where(x => strarr.Contains(x.UserName)).ToList();

                StaticUniversityList = universityList.ToList(); //StaticUniversityList will be used for displaying record
                if (universityList.Count < 6)
                    StaticUniversityList.RemoveAll(x => 1 == 1);
                else
                    StaticUniversityList.RemoveRange(0, 6);

                ddlunivercitylist.DataSource = universityList;
                ddlunivercitylist.DataBind();

                divIntersted.Visible = false;
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StaticUniversityList.RemoveAll(x => 1 == 1);

            StudentBLL studentBll = new StudentBLL();
            UniversityBLL universityBll = new UniversityBLL();
            List<University> universityList = new List<University>();

            List<Student> studentList = studentBll.GetAll().Where(x => x.UserName == CookieHelper.Username).ToList();
            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                universityList = universityBll.GetAll();
            }
            else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                universityList = universityBll.GetAll().Where(x => x.UserName != CookieHelper.Username).ToList();
            }


            if (universityList.Count != 0)
            {

                string lookingFr = ddlLookingFor.SelectedItem.Text;
                if (lookingFr == "Bachelor's program")
                    lookingFr = "Bachelors";
                else if (lookingFr == "Master's program")
                    lookingFr = "Masters";
                else if (lookingFr == "Phd Program")
                    lookingFr = "Phd";
                else if (lookingFr == "Other")
                    lookingFr = "Associates";
                else
                    lookingFr = "";


                #region Conditions for searching results

                if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex == 0 && ddlCourses.SelectedIndex == 0)
                {
                    universityList = universityList.Where(x => x.Country == ddlCountryList.SelectedItem.Text).ToList();
                }
                else if (ddlCountryList.SelectedIndex == 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex == 0)
                {
                    universityList = universityList.Where(x => x.Degree != null && x.Degree.Contains(lookingFr)).ToList();
                }
                else if (ddlCountryList.SelectedIndex == 0 && ddlLookingFor.SelectedIndex == 0 && ddlCourses.SelectedIndex != 0)
                {
                    universityList = universityList.Where(x => x.Courses != null && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                }
                else if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex == 0)
                {
                    universityList = universityList.Where(x => x.Country == ddlCountryList.SelectedItem.Text && x.Degree != null && x.Degree.Contains(lookingFr)).ToList();
                }

                else if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex == 0 && ddlCourses.SelectedIndex != 0)
                {
                    universityList = universityList.Where(x => x.Country == ddlCountryList.SelectedItem.Text && x.Courses != null && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                }
                else if (ddlCountryList.SelectedIndex == 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex != 0)
                {
                    universityList = universityList.Where(x => x.Degree != null && x.Degree.Contains(lookingFr) && x.Courses != null && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                }
                else if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex != 0)
                {
                    universityList = universityList.Where(x => x.Country == ddlCountryList.SelectedItem.Text && x.Degree != null && x.Degree.Contains(lookingFr) && x.Courses != null && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                }
                else
                {
                    universityList = universityList.Where(x => (x.Degree != null || x.Courses != null)).ToList();
                }

                //if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex != 0)
                //{
                //    universityList = universityList.Where(x => (x.Degree != null || x.Courses != null) && x.Country == ddlCountryList.SelectedItem.Text && x.Degree.Contains(lookingFr) && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                //}

                //else if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex == 0)
                //{
                //    universityList = universityList.Where(x => x.Degree != null && x.Country == ddlCountryList.SelectedItem.Text && x.Degree.Contains(lookingFr)).ToList();
                //}

                //else if (ddlCountryList.SelectedIndex == 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex == 0)
                //{
                //    universityList = universityList.Where(x => x.Degree != null && x.Degree.Contains(lookingFr)).ToList();
                //}
                //else if (ddlCountryList.SelectedIndex == 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex != 0)
                //{
                //    universityList = universityList.Where(x => x.Degree != null && x.Courses != null && x.Degree.Contains(lookingFr) && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                //}

                //else if (ddlCountryList.SelectedIndex == 0 && ddlLookingFor.SelectedIndex == 0 && ddlCourses.SelectedIndex != 0)
                //{
                //    universityList = universityList.Where(x => x.Courses != null && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                //}

                //else if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex == 0 && ddlCourses.SelectedIndex == 0)
                //{
                //    universityList = universityList.Where(x => x.Country == ddlCountryList.SelectedItem.Text).ToList();
                //}

                //else if (ddlCountryList.SelectedIndex != 0 && ddlLookingFor.SelectedIndex != 0 && ddlCourses.SelectedIndex != 0)
                //{
                //    universityList = universityList.Where(x => x.Courses != null && x.Country == ddlCountryList.SelectedItem.Text && x.Courses.Contains(ddlCourses.SelectedItem.Text)).ToList();
                //}
                //else
                //{
                //    universityList = universityList.Where(x => (x.Degree != null || x.Courses != null)).ToList();
                //}
                #endregion

                UserBLL user = new UserBLL();
                List<string> userlist = user.GetAll().Where(x => x.LoginTypeId == 2 && x.IsActive == true).Select(y => y.UserName).ToList();
                string[] strarr = (string[])userlist.ToArray();

                universityList = universityList.Where(x => strarr.Contains(x.UserName)).ToList();

                string str = string.Join(",", strarr);
                StaticUniversityList = universityList.ToList();
                if (universityList.Count < 6)
                    StaticUniversityList.RemoveAll(x => 1 == 1);
                else
                    StaticUniversityList.RemoveRange(0, 6);


                ddlunivercitylist.DataSource = universityList.Take(6);
                ddlunivercitylist.DataBind();
                if (universityList.Count == 0)
                    errorMsgDiv.InnerHtml = "No University Found";
                else
                    errorMsgDiv.InnerHtml = "";

            }


        }

        protected void ddlunivercitylist_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            StudentBLL studentBll = new StudentBLL();
            StudentInterestBLL StudentInterestBLL = new StudentInterestBLL();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HnduniversityUserName");
                LinkButton lnkStudentName = (LinkButton)e.Item.FindControl("lnkBtnViewProfile");
                Button btnapply = (Button)e.Item.FindControl("btnapply");
                Button btndecline = (Button)e.Item.FindControl("btndecline");
                Label lblapproved = (Label)e.Item.FindControl("lblApproved");
                Image img = (Image)e.Item.FindControl("Imgunvercity");

                Label lblundergraduatestudFee = (Label)e.Item.FindControl("lblundergraduatestudFee");
                Label lblgraduatestudFee = (Label)e.Item.FindControl("lblgraduatestudFee");
                Label lblbookstudFee = (Label)e.Item.FindControl("lblbookstudFee");
                Label lblboardstudFee = (Label)e.Item.FindControl("lblboardstudFee");
                string[] tmp;

                if (((University)e.Item.DataItem).UniversityImage != null)
                    img.ImageUrl = "\\Images\\Profile\\" + ((University)e.Item.DataItem).UniversityImage.ToString();
                else
                    img.ImageUrl = "\\Images\\no_image.jpg";

                if (!string.IsNullOrWhiteSpace(lblundergraduatestudFee.Text))
                {
                    tmp = lblundergraduatestudFee.Text.ToString().Split('-');
                    lblundergraduatestudFee.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblundergraduatestudFee.Text = "Not Available";

                if (!string.IsNullOrWhiteSpace(lblgraduatestudFee.Text))
                {
                    tmp = lblgraduatestudFee.Text.ToString().Split('-');
                    lblgraduatestudFee.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblgraduatestudFee.Text = "Not Available";

                if (!string.IsNullOrWhiteSpace(lblbookstudFee.Text))
                {
                    tmp = lblbookstudFee.Text.ToString().Split('-');
                    lblbookstudFee.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblbookstudFee.Text = "Not Available";

                if (!string.IsNullOrWhiteSpace(lblboardstudFee.Text))
                {
                    tmp = lblboardstudFee.Text.ToString().Split('-');
                    lblboardstudFee.Text = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                else
                    lblboardstudFee.Text = "Not Available";


                if (username != null)
                {
                    StudentInterest studentintrest = StudentInterestBLL.GetByuniversityName(username.Value);
                    if (studentintrest != null)
                    {
                        if (studentintrest.Approved == (int)StudentInterestApproved.Applied)
                        {
                            btndecline.Visible = true;
                            lblapproved.Text = " you have Applied For This college";
                        }
                        else if (studentintrest.Approved == (int)StudentInterestApproved.Approved)
                        {
                            btndecline.Visible = true;
                            lblapproved.Text = "You have Approved For This college";
                        }
                        else
                        {
                            btnapply.Visible = true;
                        }
                    }
                }

            }
        }

        protected void btndecline_Click(object sender, EventArgs e)
        {
            try
            {
                string universityname = ((Button)sender).CommandArgument.ToString();
                UserBLL userBll = new UserBLL();
                SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);

                StudentInterestBLL StudentInterestBLL = new StudentInterestBLL();
                StudentInterest StudentInterest = new StudentInterest();
                StudentInterest = StudentInterestDAL.Get(x => x.UniversityUserName == universityname && x.StudentUserName == user.UserName);
                StudentInterest.Approved = (int)StudentInterestApproved.Decline;
                StudentInterestBLL.Save(StudentInterest);
                //string Message = "Decline Succesfully";
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message.ToString() + "');", true);
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message.ToString() + "');", true);
            }

        }

        protected void btnapply_Click(object sender, EventArgs e)
        {

        }

        [WebMethod()]
        public static string[] GetUnivercityData(string UniversityName)
        {
            //string str=string.Empty;
            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            Student std = new Student();
            StudentBLL studentBll = new StudentBLL();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            StringBuilder sb = new StringBuilder();
            string[] strArr = new string[15];
            string[] tmp;
            string[] tmpCourseArr;

            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                std = studentBll.GetByUserName(CookieHelper.Username);


                List<StudentTest> stdTest = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();

                if (stdTest.Count == 0)
                {
                    //return str = "Please input atleast one test scores in order to contact universities";
                    university = null;
                    return null;
                }
                else
                {

                    university = universityBll.GetByUniversityName(UniversityName);

                    if (university.Address != null)
                        strArr[0] = university.Address;
                    if (university.City != null)
                        strArr[1] = university.City;
                    if (university.Country != null)
                        strArr[2] = university.Country;
                    if (university.Graduates != null)
                        strArr[3] = university.Graduates.ToString();
                    if (university.UnderGraduates != null)
                        strArr[4] = university.UnderGraduates.ToString();
                    if (university.InternationalGraduate != null)
                        strArr[5] = university.InternationalGraduate.ToString();
                    if (university.Image != null)
                        strArr[6] = university.Image;
                    if (university.Description != null)
                        strArr[7] = university.Description;
                    if (university.UnderGraduateFee != null)
                    {
                        tmp = university.UnderGraduateFee.ToString().Split('-');
                        strArr[8] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                    }
                    if (university.GraduateFee != null)
                    {
                        tmp = university.GraduateFee.ToString().Split('-');
                        strArr[9] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                    }
                    if (university.BookFee != null)
                    {
                        tmp = university.BookFee.ToString().Split('-');
                        strArr[10] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                    }
                    if (university.BoardFee != null)
                    {
                        tmp = university.BoardFee.ToString().Split('-');
                        strArr[11] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                    }
                    if (university.Degree != null)
                        strArr[12] = university.Degree;
                    if (university.Courses != null)
                    {
                        tmpCourseArr= university.Courses.ToString().Split(',');

                        sb.Append("<ul class='list_7 listli_50per'>");
                        for(int i=0;i<tmpCourseArr.Length;i++)
                        {
                            sb.Append("<li>"+tmpCourseArr[i]+"</li>");
                        }
                        sb.Append("</ul>");
                        strArr[13] =sb.ToString();
                    }

                    StudentInterest stduser = studentInterestBLL.GetAll().Where(x => x.StudentUserName == CookieHelper.Username && x.UniversityUserName == university.UserName).FirstOrDefault();
                    if (stduser != null)
                        strArr[14] = "hideStdInt";
                    else
                        strArr[14] = "ShowStdInt";

                    return strArr;
                    // return JsonConvert.SerializeObject(university);
                }
            }
            else
            {
                university = universityBll.GetByUniversityName(UniversityName);
                if (university.Address != null)
                    strArr[0] = university.Address;
                if (university.City != null)
                    strArr[1] = university.City;
                if (university.Country != null)
                    strArr[2] = university.Country;
                if (university.Graduates != null)
                    strArr[3] = university.Graduates.ToString();
                if (university.UnderGraduates != null)
                    strArr[4] = university.UnderGraduates.ToString();
                if (university.InternationalGraduate != null)
                    strArr[5] = university.InternationalGraduate.ToString();
                if (university.Image != null)
                    strArr[6] = university.Image;
                if (university.Description != null)
                    strArr[7] = university.Description;
                if (university.UnderGraduateFee != null)
                {
                    tmp = university.UnderGraduateFee.ToString().Split('-');
                    strArr[8] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                if (university.GraduateFee != null)
                {
                    tmp = university.GraduateFee.ToString().Split('-');
                    strArr[9] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                if (university.BookFee != null)
                {
                    tmp = university.BookFee.ToString().Split('-');
                    strArr[10] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                if (university.BoardFee != null)
                {
                    tmp = university.BoardFee.ToString().Split('-');
                    strArr[11] = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                }
                if (university.Degree != null)
                    strArr[12] = university.Degree;
                if (university.Courses != null)
                {
                    tmpCourseArr = university.Courses.ToString().Split(',');

                    sb.Append("<ul class='list_7 listli_50per'>");
                    for (int i = 0; i < tmpCourseArr.Length; i++)
                    {
                        sb.Append("<li>" + tmpCourseArr[i] + "</li>");
                    }
                    sb.Append("</ul>");
                    strArr[13] = sb.ToString();
                }
                return strArr;
                //return JsonConvert.SerializeObject(university);
            }


        }

        //for message
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
            messageBLL.Save(msg);

            if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                AlertBLL alertBLL = new AlertBLL();
                UniversityBLL universityBLL = new UniversityBLL();
                string universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
                Alert alert = new Alert();
                alert.Message = universityname + " has Send Message";
                alert.CreatedDate = DateTime.Now;
                alert.CreatedBy = CookieHelper.Username;
                alert.UserName = sendToUserName;
                alertBLL.Save(alert);
            }

            return str;
        }

        [WebMethod()]
        public static string Getstatus(string ToUserName)
        {
            Message msg = new Message();
            MessageBLL messageBLL = new MessageBLL();
            //msg = messageBLL.GetAll().Where(x => x.ToUserName == ToUserName || x.FromUserName == CookieHelper.Username || x.ToUserName == CookieHelper.Username || x.FromUserName == ToUserName && x.ParentId == 0).FirstOrDefault();
            msg = messageBLL.GetAll().Where(x => x.ToUserName == ToUserName && x.FromUserName == CookieHelper.Username || x.ToUserName == CookieHelper.Username && x.FromUserName == ToUserName && x.ParentId == 0).FirstOrDefault();
            if (msg == null)
                return string.Empty;
            else
                return msg.MessageId.ToString();
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST
        [WebMethod()]
        public static string AppendAndDisplayUniversity()
        {
            StringBuilder sb = new StringBuilder();
            string[] tmp;
            List<University> tmpUniversityList = new List<University>();
            if (StaticUniversityList != null)
            {
                if (StaticUniversityList.Count != 0)
                {
                    if (StaticUniversityList.Count < 6)
                    {
                        tmpUniversityList = StaticUniversityList.ToList();
                        StaticUniversityList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpUniversityList = StaticUniversityList.GetRange(0, 6);
                        StaticUniversityList.RemoveRange(0, 6);
                    }

                    string img;
                    string undgradFee;
                    string gradFee;
                    string BookFee;
                    string BoardFee;
                    string InternationalGraduate;
                    string Graduates;
                    string UnderGraduates;
                    for (int i = 0; i < tmpUniversityList.Count; i++)
                    {
                        if (tmpUniversityList[i].Image != null)
                            img = @"Images\Profile\" + tmpUniversityList[i].Image;
                        else
                            img = @"\Images\no_image.jpg";

                        if (tmpUniversityList[i].UnderGraduateFee != null)
                        {
                            tmp = tmpUniversityList[i].UnderGraduateFee.ToString().Split('-');
                            undgradFee = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                        }
                        else
                            undgradFee = "Not Available";

                        if (tmpUniversityList[i].GraduateFee != null)
                        {
                            tmp = tmpUniversityList[i].UnderGraduateFee.ToString().Split('-');
                            gradFee = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                        }
                        else
                            gradFee = "Not Available";

                        if (tmpUniversityList[i].BookFee != null)
                        {
                            tmp = tmpUniversityList[i].UnderGraduateFee.ToString().Split('-');
                            BookFee = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                        }
                        else
                            BookFee = "Not Available";

                        if (tmpUniversityList[i].BoardFee != null)
                        {
                            tmp = tmpUniversityList[i].UnderGraduateFee.ToString().Split('-');
                            BoardFee = tmp[0] + '-' + tmp[1] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[2])) + ' ' + tmp[3];
                        }
                        else
                            BoardFee = "Not Available";

                        if (tmpUniversityList[i].Graduates != null)
                            Graduates = tmpUniversityList[i].Graduates.ToString();
                        else
                            Graduates = "Not Available";
                        if (tmpUniversityList[i].UnderGraduates != null)
                            UnderGraduates = tmpUniversityList[i].UnderGraduates.ToString();
                        else
                            UnderGraduates = "Not Available";
                        if (tmpUniversityList[i].InternationalGraduate != null)
                            InternationalGraduate = tmpUniversityList[i].InternationalGraduate.ToString();
                        else
                            InternationalGraduate = "Not Available";

                        sb.Append("<br/><span><li><div class='img_padding'> <img src='" + img + "' /> </div>");
                        sb.Append(" <div class='university_detail'><div class='name'><span>" + tmpUniversityList[i].UniversityName + "</span></div>");
                        sb.Append("<div class='establishment'>In " + tmpUniversityList[i].City + ", " + tmpUniversityList[i].Country + ". &nbsp;&nbsp; Year of establishment  " + tmpUniversityList[i].EstablishedYear + " </div>");
                        sb.Append("<ul class='small_university_box'><li><div class='h2_heading'><h2>Cost for International students :</h2></div>");
                        sb.Append("</li><li>&raquo; Tutition for Undergraduate students : " + undgradFee + "	</li><li>&raquo; Tuition for graduate studens : " + gradFee + "</li><li>&raquo; Books and supplies : " + BookFee + "</li><li>&raquo; Off-campus room and Board : " + BoardFee + "</li></ul>");
                        sb.Append("<div class='width100per'></div>");
                        sb.Append("<ul class='small_university_box'><li><div class='h2_heading'><h2>Enrollment Numbers</h2> </div></li>");
                        sb.Append("<li>&raquo; Number of Graduate students : " + Graduates + "</li><li>&raquo;  Number of Undergraduate Students : " + UnderGraduates + "</li>");
                        sb.Append("<li>&raquo;  Number of International students : " + InternationalGraduate + "</li></ul><div class='moreinformation'>");
                        sb.Append("<div class='width100per'></div>");

                        sb.Append("<div class='moreinformation'><a id='" + tmpUniversityList[i].UniversityName + "' class=\"anUniversityDetail button fright\" onclick=\"javascript: Openpopup('" + tmpUniversityList[i].UniversityName + "'); \" >More Information</a></div></div> </li></span>");
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
