using SpotCollege.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.DAL;
using SpotCollege.BLL;
using System.Configuration;

namespace SpotCollege.Account
{
    public partial class EditProfileUniversity : System.Web.UI.Page
    {
        UserBLL userBll = new UserBLL();
        UniversityBLL universityBll = new UniversityBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCheckBoxList();
                bindDropdown();
                loadDetails();
                setCurrentTab();
            }
        }

        void setCurrentTab()
        {
            string sec = Request.QueryString["sec"];
            if (hndUniversityId.Value != "0")
            {
                if (sec == "cost")
                {
                    pnlBasicDetail.Visible = false;
                    pnlCostForInternationalStudents.Visible = true;
                    hndCurrentTab.Value = "2";
                }
                else if (sec == "enroll")
                {
                    pnlBasicDetail.Visible = false;
                    pnlEnrollmentNumber.Visible = true;
                    hndCurrentTab.Value = "3";
                }
                else if (sec == "program")
                {
                    pnlBasicDetail.Visible = false;
                    pnlProgramsAndMajors.Visible = true;
                    hndCurrentTab.Value = "4";
                }
            }
            else
            {
                CostForInternationalStudents.Visible = false;
                EnrollmentNumbers.Visible = false;
                ProgramAndMajors.Visible = false;
                hndCurrentTab.Value = "1";
            }
        }


        void BindCheckBoxList()
        {
            //Bind Program Offered
            string[] names = Enum.GetNames(typeof(Programs));
            Array value = Enum.GetValues(typeof(Programs));
            int[] Values = (int[])value;
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(names[i], Values[i].ToString());
                chkDegreeOffredList.Items.Add(item);
            }
            chkDegreeOffredList.Items[0].Selected = true;
            chkDegreeOffredList.Items[1].Selected = true;

            //Bind Courses Offered List
            names = Enum.GetNames(typeof(CoursesOffered));
            value = Enum.GetValues(typeof(CoursesOffered));
            Values = (int[])value;
            for (int i = 0; i < names.Length; i++)
            {
                //ListItem item = new ListItem(names[i], Values[i].ToString());
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Values[i]), Values[i].ToString());
                chkCoursesOfferedList.Items.Add(item);
            }
        }

        void bindDropdown()
        {
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


            string[] currnecyNames = Enum.GetNames(typeof(CurrencyTypes));
            Array currncyValue = Enum.GetValues(typeof(CurrencyTypes));
            int[] currncyValues = (int[])currncyValue;
            for (int i = 0; i < currnecyNames.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)currncyValues[i]), currncyValues[i].ToString());
                ddlCurrnecyForOCR.Items.Add(item);
            }
            for (int i = 0; i < currnecyNames.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)currncyValues[i]), currncyValues[i].ToString());
                ddlCurrencyForBS.Items.Add(item);
            }
            for (int i = 0; i < currnecyNames.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)currncyValues[i]), currncyValues[i].ToString());
                ddlCurrencyForGS.Items.Add(item);
            }
            for (int i = 0; i < currnecyNames.Length; i++)
            {
                ListItem item = new ListItem(EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)currncyValues[i]), currncyValues[i].ToString());
                ddlCurrencyForUGS.Items.Add(item);
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

        void loadDetails()
        {
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            if (user != null)
            {
                University university = universityBll.GetByUserName(user.UserName);
                if (university != null)
                {
                    hndUniversityId.Value = university.UniversityId.ToString();

                    //Basic Profile
                    txtUniversityName.Text = university.UniversityName;
                    txtAddress.Text = university.Address;
                    txtUserName.Text = user.UserName;
                    txtAdministratorName.Text = university.AdminName;
                    txtCity.Text = university.City;
                    ddlCountry.SelectedValue = ((int)Enum.Parse(typeof(CountryList), university.Country)).ToString();
                    txtEstablishedYear.Text = university.EstablishedYear.ToString();
                    txtDescription.Text = university.Description;
                    imgUniversityLogo.Src = "../Images/Profile/" + university.Image;
                    imgUniversityImage.Src = "../Images/Profile/" + university.UniversityImage;
                    if (university.ContactNo != null)
                        txtcontactno.Text = university.ContactNo;
                    if (!string.IsNullOrEmpty( university.CountryCode))
                        ddlCountryCode.SelectedValue = university.CountryCode.ToString();

                    //Cost of international Student
                    string[] arr = new string[3];

                    if (university.UnderGraduateFee != null)
                    {
                        arr = university.UnderGraduateFee.Split('-');
                        ddlCurrencyForUGS.SelectedValue = arr[2];
                        txtUnderGraduateFee.Text = arr[0] + '-' + arr[1];
                        ddlunitForUGS.SelectedValue = arr[3];
                    }



                    if (university.GraduateFee != null)
                    {
                        arr = university.GraduateFee.ToString().Split('-');
                        ddlCurrencyForGS.SelectedValue = arr[2];
                        txtGraduateFee.Text = arr[0] + '-' + arr[1];
                        ddlunitForGS.SelectedValue = arr[3];
                    }




                    if (university.BookFee != null)
                    {
                        arr = university.BookFee.ToString().Split('-');
                        ddlCurrencyForBS.SelectedValue = arr[2];

                        txtBookFee.Text = arr[0] + '-' + arr[1];
                        ddlunitForBook.SelectedValue = arr[3];
                    }

                    if (university.BoardFee != null)
                    {
                        arr = university.BoardFee.ToString().Split('-');
                        ddlCurrnecyForOCR.SelectedValue = arr[2];
                        txtBoardFee.Text = arr[0] + '-' + arr[1];
                        ddlunitForBoard.SelectedValue = arr[3];
                    }

                    //Enrollment Numbers
                    if (university.Graduates != null)
                        txtNoOfGraduates.Text = university.Graduates.ToString();
                    if (university.UnderGraduates != null)
                        txtNoOfUnderGraduates.Text = university.UnderGraduates.ToString();
                    if (university.InternationalGraduate != null)
                        txtNoOfInterNationalGraduates.Text = university.InternationalGraduate.ToString();

                    if (university.Degree != null)
                    {
                        string SelectedValue = university.Degree.ToString();
                        string[] degree = SelectedValue.Split(',');
                        for (int i = 0; i < chkDegreeOffredList.Items.Count; i++)
                        {
                            for (int j = 0; j < degree.Count(); j++)
                            {
                                if (chkDegreeOffredList.Items[i].Text == degree[j])
                                {
                                    chkDegreeOffredList.Items[i].Selected = true;
                                }
                            }
                        }
                    }
                    if (university.Courses != null)
                    {
                        string SelectedValue = university.Courses.ToString();
                        string[] courses = SelectedValue.Split(',');
                        for (int i = 0; i < chkCoursesOfferedList.Items.Count; i++)
                        {
                            for (int j = 0; j < courses.Count(); j++)
                            {
                                if (chkCoursesOfferedList.Items[i].Text == courses[j])
                                {
                                    chkCoursesOfferedList.Items[i].Selected = true;
                                }
                            }
                        }
                    }

                }
                else
                {
                    txtUserName.Text = CookieHelper.Username;
                }
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            University university = universityBll.Get(Convert.ToInt32(hndUniversityId.Value));
            if (university != null)
            {
                string offerdegree = "";
                foreach (ListItem ls in chkDegreeOffredList.Items)
                {
                    if (ls.Selected == true)
                    {
                        offerdegree = offerdegree + ls.Text + ",";

                    }
                }

                string CoursesOffered = "";
                foreach (ListItem ls in chkCoursesOfferedList.Items)
                {
                    if (ls.Selected == true)
                    {
                        CoursesOffered = CoursesOffered + ls.Text + ",";

                    }
                }

                university.Courses = CoursesOffered;
                university.Degree = offerdegree;
                universityBll.Save(university);

                Response.Redirect("ProfileOverviewUniversity.aspx");
            }
        }

        protected void btnSaveEnrollNumber_Click(object sender, EventArgs e)
        {
            University university = universityBll.Get(Convert.ToInt32(hndUniversityId.Value));
            if (university != null)
            {
                if (txtNoOfUnderGraduates.Text != string.Empty)
                    university.UnderGraduates = Convert.ToInt32(txtNoOfUnderGraduates.Text);
                else
                    university.UnderGraduates = null;

                if (txtNoOfGraduates.Text != string.Empty)
                    university.Graduates = Convert.ToInt32(txtNoOfGraduates.Text);
                else
                    university.Graduates = null;

                if (txtNoOfInterNationalGraduates.Text != string.Empty)
                    university.InternationalGraduate = Convert.ToInt32(txtNoOfInterNationalGraduates.Text);
                else
                    university.InternationalGraduate = null;

                universityBll.Save(university);
            }

            pnlEnrollmentNumber.Visible = false;
            pnlProgramsAndMajors.Visible = true;
            hndCurrentTab.Value = "4";
        }

        protected void btnCostForInternationalStudentSave_Click(object sender, EventArgs e)
        {
            University university = universityBll.Get(Convert.ToInt32(hndUniversityId.Value));
            if (university != null)
            {
                if (txtUnderGraduateFee.Text != string.Empty)
                    university.UnderGraduateFee = (txtUnderGraduateFee.Text) + "-" + ddlCurrencyForUGS.SelectedValue.ToString() +"-" + ddlunitForUGS.SelectedValue.ToString();
                else
                    university.UnderGraduateFee = null;

                if (txtGraduateFee.Text != string.Empty)
                    university.GraduateFee = (txtGraduateFee.Text) + "-" + ddlCurrencyForGS.SelectedValue.ToString() + "-" + ddlunitForGS.SelectedValue.ToString();
                else
                    university.GraduateFee = null;

                if (txtBookFee.Text != string.Empty)
                    university.BookFee = (txtBookFee.Text) + "-" + ddlCurrencyForBS.SelectedValue.ToString() + "-" + ddlunitForBook.SelectedValue.ToString();
                else
                    university.BookFee = null;

                if (txtBoardFee.Text != string.Empty)
                    university.BoardFee = (txtBoardFee.Text) + "-" + ddlCurrnecyForOCR.SelectedValue.ToString() + "-" + ddlunitForBoard.SelectedValue.ToString();
                else
                    university.BoardFee = null;

                universityBll.Save(university);
            }

            pnlCostForInternationalStudents.Visible = false;
            pnlEnrollmentNumber.Visible = true;
            hndCurrentTab.Value = "3";
        }

        protected void btnBasicSave_Click(object sender, EventArgs e)
        {
            if (imgUniversityLogo.Src == "" && !fluploadImage.HasFile)
            {
                lblImageValidation.Visible = true;
                return;
            }
            University university = null;
            if (hndUniversityId.Value == "0")
            {
                university = new University();
                //Other Fields Entry

                university.UserName = txtUserName.Text;
                university.Degree = "";
                university.Courses = "";
                //university.UnderGraduateFee = "0";
                //university.GraduateFee = "0";
                //university.BookFee = "0";
                //university.BoardFee = "0";
                //university.Graduates = 0;
                //university.UnderGraduates = 0;
                //university.InternationalGraduate = 0;
            }
            else
            {
                university = universityBll.Get(Convert.ToInt32(hndUniversityId.Value));
                if (university == null)
                {
                    university = new University();
                    //Other Fields Entry

                    university.UserName = txtUserName.Text;
                    university.Degree = "";
                    university.Courses = "";
                    //university.UnderGraduateFee = "0";
                    //university.GraduateFee = "0";
                    //university.BookFee = "0";
                    //university.BoardFee = "0";
                    //university.Graduates = 0;
                    //university.UnderGraduates = 0;
                    //university.InternationalGraduate = 0;
                }
            }
            university.UniversityName = txtUniversityName.Text;
            university.Address = txtAddress.Text;
            university.AdminName = txtAdministratorName.Text;
            university.City = txtCity.Text;
            university.Country = ddlCountry.SelectedItem.Text;
            university.EstablishedYear =Convert.ToInt32( txtEstablishedYear.Text);
            university.ContactNo = txtcontactno.Text;
            university.CountryCode = ddlCountryCode.SelectedValue.ToString();
            university.Description = txtDescription.Text;
            if (fluploadImage.HasFile)
            {
                string Imagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fluploadImage.PostedFile.FileName);
                //Imagename="Images\Photos\" +
                fluploadImage.SaveAs(Server.MapPath("\\Images\\Profile\\") + Imagename);
                university.Image = Imagename;
            }

            if (fluploadUniversityImage.HasFile)
            {
                string UniversityImagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fluploadUniversityImage.PostedFile.FileName);
                fluploadUniversityImage.SaveAs(Server.MapPath("\\Images\\Profile\\") + UniversityImagename);
                university.UniversityImage = UniversityImagename;
            }
            universityBll.Save(university);

            hndUniversityId.Value = university.UniversityId.ToString();

            pnlBasicDetail.Visible = false;
            pnlCostForInternationalStudents.Visible = true;
            hndCurrentTab.Value = "2";

            CostForInternationalStudents.Visible = true;
            EnrollmentNumbers.Visible = true;
            ProgramAndMajors.Visible = true;

        }

        protected void lnkCostForInternationalStudents_Click(object sender, EventArgs e)
        {
            pnlCostForInternationalStudents.Visible = true;
            hndCurrentTab.Value = "2";

            pnlBasicDetail.Visible = false;
            pnlEnrollmentNumber.Visible = false;
            pnlProgramsAndMajors.Visible = false;
        }

        protected void lnkBasicInformation_Click(object sender, EventArgs e)
        {
            pnlBasicDetail.Visible = true;
            hndCurrentTab.Value = "1";


            pnlCostForInternationalStudents.Visible = false;
            pnlEnrollmentNumber.Visible = false;
            pnlProgramsAndMajors.Visible = false;
        }

        protected void lnkEnrollmentNumbers_Click(object sender, EventArgs e)
        {
            pnlEnrollmentNumber.Visible = true;
            hndCurrentTab.Value = "3";

            pnlCostForInternationalStudents.Visible = false;
            pnlBasicDetail.Visible = false;
            pnlProgramsAndMajors.Visible = false;
        }

        protected void lnkProgramAndMajors_Click(object sender, EventArgs e)
        {
            pnlProgramsAndMajors.Visible = true;
            hndCurrentTab.Value = "4";

            pnlCostForInternationalStudents.Visible = false;
            pnlBasicDetail.Visible = false;
            pnlEnrollmentNumber.Visible = false;
        }

    }
}