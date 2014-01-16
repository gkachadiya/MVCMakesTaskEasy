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
using System.Text;

namespace SpotCollege.Account
{
    public partial class ProfileOverView : System.Web.UI.Page
    {
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();
        Student student;
        StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
        AlertBLL alertBll = new AlertBLL();
        StudentTestBLL studentTestBLL = new StudentTestBLL();
        UniversityBLL universityBLL = new UniversityBLL();
        string universityname = "";

        protected void Page_Init(Object sender, EventArgs e)
        {
            if (Request.QueryString["intu"] != null)
            {
                hdnQueryUID.Value = studentBll.Get(Convert.ToInt32(Request.QueryString["intu"])).UserName;
                GrdInternationalTest.Columns[GrdInternationalTest.Columns.Count - 1].Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["intu"] != null)
            {
                student = studentBll.Get(Convert.ToInt32(Request.QueryString["intu"]));
                lnkEditBasicInfo.Visible = false;
                lnkEducationPreferences.Visible = false;
                lnkPhotoEdit.Visible = false;
                lnkEditCurrentAcademics.Visible = false;
                lnkWorkhistory.Visible = false;
                lnkExtraCurricularActivies.Visible = false;
                lnkInternationalTest.Visible = false;

                if (Request.QueryString["dec"] == "show")
                {
                    lnkBtnApprove.Visible = true;
                    lnkBtnDecline.Visible = true;
                }
                else if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                {
                    lnkBtnApprove.Visible = false;
                    lnkBtnDecline.Visible = false;
                }
                else
                {
                    lnkBtnApprove.Visible = true;
                    lnkBtnDecline.Visible = true;
                }

            }
            else
            {
                student = studentBll.GetByUserName(CookieHelper.Username);
                lnkEditBasicInfo.Visible = true;
                lnkEducationPreferences.Visible = true;
                lnkPhotoEdit.Visible = true;
                lnkEditCurrentAcademics.Visible = true;
                lnkWorkhistory.Visible = true;
                lnkExtraCurricularActivies.Visible = true;
                lnkInternationalTest.Visible = true;


            }

            if (student != null)
            {
                //Load Basic Detail

                lblFirstName.Text = student.FirstName;
                lblMiddleName.Text = student.MiddleName;
                lblLastName.Text = student.LastName;
                lblAddress1.Text = student.Address1;
                lblAddress2.Text = student.Address2;
                lblzipcode.Text = student.ZipCode;
                lblCity.Text = student.City;
                if (student.Country != null && student.Country != "")
                {

                    lblCountry.Text = student.Country.ToString();
                }
                else
                {
                    lblCountry.Text = "";
                }
                if (student.PrimaryType != null && student.PrimaryType != "")
                {
                    lblPrimaryNumber.Text = "(" + Enum.Parse(typeof(PhoneTypes), student.PrimaryType).ToString() + ") " + student.PrimaryNumber;
                }
                if (student.SecondaryType != null)
                    lblSecondaryNumber.Text = "(" + Enum.Parse(typeof(PhoneTypes), student.SecondaryType).ToString() + ") " + student.SecondaryNumber;
                else
                    lblSecondaryNumber.Text = student.SecondaryNumber;
                lblPrimaryEmail.Text = student.PrimaryEmail;
                if (student.BirthDate != null)
                {
                    lblBirthDate.Text = Convert.ToDateTime(student.BirthDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    lblBirthDate.Text = "";
                }
                if (student.Citizenship != null && student.Citizenship != "")
                {
                    // int val = (int)(EnumHelper.GetEnumValueFromDescription<CountryList>(student.Citizenship));
                    lblCountryOfCitizenship.Text = student.Citizenship.ToString();
                }
                else
                {
                    lblCountryOfCitizenship.Text = "";
                }

                //Bind International Test
                bindInternationalTestGrid(student.StudentId);

                //Bind Academic

                //StudentAcademic stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                //if (stdAca != null)
                //{
                bindAcademicGrid(student.StudentId);
                //}
                //else
                //{
                //    Response.Redirect("EditProfile.aspx?sec=Academics");
                //}

                //Load Education Preferences
                if (student.StudyingIn != 0)
                {
                    lblCurrentlyIn.Text = EnumHelper.GetDescriptionFromEnumValue((CurrentlyIn)student.StudyingIn).ToString();
                    //  lblCurrentlyIn.Text = Enum.Parse(typeof(CurrentlyIn), student.StudyingIn.ToString()).ToString();
                }


                if (student.LookingFor != 0)
                {

                    lblLookingFor.Text = EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)student.LookingFor).ToString();
                    //lblLookingFor.Text = Enum.Parse(typeof(CountryList), student.LookingFor.ToString()).ToString();
                }

                if (student.LookingForCountry != "")
                {
                    lblPreferStudyIn.Text = student.LookingForCountry.ToString();
                }

                if (student.StartDate != 0)
                {
                    lblExpectedStartDate.Text = EnumHelper.GetDescriptionFromEnumValue((expectedStart)student.StartDate).ToString();
                }

                if (student.Payout == 1)
                {
                    lblPayout.Text += "3000-5000";
                }
                else if (student.Payout == 2)
                {
                    lblPayout.Text += "5000-10000";
                }
                else if (student.Payout == 3)
                {
                    lblPayout.Text += "10000-15000";
                }
                else if (student.Payout == 4)
                {
                    lblPayout.Text += "15000+";
                }
                if (student.DesiredFieldofStudy != "")
                {
                    lbldesiredfieldstudy.Text = student.DesiredFieldofStudy;
                }
                //Load Photo
                if (student.Photo != null)
                {
                    imgProfileImage.Visible = true;
                    imgProfileImage.Src = "../Images/Profile/" + student.Photo;
                    hoverimg.HRef = "../Images/Profile/" + student.Photo;
                }
                else
                {
                    imgProfileImage.Visible = false;
                }

                //Bind Work History
                bindWorkHistoryGrid(student.StudentId);

                //Bind Extra Curricular Activies
                lblSports.Text = student.SportActivities;
                lblLeadership.Text = student.LeadershipActivies;
                lblOtherActivities.Text = student.OtherActivities;
            }
            else
            {
                CurrentAcademics.Visible = false;
                WorkHistory.Visible = false;
            }

        }

        protected void lnkEditBasicInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=basic");
        }

        protected void lnkEditCurrentAcademics_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=Academics");
        }

        void bindAcademicGrid(int studentId)
        {
            List<StudentAcademic> AcademicList = academicBll.GetAll().Where(x => x.StudentId == studentId).ToList();
            grvAcademicDetails.DataSource = AcademicList;
            grvAcademicDetails.DataBind();
        }

        void bindInternationalTestGrid(int StudentId)
        {
            List<StudentTest> TestList = studentTestBLL.GetAll().Where(x => x.StudentId == StudentId).ToList();
            GrdInternationalTest.DataSource = TestList;
            GrdInternationalTest.DataBind();
        }
        protected void lnkEducationPreferences_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=EducationPreferences");
        }

        protected void lnkPhotoEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=Photo");

        }

        protected void lnkWorkhistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=Workhistory");
        }

        void bindWorkHistoryGrid(int StudentId)
        {
            List<StudentWorkHistory> WorkHistoryList = workHistoryBll.GetAll().Where(x => x.StudentId == StudentId).ToList();
            grvWorkHistory.DataSource = WorkHistoryList;
            grvWorkHistory.DataBind();
        }

        protected void lnkExtraCurricularActivies_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=Curricular");
        }

        protected void lnkEditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=basic");
        }

        //protected void lnkBtnApprove_Click(object sender, EventArgs e)
        //{
        //    StudentInterest std = studentInterestBLL.GetAll().Where(x => x.StudentUserName == Request.QueryString["intu"] && x.UniversityUserName == CookieHelper.Username).FirstOrDefault();
        //    if (std != null)
        //    {
        //        std.StudentUserName = std.StudentUserName;
        //        std.StudentInterestId = std.StudentInterestId;
        //        std.Approved = 2;
        //        studentInterestBLL.Save(std);
        //        lnkBtnApprove.Visible = false;
        //    }
        //    universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
        //    Alert alert = new Alert();
        //    alert.Message = universityname + " has Approved";
        //    alert.CreatedDate = DateTime.Now;
        //    alert.CreatedBy = CookieHelper.Username;
        //    alert.UserName = Request.QueryString["intu"];
        //    alertBll.Save(alert);
        //}

        protected void lnkBtnDecline_Click(object sender, EventArgs e)
        {
            Student student = studentBll.Get(Convert.ToInt32(Request.QueryString["intu"]));
            if (student != null)
            {
                StudentInterest std = studentInterestBLL.GetAll().Where(x => x.StudentUserName == student.UserName && x.UniversityUserName == CookieHelper.Username).FirstOrDefault();
                if (std != null)
                {
                    std.StudentUserName = std.StudentUserName;
                    std.StudentInterestId = std.StudentInterestId;
                    std.Approved = 3;
                    studentInterestBLL.Save(std);
                    lnkBtnDecline.Visible = false;
                }
                universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
                Alert alert = new Alert();
                alert.Message = universityname + " has Decline";
                alert.CreatedDate = DateTime.Now;
                alert.CreatedBy = CookieHelper.Username;
                alert.UserName = student.UserName;
                alertBll.Save(alert);
            }
        }
        protected void lnkInternationalTest_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx?sec=IntTest");
        }

        [WebMethod()]
        public static object TestDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return JsonConvert.SerializeObject(str);
        }

        protected void GrdInternationalTest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbltestName = (Label)e.Row.FindControl("lblTestname");
                HiddenField hndStdTestID = (HiddenField)e.Row.FindControl("hndStudentTestId");

                if (lbltestName != null)
                {
                    StudentTest stdTest = studentTestBLL.Get(Convert.ToInt32(hndStdTestID.Value));
                    int testId = Convert.ToInt32(lbltestName.Text);

                    lbltestName.Text = EnumHelper.GetDescriptionFromEnumValue((InternationalTestRecord)testId) + "-" + stdTest.Date;

                }
            }
        }

        [WebMethod()]
        public static string Getstatus(string ToUserName)
        {
            Message msg = new Message();
            MessageBLL messageBLL = new MessageBLL();
            msg = messageBLL.GetAll().Where(x => x.ToUserName == ToUserName && x.FromUserName == CookieHelper.Username || x.ToUserName == CookieHelper.Username && x.FromUserName == ToUserName && x.ParentId == 0).FirstOrDefault();
            if (msg != null)
                return msg.MessageId.ToString();
            else
                return string.Empty;
        }

        [System.Web.Services.WebMethod]
        public static String[] GetAllConversation(string messageId)
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
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + std.Photo + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + std.FirstName + " " + std.LastName + "</label></div>");

                    builder_1.Append("<div class='std_time_country'><span>In " + std.City + "," + std.Country + "</span></div></li>");

                }
                else if (toUserType == "2")
                {
                    //when user is university
                    uni = universityBLL.GetByUserName(toUser);
                    if (!string.IsNullOrEmpty(uni.Image))
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + uni.Image + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + uni.UniversityName + "</label></div>");
                    builder_1.Append("<div class='std_time_country'><span>In " + uni.City + "," + uni.Country + "</span></div></li>");
                }
                else
                {
                    builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
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
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + std.Photo + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + std.FirstName + " " + std.LastName + "</label></div>");
                    builder_1.Append("<div class='std_time_country'><span>In " + std.City + "," + std.Country + "</span></div></li>");

                }
                else if (fromUserType == "2")
                {
                    //when user is university
                    uni = universityBLL.GetByUserName(fromUser);
                    if (!string.IsNullOrEmpty(uni.Image))
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + uni.Image + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + uni.UniversityName + "</label></div>");
                    builder_1.Append("<div class='std_time_country'><span>In " + uni.City + "," + uni.Country + "</span></div></li>");
                }
                else
                {
                    builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>Administrator</label></div>");
                    builder_1.Append("</li>");
                }

                #endregion

                builder.Append("<div class='h2_heading'><h2>" + ParentMessage.Title + "</h2></div>");
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
                            builder.Append(" <span class='date'>" + msgList[i].CreatedDate + "</span>");
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
                            builder.Append(" <span class='date'>" + msgList[i].CreatedDate + "</span>");
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
                        builder.Append(" <span class='date'>" + msgList[i].CreatedDate + "</span>");
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
            String[] strarr = new String[2];
            strarr[0] = builder.ToString();
            strarr[1] = builder_1.ToString();
            return strarr;
        }

        [WebMethod()]
        public static string SendMessage(string Title, string Description, string sendToUserName, string ParentId)
        {
            UniversityBLL universityBLL = new UniversityBLL();
            AlertBLL alertBll = new AlertBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            string str = string.Empty;

            StudentInterest std = studentInterestBLL.GetAll().Where(x => x.StudentUserName == sendToUserName && x.UniversityUserName == CookieHelper.Username).FirstOrDefault();
            if (std != null)
            {
                std.StudentUserName = std.StudentUserName;
                std.StudentInterestId = std.StudentInterestId;
                std.Approved = 2;
                studentInterestBLL.Save(std);
                //lnkBtnApprove.Visible = false;
            }
           string universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
            Alert alert = new Alert();
            alert.Message = universityname + " has Approved";
            alert.CreatedDate = DateTime.Now;
            alert.CreatedBy = CookieHelper.Username;
            alert.UserName = sendToUserName;
            alertBll.Save(alert);



            MessageBLL messageBLL = new MessageBLL();
            SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
            msg.Title = Title;
            msg.Description = Description;
            msg.ParentId = Convert.ToInt32(ParentId);
            msg.FromUserName = CookieHelper.Username;
            msg.ToUserName = sendToUserName;
            msg.IsRead = false;
            msg.IsApproved = false;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);
            return msg.MessageId.ToString();
        }


        [WebMethod]
        public static string SaveMessageReply(string parentMsgId, string msgDesc, string msgTitle, string fromusername, string tousername)
        {
            MessageBLL messageBLL = new MessageBLL();

            Message msgdata = messageBLL.Get(Convert.ToInt32(parentMsgId));
            if (msgdata != null)
            {
                Message msg = new Message();
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
                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                return builder.ToString();
            }
            else
            {
                builder.Append("<li class='admin'><div class='page_collapsible' id='first'></div>");
                builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                builder.Append(" <div class='list_2_detail'><span>Administrator</span>");
                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                return builder.ToString();
            }
        }
    }
}