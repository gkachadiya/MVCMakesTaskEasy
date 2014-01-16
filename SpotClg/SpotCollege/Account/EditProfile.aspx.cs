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

namespace SpotCollege.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
        UserBLL userBll = new UserBLL();
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();
        StudentTestBLL studentTestBLL = new StudentTestBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtPrimaryEmail.Text = CookieHelper.Username;
                txtPrimaryEmail.Visible = false;
                initPage();
                bindBasicDropDown();
                bindAcademicDropdown();
                bindEducationPreferenceDropdown();
                bindworkDropdown();
                loadPageDetail();
                bindAcademicGrid();
                bindWorkHistoryGrid();

            }
            bindACTList();
            bindSATList();
            bindAPList();
            bindGREList();
            bindGMATList();
            bindIBList();
            bindIELTSList();
            bindLSATList();
            bindMCATList();
            bindPSATList();
            bindSAT2List();
            bindTOEFLInternetbasedList();
            bindTOEFLPaperbasedList();
        }

        void initPage()
        {
            Student student = studentBll.GetByUserName(CookieHelper.Username);
            if (student == null)
            {
                lnkCurrentAcademics.Enabled = false;
                // lblcurrentacademic.Visible = false;
                lnkWorkhistory.Enabled = false;
                // lblworkhistory.Visible = false;
            }
            else
            {
                lnkCurrentAcademics.Enabled = true;
                lnkWorkhistory.Enabled = true;
                // lblcurrentacademic.Visible = true;
                //lblworkhistory.Visible = true;
            }

            string sec = Request.QueryString["sec"];
            if (sec == "basic")
            {
                pnlBasicDetail.Visible = true;
                hndCurrentTab.Value = "1";
            }
            else if (sec == "Academics")
            {
                pnlCurrentAcademics.Visible = true;
                hndCurrentTab.Value = "2";
            }
            else if (sec == "EducationPreferences")
            {
                Student stud = studentBll.GetByUserName(CookieHelper.Username);
                StudentAcademic stdAca = new StudentAcademic();
                if (stud != null)
                {
                    stdAca = academicBll.GetAll().Where(x => x.StudentId == stud.StudentId).FirstOrDefault();
                }
                if (stdAca != null)
                {
                    pnlEducationPreferences.Visible = true;
                    hndCurrentTab.Value = "4";
                }
                else
                {
                    pnlCurrentAcademics.Visible = true;
                    hndCurrentTab.Value = "2";
                }

            }
            else if (sec == "Photo")
            {
                pnlProfileImage.Visible = true;
                hndCurrentTab.Value = "5";
            }
            else if (sec == "Workhistory")
            {
                pnlWorkhistory.Visible = true;
                hndCurrentTab.Value = "6";
            }
            else if (sec == "Curricular")
            {
                pnlCurricularActivies.Visible = true;
                hndCurrentTab.Value = "7";
            }
            else if (sec == "IntTest")
            {
                pnlInternationalTest.Visible = true;
                hndCurrentTab.Value = "3";
            }
            else
            {
                Student stud = studentBll.GetByUserName(CookieHelper.Username);
                StudentAcademic stdAca = new StudentAcademic();
                if (stud != null)
                {
                    stdAca = academicBll.GetAll().Where(x => x.StudentId == stud.StudentId).FirstOrDefault();
                    if (stdAca == null)
                    {
                        pnlCurrentAcademics.Visible = true;
                        hndCurrentTab.Value = "2";
                    }
                    else
                    {
                        pnlBasicDetail.Visible = true;
                        hndCurrentTab.Value = "1";
                    }
                }
                else
                {
                    pnlBasicDetail.Visible = true;
                    hndCurrentTab.Value = "1";
                }
            }
        }

        void loadPageDetail()
        {
            Student student = studentBll.GetByUserName(CookieHelper.Username);
            if (student != null)
            {
                //Load Basic Detail
                try
                {
                    lbltitle.Text = "Hi ["+ student.FirstName + " " + student.LastName+"]" ;

                    hndStudentId.Value = student.StudentId.ToString();
                    txtFirstName.Text = student.FirstName;
                    txtMiddleName.Text = student.MiddleName;
                    txtLastName.Text = student.LastName;
                    txtAddress1.Text = student.Address1;
                    txtAddress2.Text = student.Address2;
                    txtzipcode.Text = student.ZipCode;
                    txtCity.Text = student.City;
                    if (student.Country != "")
                    {

                        int val = (int)(EnumHelper.GetEnumValueFromDescription<CountryList>(student.Country));
                        if (val != 0)
                        {
                            ddlCountry.SelectedValue = val.ToString();
                        }
                        else
                        {
                            ddlCountry.SelectedValue = ((int)Enum.Parse(typeof(CountryList), student.Country)).ToString();
                        }
                    }
                    string number = student.PrimaryNumber;
                    string[] strArr = number.Split('-');
                    ddlCountryCode.SelectedValue = strArr[0].ToString();
                    txtAreaCode.Text = strArr[1];
                    txtPrimaryNumber.Text = strArr[2];
                    if (student.PrimaryType != "")
                        ddlPhoneType.Text = student.PrimaryType;
                    if (student.SecondaryNumber != null)
                    {
                        string number2 = student.SecondaryNumber;
                        string[] strArr2 = number2.Split('-');
                        ddlCountryCode2.SelectedValue = strArr2[0].ToString();
                        txtAreaCode2.Text = strArr2[1];
                        txtSecondaryNumber.Text = strArr2[2];
                        if (student.SecondaryType != "")
                        {
                            ddlSecondaryPhoneType.Text = student.SecondaryType;
                        }
                    }
                    txtPrimaryEmail.Text = student.PrimaryEmail;


                    txtBirthDate.Text = Convert.ToDateTime(student.BirthDate).ToString("MM/dd/yyyy");

                    DateTime dt = Convert.ToDateTime(student.BirthDate);

                    int birthday = Convert.ToInt32(dt.Day);
                    int birthmonth = Convert.ToInt32(dt.Month);
                    int birthyear = Convert.ToInt32(dt.Year);

                    ddlday.SelectedValue = birthday.ToString();
                    ddlmonth.SelectedValue = birthmonth.ToString();
                    ddlyear.SelectedValue = birthyear.ToString();

                    if (student.Citizenship != "")
                    {
                        int val1 = (int)(EnumHelper.GetEnumValueFromDescription<CountryList>(student.Citizenship));
                        if (val1 != 0)
                        {
                            ddlCountryOfCitizenship.SelectedValue = val1.ToString();
                        }
                        else
                        {
                            ddlCountryOfCitizenship.SelectedValue = ((int)Enum.Parse(typeof(CountryList), student.Citizenship)).ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //Load EducationPreference
                ddlCurrentlyIn.SelectedValue = student.StudyingIn.ToString();
                ddlLookingFor.SelectedValue = student.LookingFor.ToString();
                if (student.LookingForCountry != "")
                {


                    int preferedval = (int)(EnumHelper.GetEnumValueFromDescription<PreferedStudyIn>(student.LookingForCountry));
                    if (preferedval != 0)
                    {
                        ddlLookingForCountry.SelectedValue = preferedval.ToString();
                    }
                    else
                    {
                        ddlLookingForCountry.SelectedValue = ((int)Enum.Parse(typeof(PreferedStudyIn), student.LookingForCountry)).ToString();
                    }
                    if (student.LookingForCountry == "Other")
                    {
                        txtotherstudy.Text = student.OtherStudy;
                    }

                }
                ddlStartDate.SelectedValue = student.StartDate.ToString();
                ddlPayout.SelectedValue = student.Payout.ToString();
                if (student.DesiredFieldofStudy != null && student.DesiredFieldofStudy!="")
                {
                    int preferedval = (int)(EnumHelper.GetEnumValueFromDescription<CoursesOffered>(student.DesiredFieldofStudy));
                    if (preferedval != 0)
                    {
                        ddldesirefieldofstudy.SelectedValue = preferedval.ToString();
                    }
                    else
                    {
                        ddldesirefieldofstudy.SelectedValue = ((int)Enum.Parse(typeof(CoursesOffered), student.DesiredFieldofStudy)).ToString();
                    }
                }


                //Load Profile Image
                if (student.Photo != null)
                {
                    imgProfileImage.Visible = true;
                    imgProfileImage.Src = "../Images/Profile/" + student.Photo;
                    hoverimage.HRef = "../Images/Profile/" + student.Photo;
                }

                //Load Extra Curricular Activies
                txtSports.Text = student.SportActivities;
                txtLeadership.Text = student.LeadershipActivies;
                txtOtherActivities.Text = student.OtherActivities;
            }
        }

        void bindBasicDropDown()
        {
            //Bind Primary Phone Tye Dropdown
            ddlPhoneType.Items.Add(new ListItem("Select One", "0"));
            ddlPhoneType.SelectedValue = "0";
            string[] PhoneTypenames = Enum.GetNames(typeof(PhoneTypes));
            Array value = Enum.GetValues(typeof(PhoneTypes));
            int[] Values = (int[])value;
            for (int i = 0; i < PhoneTypenames.Length; i++)
            {
                ListItem item = new ListItem(PhoneTypenames[i], Values[i].ToString());
                ddlPhoneType.Items.Add(item);
            }

            //Bind CuntryCode List
            ddlCountryCode.Items.Add(new ListItem("Select Country Code", "0"));
            ddlCountryCode.SelectedValue = "0";
            string[] CountryCodenames = Enum.GetNames(typeof(CountryCode));
            Array value1 = Enum.GetValues(typeof(CountryCode));
            int[] Values1 = (int[])value1;
            for (int i = 0; i < CountryCodenames.Length; i++)
            {
                ListItem item = new ListItem(CountryCodenames[i] + " [+" + Values1[i] + "]", Values1[i].ToString());
                ddlCountryCode.Items.Add(item);
            }
            ReorderAlphabetized(ddlCountryCode);

            //Bind CuntryCode List
            ddlCountryCode2.Items.Add(new ListItem("Select Country Code", "0"));
            ddlCountryCode2.SelectedValue = "0";
            string[] CountryCodenames2 = Enum.GetNames(typeof(CountryCode));
            Array value2 = Enum.GetValues(typeof(CountryCode));
            int[] Values2 = (int[])value2;
            for (int i = 0; i < CountryCodenames2.Length; i++)
            {
                ListItem item = new ListItem(CountryCodenames2[i] + " [+" + Values2[i] + "]", Values2[i].ToString());
                ddlCountryCode2.Items.Add(item);
            }
            ReorderAlphabetized(ddlCountryCode2);

            //Bind Secondary Phone Tye Dropdown
            ddlSecondaryPhoneType.Items.Add(new ListItem("Select One", "0"));
            ddlSecondaryPhoneType.SelectedValue = "0";
            for (int i = 0; i < PhoneTypenames.Length; i++)
            {
                ListItem item = new ListItem(PhoneTypenames[i], Values[i].ToString());
                ddlSecondaryPhoneType.Items.Add(item);
            }


            //Bind Country Dropdown            					

            ddlCountry.Items.Add(new ListItem("Select Country", "0"));
            ddlCountry.SelectedValue = "0";
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));
            int[] CountryValues = (int[])Countryvalue;
            for (int i = 0; i < Countrynames.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]), CountryValues[i].ToString());
                ddlCountry.Items.Add(item);
            }

            //Bind Country of Citizenship Dropdown
            ddlCountryOfCitizenship.Items.Add(new ListItem("Select Country", "0"));
            ddlCountryOfCitizenship.SelectedValue = "0";
            for (int i = 0; i < Countrynames.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]), CountryValues[i].ToString());
                ddlCountryOfCitizenship.Items.Add(item);
            }


            //Bind Dropdown Date

            //Date
            ddlday.Items.Add(new ListItem("Select Date", "0"));
            ddlday.SelectedValue = "0";
            for (int i = 1; i < 32; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlday.Items.Add(item);
            }

            //year
            ddlyear.Items.Add(new ListItem("Select Year", "0"));
            ddlyear.SelectedValue = "0";

            for (int i = 1950; i < 2021; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlyear.Items.Add(item);
            }

        }

        public static void ReorderAlphabetized(DropDownList ddl)
        {
            List<ListItem> listCopy = new List<ListItem>();
            foreach (ListItem item in ddl.Items)
                listCopy.Add(item);
            ddl.Items.Clear();
            foreach (ListItem item in listCopy.OrderBy(item => item.Text))
                ddl.Items.Add(item);
        }

        void bindEducationPreferenceDropdown()
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            //Bind Currently In Dropdown          

            names = Enum.GetNames(typeof(CurrentlyIn));
            value = Enum.GetValues(typeof(CurrentlyIn));
            Values = (int[])value;

            ddlCurrentlyIn.Items.Add(new ListItem("Select Any", "0"));
            ddlCurrentlyIn.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CurrentlyIn)Values[i]), Values[i].ToString());
                ddlCurrentlyIn.Items.Add(item);
            }

            //Bind LookingFor In Dropdown
            names = Enum.GetNames(typeof(ProgramLookingFor));
            value = Enum.GetValues(typeof(ProgramLookingFor));
            Values = (int[])value;

            ddlLookingFor.Items.Add(new ListItem("Select Any", "0"));
            ddlLookingFor.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)Values[i]), Values[i].ToString());
                ddlLookingFor.Items.Add(item);
            }

            //Bind LookingForCountry In Dropdown
            names = Enum.GetNames(typeof(PreferedStudyIn));
            value = Enum.GetValues(typeof(PreferedStudyIn));
            Values = (int[])value;

            ddlLookingForCountry.Items.Add(new ListItem("Select Any", "0"));
            ddlLookingForCountry.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((PreferedStudyIn)Values[i]), Values[i].ToString());
                ddlLookingForCountry.Items.Add(item);
            }


            //Bind Expected Joining In Dropdown
            names = Enum.GetNames(typeof(expectedStart));
            value = Enum.GetValues(typeof(expectedStart));
            Values = (int[])value;

            ddlStartDate.Items.Add(new ListItem("Select Any", "0"));
            ddlStartDate.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((expectedStart)Values[i]), Values[i].ToString());
                ddlStartDate.Items.Add(item);
            }


            //Bind EffortPerYear In Dropdown
            ddlPayout.Items.Add(new ListItem("Select Any", "0"));
            ddlPayout.Items.Add(new ListItem("$3000-5000", "1"));
            ddlPayout.Items.Add(new ListItem("$5000-10000", "2"));
            ddlPayout.Items.Add(new ListItem("$10000-15000", "3"));
            ddlPayout.Items.Add(new ListItem("$15000+", "4"));

            ddlPayout.SelectedValue = "0";

            //Bind Desire Field Of Study
            names = Enum.GetNames(typeof(CoursesOffered));
            value = Enum.GetValues(typeof(CoursesOffered));
            Values = (int[])value;

            ddldesirefieldofstudy.Items.Add(new ListItem("Select Any", "0"));
            ddldesirefieldofstudy.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Values[i]), Values[i].ToString());
                ddldesirefieldofstudy.Items.Add(item);
            }

            //ddldesirefieldofstudy.Items.Add(new ListItem("Select Any Course", "0"));
            //ddldesirefieldofstudy.SelectedValue = "0";
            //for (int i = 0; i < names.Length; i++)
            //{
            //    ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Values[i]), Values[i].ToString());
            //    ddldesirefieldofstudy.Items.Add(item);
            //}


        }

        void bindAcademicDropdown()
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            //Bind SchoolCountry In Dropdown
            names = Enum.GetNames(typeof(CountryList));
            value = Enum.GetValues(typeof(CountryList));
            Values = (int[])value;

            ddlSchoolCountry.Items.Add(new ListItem("Select Any", "0"));
            ddlSchoolCountry.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CountryList)Values[i]), Values[i].ToString());
                ddlSchoolCountry.Items.Add(item);
            }

            //Bind GraduateStatus In RadioButtonList
            names = Enum.GetNames(typeof(GraduateStatus));
            value = Enum.GetValues(typeof(GraduateStatus));
            Values = (int[])value;

            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((GraduateStatus)Values[i]), Values[i].ToString());
                rdoDidYouGraduate.Items.Add(item);
            }


            //Bind YearOfGraduation  In Dropdown
            ddlYearOfGraduation.Items.Add(new ListItem("Select Any", "0"));
            ddlYearOfGraduation.SelectedValue = "0";
            for (int i = 1985; i < DateTime.Now.Year; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlYearOfGraduation.Items.Add(item);
            }


            //Bind LevelCompleted In Dropdown
            names = Enum.GetNames(typeof(LevelCompleted));
            value = Enum.GetValues(typeof(LevelCompleted));
            Values = (int[])value;

            ddlLevelCompleted.Items.Add(new ListItem("Select Any", "0"));
            ddlLevelCompleted.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((LevelCompleted)Values[i]), Values[i].ToString());
                ddlLevelCompleted.Items.Add(item);
            }

            //Bind Degree being pursued Detail Ongoing
            names = Enum.GetNames(typeof(Degreepursued));
            value = Enum.GetValues(typeof(Degreepursued));
            Values = (int[])value;

            ddldegreepusued.Items.Add(new ListItem("Select Any", "0"));
            ddldegreepusued.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((Degreepursued)Values[i]), Values[i].ToString());
                ddldegreepusued.Items.Add(item);
            }


            //Bind YearOfGraduation Ongoing
            ddlexpectedgraduation.Items.Add(new ListItem("Select Any", "0"));
            ddlexpectedgraduation.SelectedValue = "0";
            for (int i = 2013; i < 2020; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlexpectedgraduation.Items.Add(item);
            }


        }

        void bindworkDropdown()
        {
            //Bind Dropdown Date

            //Date
            ddlstartday.Items.Add(new ListItem("Select Date", "0"));
            ddlstartday.SelectedValue = "0";
            for (int i = 1; i < 32; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlstartday.Items.Add(item);
            }

            //year
            ddlstartyear.Items.Add(new ListItem("Select Year", "0"));
            ddlstartyear.SelectedValue = "0";

            for (int i = 1950; i < 2021; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlstartyear.Items.Add(item);
            }


            //BindEnddate
            ddlendday.Items.Add(new ListItem("Select Date", "0"));
            ddlendday.SelectedValue = "0";
            for (int i = 1; i < 32; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlendday.Items.Add(item);
            }

            //year
            ddlendyear.Items.Add(new ListItem("Select Year", "0"));
            ddlendyear.SelectedValue = "0";

            for (int i = 1950; i < 2021; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlendyear.Items.Add(item);
            }


        }

        void bindAcademicGrid()
        {
            int StudentId = Convert.ToInt32(hndStudentId.Value);
            if (StudentId != 0)
            {
                List<StudentAcademic> AcademicList = academicBll.GetAll().Where(x => x.StudentId == StudentId).ToList();
                grvAcademicDetails.DataSource = AcademicList;
                grvAcademicDetails.DataBind();
            }
        }

        void bindWorkHistoryGrid()
        {
            int StudentId = Convert.ToInt32(hndStudentId.Value);
            if (StudentId != 0)
            {
                List<StudentWorkHistory> WorkHistoryList = workHistoryBll.GetAll().Where(x => x.StudentId == StudentId).ToList();
                grvWorkHistory.DataSource = WorkHistoryList;
                grvWorkHistory.DataBind();
            }
        }

        // For Binding International Test Details

        
        void bindACTList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 2).ToList();
                dlACT.DataSource = ACTlist;
                dlACT.DataBind();
            }
        }
        void bindSATList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 3).ToList();
                dlSAT.DataSource = ACTlist;
                dlSAT.DataBind();
            }
        }
        void bindAPList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 4).ToList();
                dlAP.DataSource = ACTlist;
                dlAP.DataBind();
            }
        }
        void bindGREList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 5).ToList();
                dlGRE.DataSource = ACTlist;
                dlGRE.DataBind();
            }
        }
        void bindGMATList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 6).ToList();
                dlGMAT.DataSource = ACTlist;
                dlGMAT.DataBind();
            }
        }
        void bindIBList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 7).ToList();
                dlIB.DataSource = ACTlist;
                dlIB.DataBind();
            }
        }
        void bindIELTSList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 8).ToList();
                dlIELTS.DataSource = ACTlist;
                dlIELTS.DataBind();
            }
        }
        void bindLSATList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 9).ToList();
                dlLSAT.DataSource = ACTlist;
                dlLSAT.DataBind();
            }
        }
        void bindMCATList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 10).ToList();
                dlMCAT.DataSource = ACTlist;
                dlMCAT.DataBind();
            }
        }
        void bindPSATList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 11).ToList();
                dlPSAT.DataSource = ACTlist;
                dlPSAT.DataBind();
            }
        }
        void bindSAT2List()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 12).ToList();
                dlSAT2.DataSource = ACTlist;
                dlSAT2.DataBind();
            }
        }
        void bindTOEFLInternetbasedList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 13).ToList();
                dlTOEFLInternetbased.DataSource = ACTlist;
                dlTOEFLInternetbased.DataBind();
            }
        }
        void bindTOEFLPaperbasedList()
        {
            Student std = studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                List<StudentTest> ACTlist = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId && x.TestId == 14).ToList();
                dlTOEFLPaperbased.DataSource = ACTlist;
                dlTOEFLPaperbased.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlPhoneType.SelectedValue != "0" && ddlCountryOfCitizenship.SelectedValue != "0" && ddlCountry.SelectedValue != "0")
            {
                SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
                if (user != null)
                {
                    Student student = studentBll.GetByUserName(user.UserName);
                    if (student == null)
                    {
                        student = new Student();

                        student.FirstName = txtFirstName.Text;
                        student.MiddleName = txtMiddleName.Text;
                        student.LastName = txtLastName.Text;
                        student.Address1 = txtAddress1.Text;
                        student.Address2 = txtAddress2.Text;
                        student.ZipCode = txtzipcode.Text;
                        student.City = txtCity.Text;
                        student.Country = ddlCountry.SelectedItem.Text;
                        student.PrimaryNumber = ddlCountryCode.SelectedValue + "-" + txtAreaCode.Text + "-" + txtPrimaryNumber.Text;
                        student.PrimaryType = ddlPhoneType.Text;
                        if (txtSecondaryNumber.Text != "")
                        {
                            student.SecondaryNumber = ddlCountryCode2.SelectedValue + "-" + txtAreaCode2.Text + "-" + txtSecondaryNumber.Text;
                            if (ddlSecondaryPhoneType.SelectedValue != "0")
                            {
                                student.SecondaryType = ddlSecondaryPhoneType.Text;
                            }
                        }

                        student.PrimaryEmail = txtPrimaryEmail.Text;
                        DateTime birthdate = new DateTime(Convert.ToInt16(ddlyear.SelectedItem.Value), Convert.ToInt16(ddlmonth.SelectedItem.Value), Convert.ToInt16(ddlday.SelectedItem.Value));

                        student.BirthDate = birthdate;
                        student.Citizenship = ddlCountryOfCitizenship.SelectedItem.Text;
                        student.UserName = CookieHelper.Username;

                        //Temporary Values to set
                        student.StudyingIn = 0;
                        student.LookingFor = 0;
                        student.LookingForCountry = "";
                        student.StartDate = 0;
                        student.Payout = 0;
                        student.CreatedDate = DateTime.Now;
                        student.DesiredFieldofStudy = string.Empty;

                        studentBll.Save(student);
                        pnlCurrentAcademics.Visible = true;
                        pnlBasicDetail.Visible = false;
                        lnkCurrentAcademics.Enabled = true;
                        lnkWorkhistory.Enabled = true;
                        lblcurrentacademic.Visible = true;
                        lblworkhistory.Visible = true;
                        hndCurrentTab.Value = "2";
                        hndStudentId.Value = Convert.ToString(student.StudentId);
                    }
                    else
                    {
                        student.FirstName = txtFirstName.Text;
                        student.MiddleName = txtMiddleName.Text;
                        student.LastName = txtLastName.Text;
                        student.Address1 = txtAddress1.Text;
                        student.Address2 = txtAddress2.Text;
                        student.ZipCode = txtzipcode.Text;
                        student.City = txtCity.Text;
                        student.Country = ddlCountry.SelectedItem.Text;
                        student.PrimaryNumber = ddlCountryCode.SelectedValue + "-" + txtAreaCode.Text + "-" + txtPrimaryNumber.Text;
                        student.PrimaryType = ddlPhoneType.Text;
                        if (txtSecondaryNumber.Text != "")
                        {
                            student.SecondaryNumber = ddlCountryCode2.SelectedValue + "-" + txtAreaCode2.Text + "-" + txtSecondaryNumber.Text;
                            if (ddlSecondaryPhoneType.SelectedValue != "0")
                            {
                                student.SecondaryType = ddlSecondaryPhoneType.Text;
                            }
                        }

                        student.PrimaryEmail = txtPrimaryEmail.Text;

                        DateTime dt = new DateTime(Convert.ToInt16(ddlyear.SelectedItem.Value), Convert.ToInt16(ddlmonth.SelectedItem.Value), Convert.ToInt16(ddlday.SelectedItem.Value));
                        student.BirthDate = dt;
                        student.Citizenship = ddlCountryOfCitizenship.SelectedItem.Text;

                        Label lbl = (Label)Master.FindControl("lblUsername");
                        if (lbl != null)
                        {
                            lbl.Text = student.FirstName + " " + student.LastName;
                        }


                        studentBll.Save(student);

                        pnlCurrentAcademics.Visible = true;
                        pnlBasicDetail.Visible = false;
                        lnkCurrentAcademics.Enabled = true;
                        lnkWorkhistory.Enabled = true;
                        lblcurrentacademic.Visible = true;
                        lblworkhistory.Visible = true;
                        hndCurrentTab.Value = "2";
                    }
                }
            }
        }

        protected void btnSaveEduPreference_Click(object sender, EventArgs e)
        {

            if (ddlCurrentlyIn.SelectedValue != "0" && ddlLookingFor.SelectedValue != "0" && ddlLookingForCountry.SelectedValue != "0" && ddlStartDate.SelectedValue != "0" && ddlPayout.SelectedValue != "0")
            {
                Student student = studentBll.GetByUserName(CookieHelper.Username);

                if (student == null)
                {
                    student = new Student();

                    student.UserName = CookieHelper.Username;
                    student.StudyingIn = Convert.ToInt32(ddlCurrentlyIn.SelectedValue);
                    student.LookingFor = Convert.ToInt32(ddlLookingFor.SelectedValue);
                    if (student.LookingForCountry != "Other")
                    {
                        student.LookingForCountry = ddlLookingForCountry.SelectedItem.Text;
                    }
                    else
                    {
                        student.LookingForCountry = ddlLookingForCountry.SelectedItem.Text;
                        student.OtherStudy = txtotherstudy.Text;
                    }
                    student.Payout = Convert.ToInt32(ddlPayout.SelectedValue);
                    student.StartDate = Convert.ToInt32(ddlStartDate.SelectedValue);
                    student.DesiredFieldofStudy = ddldesirefieldofstudy.SelectedItem.Text;

                    //Dummy Entries for OtherFields
                    student.FirstName = "";
                    student.MiddleName = "";
                    student.LastName = "";
                    student.Photo = "";
                    student.Address1 = "";
                    student.City = "";
                    student.Country = "";
                    student.PrimaryNumber = "";
                    student.PrimaryType = "";
                    student.PrimaryEmail = "";
                    student.Citizenship = "";
                    studentBll.Save(student);
                }

                else
                {
                    student.StudyingIn = Convert.ToInt32(ddlCurrentlyIn.SelectedValue);
                    student.LookingFor = Convert.ToInt32(ddlLookingFor.SelectedValue);
                    if (student.LookingForCountry != "Other")
                    {
                        student.LookingForCountry = ddlLookingForCountry.SelectedItem.Text;
                    }
                    else
                    {
                        student.LookingForCountry = ddlLookingForCountry.SelectedItem.Text;
                        student.OtherStudy = txtotherstudy.Text;
                    }
                    student.Payout = Convert.ToInt32(ddlPayout.SelectedValue);
                    student.StartDate = Convert.ToInt32(ddlStartDate.SelectedValue);
                    student.DesiredFieldofStudy = ddldesirefieldofstudy.SelectedItem.Text;
                    studentBll.Save(student);
                }
                pnlProfileImage.Visible = true;
                pnlEducationPreferences.Visible = false;
                lnkCurrentAcademics.Enabled = true;
                lnkWorkhistory.Enabled = true;
                lblcurrentacademic.Visible = true;
                lblworkhistory.Visible = true;
                hndCurrentTab.Value = "5";



            }
        }

        protected void btnSubmitAcademics_Click(object sender, EventArgs e)
        {
            Student student = studentBll.GetByUserName(CookieHelper.Username);
            StudentAcademic stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();
            if (stdAca != null)
            {
                pnlCurrentAcademics.Visible = false;
                pnlInternationalTest.Visible = true;
                hndCurrentTab.Value = "3";
                lblAcademicMsg.Text = "";
            }
            else
            {
                pnlCurrentAcademics.Visible = true;
                hndCurrentTab.Value = "2";
                lblAcademicMsg.Text = "Please Enter Atleast ONE Current Academics Detail";
            }

        }

        protected void btnSaveAcademics_Click(object sender, EventArgs e)
        {
            //rqdcertificate.Enabled = true;
            if (ddlSchoolCountry.SelectedValue != "0" && rdoDidYouGraduate.SelectedValue != "")
            {
                try
                {
                    string docPath = String.Empty;
                    if (fuCertificate.HasFile)
                    {
                        docPath = Guid.NewGuid().ToString() + Path.GetExtension(fuCertificate.FileName);
                        fuCertificate.SaveAs(Server.MapPath("\\Images\\Certificate\\") + docPath);
                    }
                    else
                    {
                        docPath = imgCertificate.Src.Substring(imgCertificate.Src.LastIndexOf(("/")) + 1);
                    }
                    StudentAcademic academic = null;
                    if (hndStudentAcademicsId.Value != "0")
                    {
                        academic = academicBll.Get(Convert.ToInt32(hndStudentAcademicsId.Value));
                    }
                    else
                    {
                        academic = new StudentAcademic();
                    }
                    academic.SchoolName = txtSchoolName.Text;
                    academic.SchoolCity = txtSchoolCity.Text;
                    academic.SchoolCountry = ddlSchoolCountry.SelectedItem.Text;
                    academic.Graduate = Convert.ToInt32(rdoDidYouGraduate.SelectedValue);
                    if (academic.Graduate == (int)GraduateStatus.Yes)
                    {
                        if (ddlYearOfGraduation.SelectedValue != "0")
                            academic.GraduationYear = Convert.ToInt32(ddlYearOfGraduation.SelectedValue);
                        if (ddlLevelCompleted.SelectedValue != "0")
                            academic.GraduationLevel = Convert.ToInt32(ddlLevelCompleted.SelectedValue);
                    }
                    else
                    {
                        academic.GraduationYear = null;
                        academic.GraduationLevel = null;
                    }
                    if (academic.Graduate == (int)GraduateStatus.OnGoing)
                    {
                        if (ddldegreepusued.SelectedValue != "0")
                            academic.DegreebeingPursued = Convert.ToInt32(ddldegreepusued.SelectedValue);
                        if (ddlexpectedgraduation.SelectedValue != "0")
                            academic.ExpectedYearofGraduation = Convert.ToInt32(ddlexpectedgraduation.SelectedValue);
                        academic.FieldofStudy = txtfieldstudy.Text;

                    }
                    else
                    {
                        academic.DegreebeingPursued = null;
                        academic.ExpectedYearofGraduation = null;
                        academic.FieldofStudy = null;
                    }

                    if (txtRankingInClass.Text != null && txtRankingInClass.Text != "")
                    {
                        academic.Rank = Convert.ToDecimal(txtRankingInClass.Text);
                    }
                    else
                    {
                        academic.Rank = null;
                    }
                    academic.CertificatePath = docPath;
                    academic.StudentId = Convert.ToInt32(hndStudentId.Value);
                    hndCurrentTab.Value = "2";
                    academicBll.Save(academic);

                    if (string.IsNullOrEmpty(docPath))
                    {
                        #region comment
                        //Alert alert = new Alert();
                        //StudentBLL studentBLL = new StudentBLL();

                        //alert.CreatedBy = CookieHelper.Username;
                        //alert.CreatedDate = DateTime.Now;
                        //alert.Message = "In order to contact universities, please upload image of passing/degree certificate from " + txtSchoolName.Text + " insititute";

                        //AlertBLL alertBLL = new AlertBLL();
                        //alertBLL.Save(alert);
                        #endregion
                    }

                    bindAcademicGrid();
                    clearAcademics();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        void clearAcademics()
        {
            txtSchoolName.Text = "";
            txtSchoolCity.Text = "";
            ddlSchoolCountry.SelectedValue = "0";
            txtRankingInClass.Text = "";
            ddlYearOfGraduation.SelectedValue = "0";
            ddlLevelCompleted.SelectedValue = "0";
            imgCertificate.Src = "";
            imgCertificate.Visible = false;
            hndStudentAcademicsId.Value = "0";
            lblAcademicMsg.Text = "";
        }

        protected void lnkSchoolName_Click(object sender, EventArgs e)
        {
            //rqdcertificate.Enabled = false;
            LinkButton linkbtn = (LinkButton)sender;
            int StudentAcademicsId = Convert.ToInt32(linkbtn.CommandArgument);
            hndStudentAcademicsId.Value = StudentAcademicsId.ToString();
            StudentAcademic academic = academicBll.Get(StudentAcademicsId);
            if (academic != null)
            {
                txtSchoolName.Text = academic.SchoolName;
                txtSchoolCity.Text = academic.SchoolCity;
                int val = (int)(EnumHelper.GetEnumValueFromDescription<CountryList>(academic.SchoolCountry));
                if (val != 0)
                {
                    ddlSchoolCountry.SelectedValue = val.ToString();
                }
                else
                {
                    ddlSchoolCountry.SelectedValue = ((int)Enum.Parse(typeof(CountryList), academic.SchoolCountry)).ToString();
                }
                rdoDidYouGraduate.SelectedValue = academic.Graduate.ToString();
                if (academic.Graduate == (int)GraduateStatus.Yes)
                {
                    pnlGraduateDetail.Visible = true;
                    if (academic.GraduationYear != null)
                    {
                        ddlYearOfGraduation.Text = academic.GraduationYear.ToString();
                    }
                    if (academic.GraduationLevel != null)
                    {
                        ddlLevelCompleted.SelectedValue = academic.GraduationLevel.ToString();
                    }
                }
                if (academic.Graduate == (int)GraduateStatus.OnGoing)
                {
                    pnlongoing.Visible = true;
                    if (academic.DegreebeingPursued != null)
                    {
                        ddldegreepusued.Text = academic.DegreebeingPursued.ToString();
                    }
                    if (academic.ExpectedYearofGraduation != null)
                    {
                        ddlexpectedgraduation.SelectedValue = academic.ExpectedYearofGraduation.ToString();
                    }
                    txtfieldstudy.Text = academic.FieldofStudy;
                }
                txtRankingInClass.Text = academic.Rank.ToString();
                imgCertificate.Visible = true;
                imgCertificate.Src = "../Images/Certificate/" + academic.CertificatePath;
                hovercertificate.HRef = "../Images/Certificate/" + academic.CertificatePath;
            }
        }

        protected void lnkDeleteAcademic_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = (LinkButton)sender;
            int StudentAcademicsId = Convert.ToInt32(linkbtn.CommandArgument);
            StudentAcademic academic = academicBll.Get(StudentAcademicsId);
            if (academic != null)
            {
                academicBll.delete(StudentAcademicsId);
            }

            bindAcademicGrid();

        }
        [WebMethod()]
        public static object AcademicDelete(string StudentAcademicId)
        {
            string str = string.Empty;
            StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
            academicBll.delete(Convert.ToInt32(StudentAcademicId));
            return JsonConvert.SerializeObject(str);
        }

        protected void btnSubmitImage_Click(object sender, EventArgs e)
        {
            if (fuProfileImage.HasFile)
            {
                string ImagePath = Guid.NewGuid().ToString() + Path.GetExtension(fuProfileImage.FileName);
                fuProfileImage.SaveAs(Server.MapPath("\\Images\\Profile\\") + ImagePath);

                Student student = studentBll.GetByUserName(CookieHelper.Username);
                if (student == null)
                {
                    student = new Student();

                    student.UserName = CookieHelper.Username;
                    student.Photo = ImagePath;

                    //Null Entries for other fields
                    student.FirstName = "";
                    student.MiddleName = "";
                    student.LastName = "";
                    student.Photo = "";
                    student.Address1 = "";
                    student.City = "";
                    student.Country = "";
                    student.PrimaryNumber = "";
                    student.PrimaryType = "";
                    student.PrimaryEmail = "";
                    student.Citizenship = "";
                    student.StudyingIn = 0;
                    student.LookingFor = 0;
                    student.LookingForCountry = "";
                    student.StartDate = 0;
                    student.Payout = 0;


                    studentBll.Save(student);
                }
                else
                {
                    student.Photo = ImagePath;
                    studentBll.Save(student);
                }

                hoverimage.Visible = true;
                hoverimage.HRef = "../Images/Profile/" + ImagePath;
                imgProfileImage.Visible = true;
                imgProfileImage.Src = "../Images/Profile/" + ImagePath;

                pnlWorkhistory.Visible = true;
                pnlProfileImage.Visible = false;
                lnkCurrentAcademics.Enabled = true;
                lnkWorkhistory.Enabled = true;
                lblcurrentacademic.Visible = true;
                lblworkhistory.Visible = true;
                hndCurrentTab.Value = "6";
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            pnlWorkhistory.Visible = true;
            pnlProfileImage.Visible = false;
            hndCurrentTab.Value = "6";
        }

        protected void btnSubmitActivities_Click(object sender, EventArgs e)
        {
            Student student = studentBll.GetByUserName(CookieHelper.Username);
            if (student == null)
            {
                student = new Student();
                student.SportActivities = txtSports.Text;
                student.LeadershipActivies = txtLeadership.Text;
                student.OtherActivities = txtOtherActivities.Text;
                student.UserName = CookieHelper.Username;


                //Null Entries for other fields
                student.FirstName = "";
                student.MiddleName = "";
                student.LastName = "";
                student.Photo = "";
                student.Address1 = "";
                student.City = "";
                student.Country = "";
                student.PrimaryNumber = "";
                student.PrimaryType = "";
                student.PrimaryEmail = "";
                student.Citizenship = "";
                student.StudyingIn = 0;
                student.LookingFor = 0;
                student.LookingForCountry = "";
                student.StartDate = 0;
                student.Payout = 0;


                studentBll.Save(student);
            }
            else
            {
                student.SportActivities = txtSports.Text;
                student.LeadershipActivies = txtLeadership.Text;
                student.OtherActivities = txtOtherActivities.Text;

                studentBll.Save(student);
            }
            hndCurrentTab.Value = "7";
            Response.Redirect("ProfileOverView.aspx");
        }

        protected void btnSaveWorkHistory_Click(object sender, EventArgs e)
        {
            SaveWorkHistory();
            hndCurrentTab.Value = "6";
        }

        protected void btnSaveAndContinueWorkHistory_Click(object sender, EventArgs e)
        {
            // SaveWorkHistory();

            pnlWorkhistory.Visible = false;
            pnlCurricularActivies.Visible = true;
            hndCurrentTab.Value = "7";
        }

        void SaveWorkHistory()
        {

            StudentWorkHistory workHistory = null;
            if (hndStudentWorkHistoryId.Value != "0")
            {
                workHistory = workHistoryBll.Get(Convert.ToInt32(hndStudentWorkHistoryId.Value));
            }
            else
            {
                workHistory = new StudentWorkHistory();
            }

            workHistory.StudentId = Convert.ToInt32(hndStudentId.Value);
            workHistory.CompanyName = txtCompanyName.Text;
            workHistory.Position = txtWorkPosition.Text;
            string Startdate = ddlstartmonth.SelectedItem.Value + "/" + ddlstartday.SelectedItem.Value + "/" + ddlstartyear.SelectedItem.Value;
            string Enddate = ddlendmonth.SelectedItem.Value + "/" + ddlendday.SelectedItem.Value + "/" + ddlendyear.SelectedItem.Value;

            workHistory.StartDate = Convert.ToDateTime(Startdate);
            workHistory.EndDate = Convert.ToDateTime(Enddate);
            workHistory.Responsibilities = txtResposibilities.Text;

            workHistoryBll.Save(workHistory);
            clearWorkHistory();
            bindWorkHistoryGrid();
        }

        void clearWorkHistory()
        {
            txtCompanyName.Text = "";
            txtWorkPosition.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtResposibilities.Text = "";
            hndStudentWorkHistoryId.Value = "0";
            ddlstartday.SelectedValue = "0";
            ddlstartmonth.SelectedValue = "0";
            ddlstartyear.SelectedValue = "0";
            ddlendday.SelectedValue = "0";
            ddlstartmonth.SelectedValue = "0";
            ddlstartyear.SelectedValue = "0";
        }

        protected void lnkCompanyName_Click(object sender, EventArgs e)
        {
            LinkButton lnkCompanyname = (LinkButton)sender;
            int StudentWorkHistoryId = Convert.ToInt32(lnkCompanyname.CommandArgument);
            StudentWorkHistory workHistory = workHistoryBll.Get(StudentWorkHistoryId);
            if (workHistory != null)
            {
                txtCompanyName.Text = workHistory.CompanyName;
                txtWorkPosition.Text = workHistory.Position;

                //Start Date
                txtStartDate.Text = workHistory.StartDate.ToString("MM/dd/yyyy");
                string[] arr = txtStartDate.Text.Split('/');
                int startday = Convert.ToInt32(arr[1]);
                int startmonth = Convert.ToInt32(arr[0]);
                int startyear = Convert.ToInt32(arr[2]);
                ddlstartday.SelectedValue = startday.ToString();
                ddlstartmonth.SelectedValue = startmonth.ToString();
                ddlstartyear.SelectedValue = startyear.ToString();

                //End Date
                txtEndDate.Text = workHistory.EndDate.ToString("MM/dd/yyyy");
                string[] arr1 = txtEndDate.Text.Split('/');
                int endday = Convert.ToInt32(arr1[1]);
                int endmonth = Convert.ToInt32(arr1[0]);
                int endyear = Convert.ToInt32(arr1[2]);

                ddlendday.SelectedValue = endday.ToString();
                ddlendmonth.SelectedValue = endmonth.ToString();
                ddlendyear.SelectedValue = endyear.ToString();

                txtResposibilities.Text = workHistory.Responsibilities;
                hndStudentWorkHistoryId.Value = workHistory.StudentWorkHistoryId.ToString();
            }
        }

        protected void lnkDeleteWorkHistory_Click(object sender, EventArgs e)
        {
            LinkButton lnkDeleteWorkHistory = (LinkButton)sender;
            int StudentWorkHistoryId = Convert.ToInt32(lnkDeleteWorkHistory.CommandArgument);
            StudentWorkHistory workHistory = workHistoryBll.Get(StudentWorkHistoryId);
            if (workHistory != null)
            {
                workHistoryBll.delete(StudentWorkHistoryId);
            }
            bindWorkHistoryGrid();
            clearWorkHistory();
        }

        #region Manage tabs
        protected void lnkBasicInformation_Click(object sender, EventArgs e)
        {
            pnlBasicDetail.Visible = true;

            pnlCurrentAcademics.Visible = false;
            pnlEducationPreferences.Visible = false;
            pnlProfileImage.Visible = false;
            pnlWorkhistory.Visible = false;
            pnlCurricularActivies.Visible = false;
            pnlInternationalTest.Visible = false;
            hndCurrentTab.Value = "1";
        }

        protected void lnkCurrentAcademics_Click(object sender, EventArgs e)
        {
            pnlCurrentAcademics.Visible = true;

            pnlBasicDetail.Visible = false;
            pnlEducationPreferences.Visible = false;
            pnlProfileImage.Visible = false;
            pnlWorkhistory.Visible = false;
            pnlCurricularActivies.Visible = false;
            pnlInternationalTest.Visible = false;
            hndCurrentTab.Value = "2";
        }

        protected void lnkEducationalPreferences_Click(object sender, EventArgs e)
        {
            pnlEducationPreferences.Visible = true;

            pnlBasicDetail.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlProfileImage.Visible = false;
            pnlWorkhistory.Visible = false;
            pnlCurricularActivies.Visible = false;
            pnlInternationalTest.Visible = false;
            hndCurrentTab.Value = "4";
        }

        protected void lnkPhoto_Click(object sender, EventArgs e)
        {
            pnlProfileImage.Visible = true;

            pnlBasicDetail.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEducationPreferences.Visible = false;
            pnlWorkhistory.Visible = false;
            pnlCurricularActivies.Visible = false;
            pnlInternationalTest.Visible = false;
            hndCurrentTab.Value = "5";
        }

        protected void lnkWorkhistory_Click(object sender, EventArgs e)
        {
            pnlWorkhistory.Visible = true;

            pnlBasicDetail.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEducationPreferences.Visible = false;
            pnlProfileImage.Visible = false;
            pnlCurricularActivies.Visible = false;
            pnlInternationalTest.Visible = false;
            hndCurrentTab.Value = "6";
        }

        protected void lnkExtraCurricularActivities_Click(object sender, EventArgs e)
        {
            pnlCurricularActivies.Visible = true;

            pnlBasicDetail.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEducationPreferences.Visible = false;
            pnlProfileImage.Visible = false;
            pnlWorkhistory.Visible = false;
            pnlInternationalTest.Visible = false;
            hndCurrentTab.Value = "7";
        }

        protected void lnkInternationalTest_Click(object sender, EventArgs e)
        {
            // Response.Redirect("~/InternationalTest.aspx");
            pnlInternationalTest.Visible = true;

            pnlBasicDetail.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEducationPreferences.Visible = false;
            pnlProfileImage.Visible = false;
            pnlWorkhistory.Visible = false;
            pnlCurricularActivies.Visible = false;
            hndCurrentTab.Value = "3";
        }
        #endregion


        [WebMethod()]
        public static object HighSchoolDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(str);
        }


        [WebMethod()]
        public static String[] ACTSave(string TestId, string Composite, string English, string Math, string Reading, string Science, string Writing, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Composite = Composite;
                stdTest.English = English;
                stdTest.Math = Math;
                stdTest.Reading = Reading;
                stdTest.Science = Science;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 2).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Composite = Composite;
                stdTest.English = English;
                stdTest.Math = Math;
                stdTest.Reading = Reading;
                stdTest.Science = Science;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static object GetACT(string StudentTestId)
        {
            StudentTest schoolACT = new StudentTest();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            schoolACT = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(schoolACT);
        }

        [WebMethod()]
        public static object ACTDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(str);
        }

        [WebMethod()]
        public static String[] SATSave(string TestId, string Reading, string Math, string Writing, string Composite, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Composite = Composite;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 3).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Composite = Composite;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static object GetSAT(string StudentTestId)
        {
            StudentTest schoolSAT = new StudentTest();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            schoolSAT = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(schoolSAT);
        }

        [WebMethod()]
        public static object SATDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(str);
        }

        [WebMethod()]
        public static String[] APSave(string TestId, string Subject, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 4).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static object GetAP(string StudentTestId)
        {
            StudentTest schoolAP = new StudentTest();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            schoolAP = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(schoolAP);
        }

        [WebMethod()]
        public static object APDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(str);
        }

        [WebMethod()]
        public static String[] GRESave(string TestId, string VerbalReasoning, string QuantitativeReasoning, string AnalyticalWriting, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.VerbalReasoning = VerbalReasoning;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 5).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.VerbalReasoning = VerbalReasoning;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }


        [WebMethod()]
        public static String[] GMATSave(string TestId, string Verbal, string QuantitativeReasoning, string Total, string AnalyticalWriting, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Verbal = Verbal;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.Total = Total;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 6).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Verbal = Verbal;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.Total = Total;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] IBSave(string TestId, string Subject, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 7).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] IELTSSave(string TestId, string Listening, string Reading, string Writing, string Speaking, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Listening = Listening;
                stdTest.Reading = Reading;
                stdTest.Writing = Writing;
                stdTest.Speaking = Speaking;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 8).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Listening = Listening;
                stdTest.Reading = Reading;
                stdTest.Writing = Writing;
                stdTest.Speaking = Speaking;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] LSATSave(string TestId, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 9).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] MCATSave(string TestId, string Biology, string Physics, string Verbal, string Writing, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Biology = Biology;
                stdTest.Physics = Physics;
                stdTest.Verbal = Verbal;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 10).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Biology = Biology;
                stdTest.Physics = Physics;
                stdTest.Verbal = Verbal;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);


                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] PSATSave(string TestId, string Reading, string Math, string Writing, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 11).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] SAT2Save(string TestId, string Subject, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 12).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] TOEFLInternetbasedSave(string TestId, string Reading, string Listening, string Speaking, string Writing, string Total, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 13).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);

            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        [WebMethod()]
        public static String[] TOEFLPaperbasedSave(string TestId, string Reading, string Listening, string Speaking, string Writing, string Total, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 14).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return (arr);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return (arr);
            }
        }

        protected void btnIntTestContinue_Click(object sender, EventArgs e)
        {
            pnlInternationalTest.Visible = false;
            pnlEducationPreferences.Visible = true;
            hndCurrentTab.Value = "4";
        }
    }
}