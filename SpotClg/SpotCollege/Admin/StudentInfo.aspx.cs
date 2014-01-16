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
using System.Data;
using System.Text;
using System.Linq.Expressions;

namespace SpotCollege.Admin
{
    public partial class StudentInfo : System.Web.UI.Page
    {
        public static List<Student> staticStudentList = new List<Student>();

        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();
        UserBLL userBll = new UserBLL();
        StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
        MessageBLL messageBLL = new MessageBLL();
        AlertBLL alertBLL = new AlertBLL();
        StudentTestBLL studentTestBll = new StudentTestBLL();
        List<Student> finalStudentSearch = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindBasicStudentInfoGrid();
                bindBasicDropDown();
                bindAcademicDropdown();
                bindEducationPreferenceDropdown();
            }
        }

        void BindBasicStudentInfoGrid()
        {
            List<Student> studentlist = studentBll.GetAll().OrderByDescending(x => x.StudentId).ToList();

            //staticStudentList = studentlist.ToList();
            //if (studentlist.Count < 10)
            //    staticStudentList.RemoveAll(x => 1 == 1);
            //else
            //    staticStudentList.RemoveRange(0, 10);
            lblTotalResult.Text = studentlist.Count.ToString();

            GrvBasicStudentInfo.DataSource = studentlist;
            GrvBasicStudentInfo.DataBind();

            GrvBasicStudentInfo2.DataSource = studentlist;
            GrvBasicStudentInfo2.DataBind();

            StringBuilder builder = new StringBuilder();
            foreach (string value in studentlist.Select(x => x.UserName).ToArray())
            {
                builder.Append(value);
                builder.Append(',');
            }
            hdnAllRecipients.Value = builder.ToString();

            if (chkAll.Checked)
            {
                for (int i = 0; i < GrvBasicStudentInfo.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GrvBasicStudentInfo.Rows[i].FindControl("chkSelect");
                    CheckBox chk2 = (CheckBox)GrvBasicStudentInfo2.Rows[i].FindControl("chkSelect");
                    chk.Checked = true;
                    chk2.Checked = true;
                }
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
                ListItem item = new ListItem(Countrynames[i], CountryValues[i].ToString());
                ddlCountry.Items.Add(item);
            }

            //Bind Country Dropdown
            dllStudentCountry.Items.Add(new ListItem("Select Country", "0"));
            dllStudentCountry.SelectedValue = "0";

            for (int i = 0; i < Countrynames.Length; i++)
            {
                ListItem item = new ListItem(Countrynames[i], CountryValues[i].ToString());
                dllStudentCountry.Items.Add(item);
            }

            string[] names = null;


            //Bind Country of Citizenship Dropdown
            ddlCountryOfCitizenship.Items.Add(new ListItem("Select Country", "0"));
            ddlCountryOfCitizenship.SelectedValue = "0";
            for (int i = 0; i < Countrynames.Length; i++)
            {
                ListItem item = new ListItem(Countrynames[i], CountryValues[i].ToString());
                ddlCountryOfCitizenship.Items.Add(item);
            }

            //Bind Country  Dropdown

            ddlDesiredCountry.Items.Add(new ListItem("Select Country", "0"));
            ddlDesiredCountry.SelectedValue = "0";
            for (int i = 0; i < Countrynames.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]), CountryValues[i].ToString());
                ddlDesiredCountry.Items.Add(item);
            }


            //Bind Desired Terms of starting
            names = Enum.GetNames(typeof(expectedStart));
            value = Enum.GetValues(typeof(expectedStart));
            Values = (int[])value;

            ddlTerms.Items.Add(new ListItem("Select Any", "0"));
            ddlTerms.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((expectedStart)Values[i]), Values[i].ToString());
                ddlTerms.Items.Add(item);
            }

            //Bind Level of study

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


            //Bind Desired Program We support
            names = Enum.GetNames(typeof(CoursesOffered));
            value = Enum.GetValues(typeof(CoursesOffered));
            Values = (int[])value;

            ddlPrograms.Items.Add(new ListItem("Select Any", "0"));
            ddlPrograms.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Values[i]), Values[i].ToString());
                ddlPrograms.Items.Add(item);
            }

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
                ListItem item = new ListItem(names[i], Values[i].ToString());
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
                ListItem item = new ListItem(names[i], Values[i].ToString());
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
                ListItem item = new ListItem(names[i], Values[i].ToString());
                ddlLookingForCountry.Items.Add(item);
            }


            //Bind LookingForCountry In Dropdown
            names = Enum.GetNames(typeof(expectedStart));
            value = Enum.GetValues(typeof(expectedStart));
            Values = (int[])value;

            ddlStartDate.Items.Add(new ListItem("Select Any", "0"));
            ddlStartDate.SelectedValue = "0";
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(names[i], Values[i].ToString());
                ddlStartDate.Items.Add(item);
            }


            //Bind EffortPerYear In Dropdown
            ddlPayout.Items.Add(new ListItem("Select Any", "0"));
            ddlPayout.Items.Add(new ListItem("3000-5000", "1"));
            ddlPayout.Items.Add(new ListItem("5000-10000", "2"));
            ddlPayout.Items.Add(new ListItem("10000-15000", "3"));
            ddlPayout.Items.Add(new ListItem("15000+)", "4"));

            ddlPayout.SelectedValue = "0";


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
                ListItem item = new ListItem(names[i], Values[i].ToString());
                ddlSchoolCountry.Items.Add(item);
            }

            //Bind GraduateStatus In RadioButtonList
            names = Enum.GetNames(typeof(GraduateStatus));
            value = Enum.GetValues(typeof(GraduateStatus));
            Values = (int[])value;

            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(names[i], Values[i].ToString());
                rdoDidYouGraduate.Items.Add(item);
            }


            //Bind YearOfGraduation  In Dropdown
            ddlYearOfGraduation.Items.Add(new ListItem("Select Any", "0"));
            ddlYearOfGraduation.SelectedValue = "0";
            for (int i = 1999; i < 2013; i++)
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
                ListItem item = new ListItem(names[i], Values[i].ToString());
                ddlLevelCompleted.Items.Add(item);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            finalStudentSearch = null;
            List<Student> studentList = new List<Student>();
            studentList = studentBll.GetAll();

            if (chkActive.Checked || chnNotActive.Checked)
            {
                string[] activeUserList = userBll.GetAll().Where(x => x.LoginTypeId == 1 && (x.IsActive == chkActive.Checked || x.IsActive == !chnNotActive.Checked)).Select(y => y.UserName).ToArray();
                finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => activeUserList.Contains(x.UserName)).ToList());
            }

            if (chkIntScore.Checked)
            {
                int[] tmp = studentTestBll.GetAll().Select(x => x.StudentId).Distinct().ToArray();
                finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => !tmp.Contains(x.StudentId)).Distinct().ToList());
            }

            if (chnEduInfo.Checked)
            {
                int[] tmp = academicBll.GetAll().Select(x => x.StudentId).Distinct().ToArray();
                finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => !tmp.Contains(x.StudentId)).Distinct().ToList());
            }

            if (chkCertiImg.Checked)
            {
                int[] cerificate = academicBll.GetAll().Where(x => string.IsNullOrEmpty(x.CertificatePath) && x.Graduate == 1).Select(y => y.StudentId).Distinct().ToArray();
                finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => cerificate.Contains(x.StudentId)).ToList());
            }

            if (chkSelfImg.Checked)
            {
                finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => string.IsNullOrEmpty(x.Photo)).ToList());
            }

            if (chkEduPref.Checked)
            {
                finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => string.IsNullOrEmpty(x.LookingForCountry)).ToList());
            }
            msgErr.Text = "";
            if (txtFromMonth.Text != "" && txtFromYear.Text != "" && txtToMonth.Text != "" && txtToYear.Text != "")
            {
                try
                {
                    DateTime todate = new DateTime(Convert.ToInt16(txtToYear.Text), Convert.ToInt16(txtToMonth.Text), 1);
                    DateTime fromdate = new DateTime(Convert.ToInt16(txtFromYear.Text), Convert.ToInt16(txtFromMonth.Text), 1);

                    finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => x.CreatedDate <= todate && x.CreatedDate >= fromdate).ToList());
                }
                catch (Exception ex)
                {
                    msgErr.Text = "please enter correct date";
                    return;
                }
            }
            if (txtFromMonth.Text != "" && txtFromYear.Text != "" && txtToMonth.Text == "" && txtToYear.Text == "")
            {
                try
                {
                    DateTime fromdate = new DateTime(Convert.ToInt16(txtFromYear.Text), Convert.ToInt16(txtFromMonth.Text), 1);
                    finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => x.CreatedDate >= fromdate).ToList());
                }
                catch (Exception ex)
                {
                    msgErr.Text = "please enter correct date";
                    return;
                }
            }
            if (txtFromMonth.Text == "" && txtFromYear.Text == "" && txtToMonth.Text != "" && txtToYear.Text != "")
            {
                try
                {
                    DateTime todate = new DateTime(Convert.ToInt16(txtToYear.Text), Convert.ToInt16(txtToMonth.Text), 1);
                    finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => x.CreatedDate <= todate).ToList());
                }
                catch (Exception ex)
                {
                    msgErr.Text = "please enter correct date";
                    return;
                }
            }

            msgErr.Text = "";


            string country = "", lookingforcountry = "", programs = "", lookingfor = "", term = "";
            if (dllStudentCountry.SelectedIndex != 0)
                country = dllStudentCountry.SelectedItem.Text;

            if (ddlDesiredCountry.SelectedIndex != 0)
                lookingforcountry = ddlDesiredCountry.SelectedItem.Text;

            if (ddlPrograms.SelectedIndex != 0)
                programs = ddlPrograms.SelectedItem.Text;

            if (ddlTerms.SelectedIndex != 0)
                term = ddlTerms.SelectedValue;

            if (ddlSearchByLookingFor.SelectedIndex != 0)
                lookingfor = ddlSearchByLookingFor.SelectedValue;


            //x.DesiredFieldofStudy!=null &&
            List<Student> tmpStudents = studentBll.GetAll()
                                             .Where(x =>
                                                 x.Country.Contains(country) &&
                                                         x.LookingForCountry.Contains(lookingforcountry) &&
                                                         x.DesiredFieldofStudy.Contains(programs) &&
                                                         x.LookingFor.ToString().Contains(lookingfor) &&
                                                         x.StartDate.ToString().Contains(term)).ToList();

            if (finalStudentSearch != null)
                finalStudentSearch = getCommonElements(finalStudentSearch, tmpStudents);
            else
                finalStudentSearch = tmpStudents;





            string[] strarr = finalStudentSearch.Select(x => x.UserName).ToArray();

            StringBuilder builder = new StringBuilder();
            foreach (string value in strarr)
            {
                builder.Append(value);
                builder.Append(',');
            }
            hdnAllRecipients.Value = builder.ToString();
            lblTotalResult.Text = finalStudentSearch.Count.ToString();
            GrvBasicStudentInfo.DataSource = finalStudentSearch;
            GrvBasicStudentInfo.DataBind();
        }

        private List<Student> getCommonElements(List<Student> list1, List<Student> list2)
        {
            List<Student> tmplist = new List<Student>();
            if (list1 != null)
                tmplist = list1.Where(x => list2.Select(y => y.UserName).ToArray().Contains(x.UserName)).ToList();
            else
                tmplist = list2;
            return tmplist;
        }


        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = true;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            txtEmail.Enabled = false;
            lblMsg.Text = "";


            string username = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                Response.Redirect("AddNewStudent.aspx?uname=" + username);


                Student student = studentBll.GetByUserName(username);
                if (student != null)
                {
                    hndStudentId.Value = student.StudentId.ToString();
                    hndUserName.Value = student.UserName.ToString();
                    txtEmail.Text = student.UserName;
                    txtFirstName.Text = student.FirstName;
                    txtMiddleName.Text = student.MiddleName;
                    txtLastName.Text = student.LastName;
                    txtAddress1.Text = student.Address1;
                    txtAddress2.Text = student.Address2;
                    txtCity.Text = student.City;
                    if (student.Country != "")
                        ddlCountry.SelectedValue = ((int)Enum.Parse(typeof(CountryList), student.Country)).ToString();
                    txtPrimaryNumber.Text = student.PrimaryNumber;
                    if (student.PrimaryType != "")
                        ddlPhoneType.Text = student.PrimaryType;
                    txtSecondaryNumber.Text = student.SecondaryNumber;
                    if (student.SecondaryType != "")
                    {
                        ddlSecondaryPhoneType.Text = student.SecondaryType;
                    }
                    txtPrimaryEmail.Text = student.PrimaryEmail;
                    txtBirthDate.Text = Convert.ToDateTime(student.BirthDate).ToString("MM/dd/yyyy");
                    if (student.Citizenship != "")
                        //ddlCountryOfCitizenship.Text = student.Citizenship;
                        ddlCountryOfCitizenship.SelectedValue = ((int)Enum.Parse(typeof(CountryList), student.Citizenship)).ToString();



                    //Load EducationPreference
                    ddlCurrentlyIn.SelectedValue = student.StudyingIn.ToString();
                    ddlLookingFor.SelectedValue = student.LookingFor.ToString();
                    if (student.LookingForCountry != "")
                        ddlLookingForCountry.SelectedValue = ((int)Enum.Parse(typeof(PreferedStudyIn), student.LookingForCountry)).ToString();
                    ddlStartDate.SelectedValue = student.StartDate.ToString();
                    ddlPayout.SelectedValue = student.Payout.ToString();


                    //Load Profile Image
                    if (student.Photo != null)
                    {
                        imgProfileImage.Visible = true;
                        imgProfileImage.Src = "../Images/Profile/" + student.Photo;
                    }


                    //Load Extra Curricular Activies
                    txtSports.Text = student.SportActivities;
                    txtLeadership.Text = student.LeadershipActivies;
                    txtOtherActivities.Text = student.OtherActivities;

                    //load password
                    //SpotCollege.DAL.User user = userBll.Get(username);
                    txtPassword.Attributes.Add("value", student.User.Password);
                    txtConfirmPassword.Attributes.Add("value", student.User.Password);

                }
            }
        }

        //protected void lnkBtnViewMore_Click(object sender, EventArgs e)
        //{
        //    pnlBasicStudentInfo.Visible = false;
        //    pnlEditBasicStudentInfo.Visible = false;
        //    pnlCurrentAcademics.Visible = false;
        //    pnlEditWorkHistory.Visible = false;
        //    pnlStudentFullDetails.Visible = true;
        //    pnlAcademicsDetail.Visible = false;
        //    pnlWorkHistory.Visible = false;
        //    lblMsg.Text = "";


        //    string username = ((LinkButton)sender).CommandArgument.ToString();
        //    if (!string.IsNullOrEmpty(username))
        //    {
        //        Student student = studentBll.GetByUserName(username);
        //        if (student != null)
        //        {
        //            //Load Basic Detail

        //            lblFirstName.Text = "First Name : " + student.FirstName;
        //            lblMiddleName.Text = "Middle Name : " + student.MiddleName;
        //            lblLastName.Text = "Last Name : " + student.LastName;
        //            lblAddress1.Text = "Address1 : " + student.Address1;
        //            lblAddress2.Text = "Address2 : " + student.Address2;
        //            lblCity.Text = "City : " + student.City;
        //            lblCountry.Text = "Country : " + Enum.Parse(typeof(CountryList), student.Country).ToString();
        //            lblPrimaryNumber.Text = "Primary Number : " + student.PrimaryNumber + " Type :" + Enum.Parse(typeof(PhoneTypes), student.PrimaryType).ToString();
        //            if (student.SecondaryType != null)
        //                lblSecondaryNumber.Text = "Secondary Number : " + student.SecondaryNumber + " Type : " + Enum.Parse(typeof(PhoneTypes), student.SecondaryType).ToString();
        //            else
        //                lblSecondaryNumber.Text = "Secondary Number : " + student.SecondaryNumber + " Type : ";
        //            lblPrimaryEmail.Text = "Primary Email : " + student.PrimaryEmail;
        //            lblBirthDate.Text = "Birth Date : " + Convert.ToDateTime(student.BirthDate).ToString("MM/dd/yyyy");
        //            lblCountryOfCitizenship.Text = "Country of Citizenship : " + Enum.Parse(typeof(CountryList), student.Citizenship).ToString();

        //        }
        //    }
        //}

        protected void lnkBtnAcademicsDetails_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = true;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";
            ReqFldValImage.Enabled = false;


            string studentId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(studentId))
            {
                lblStudentName.Text = "Student ID : " + studentBll.Get(Convert.ToInt32(studentId)).UserName.ToString();
                hndStudentId.Value = studentId.ToString();
                List<StudentAcademic> AcademicList = academicBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                grvAcademicDetails.DataSource = AcademicList;
                grvAcademicDetails.DataBind();
            }
        }

        protected void lnkBtnWorkHistory_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = true;
            lblMsg.Text = "";


            string studentId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(studentId))
            {
                lblstdname.Text = "Student ID : " + studentBll.Get(Convert.ToInt32(studentId)).UserName.ToString();
                hndStudentId.Value = studentId.ToString();
                List<StudentWorkHistory> WorkHistoryList = workHistoryBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                grvWorkHistory.DataSource = WorkHistoryList;
                grvWorkHistory.DataBind();
            }
        }

        [WebMethod()]
        public static object StudentDelete(string StudentId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            AlertBLL alertBLL = new AlertBLL();
            UserBLL userBll = new UserBLL();
            StudentTestBLL studentTestbll = new StudentTestBLL();
            StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
            StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();
            StudentBLL studentBll = new StudentBLL();

            if (!string.IsNullOrEmpty(StudentId))
            {

                List<StudentTest> studentTest = studentTestbll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                foreach (var li in studentTest)
                {
                    studentTestbll.delete(li.StudentTestId);
                }

                List<StudentAcademic> studentAcademics = academicBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                foreach (var li in studentAcademics)
                {
                    academicBll.delete(li.StudentAcademicsId);
                }
                List<StudentWorkHistory> StudentWorkhistory = workHistoryBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                foreach (var lis in StudentWorkhistory)
                {
                    workHistoryBll.delete(lis.StudentWorkHistoryId);
                }

                Student student = studentBll.Get(Convert.ToInt32(StudentId));
                string usernamedel = student.UserName;

                List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.StudentUserName == usernamedel).ToList();
                foreach (var lia in studentInterest)
                {
                    studentInterestBLL.delete(lia.StudentInterestId);
                }

                List<Alert> Alert = alertBLL.GetAll().Where(x => x.UserName == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }
                List<Message> message = messageBLL.GetAll().Where(x => x.FromUserName == usernamedel).ToList();
                foreach (var lim in message)
                {
                    messageBLL.delete(lim.MessageId);
                }

                List<Message> messaget = messageBLL.GetAll().Where(x => x.ToUserName == usernamedel).ToList();
                foreach (var limt in messaget)
                {
                    messageBLL.delete(limt.MessageId);
                }

                if (studentBll.delete(Convert.ToInt32(StudentId)))
                {
                    userBll.delete(usernamedel);
                }
                else
                {
                    return str;
                }
            }
            return JsonConvert.SerializeObject(str);
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            string studentId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(studentId))
            {
                StudentTestBLL studentTestbll = new StudentTestBLL();
                List<StudentTest> studentTest = studentTestbll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                foreach (var li in studentTest)
                {
                    studentTestbll.delete(li.StudentTestId);
                }

                List<StudentAcademic> studentAcademics = academicBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                foreach (var li in studentAcademics)
                {
                    academicBll.delete(li.StudentAcademicsId);
                }
                List<StudentWorkHistory> StudentWorkhistory = workHistoryBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                foreach (var lis in StudentWorkhistory)
                {
                    workHistoryBll.delete(lis.StudentWorkHistoryId);
                }

                Student student = studentBll.Get(Convert.ToInt32(studentId));
                string usernamedel = student.UserName;

                List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.StudentUserName == usernamedel).ToList();
                foreach (var lia in studentInterest)
                {
                    studentInterestBLL.delete(lia.StudentInterestId);
                }

                List<Alert> Alert = alertBLL.GetAll().Where(x => x.UserName == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }
                List<Message> message = messageBLL.GetAll().Where(x => x.FromUserName == usernamedel).ToList();
                foreach (var lim in message)
                {
                    messageBLL.delete(lim.MessageId);
                }

                List<Message> messaget = messageBLL.GetAll().Where(x => x.ToUserName == usernamedel).ToList();
                foreach (var limt in messaget)
                {
                    messageBLL.delete(limt.MessageId);
                }

                if (studentBll.delete(Convert.ToInt32(studentId)))
                {
                    userBll.delete(usernamedel);
                    lblMsg.Text = "Student Record Deleted";
                    BindBasicStudentInfoGrid();
                }
                else
                {
                    lblMsg.Text = "Unable to delete Student Record. Some error occured..!";
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SpotCollege.DAL.User user = userBll.Get(hndUserName.Value);
            if (user == null)
            {
                SpotCollege.DAL.User user1 = new SpotCollege.DAL.User();
                user1.UserName = txtEmail.Text;
                user1.Password = txtPassword.Text;
                user1.LoginTypeId = (int)LoginTypes.Student;
                user1.IsActive = true;
                userBll.Save(user1);
                user = user1;
            }

            string ImagePath = string.Empty;

            Student student = studentBll.GetByUserName(user.UserName);
            if (student == null)
            {
                student = new Student();

                student.FirstName = txtFirstName.Text;
                student.MiddleName = txtMiddleName.Text;
                student.LastName = txtLastName.Text;
                student.Address1 = txtAddress1.Text;
                student.Address2 = txtAddress2.Text;
                student.City = txtCity.Text;
                student.Country = ddlCountry.SelectedItem.Text;
                student.PrimaryNumber = txtPrimaryNumber.Text;
                student.PrimaryType = ddlPhoneType.Text;
                if (txtSecondaryNumber.Text != "")
                {
                    student.SecondaryNumber = txtSecondaryNumber.Text;
                    if (ddlSecondaryPhoneType.SelectedValue != "0")
                    {
                        student.SecondaryType = ddlSecondaryPhoneType.Text;
                    }
                }

                student.PrimaryEmail = txtPrimaryEmail.Text;
                student.BirthDate = Convert.ToDateTime(txtBirthDate.Text);
                student.Citizenship = ddlCountryOfCitizenship.SelectedItem.Text;

                //Education Preference
                //student.StudyingIn = Convert.ToInt32(ddlCurrentlyIn.SelectedValue);
                student.StudyingIn = Convert.ToInt32(ddlCurrentlyIn.SelectedValue);
                student.LookingFor = Convert.ToInt32(ddlLookingFor.SelectedValue);
                student.LookingForCountry = ddlLookingForCountry.SelectedItem.Text;
                student.Payout = Convert.ToInt32(ddlPayout.SelectedValue);
                student.StartDate = Convert.ToInt32(ddlStartDate.SelectedValue);

                //Activities
                student.SportActivities = txtSports.Text;
                student.LeadershipActivies = txtLeadership.Text;
                student.OtherActivities = txtOtherActivities.Text;

                //Image
                if (fuProfileImage.HasFile)
                {
                    ImagePath = Guid.NewGuid().ToString() + Path.GetExtension(fuProfileImage.FileName);
                    fuProfileImage.SaveAs(ConfigurationManager.AppSettings["ServerPath"] + "Images\\Profile\\" + ImagePath);
                    student.Photo = ImagePath;
                }

                student.UserName = user.UserName;

                studentBll.Save(student);
                BindBasicStudentInfoGrid();
                clearStudentInfo();

                pnlBasicStudentInfo.Visible = true;
                pnlEditBasicStudentInfo.Visible = false;
                pnlCurrentAcademics.Visible = false;
                pnlEditWorkHistory.Visible = false;
                pnlStudentFullDetails.Visible = false;
                pnlAcademicsDetail.Visible = false;
                pnlWorkHistory.Visible = false;

            }
            else
            {
                student.FirstName = txtFirstName.Text;
                student.MiddleName = txtMiddleName.Text;
                student.LastName = txtLastName.Text;
                student.Address1 = txtAddress1.Text;
                student.Address2 = txtAddress2.Text;
                student.City = txtCity.Text;
                student.Country = ddlCountry.SelectedItem.Text;
                student.PrimaryNumber = txtPrimaryNumber.Text;
                student.PrimaryType = ddlPhoneType.Text;
                if (txtSecondaryNumber.Text != "")
                {
                    student.SecondaryNumber = txtSecondaryNumber.Text;
                    if (ddlSecondaryPhoneType.SelectedValue != "0")
                    {
                        student.SecondaryType = ddlSecondaryPhoneType.Text;
                    }
                }

                student.PrimaryEmail = txtPrimaryEmail.Text;
                student.BirthDate = Convert.ToDateTime(txtBirthDate.Text);
                student.Citizenship = ddlCountryOfCitizenship.SelectedItem.Text;

                //Education Preference
                student.StudyingIn = Convert.ToInt32(ddlCurrentlyIn.SelectedValue);
                student.LookingFor = Convert.ToInt32(ddlLookingFor.SelectedValue);
                student.LookingForCountry = ddlLookingForCountry.SelectedItem.Text;
                student.Payout = Convert.ToInt32(ddlPayout.SelectedValue);
                student.StartDate = Convert.ToInt32(ddlStartDate.SelectedValue);

                //Activities
                student.SportActivities = txtSports.Text;
                student.LeadershipActivies = txtLeadership.Text;
                student.OtherActivities = txtOtherActivities.Text;

                //Image
                if (fuProfileImage.HasFile)
                {
                    ImagePath = Guid.NewGuid().ToString() + Path.GetExtension(fuProfileImage.FileName);
                    fuProfileImage.SaveAs(ConfigurationManager.AppSettings["ServerPath"] + "Images\\Profile\\" + ImagePath);
                    student.Photo = ImagePath;
                }

                studentBll.Save(student);

                //Password Update
                if (txtPassword.Text != "")
                {
                    user.UserName = hndUserName.Value;
                    user.Password = txtPassword.Text;
                    userBll.Save(user);
                }
                BindBasicStudentInfoGrid();
                clearStudentInfo();

                pnlBasicStudentInfo.Visible = true;
                pnlEditBasicStudentInfo.Visible = false;
                pnlCurrentAcademics.Visible = false;
                pnlEditWorkHistory.Visible = false;
                pnlStudentFullDetails.Visible = false;
                pnlAcademicsDetail.Visible = false;
                pnlWorkHistory.Visible = false;

            }

            //}
        }


        void clearStudentInfo()
        {
            txtEmail.Text = "";
            txtPassword.Attributes.Add("value", "");
            txtConfirmPassword.Attributes.Add("value", "");
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            ddlCountry.SelectedValue = "0";
            ddlPhoneType.SelectedValue = "0";
            txtPrimaryNumber.Text = "";
            ddlSecondaryPhoneType.SelectedValue = "0";
            txtSecondaryNumber.Text = "";
            txtPrimaryEmail.Text = "";
            txtBirthDate.Text = "";
            txtSports.Text = "";
            txtLeadership.Text = "";
            txtOtherActivities.Text = "";
            ddlCountryOfCitizenship.SelectedValue = "0";
            ddlCurrentlyIn.SelectedValue = "0";
            ddlLookingFor.SelectedValue = "0";
            ddlLookingForCountry.SelectedValue = "0";
            ddlStartDate.SelectedValue = "0";
            ddlPayout.SelectedValue = "0";
            imgProfileImage.Src = "";
        }

        protected void lnkBtnAcademicEdit_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = true;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";
            ReqFldValImage.Enabled = false;


            string studentAcademicId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(studentAcademicId))
            {
                StudentAcademic academic = academicBll.Get(Convert.ToInt32(studentAcademicId));
                if (academic != null)
                {
                    hndStudentAcademicsId.Value = academic.StudentAcademicsId.ToString();
                    hndStudentId.Value = academic.StudentId.ToString();
                    txtSchoolName.Text = academic.SchoolName;
                    txtSchoolCity.Text = academic.SchoolCity;
                    ddlSchoolCountry.SelectedValue = ((int)Enum.Parse(typeof(CountryList), academic.SchoolCountry)).ToString();
                    rdoDidYouGraduate.SelectedValue = academic.Graduate.ToString();
                    if (academic.Graduate == (int)GraduateStatus.Yes)
                    {
                        if (academic.GraduationYear != null)
                        {
                            ddlYearOfGraduation.Text = academic.GraduationYear.ToString();
                        }
                        if (academic.GraduationLevel != null)
                        {
                            ddlLevelCompleted.SelectedValue = academic.GraduationLevel.ToString();
                        }
                    }
                    txtRankingInClass.Text = academic.Rank.ToString();
                    imgCertificate.Visible = true;
                    imgCertificate.Src = "../Images/Certificate/" + academic.CertificatePath;
                }
            }
        }

        protected void rdoDidYouGraduate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoDidYouGraduate.SelectedValue == ((int)GraduateStatus.Yes).ToString())
            {
                pnlGraduateDetail.Visible = true;
            }
            else
            {
                pnlGraduateDetail.Visible = false;
            }
        }

        protected void btnSaveAcademics_Click(object sender, EventArgs e)
        {

            try
            {

                string docPath = String.Empty;
                if (fuCertificate.HasFile)
                {
                    docPath = Guid.NewGuid().ToString() + Path.GetExtension(fuCertificate.FileName);
                    fuCertificate.SaveAs(ConfigurationManager.AppSettings["ServerPath"] + "Images\\Certificate\\" + docPath);
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
                if (txtRankingInClass.Text != "")
                {
                    academic.Rank = Convert.ToDecimal(txtRankingInClass.Text);
                }
                academic.CertificatePath = docPath;
                academic.StudentId = Convert.ToInt32(hndStudentId.Value);

                academicBll.Save(academic);

                //bindAcademicGrid();

                clearAcademics();

                int studentId = Convert.ToInt32(hndStudentId.Value);
                if (studentId != null)
                {
                    List<StudentAcademic> AcademicList = academicBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                    grvAcademicDetails.DataSource = AcademicList;
                    grvAcademicDetails.DataBind();
                }
                pnlBasicStudentInfo.Visible = false;
                pnlEditBasicStudentInfo.Visible = false;
                pnlCurrentAcademics.Visible = false;
                pnlEditWorkHistory.Visible = false;
                pnlStudentFullDetails.Visible = false;
                pnlAcademicsDetail.Visible = true;
                pnlWorkHistory.Visible = false;
            }
            catch (Exception ex)
            {

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
            rdoDidYouGraduate.ClearSelection();
        }

        protected void lnkBtnAcademicDelete_Click(object sender, EventArgs e)
        {
            string studentAcademicId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(studentAcademicId))
            {
                StudentAcademic academic = academicBll.Get(Convert.ToInt32(studentAcademicId));
                if (academicBll.delete(Convert.ToInt32(studentAcademicId)))
                {
                    lblMsg.Text = "Student Academic Record Deleted";
                    //BindBasicStudentInfoGrid();
                    int studentId = Convert.ToInt32(hndStudentId.Value);
                    if (studentId != null)
                    {
                        List<StudentAcademic> AcademicList = academicBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                        grvAcademicDetails.DataSource = AcademicList;
                        grvAcademicDetails.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Unable to delete Student Academic Record. Some error occured..!";
                }
            }
        }

        protected void lnkDeleteWorkHistory_Click(object sender, EventArgs e)
        {
            string studentWorkHistoryId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(studentWorkHistoryId))
            {
                StudentWorkHistory academic = workHistoryBll.Get(Convert.ToInt32(studentWorkHistoryId));
                if (workHistoryBll.delete(Convert.ToInt32(studentWorkHistoryId)))
                {
                    lblMsg.Text = "Student Work History Deleted";
                    int studentId = Convert.ToInt32(hndStudentId.Value);
                    if (studentId != null)
                    {
                        List<StudentWorkHistory> WorkHistoryList = workHistoryBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                        grvWorkHistory.DataSource = WorkHistoryList;
                        grvWorkHistory.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Unable to delete Student Work History. Some error occured..!";
                }
            }
        }

        protected void lnkEditWorkHistory_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = true;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";


            LinkButton lnkCompanyname = (LinkButton)sender;
            int StudentWorkHistoryId = Convert.ToInt32(lnkCompanyname.CommandArgument);
            StudentWorkHistory workHistory = workHistoryBll.Get(StudentWorkHistoryId);

            if (workHistory != null)
            {
                hndStudentId.Value = workHistory.StudentId.ToString();
                txtCompanyName.Text = workHistory.CompanyName;
                txtWorkPosition.Text = workHistory.Position;
                txtStartDate.Text = workHistory.StartDate.ToString("MM/dd/yyyy");
                txtEndDate.Text = workHistory.EndDate.ToString("MM/dd/yyyy");
                txtResposibilities.Text = workHistory.Responsibilities;
                hndStudentWorkHistoryId.Value = workHistory.StudentWorkHistoryId.ToString();
            }
        }

        protected void btnSaveWorkHistory_Click(object sender, EventArgs e)
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
            workHistory.StartDate = Convert.ToDateTime(txtStartDate.Text);
            workHistory.EndDate = Convert.ToDateTime(txtEndDate.Text);
            workHistory.Responsibilities = txtResposibilities.Text;

            workHistoryBll.Save(workHistory);
            clearWorkHistory();
            int studentId = Convert.ToInt32(hndStudentId.Value);
            if (studentId != null)
            {
                List<StudentWorkHistory> WorkHistoryList = workHistoryBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(studentId)).ToList();
                grvWorkHistory.DataSource = WorkHistoryList;
                grvWorkHistory.DataBind();
            }
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = true;

        }

        void clearWorkHistory()
        {
            txtCompanyName.Text = "";
            txtWorkPosition.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtResposibilities.Text = "";
            hndStudentWorkHistoryId.Value = "0";
        }

        protected void LnkbtnAddnewWorkHistory_Click(object sender, EventArgs e)
        {
            clearWorkHistory();
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = true;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearWorkHistory();
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = true;
            lblMsg.Text = "";


        }

        protected void btnCancelAcademic_Click(object sender, EventArgs e)
        {
            clearAcademics();
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = true;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";
            ReqFldValImage.Enabled = true;

        }

        protected void lnkBtnAddNewAcademicDetail_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = true;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";
            ReqFldValImage.Enabled = true;

        }

        protected void lnkbtnAddNewStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewStudent.aspx");

            clearStudentInfo();
            txtEmail.Enabled = true;
            pnlBasicStudentInfo.Visible = false;
            pnlEditBasicStudentInfo.Visible = true;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";



        }



        protected void btnCancelEditUser_Click(object sender, EventArgs e)
        {
            clearStudentInfo();
            pnlBasicStudentInfo.Visible = true;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";
        }

        protected void lnkbtnBackAcademicsdetail_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = true;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";

        }

        protected void lnkbtnBackworkhistory_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = true;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";
        }

        protected void lnkbtnFullinfoBack_Click(object sender, EventArgs e)
        {
            pnlBasicStudentInfo.Visible = true;
            pnlEditBasicStudentInfo.Visible = false;
            pnlCurrentAcademics.Visible = false;
            pnlEditWorkHistory.Visible = false;
            pnlStudentFullDetails.Visible = false;
            pnlAcademicsDetail.Visible = false;
            pnlWorkHistory.Visible = false;
            lblMsg.Text = "";
        }

        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;

            int id = Convert.ToInt32(GrvBasicStudentInfo.DataKeys[row.DataItemIndex].Value);

            bool status = chkStatus.Checked;

            Student std = studentBll.Get(id);
            if (std != null)
            {
                User us = userBll.Get(std.UserName);
                if (us != null)
                {
                    us.UserName = std.UserName;
                    us.IsActive = status;
                    userBll.Save(us);

                }
            }
            BindBasicStudentInfoGrid();
        }

        [WebMethod()]
        public static string ChechUniversityDuplicate(string UserName)
        {
            UserBLL userbl = new UserBLL();
            string str = string.Empty;
            User user = userbl.Get(UserName);
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

        protected void GrvBasicStudentInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrvBasicStudentInfo.PageIndex = e.NewPageIndex;

            if (finalStudentSearch == null)
                BindBasicStudentInfoGrid();
            else
            {
                StringBuilder builder = new StringBuilder();
                foreach (string value in finalStudentSearch.Select(x => x.UserName).ToArray())
                {
                    builder.Append(value);
                    builder.Append(',');
                }
                hdnAllRecipients.Value = builder.ToString();

                GrvBasicStudentInfo.DataSource = finalStudentSearch;
                GrvBasicStudentInfo.DataBind();

                if (chkAll.Checked)
                {
                    for (int i = 0; i < GrvBasicStudentInfo.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)GrvBasicStudentInfo.Rows[i].FindControl("chkSelect");
                        chk.Checked = true;
                    }
                }
            }
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
            msg.IsApproved = true;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);

            AlertBLL alertBLL = new AlertBLL();
            Alert alert = new Alert();
            alert.Message = "Admin has Send Message";
            alert.CreatedDate = DateTime.Now;
            alert.CreatedBy = CookieHelper.Username;
            alert.UserName = sendToUserName;
            alertBLL.Save(alert);

            return msg.MessageId.ToString();
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StudentBLL studentBll = new StudentBLL();
            StudentAcademicsBLL studacd = new StudentAcademicsBLL();
            int id = 0;
            if (chkAll.Checked)
            {
                string[] arr = hdnAllRecipients.Value.Split(',');
                List<Student> studentList = studentBll.GetAll();

                foreach (string tmp in arr)
                {
                    string a = studentList.Where(x => x.UserName == tmp).Select(y => y.StudentId).FirstOrDefault().ToString();
                    id = Convert.ToInt16(a);

                    alertBLL.deleteByUser(tmp);
                    studentInterestBLL.deleteByUsername(tmp);
                    studentTestBll.deleteByStudentId(id);
                    academicBll.deleteByStudentId(id);
                    workHistoryBll.deleteByStudentId(id);
                    List<Message> message = messageBLL.GetAll().Where(x => x.FromUserName == tmp).ToList();
                    foreach (var lim in message)
                    {
                        messageBLL.delete(lim.MessageId);
                    }

                    List<Message> messaget = messageBLL.GetAll().Where(x => x.ToUserName == tmp).ToList();
                    foreach (var limt in messaget)
                    {
                        messageBLL.delete(limt.MessageId);
                    }

                    studentBll.delete(id);
                    userBll.delete(tmp);
                }
            }
            else
            {
                foreach (GridViewRow r in GrvBasicStudentInfo.Rows)
                {
                    CheckBox chk = (CheckBox)r.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        id = Convert.ToInt16(((HiddenField)r.FindControl("hdnId")).Value);
                        string user = ((Label)r.FindControl("Label1")).Text;
                        alertBLL.deleteByUser(user);
                        studentInterestBLL.deleteByUsername(user);
                        studentTestBll.deleteByStudentId(id);
                        academicBll.deleteByStudentId(id);
                        workHistoryBll.deleteByStudentId(id);


                        List<Message> message = messageBLL.GetAll().Where(x => x.FromUserName == user).ToList();
                        foreach (var lim in message)
                        {
                            messageBLL.delete(lim.MessageId);
                        }

                        List<Message> messaget = messageBLL.GetAll().Where(x => x.ToUserName == user).ToList();
                        foreach (var limt in messaget)
                        {
                            messageBLL.delete(limt.MessageId);
                        }


                        studentBll.delete(id);
                        userBll.delete(user);
                    }
                }
            }
            BindBasicStudentInfoGrid();
        }

        [WebMethod]
        public static bool sendGroupMessage(string users, string Title, string Description)
        {
            string[] strarr = users.Split(',');
            string u1 = "";
            for (int i = 0; i < strarr.Length; i++)
            {
                u1 = strarr[i];
                //if (!u1.Contains('@'))
                //{
                //    break;
                //}
               // string status = Getstatus(u1);

                //if (status == string.Empty)
                //{
                    string str = string.Empty;
                    MessageBLL messageBLL = new MessageBLL();
                    SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
                    msg.Title = Title;
                    msg.Description = Description;
                    msg.ParentId = 0;
                    msg.FromUserName = CookieHelper.Username;
                    msg.ToUserName = u1;
                    msg.IsRead = false;
                    msg.IsApproved = true;
                    msg.CreatedDate = DateTime.Now;

                    messageBLL.Save(msg);

                    AlertBLL alertBLL = new AlertBLL();
                    Alert alert = new Alert();
                    alert.Message = "Admin has Send Message";
                    alert.CreatedDate = DateTime.Now;
                    alert.CreatedBy = CookieHelper.Username;
                    alert.UserName = CookieHelper.Username;
                    alertBLL.Save(alert);
                //}
                //else
                //{
                //    int parentId = Convert.ToInt16(status);
                //    MessageBLL messageBLL = new MessageBLL();

                //    Message msgdata = messageBLL.Get(parentId);
                //    if (msgdata != null)
                //    {
                //        Message msg = new Message();
                //        msg.Title = Title;
                //        msg.Description = Description;
                //        msg.ParentId = parentId;
                //        msg.FromUserName = CookieHelper.Username;
                //        msg.ToUserName = u1;
                //        msg.IsRead = false;
                //        msg.IsApproved = true;
                //        msg.CreatedDate = DateTime.Now;
                //        messageBLL.Save(msg);
                //    }
                //}
            }
            return false;
        }


        [WebMethod]
        public static string SaveMessageReply(string parentMsgId, string msgDesc, string msgTitle, string fromusername, string tousername)
        {
            MessageBLL messageBLL = new MessageBLL();

            Message msgdata = messageBLL.Get(Convert.ToInt32(parentMsgId));
            Message msg = new Message();

            if (msgdata != null)
            {
                msg.Title = msgTitle;
                msg.Description = msgDesc;
                msg.ParentId = Convert.ToInt32(parentMsgId);
                msg.FromUserName = fromusername;
                msg.ToUserName = tousername;
                msg.IsRead = false;

                if (tousername == "admin@gmail.com" || fromusername == "admin@gmail.com")
                    msg.IsApproved = true;
                else
                    msg.IsApproved = false;
                msg.CreatedDate = DateTime.Now;
                messageBLL.Save(msg);

                UniversityBLL universityBLL = new UniversityBLL();
                AlertBLL alertBLL = new AlertBLL();

                //if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                //{
                //    string universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
                //    Alert alert = new Alert();
                //    alert.Message = universityname + " has Send Message";
                //    alert.CreatedDate = DateTime.Now;
                //    alert.CreatedBy = msgdata.FromUserName;
                //    alert.UserName = msgdata.ToUserName;
                //    alertBLL.Save(alert);
                //}
            }

            StudentBLL studentBLL = new StudentBLL();
            UniversityBLL UniversityBLL = new UniversityBLL();
            StringBuilder builder = new StringBuilder();

            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                Student std = studentBLL.GetByUserName(fromusername);
                builder.Append("<li class='admin'><div class='page_collapsible' id='first'></div>");

                if (!string.IsNullOrEmpty(std.Photo))
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + std.Photo + "'/></div>");
                else
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                builder.Append(" <div class='list_2_detail'><span>" + std.FirstName + " " + std.LastName + "</span>");
                builder.Append("<a id='msgdel" + msg.MessageId + "' onclick=\"MsgDelete('" + msg.MessageId + "')\" class='fright' style='color:red; line-height:35px'>&nbsp;&nbsp;X&nbsp;&nbsp;</a>");
                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                return builder.ToString();
            }
            else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                University uni = UniversityBLL.GetByUserName(fromusername);
                builder.Append("<li class='admin'><div class='page_collapsible' id='first'></div>");

                if (!string.IsNullOrEmpty(uni.Image))
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + uni.Image + "'/></div>");
                else
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                builder.Append(" <div class='list_2_detail'><span>" + uni.UniversityName + "</span>");
                builder.Append("<a id='msgdel" + msg.MessageId + "' onclick=\"MsgDelete('" + msg.MessageId + "')\" class='fright' style='color:red; line-height:35px'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");

                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                return builder.ToString();
            }
            else
            {
                builder.Append("<li class='admin'><div class='page_collapsible' id='first'></div>");
                builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                builder.Append(" <div class='list_2_detail'><span>Administrator</span>");
                builder.Append("<a id='msgdel" + msg.MessageId + "' onclick=\"MsgDelete('" + msg.MessageId + "')\" class='fright' style='color:red; line-height:35px; cursor:pointer'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");

                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                return builder.ToString();
            }
        }

        [WebMethod]
        public static String[] GetAllConversation(string messageId)
        {
            String[] strarr = new String[2];
            if (messageId != string.Empty)
            {
                MessageBLL messageBLL = new MessageBLL();
                Message ParentMessage = messageBLL.Get(Convert.ToInt32(messageId));
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                System.Text.StringBuilder builder_1 = new System.Text.StringBuilder();
                Student std = null;
                StudentBLL studentBLL = new StudentBLL();
                UniversityBLL universityBLL = new UniversityBLL();
                University uni = new University();
                String toUser = "", fromUser = "", strMsgToSens = "";

                List<Message> tmpMsgList = new List<Message>();
                tmpMsgList = messageBLL.GetAll();
                if (ParentMessage != null)
                {
                    Message msgforup;
                    List<Message> msg = tmpMsgList.Where(x => x.ParentId == ParentMessage.MessageId && x.ToUserName == CookieHelper.Username && x.IsApproved == true).ToList();
                    for (int i = 0; i < msg.Count; i++)
                    {
                        msgforup = msg[i];
                        msgforup.IsRead = true;
                        messageBLL.Save(msgforup);
                    }
                    Message msgtmp = tmpMsgList.Where(x => x.ParentId == ParentMessage.ParentId && x.IsRead == false).FirstOrDefault();
                    if (msgtmp != null)
                    {
                        msgtmp.IsRead = true;
                        messageBLL.Save(msgtmp);
                    }


                    if (ParentMessage.FromUserName == CookieHelper.Username)
                    {
                        fromUser = CookieHelper.Username;
                        toUser = ParentMessage.ToUserName;
                        strMsgToSens = ParentMessage.ToUserName;
                    }
                    else
                    {
                        fromUser = ParentMessage.FromUserName;
                        toUser = CookieHelper.Username;
                        strMsgToSens = ParentMessage.FromUserName;
                    }

                    UserBLL userBll = new UserBLL();

                    string toUserType = userBll.Get(toUser).LoginTypeId.ToString();
                    string fromUserType = userBll.Get(fromUser).LoginTypeId.ToString();
                    //FOR TOUSER DISPLAY
                    #region ToUser
                    if (toUserType == "1")
                    {
                        //When User is Student
                        std = studentBLL.GetByUserName(toUser);
                        if (!string.IsNullOrEmpty(std.Photo))
                            builder_1.Append("<li><div class='std_img'><img src='Images/Profile/" + std.Photo + "' alt='' /></div>");
                        else
                            builder_1.Append("<li><div class='std_img'><img src='Images/no_image.jpg' alt='' /></div>");
                        builder_1.Append("<div class='std_name'><label>" + std.FirstName + " " + std.LastName + "</label></div>");
                        builder_1.Append("<div class='std_time_country'><span>In " + std.City + "," + std.Country + "</span></div></li>");

                    }
                    else if (toUserType == "2")
                    {
                        //when user is university
                        uni = universityBLL.GetByUserName(toUser);
                        if (!string.IsNullOrEmpty(uni.Image))
                            builder_1.Append("<li><div class='std_img'><img src='Images/Profile/" + uni.Image + "' alt='' /></div>");
                        else
                            builder_1.Append("<li><div class='std_img'><img src='Images/no_image.jpg' alt='' /></div>");
                        builder_1.Append("<div class='std_name'><label>" + uni.UniversityName + "</label></div>");
                        builder_1.Append("<div class='std_time_country'><span>In " + uni.City + "," + uni.Country + "</span></div></li>");
                    }
                    else
                    {
                        builder_1.Append("<li><div class='std_img'><img src='Images/no_image.jpg' alt='' /></div>");
                        builder_1.Append("<div class='std_name'><label>Administrator</label></div>");
                        builder_1.Append("</li>");
                    }

                    #endregion
                    //FOR FROM USER DISPLAY
                    #region FromUser
                    if (fromUserType == "1")
                    {
                        //When User is Student
                        std = studentBLL.GetByUserName(fromUser);
                        if (!string.IsNullOrEmpty(std.Photo))
                            builder_1.Append("<li><div class='std_img'><img src='Images/Profile/" + std.Photo + "' alt='' /></div>");
                        else
                            builder_1.Append("<li><div class='std_img'><img src='Images/no_image.jpg' alt='' /></div>");
                        builder_1.Append("<div class='std_name'><label>" + std.FirstName + " " + std.LastName + "</label></div>");
                        builder_1.Append("<div class='std_time_country'><span>In " + std.City + "," + std.Country + "</span></div></li>");

                    }
                    else if (fromUserType == "2")
                    {
                        //when user is university
                        uni = universityBLL.GetByUserName(fromUser);
                        if (!string.IsNullOrEmpty(uni.Image))
                            builder_1.Append("<li><div class='std_img'><img src='Images/Profile/" + uni.Image + "' alt='' /></div>");
                        else
                            builder_1.Append("<li><div class='std_img'><img src='Images/no_image.jpg' alt='' /></div>");
                        builder_1.Append("<div class='std_name'><label>" + uni.UniversityName + "</label></div>");
                        builder_1.Append("<div class='std_time_country'><span>In " + uni.City + "," + uni.Country + "</span></div></li>");
                    }
                    else
                    {
                        builder_1.Append("<li><div class='std_img'><img src='Images/no_image.jpg' alt='' /></div>");
                        builder_1.Append("<div class='std_name'><label>Administrator</label></div>");
                        builder_1.Append("</li>");
                    }

                    #endregion

                    builder.Append("<div class='h2_heading'><h2>" + ParentMessage.Title + "</h2></div>");
                    builder.Append("<a id='msgdel' onclick=\"MsgDeleteAll('" + ParentMessage.MessageId + "')\" class='fright' style='color:red; cursor:pointer'>&nbsp;&nbsp;Delete All Messages&nbsp;&nbsp;</a>");

                    builder.Append("<ul id='ulMessage' class='list_2 collapsible_msg'>");


                    List<Message> msgList = messageBLL.GetAll()

                        .Where(x => x.ParentId == ParentMessage.MessageId && x.FromUserName == CookieHelper.Username
                            ||
                            (x.ParentId == ParentMessage.MessageId && x.IsApproved == true)

                            ||

                            x.MessageId == ParentMessage.MessageId && x.FromUserName == CookieHelper.Username

                            ||

                           x.MessageId == ParentMessage.MessageId && x.IsApproved == true)

                           .OrderByDescending(x => x.MessageId).ToList();

                    for (int i = 0; i < msgList.Count; i++)
                    {
                        string userType = userBll.Get(msgList[i].FromUserName).LoginTypeId.ToString();
                        if (userType == "1")
                        {
                            var res = studentBLL.GetByUserName(msgList[i].FromUserName);
                            if (i < 5)
                                builder.Append("<li><div class='page_collapsible'></div>");
                            else
                                builder.Append("<li class='remainMsg' style='display:none'><div class='page_collapsible'></div>");
                            if (res != null)
                            {
                                if (!string.IsNullOrEmpty(res.Photo))
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + res.Photo + "'/></div>");
                                else
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                                builder.Append(" <div class='list_2_detail'><span>" + res.FirstName + " " + res.LastName + "</span>");
                                builder.Append("<a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright' style='color:red; line-height:35px; cursor:pointer'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");

                                builder.Append("<p>" + msgList[i].Description + "</p></div></div></li>");
                            }
                        }

                        else if (userType == "2")
                        {
                            var res = universityBLL.GetByUserName(msgList[i].FromUserName);
                            if (i < 5)
                                builder.Append("<li><div class='page_collapsible'></div>");
                            else
                                builder.Append("<li class='remainMsg' style='display:none'><div class='page_collapsible'></div>");
                            if (res != null)
                            {
                                if (!string.IsNullOrEmpty(res.Image))
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + res.Image + "'/></div>");
                                else
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                                builder.Append(" <div class='list_2_detail'><span>" + res.UniversityName + "</span>");
                                builder.Append("<a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright' style='color:red; line-height:35px; cursor:pointer'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");

                                builder.Append("<p>" + msgList[i].Description + "</p></div></div></li>");
                            }
                        }
                        else
                        {
                            if (i < 5)
                                builder.Append("<li><div class='page_collapsible'></div>");
                            else
                                builder.Append("<li class='remainMsg' style='display:none'><div class='page_collapsible'></div>");
                            builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                            builder.Append(" <div class='list_2_detail'><span>Administrator</span>");
                            builder.Append("<a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright' style='color:red; cursor:pointer; line-height:35px;'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");
                            builder.Append("<p>" + msgList[i].Description + "</p></div></div></li>");
                        }
                    }
                    builder.Append("</ul>");


                    if (msgList.Count > 6)
                        builder.Append("<div class='openclose_collapsible'> <a id='openAll' title='Open All'>" + (msgList.Count - 5) + " Message</a></div>");


                    builder.Append("<ul class='list_4'><li><label>Reply to all</label><textarea id='txtReplyDesc' placeholder='type your message here'></textarea></li>");
                    builder.Append("<li><label></label><input type='button' id='btnReply' class='button' value='Send' /></li></ul>");
                    builder.Append("<script type='text/javascript'>$('#btnReply').click(function () {SaveMessageReply('" + ParentMessage.MessageId + "','" + ParentMessage.Title + "','" + CookieHelper.Username + "','" + strMsgToSens + "')});</script>");
                }

                strarr[0] = builder.ToString();
                strarr[1] = builder_1.ToString();
                return strarr;
            }
            else
            {
                return strarr;
            }
        }


        [WebMethod]
        public static bool DeleteAllConversation(string parentId)
        {
            int msgId = Convert.ToInt32(parentId);
            MessageBLL msgbll = new MessageBLL();
            Message msgDel = msgbll.Get(msgId);
            if (msgDel != null)
            {
                List<Message> msgDelList = msgbll.GetAll().Where(x => x.ParentId == msgDel.MessageId || x.MessageId == msgDel.MessageId).ToList();

                foreach (Message mDel in msgDelList)
                {
                    msgbll.delete(mDel.MessageId);
                }
            }
            return true;
        }

        ////METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST
        //[WebMethod()]
        //public static string AppendAndDisplayStudentList()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    List<Student> tmpStudentList = new List<Student>();
        //    if (staticStudentList != null)
        //    {
        //        if (staticStudentList.Count != 0)
        //        {
        //            if (staticStudentList.Count < 10)
        //            {
        //                tmpStudentList = staticStudentList.ToList();
        //                staticStudentList.RemoveAll(x => 1 == 1);
        //            }
        //            else
        //            {
        //                tmpStudentList = staticStudentList.GetRange(0, 10);
        //                staticStudentList.RemoveRange(0,10);
        //            }

        //           // string img;
        //            for (int i = 0; i < tmpStudentList.Count; i++)
        //            {
        //                if (tmpStudentList[i].Photo != null)
        //                    img = @"Images\Profile\" + tmpStudentList[i].Photo;
        //                else
        //                    img = @"\Images\no_image.jpg";

        //                sb.Append("<tr><td style='width:12%;'><a href=\"AddNewStudent.aspx?uname=" + tmpStudentList[i].UserName + "\">" + tmpStudentList[i].UserName + "</a></td>");
        //                sb.Append("<td style='width:10%;'><span>" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "</span></td>");
        //                sb.Append("<td style='width:10%;'>" + tmpStudentList[i].Address1 + "</td>");
        //                sb.Append("<td style='width:5%;'>" + tmpStudentList[i].City + "</td>");
        //                sb.Append("<td style='width:7%;'><input type='checkbox' ></input><label></label></td>");
        //                sb.Append("<td style='width:10%;'><a href=''>ViewMore</a></td>");
        //                sb.Append("<td style='width:10%;'><a href=''>Academic Details</a></td>");
        //                sb.Append("<td style='width:10%;'><a href=''>Work History</a></td>");


        //            }

        //            return sb.ToString();
        //        }
        //        else
        //        {
        //            return "no";
        //        }
        //    }
        //    else
        //    {
        //        return "Error";
        //    }
        //}

        [WebMethod()]
        public static string[] GetStudentData(string UserName)
        {
            //string str=string.Empty;
            Student student = new Student();
            StudentBLL studentBLL = new StudentBLL();
            string[] strArr = new string[25];

            student = studentBLL.GetByUserName(UserName);

            #region personal basic information

            if (student.FirstName != null)
                strArr[0] = student.FirstName;
            if (student.MiddleName != null)
                strArr[1] = student.MiddleName;
            if (student.LastName != null)
                strArr[2] = student.LastName;
            if (student.Citizenship != null)
                strArr[3] = student.Citizenship;
            if (student.Address1 != null)
                strArr[4] = student.Address1;
            if (student.Photo != null)
                strArr[5] = student.Photo;
            if (student.Address2 != null)
                strArr[6] = student.Address2;
            if (student.ZipCode != null)
                strArr[7] = student.ZipCode;
            if (student.PrimaryNumber != null)
                strArr[8] = student.PrimaryNumber;
            if (student.BirthDate != null)
            {
                strArr[9] = Convert.ToDateTime(student.BirthDate).ToString("MM/dd/yyyy");
            }
            if (student.City != null)
                strArr[10] = student.City;
            if (student.Country != null)
                strArr[11] = student.Country;
            if (student.PrimaryEmail != null)
                strArr[12] = student.PrimaryEmail;

            #endregion

            #region Educational Preference

            if (student.StudyingIn != null)
            {
                string strStudingIn = (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CurrentlyIn)student.StudyingIn)).ToString();
                strArr[13] = strStudingIn;
            }
            if (student.LookingFor != null)
            {
                string strLookingFor = (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.ProgramLookingFor)student.LookingFor)).ToString();
                strArr[14] = strLookingFor;
            }
            if (student.LookingForCountry != null)
            {
                strArr[15] = student.LookingForCountry;
            }
            if (student.StartDate != null)
            {
                string strStudingIn = (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.expectedStart)student.StartDate)).ToString();
                strArr[16] = strStudingIn;
            }
            if (student.Payout != null)
            {
                string pay = "";
                if (student.Payout == 1)
                    pay = "$3000-5000";
                if (student.Payout == 2)
                    pay = "$5000-10000";
                if (student.Payout == 3)
                    pay = "$10000-15000";
                if (student.Payout == 4)
                    pay = "$15000+";

                strArr[17] = pay;
            }
            if (student.DesiredFieldofStudy != null)
            {
                strArr[18] = student.DesiredFieldofStudy;
            }
            #endregion

            StringBuilder sb = new StringBuilder();

            #region Current Academics
            try
            {
                StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
                List<StudentAcademic> AcademicList = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
                sb.Append("<table class='box simple_table' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'><tbody>");
                sb.Append("<tr><th scope='col' style='width:10%;'>SchoolName</th><th scope='col' style='width:10%;'>Graduate?</th><th scope='col' style='width:8%;'>Rank</th><th scope='col' style='width:8%;'>Certificate</th></tr>");
                for (int i = 0; i < AcademicList.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='width:10%;'>");
                    sb.Append("<span>" + AcademicList[i].SchoolName + "</span></td>");
                    sb.Append("<td style='width:10%;'><span>" + EnumHelper.GetDescriptionFromEnumValue((GraduateStatus)AcademicList[i].Graduate) + "</span></td>");

                    if (AcademicList[i].Rank != null)
                        sb.Append("<td style='width:8%;'>" + (int)AcademicList[i].Rank + "</td>");
                    else
                        sb.Append("<td style='width:8%;'>Not Available</td>");

                    sb.Append("<td style='width:8%;'>");
                    sb.Append(@"<a href='../Images/Certificate/" + AcademicList[i].CertificatePath + "' class='preview'>");
                    sb.Append(@"<img class='large_images' src='../Images/Certificate/" + AcademicList[i].CertificatePath + "' height='30' width='30'></a>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                strArr[19] = sb.ToString();
            }
            catch (Exception ex) { }
            #endregion

            #region International Test
            try{
            sb = new StringBuilder();

            StudentTestBLL studentTestBLL = new StudentTestBLL();

            List<StudentTest> TestList = studentTestBLL.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
            sb.Append("<table class='box simple_table' cellspacing='0' border='1' style='border-collapse:collapse;'><tbody>");
            sb.Append("<tr><th scope='col' style='width:10%;'>Test Name</th></tr>");
            for (int i = 0; i < TestList.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td style='width:10%;'>");
                sb.Append("<span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.InternationalTestRecord)TestList[i].TestId)).ToString() + "-" + TestList[i].Date + "</span></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            strArr[20] = sb.ToString();
            }
            catch (Exception ex) { }
            #endregion

            #region Work History
            try{
            sb = new StringBuilder();

            StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();
            List<StudentWorkHistory> WorkHistoryList = workHistoryBll.GetAll().Where(x => x.StudentId == student.StudentId).ToList();

            sb.Append("<table class='box simple_table' cellspacing='0' border='1' style='border-collapse:collapse;'><tbody>");

            sb.Append("<tr><th scope='col' style='width:10%;'>Company Name</th><th scope='col' style='width:10%;'>Start Date</th><th scope='col' style='width:8%;'>End Date</th></tr>");
            
            for (int i = 0; i < WorkHistoryList.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td style='width:10%;'>");
                sb.Append("<span>" + WorkHistoryList[i].CompanyName + "</span></td>");
                sb.Append("<td style='width:8%;'>"+WorkHistoryList[i].StartDate.ToString("dd/MM/yyyy")+"</td>");
                sb.Append("<td style='width:8%;'>" + WorkHistoryList[i].EndDate.ToString("dd/MM/yyyy"));
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            strArr[21] = sb.ToString();
            }
            catch (Exception ex) { }
            #endregion

            #region Extra Activity
            
            strArr[22] = student.SportActivities;
            strArr[23] = student.LeadershipActivies;
            strArr[24] = student.OtherActivities;

            #endregion
            return strArr;
            //return JsonConvert.SerializeObject(student);
        }

        protected void GrvBasicStudentInfo_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            List<Student> studentList = new List<Student>();
            // List<Project> projectlist = projectBll.GetAll().Where(x => x.UserName == CookieHelper.Username).ToList();

            if (GetSortDirection() == "ASC")
            {
                studentList = studentBll.GetAll().OrderBy(x => x.FirstName).ToList();
            }
            else
            {
                studentList = studentBll.GetAll().OrderByDescending(x => x.FirstName).ToList();
            }


            GrvBasicStudentInfo.DataSource = studentList;
            GrvBasicStudentInfo.DataBind();
        }

        #region Gridview Methods

        #region GridViewSortDirection
        private string GridViewSortDirection
        {
            get
            {
                return ViewState["SortDirection"] as string ?? "ASC";
            }
            set
            {

                ViewState["SortDirection"] = value;
            }
        }
        #endregion

        #region GridViewSortExpression
        /// Gets or Sets the gridview sortexpression property
        private string GridViewSortExpression
        {
            get
            {
                return ViewState["SortExpression"] as string ?? string.Empty;
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
        #endregion

        #region GetSortDirection
        /// Returns the direction of the sorting
        private string GetSortDirection()
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;
                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }
            return GridViewSortDirection;
        }
        #endregion

        #endregion GridView methods

    }
}