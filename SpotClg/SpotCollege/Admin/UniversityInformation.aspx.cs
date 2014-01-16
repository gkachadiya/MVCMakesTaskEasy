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
using System.Web.Services;
using Newtonsoft.Json;
using System.Text;


namespace SpotCollege.Admin
{
    public partial class UniversityInformation : System.Web.UI.Page
    {
        UserBLL userBll = new UserBLL();
        UniversityBLL universityBll = new UniversityBLL();
        StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
        StudentBLL studentBll = new StudentBLL();
        AlertBLL alertBLL = new AlertBLL();
        MessageBLL messageBLL = new MessageBLL();
        List<University> universityList = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
                BindCheckBoxList();
                bindDropdown();
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

            //Bind Search by Country Dropdown
            ddlFromCountry.Items.Add(new ListItem("Select Country", "0"));
            ddlFromCountry.SelectedValue = "0";
            for (int i = 0; i < Countrynames.Length; i++)
            {
                ListItem item = new ListItem(Countrynames[i], CountryValues[i].ToString());
                ddlFromCountry.Items.Add(item);
            }

            //bind Establish Year Dropdown
            ddlEstablishYear.Items.Add(new ListItem("Select Any Year", "0"));
            for (int i = 1900; i <= 2013; i++)
            {
                ddlEstablishYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            //bind Interested Student status Dropdown
            string[] Studentnames = Enum.GetNames(typeof(StudentInterestApproved));
            Array Studentsvalue = Enum.GetValues(typeof(StudentInterestApproved));
            int[] Studentsvalues = (int[])Studentsvalue;
            for (int i = 0; i < Studentnames.Length; i++)
            {
                ListItem item = new ListItem(Studentnames[i], Studentsvalues[i].ToString());
                ddlstudentStatus.Items.Add(item);
            }

        }

        void ClearAll()
        {
            txtUserName.Text = "";
            txtPassword.Attributes.Add("value", "");
            txtConfirmPassword.Attributes.Add("value", "");
            txtUniversityName.Text = "";
            txtAddress.Text = "";
            txtAdministratorName.Text = "";
            txtCity.Text = "";
            ddlCountry.SelectedValue = "0";
            ddlEstablishYear.SelectedValue = "0";
            //txtEstablishedYear.Text = "";
            txtUnderGraduateFee.Text = "";
            txtGraduateFee.Text = "";
            txtBookFee.Text = "";
            txtBoardFee.Text = "";
            txtGraduates.Text = "";
            txtUnderGraduates.Text = "";
            txtInterNationalGraduates.Text = "";
            txtDescription.Text = "";
            foreach (ListItem ls in chkDegreeOffredList.Items)
            {
                ls.Selected = false;
            }

            foreach (ListItem ls in chkCoursesOfferedList.Items)
            {
                ls.Selected = false;
            }
            hndUserName.Value = "";
            hndUniversityId.Value = "0";
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


            //Bind Courses Offered List
            names = Enum.GetNames(typeof(CoursesOffered));
            value = Enum.GetValues(typeof(CoursesOffered));
            Values = (int[])value;
            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(names[i], Values[i].ToString());
                chkCoursesOfferedList.Items.Add(item);
            }
        }

        void BindGrid()
        {
            List<User> user = userBll.GetAll().ToList();

            List<University> universityList = new List<University>();
            List<University> tmpUniList = universityBll.GetAll().Where(x => x.User.IsActive == true).ToList();
            foreach (var intrestuniversity in user)
            {
                University u = tmpUniList.Where(x => x.UserName == intrestuniversity.UserName).FirstOrDefault();
                if (u != null)
                {
                    universityList.Add(u);
                }
                else
                {
                    universityList.Remove(u);
                }
            }


            StringBuilder builder = new StringBuilder();
            foreach (string value in universityList.Select(x => x.UserName).ToArray())
            {
                builder.Append(value);
                builder.Append(',');
            }

            hdnAllRecipient.Value = builder.ToString();
            lblNoOfResult.Text = universityList.Count.ToString() + " Record Found";

            //  List<University> universityList = universityBll.GetAll().ToList();
            GrvUniversityInfo.DataSource = universityList;
            GrvUniversityInfo.DataBind();

            GrvUniversityInfo2.DataSource = universityList;
            GrvUniversityInfo2.DataBind();
            if (chkAll.Checked)
            {
                for (int i = 0; i < GrvUniversityInfo.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GrvUniversityInfo.Rows[i].FindControl("chkUni");
                    chk.Checked = true;
                }
            }
        }


        #region Page Methods
        protected void lnkbtnAddNeWUniversity_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewUniversity.aspx");

            pnlEditUniverSityInfo.Visible = true;
            txtUserName.Enabled = true;
            ReqFldValImage.Enabled = true;
            pnlUniversityInfo.Visible = false;
            lblMsg.Text = "";
            ClearAll();
            pnlInterestedStudent.Visible = false;
            pnlInterestedStudentEdit.Visible = false;
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            string username = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                Response.Redirect("AddNewUniversity.aspx?uname=" + username);
            }

            pnlEditUniverSityInfo.Visible = true;
            pnlUniversityInfo.Visible = false;
            pnlInterestedStudent.Visible = false;
            pnlInterestedStudentEdit.Visible = false;
            txtUserName.Enabled = false;
            ReqFldValImage.Enabled = false;
            lblMsg.Text = "";

            University university = universityBll.GetAll().Where(x => x.UserName == username).SingleOrDefault();
            if (university != null)
            {
                hndUniversityId.Value = university.UniversityId.ToString();
                hndUserName.Value = university.UserName.ToString();
                txtUniversityName.Text = university.UniversityName;
                txtAddress.Text = university.UniversityName;
                txtUserName.Text = university.UserName;
                txtAdministratorName.Text = university.AdminName;
                txtPassword.Attributes.Add("value", university.User.Password);
                txtConfirmPassword.Attributes.Add("value", university.User.Password);
                txtDescription.Text = university.Description;
                txtGraduateFee.Text = university.GraduateFee.ToString();
                txtUnderGraduateFee.Text = university.UnderGraduateFee.ToString();
                txtBookFee.Text = university.BookFee.ToString();
                txtBoardFee.Text = university.BoardFee.ToString();
                txtGraduates.Text = university.Graduates.ToString();
                txtUnderGraduates.Text = university.UnderGraduates.ToString();
                txtInterNationalGraduates.Text = university.InternationalGraduate.ToString();
                txtCity.Text = university.City;
                if (university.EstablishedYear != null)
                {
                    ddlEstablishYear.SelectedValue = university.EstablishedYear.ToString();
                }
                //txtEstablishedYear.Text = university.EstablishedYear.ToString();
                ddlCountry.SelectedValue = ((int)Enum.Parse(typeof(CountryList), university.Country)).ToString();
                //Load Profile Image
                if (university.Image != null)
                {
                    imgProfileImage.Visible = true;
                    imgProfileImage.Src = "../Images/Profile/" + university.Image;
                }
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
                    string SelectedVal = university.Courses.ToString();
                    string[] courses = SelectedVal.Split(',');
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
        }

        [WebMethod()]
        public static object UniversityDelete(string UniversityId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            UniversityBLL universityBll = new UniversityBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            AlertBLL alertBLL = new AlertBLL();
            UserBLL userBll = new UserBLL();


            if (!string.IsNullOrEmpty(UniversityId))
            {
                University univ = universityBll.Get(Convert.ToInt32(UniversityId));
                string usernamedel = univ.UserName;

                List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == usernamedel).ToList();
                foreach (var lia in studentInterest)
                {
                    studentInterestBLL.delete(lia.StudentInterestId);
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



                List<Alert> Alert = alertBLL.GetAll().Where(x => x.CreatedBy == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }

                Alert = alertBLL.GetAll().Where(x => x.UserName == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }

                if (universityBll.delete(Convert.ToInt32(UniversityId)))
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
            string universityId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(universityId))
            {
                University univ = universityBll.Get(Convert.ToInt32(universityId));
                string usernamedel = univ.UserName;



                List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == usernamedel).ToList();
                foreach (var lia in studentInterest)
                {
                    studentInterestBLL.delete(lia.StudentInterestId);
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



                List<Alert> Alert = alertBLL.GetAll().Where(x => x.CreatedBy == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }

                Alert = alertBLL.GetAll().Where(x => x.UserName == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }

                if (universityBll.delete(Convert.ToInt32(universityId)))
                {
                    userBll.delete(usernamedel);
                    lblMsg.Text = "University Record Deleted";
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Unable to delete University Record. Some error occured..!";
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            //Save University Login Detail 
            SpotCollege.DAL.User user = userBll.Get(hndUserName.Value);
            if (user == null)
            {
                SpotCollege.DAL.User user1 = new SpotCollege.DAL.User();
                user1.UserName = txtUserName.Text;
                user1.Password = txtPassword.Text;
                user1.LoginTypeId = (int)LoginTypes.University;
                user1.IsActive = true;
                userBll.Save(user1);
                user = user1;
            }
            University university = universityBll.GetAll().Where(x => x.UserName == user.UserName).SingleOrDefault();
            if (university == null)
            {
                //Save University Profile Detail
                university = new University();
                university.UserName = txtUserName.Text;
                university.UniversityName = txtUniversityName.Text;
                university.Address = txtAddress.Text;
                university.AdminName = txtAdministratorName.Text;
                university.City = txtCity.Text;
                university.Country = ddlCountry.SelectedItem.Text;
                university.EstablishedYear = Convert.ToInt32(ddlEstablishYear.SelectedItem.Text);
                //university.EstablishedYear = Convert.ToInt32(txtEstablishedYear.Text);
                university.Description = txtDescription.Text;
                university.UnderGraduateFee = (txtUnderGraduateFee.Text);
                university.GraduateFee = (txtGraduateFee.Text);
                university.BookFee = (txtBookFee.Text);
                university.BoardFee = (txtBoardFee.Text);
                university.Graduates = Convert.ToInt32(txtGraduates.Text);
                university.UnderGraduates = Convert.ToInt32(txtUnderGraduates.Text);
                university.InternationalGraduate = Convert.ToInt32(txtInterNationalGraduates.Text);


                string Imagename = string.Empty;
                if (fluploadImage.HasFile)
                {
                    Imagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fluploadImage.PostedFile.FileName);
                    //Imagename="Images\Photos\" +
                    fluploadImage.SaveAs(ConfigurationManager.AppSettings["ServerPath"] + "Images\\Profile\\" + Imagename);
                    university.Image = Imagename;
                }

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
                ClearAll();
                BindGrid();
                pnlUniversityInfo.Visible = true;
                pnlEditUniverSityInfo.Visible = false;

            }

            else
            {
                university.UserName = txtUserName.Text;
                university.UniversityName = txtUniversityName.Text;
                university.Address = txtAddress.Text;
                university.AdminName = txtAdministratorName.Text;
                university.City = txtCity.Text;
                university.Country = ddlCountry.SelectedItem.Text;
                university.EstablishedYear = Convert.ToInt32(ddlEstablishYear.SelectedItem.Text);
                //university.EstablishedYear = Convert.ToInt32(txtEstablishedYear.Text);
                university.Description = txtDescription.Text;
                university.UnderGraduateFee = (txtUnderGraduateFee.Text);
                university.GraduateFee = (txtGraduateFee.Text);
                university.BookFee = (txtBookFee.Text);
                university.BoardFee = (txtBoardFee.Text);
                university.Graduates = Convert.ToInt32(txtGraduates.Text);
                university.UnderGraduates = Convert.ToInt32(txtUnderGraduates.Text);
                university.InternationalGraduate = Convert.ToInt32(txtInterNationalGraduates.Text);


                string Imagename = string.Empty;
                if (fluploadImage.HasFile)
                {
                    Imagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fluploadImage.PostedFile.FileName);
                    //Imagename="Images\Photos\" +
                    fluploadImage.SaveAs(ConfigurationManager.AppSettings["ServerPath"] + "Images\\Profile\\" + Imagename);
                    university.Image = Imagename;
                }

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
                ClearAll();
                BindGrid();
                pnlUniversityInfo.Visible = true;
                pnlEditUniverSityInfo.Visible = false;
                pnlInterestedStudent.Visible = false;
                pnlInterestedStudentEdit.Visible = false;
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearAll();
            lblMsg.Text = "";
            pnlUniversityInfo.Visible = true;
            pnlEditUniverSityInfo.Visible = false;
            pnlInterestedStudent.Visible = false;
            pnlInterestedStudentEdit.Visible = false;

        }

        protected void lnkbtnFullinfoBack_Click(object sender, EventArgs e)
        {
            pnlUniversityInfo.Visible = true;
            pnlEditUniverSityInfo.Visible = false;
            pnlInterestedStudent.Visible = false;
            pnlInterestedStudentEdit.Visible = false;
            lblMsg.Text = "";
        }

        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;

            int id = Convert.ToInt32(GrvUniversityInfo.DataKeys[row.DataItemIndex].Value);

            bool status = chkStatus.Checked;

            University univer = universityBll.Get(id);
            if (univer != null)
            {
                User us = userBll.Get(univer.UserName);
                if (us != null)
                {
                    us.UserName = univer.UserName;
                    us.IsActive = status;
                    userBll.Save(us);

                }
            }
            BindGrid();
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

        protected void lnkBtnStudents_Click(object sender, EventArgs e)
        {
            pnlUniversityInfo.Visible = false;
            pnlEditUniverSityInfo.Visible = false;
            pnlInterestedStudent.Visible = true;
            pnlInterestedStudentEdit.Visible = false;

            string Username = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(Username))
            {
                hnduniUsername.Value = Username.ToString();
                lblUniversityId.Text = "University ID : " + Username.ToString();
                List<StudentInterest> studlist = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == Username).ToList();
                grvInterestedStudents.DataSource = studlist;
                grvInterestedStudents.DataBind();
            }
        }

        protected void lnkBtnStudentEdit_Click(object sender, EventArgs e)
        {
            pnlEditUniverSityInfo.Visible = false;
            pnlUniversityInfo.Visible = false;
            pnlInterestedStudent.Visible = false;
            pnlInterestedStudentEdit.Visible = true;
            string stdIntId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(stdIntId))
            {
                StudentInterest stdint = studentInterestBLL.GetAll().Where(x => x.StudentInterestId == Convert.ToInt32(stdIntId)).SingleOrDefault();
                if (stdint != null)
                {
                    ddlstudentStatus.SelectedValue = stdint.Approved.ToString();
                    hndStudentUserName.Value = stdint.StudentInterestId.ToString();
                }
            }
        }

        protected void lnkBtnStudentDelete_Click(object sender, EventArgs e)
        {
            string studIntId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(studIntId))
            {

                if (studentInterestBLL.delete(Convert.ToInt32(studIntId)))
                {
                    List<StudentInterest> studlist = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == hnduniUsername.Value).ToList();
                    grvInterestedStudents.DataSource = studlist;
                    grvInterestedStudents.DataBind();
                }

            }
        }

        protected void BtnSaveStudent_Click(object sender, EventArgs e)
        {

            StudentInterest stdint = studentInterestBLL.GetAll().Where(x => x.StudentInterestId == Convert.ToInt32(hndStudentUserName.Value)).SingleOrDefault();
            if (stdint != null)
            {
                stdint.Approved = Convert.ToInt32(ddlstudentStatus.SelectedValue);
                stdint.StudentInterestId = stdint.StudentInterestId;
                studentInterestBLL.Save(stdint);

                List<StudentInterest> studlist = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == stdint.UniversityUserName.ToString()).ToList();
                grvInterestedStudents.DataSource = studlist;
                grvInterestedStudents.DataBind();

                pnlUniversityInfo.Visible = false;
                pnlEditUniverSityInfo.Visible = false;
                pnlInterestedStudent.Visible = true;
                pnlInterestedStudentEdit.Visible = false;
            }
        }

        protected void BtnCancelstudent_Click(object sender, EventArgs e)
        {
            pnlUniversityInfo.Visible = false;
            pnlEditUniverSityInfo.Visible = false;
            pnlInterestedStudent.Visible = true;
            pnlInterestedStudentEdit.Visible = false;
        }

        protected void lnkbtnstudBack_Click(object sender, EventArgs e)
        {
            pnlUniversityInfo.Visible = true;
            pnlEditUniverSityInfo.Visible = false;
            pnlInterestedStudent.Visible = false;
            pnlInterestedStudentEdit.Visible = false;
        }

        protected void grvInterestedStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkBtnStudentEdit = ((LinkButton)e.Row.FindControl("lnkBtnStudentEdit"));
                HiddenField username = (HiddenField)e.Row.FindControl("HndstudentUserName");
                Label lblstdStatus = (Label)e.Row.FindControl("lblStudentStatus");
                lblstdStatus.Text = Enum.Parse(typeof(StudentInterestApproved), lblstdStatus.Text).ToString();
                if (username != null)
                {
                    Student std = studentBll.GetByUserName(username.Value);
                    if (std != null)
                    {
                        lnkBtnStudentEdit.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                    }
                }
            }
        }

        protected void GrvUniversityInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrvUniversityInfo.PageIndex = e.NewPageIndex;
            if (universityList == null)
                BindGrid();
            else
            {
                GrvUniversityInfo.DataSource = universityList;
                GrvUniversityInfo.DataBind();
                StringBuilder builder = new StringBuilder();
                foreach (string value in universityList.Select(x => x.UserName).ToArray())
                {
                    builder.Append(value);
                    builder.Append(',');
                }
                hdnAllRecipient.Value = builder.ToString();
                lblNoOfResult.Text = universityList.Count.ToString() + " Record Found";

                if (chkAll.Checked)
                {
                    for (int i = 0; i < GrvUniversityInfo.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)GrvUniversityInfo.Rows[i].FindControl("chkUni");
                        chk.Checked = true;
                    }
                }
            }
        }

        protected void grvInterestedStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInterestedStudents.PageIndex = e.NewPageIndex;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            universityList = null;
            if (chkIncompCost.Checked)
                universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => string.IsNullOrEmpty(x.BookFee) || string.IsNullOrEmpty(x.GraduateFee)||string.IsNullOrEmpty(x.BoardFee)||string.IsNullOrEmpty(x.UnderGraduateFee)).ToList());

            if (chkNoLargeImg.Checked)
                universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => string.IsNullOrEmpty(x.UniversityImage)).ToList());

            if (chkNoLogo.Checked)
                universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => string.IsNullOrEmpty(x.Image)).ToList());


            if (chkIsActive.Checked || chkIsNotActive.Checked)
            {
                string[] activeUserList = userBll.GetAll().Where(x => x.LoginTypeId == 2 && (x.IsActive == chkIsActive.Checked || x.IsActive == !chkIsNotActive.Checked)).Select(y => y.UserName).ToArray();
                universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => activeUserList.Contains(x.UserName)).ToList());
            }
            if (chnIncompEnrollNo.Checked)
            {
                universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => x.Graduates == null || x.UnderGraduates == null || x.InternationalGraduate == null).ToList());
            }


            string fromcntry = "";
            if (ddlFromCountry.SelectedIndex != 0)
                fromcntry = ddlFromCountry.SelectedItem.Text;
            if (universityList != null)
                universityList = universityList.Where(x => x.Country.Contains(fromcntry)).ToList();
            else
                universityList = universityBll.GetAll().Where(x => x.Country.Contains(fromcntry)).ToList();




            StringBuilder builder = new StringBuilder();
            foreach (string value in universityList.Select(x => x.UserName).ToArray())
            {
                builder.Append(value);
                builder.Append(',');
            }
            hdnAllRecipient.Value = builder.ToString();

            GrvUniversityInfo.DataSource = universityList;
            GrvUniversityInfo.DataBind();
            lblNoOfResult.Text = universityList.Count.ToString() + " Records Found";
        }

        private List<University> getCommonElements(List<University> list1, List<University> list2)
        {
            List<University> tmplist = new List<University>();
            if (list1 != null)
                tmplist = list1.Where(x => list2.Select(y => y.UserName).ToArray().Contains(x.UserName)).ToList();
            else
                tmplist = list2;
            return tmplist;
        }

        StudentTestBLL studentTestBll = new StudentTestBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StudentBLL studentBll = new StudentBLL();
            StudentAcademicsBLL studacd = new StudentAcademicsBLL();
            int id = 0;
            if (chkAll.Checked)
            {
                string[] arr = hdnAllRecipient.Value.Split(',');
                List<Student> studentList = studentBll.GetAll();

                foreach (string tmp in arr)
                {
                    universityBll.deleteByUserName(tmp);
                }
            }
            else
            {
                foreach (GridViewRow r in GrvUniversityInfo.Rows)
                {
                    CheckBox chk = (CheckBox)r.FindControl("chkUni");
                    if (chk.Checked)
                    {
                        string user = ((HiddenField)r.FindControl("lnkBtnShowDetails")).Value;
                        universityBll.deleteByUserName(user);
                    }
                }
            }
            BindGrid();
        }
        #endregion

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
            return msg.MessageId.ToString();
        }

        //For Full information of University
        [WebMethod()]
        public static string[] GetUnivercityData(string UniversityUserName)
        {
            //string str=string.Empty;
            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            StringBuilder sb = new StringBuilder();
            string[] strArr = new string[15];
            string[] tmp;
            string[] tmpCourseArr;
            university = universityBll.GetByUserName(UniversityUserName);
            if (university != null)
            {
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
                if (university.UniversityName != null)
                    strArr[14] = university.UniversityName;
                return strArr;
            }
            else
            {
                return strArr;
            }
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


        static List<Student> staticStudentList = null;

        private static string getStudentListtag(List<Student> student)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < student.Count; i++)
            {
                sb.Append("<li>");
                sb.Append("<div class='user_img'>");
                if (student[i].Photo == null)
                    sb.Append("<img src='../Images/no_image.jpg' />");
                else
                    sb.Append("<img src='../Images/Profile/" + student[i].Photo + "' />");

                sb.Append("</div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<label>");
                sb.Append("<a id='" + student[i].UserName + "' class='msg_conversation_open_click'>");
                sb.Append(student[i].FirstName + " " + student[i].LastName);
                sb.Append("</a>");
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            return sb.ToString();
        }

        [WebMethod()]
        public static string retriveStudentList(string university)
        {
            List<Student> tmpList = new List<Student>();
            staticStudentList = new List<Student>();


            StudentBLL studentBll = new StudentBLL();
            StudentInterestBLL studentInterest = new StudentInterestBLL();
            MessageBLL msgbll = new MessageBLL();
            List<Message> tmpmsgList = new List<Message>();
            tmpmsgList = msgbll.GetAll();
            string[] recentMsgUser = tmpmsgList
                                          .Where(
                                           x => (x.ToUserName == university) && !(x.FromUserName.Contains("admin"))).OrderBy(x => x.MessageId).Select(y => y.FromUserName).ToArray();



            string[] usernames = studentInterest.GetAll().Where(x => x.UniversityUserName == university && x.Approved == 2).Select(y => y.StudentUserName).ToArray();

            usernames = recentMsgUser.Concat(usernames).Distinct().ToArray();


            List<Student> tmpstud = new List<Student>();
            tmpstud = studentBll.GetAll();
            Student stmp = new Student();

            foreach (string user in usernames)
            {
                stmp = tmpstud.Where(x => x.UserName == user).FirstOrDefault();
                if (stmp != null)
                    staticStudentList.Add(stmp);
            }
            //staticStudentList = tmpstud.Where(x => usernames.Contains(x.UserName)).ToList();

            if (staticStudentList.Count == 0)
                return "no";

            if (staticStudentList.Count < 8)
            {
                tmpList = staticStudentList.ToList();
                staticStudentList.RemoveAll(x => 1 == 1);
            }
            else
            {
                tmpList = staticStudentList.GetRange(0, 8);
                staticStudentList.RemoveRange(0, 8);
            }

            return getStudentListtag(tmpList);
        }

        [WebMethod()]
        public static string retriveConversationBetweenUni(string studentusername, string university)
        {
            MessageBLL msgBll = new MessageBLL();

            List<Message> msgList = msgBll.GetAll()
                                          .Where(
                                           x => (x.ToUserName == studentusername && x.FromUserName == university && x.IsApproved == true) ||
                                           (x.FromUserName == studentusername && x.ToUserName == university && x.IsApproved == true)).OrderBy(x => x.MessageId).ToList();

            StringBuilder sb = new StringBuilder();
            UniversityBLL uniBll = new UniversityBLL();
            StudentBLL studBll = new StudentBLL();

            List<University> universityList = uniBll.GetAll();
            List<Student> studList = studBll.GetAll();
            Student tmpStudent = new Student();
            University tmpUni = new University();



            string date = "", name = "";
            for (int i = 0; i < msgList.Count; i++)
            {
                if (studList.Select(x => x.UserName).ToArray().Contains(msgList[i].FromUserName))
                {
                    tmpStudent = studList.Where(x => x.UserName == msgList[i].FromUserName).FirstOrDefault();
                    if (tmpStudent != null)
                    {
                        name = tmpStudent.FirstName + " " + tmpStudent.LastName;
                    }
                }
                else
                {
                    tmpUni = universityList.Where(x => x.UserName == msgList[i].FromUserName).FirstOrDefault();
                    if (tmpUni != null)
                    {
                        name = tmpUni.UniversityName;
                    }
                }

                sb.Append("<li>");
                //sb.Append("<div class='user_img'>");
                //sb.Append("<img src='"+img+"' />");
                //sb.Append("</div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<label>");
                sb.Append("<a>");
                sb.Append(name + " says:");
                sb.Append("</a>");
                sb.Append("</label>");
                sb.Append("<i>");
                sb.Append(msgList[i].CreatedDate.ToString("dd/MM/yyyy"));
                sb.Append("</i>");
                sb.Append("<br/><br/><p>");
                sb.Append(msgList[i].Description);
                sb.Append("</p>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            return sb.ToString();
        }


        [WebMethod]
        public static string AppendAndDisplayStudent()
        {
            if (staticStudentList != null)
            {
                if (staticStudentList.Count != 0)
                {
                    List<Student> tmplist = new List<Student>();
                    if (staticStudentList.Count < 8)
                    {
                        tmplist = staticStudentList.ToList();
                        staticStudentList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmplist = staticStudentList.GetRange(0, 8);
                        staticStudentList.RemoveRange(0, 8);
                    }
                    return getStudentListtag(tmplist);
                }
                else
                {
                    return "no";
                }
            }
            else
                return "";
        }

        protected void GrvUniversityInfo_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            List<University> universitylist = new List<University>();
            // List<Project> projectlist = projectBll.GetAll().Where(x => x.UserName == CookieHelper.Username).ToList();

            if (GetSortDirection() == "ASC")
            {
                universitylist = universityBll.GetAll().Where(x => x.User.IsActive == true).OrderBy(x => x.UniversityName).ToList();
            }
            else
            {
                universitylist = universityBll.GetAll().Where(x => x.User.IsActive == true).OrderByDescending(x => x.UniversityName).ToList();
            }


            GrvUniversityInfo.DataSource = universitylist;
            GrvUniversityInfo.DataBind();
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