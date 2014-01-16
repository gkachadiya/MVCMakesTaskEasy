using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.DAL;
using SpotCollege.Common;
using SpotCollege.BLL;
using System.Web.Services;
using System.Text;

namespace SpotCollege
{
    public partial class AllMessage : System.Web.UI.Page
    {
        public static List<Message> msgStaticList = new List<Message>();
        static int NoOfMsgToDisplay = 7;
        StudentBLL studentBll = new StudentBLL();
        MessageBLL messageBLL = new MessageBLL();
        UserBLL userBLL = new UserBLL();
        User user = new User();
        List<User> tmpUserList = new List<User>();


        static string[] universityUser = { };
        static string[] students = { };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                lnkFromUniversity.Visible = false;
                tmpUserList = userBLL.GetAll();
                universityUser = tmpUserList.Where(x => x.LoginTypeId == 2).Select(y => y.UserName).ToArray();
                students = tmpUserList.Where(x => x.LoginTypeId == 1).Select(y => y.UserName).ToArray();
                BindStudentShownList("students");
            }

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
                    tmpUserList = userBLL.GetAll();
                    universityUser = tmpUserList.Where(x => x.LoginTypeId == 2).Select(y => y.UserName).ToArray();
                    students = tmpUserList.Where(x => x.LoginTypeId == 1).Select(y => y.UserName).ToArray();
                    BindStudentShownList("students");
                }
                else
                {
                    Response.Redirect("Account/EditProfile.aspx?sec=basic");
                }

                //List<Message> msgalertUni = messageBLL.GetAll().Where(x => x.ToUserName == CookieHelper.Username && x.IsRead == false && x.User.LoginTypeId == 2).ToList();
                //List<Message> msgalertStudent = messageBLL.GetAll().Where(x => x.ToUserName == CookieHelper.Username && x.IsRead == false && x.User.LoginTypeId == 1).ToList();
                //lblNewMessageFromUniversity.Text = "You have " + msgalertUni.Count + " new messages from Universities";
                //lblNewMessageFromStudent.Text = "You have " + msgalertStudent.Count + " new messages from OtherStudents";
            }
        }

        void BindStudentShownList(string fromuser)
        {
            lblrec.Visible = false;
            List<Message> msgList = messageBLL.GetAll().Where(x => x.FromUserName == CookieHelper.Username
                                                                && x.ParentId == 0 && x.IsApproved == true

                                                                ||

                                                                x.ToUserName == CookieHelper.Username
                                                                && x.ParentId == 0
                                                                && x.IsApproved == true)

                                                                .OrderByDescending(x => x.MessageId).ToList();

            List<Message> msg = messageBLL.GetAll().Where(x => !string.IsNullOrEmpty(x.IsApproved.ToString()) && x.ToUserName == CookieHelper.Username && x.IsRead == false && x.IsApproved == true).ToList();

            int std, uni, admin;
            std = msg.Where(x => students.Contains(x.FromUserName) && x.IsRead == false).ToList().Count;
            uni = msg.Where(x => universityUser.Contains(x.FromUserName) && x.IsRead == false).ToList().Count;
            admin = msg.Where(x => x.FromUserName == "admin@gmail.com" && x.IsRead == false).ToList().Count;

            if (std != 0)
                lnkFromStudents.Text = "Messages From Students (" + std.ToString() + ")";
            else
                lnkFromStudents.Text = "Messages From Students";

            if (uni != 0)
                lnkFromUniversity.Text = "Messages From University (" + uni.ToString() + ")";
            else
                lnkFromUniversity.Text = "Messages From University";

            if (admin != 0)
                lnkFromAdmin.Text = "Messages From Admin (" + admin.ToString() + ")";
            else
                lnkFromAdmin.Text = "Messages From Admin";

            if (fromuser == "students")
            {


                if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    msgList = msgList.Where(x => students.Contains(x.ToUserName)).ToList();
                }
                else
                    msgList = msgList.Where(x => students.Contains(x.FromUserName)).ToList();
            }
            else if (fromuser == "university")
            {
                msgList = msgList.Where(x => universityUser.Contains(x.FromUserName)).ToList();
            }
            else if (fromuser == "admin")
            {
                msgList = msgList.Where(x => (x.FromUserName == "admin@gmail.com")).ToList();
            }
            else
            {
                msgList = msgList.Where(x => x.Title.Contains(txtSearch.Text.Trim())).ToList();
            }

            msgStaticList = msgList.ToList(); //StaticUniversityList will be used for displaying record
            if (msgList.Count < NoOfMsgToDisplay)
                msgStaticList.RemoveAll(x => 1 == 1);
            else
                msgStaticList.RemoveRange(0, NoOfMsgToDisplay);

            dlStudentlist.DataSource = msgList.Take(NoOfMsgToDisplay);
            dlStudentlist.DataBind();

            dlStudentlist1.DataSource = msgList.Take(NoOfMsgToDisplay);
            dlStudentlist1.DataBind();

            if (dlStudentlist.Items.Count == 0)
            {
                lblrec.Visible = true;
            }

        }

        protected void dlStudentlist_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName1");
                Label lblStudentname = (Label)e.Item.FindControl("lblStudentname");
                Label lblChildMsgCounting = (Label)e.Item.FindControl("lblChildMsgCounting");
                Image img = (Image)e.Item.FindControl("ImgBtnPhoto1");
                int n = Convert.ToInt32(lblChildMsgCounting.Text);

                List<Message> msglist = messageBLL.GetAll().Where(x => x.ParentId == Convert.ToInt32(lblChildMsgCounting.Text) || x.MessageId == Convert.ToInt32(lblChildMsgCounting.Text)).ToList();
                lblChildMsgCounting.Text = "(" + msglist.Count.ToString() + ")";

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    object row_items = e.Item.DataItem;
                    object items = e.Item.DataItem;
                    string FromUserName = Convert.ToString(DataBinder.Eval(row_items, "FromUserName"));


                    //Check for Admin
                    if (FromUserName == "admin@admin.com" || FromUserName == "admin@gmail.com")
                    {
                        img.ImageUrl = "~/Images/no_image.jpg";

                        List<Message> msgalertStudent = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username && x.ParentId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true) ||

                            (x.ToUserName == CookieHelper.Username && x.MessageId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true)
                            ).ToList();

                        if (msgalertStudent.Count != 0)
                        {
                            lblStudentname.Text = "Administrator";
                            lblStudentname.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblStudentname.Text = "Administrator";
                        }
                    }


                    if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                    {
                        Student std = studentBll.GetByUserName(username.Value);
                        if (std != null)
                        {
                            if (std.Photo != null)
                            {
                                img.ImageUrl = "Images/Profile/" + std.Photo;
                            }
                            else
                            {
                                img.ImageUrl = "Images/no_image.jpg";
                            }

                            List<Message> msgalertStudent = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username && x.ParentId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true) ||

                           (x.ToUserName == CookieHelper.Username && x.MessageId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true)
                           ).ToList();
                            if (msgalertStudent.Count != 0)
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                                lblStudentname.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                            }
                        }
                    }
                    else
                    {
                        //check for student data.
                        Student std = studentBll.GetByUserName(FromUserName);
                        if (std != null)
                        {
                            if (std.Photo != null)
                            {
                                img.ImageUrl = "Images/Profile/" + std.Photo;
                            }
                            else
                            {
                                img.ImageUrl = "Images/no_image.jpg";
                            }

                            List<Message> msgalertStudent = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username && x.ParentId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true) ||

                          (x.ToUserName == CookieHelper.Username && x.MessageId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true)
                          ).ToList();
                            if (msgalertStudent.Count != 0)
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                                lblStudentname.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                            }
                        }
                        else
                        {
                            //check for university data
                            UniversityBLL universityBLL = new UniversityBLL();
                            University uni = universityBLL.GetByUserName(FromUserName);
                            if (uni != null)
                            {
                                if (uni.Image != null)
                                {
                                    img.ImageUrl = "Images/Profile/" + uni.Image;
                                }
                                else
                                {
                                    img.ImageUrl = "Images/no_image.jpg";
                                }
                                List<Message> msgalertStudent = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username && x.ParentId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true) ||

                           (x.ToUserName == CookieHelper.Username && x.MessageId == n && x.IsRead == false && x.FromUserName == FromUserName && x.IsApproved == true)
                           ).ToList();
                                if (msgalertStudent.Count != 0)
                                {
                                    lblStudentname.Text = uni.UniversityName;
                                    lblStudentname.ForeColor = System.Drawing.Color.Green;
                                }
                                else
                                {
                                    lblStudentname.Text = uni.UniversityName;

                                }
                            }


                        }
                    }
                }
            }
        }

        protected void dlStudentlist_ItemDataBound1(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName11");
                Label lblStudentname = (Label)e.Item.FindControl("lblStudentname1");
                Image img = (Image)e.Item.FindControl("ImgBtnPhoto11");

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    object row_items = e.Item.DataItem;
                    object items = e.Item.DataItem;
                    string FromUserName = Convert.ToString(DataBinder.Eval(row_items, "FromUserName"));

                    //Check for Admin
                    if (FromUserName == "admin@admin.com" || FromUserName == "admin@gmail.com")
                    {
                        img.ImageUrl = "~/Images/no_image.jpg";
                        lblStudentname.Text = FromUserName;
                    }


                    if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                    {
                        Student std = studentBll.GetByUserName(username.Value);
                        if (std != null)
                        {
                            if (std.Photo != null)
                            {
                                img.ImageUrl = "Images/Profile/" + std.Photo;
                            }
                            else
                            {
                                img.ImageUrl = "Images/no_image.jpg";
                            }

                            List<Message> msgalertStudent = messageBLL.GetAll().Where(x => x.ToUserName == CookieHelper.Username && x.IsRead == false && x.FromUserName == FromUserName).ToList();

                            if (msgalertStudent.Count != 0)
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                                lblStudentname.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                            }
                        }
                    }
                    else
                    {
                        //check for student data.
                        Student std = studentBll.GetByUserName(FromUserName);
                        if (std != null)
                        {
                            if (std.Photo != null)
                            {
                                img.ImageUrl = "Images/Profile/" + std.Photo;
                            }
                            else
                            {
                                img.ImageUrl = "Images/no_image.jpg";
                            }

                            List<Message> msgalertStudent = messageBLL.GetAll().Where(x => x.ToUserName == CookieHelper.Username && x.IsRead == false && x.FromUserName == FromUserName).ToList();

                            if (msgalertStudent.Count != 0)
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                                lblStudentname.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                            }
                        }
                        else
                        {
                            //check for university data
                            UniversityBLL universityBLL = new UniversityBLL();
                            University uni = universityBLL.GetByUserName(FromUserName);
                            if (uni != null)
                            {
                                if (uni.Image != null)
                                {
                                    img.ImageUrl = "Images/Profile/" + uni.Image;
                                }
                                else
                                {
                                    img.ImageUrl = "Images/no_image.jpg";
                                }
                                List<Message> msgalertStudent = messageBLL.GetAll().Where(x => x.ToUserName == CookieHelper.Username && x.IsRead == false && x.FromUserName == FromUserName).ToList();

                                if (msgalertStudent.Count != 0)
                                {
                                    lblStudentname.Text = uni.UniversityName;
                                    lblStudentname.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    lblStudentname.Text = uni.UniversityName;

                                }
                            }


                        }
                    }

                }
            }
        }

        protected void lnkBtnTitle_Click(object sender, EventArgs e)
        {
            string MessageId = ((LinkButton)sender).CommandArgument.ToString();
            if (!string.IsNullOrEmpty(MessageId))
            {
                Message mRead = messageBLL.Get(Convert.ToInt32(MessageId));
                List<Message> mli = messageBLL.GetAll().Where(x => x.ParentId == mRead.MessageId || x.MessageId == mRead.MessageId).ToList();
                foreach (Message msgRead in mli)
                {
                    msgRead.MessageId = msgRead.MessageId;
                    msgRead.Title = msgRead.Title;
                    msgRead.Description = msgRead.Description;
                    msgRead.ParentId = msgRead.ParentId;
                    msgRead.FromUserName = msgRead.FromUserName;
                    msgRead.ToUserName = msgRead.ToUserName;
                    msgRead.IsRead = true;
                    msgRead.IsApproved = msgRead.IsApproved;
                    messageBLL.Save(msgRead);
                }
                Response.Redirect("StudentMessage.aspx?id=" + MessageId);
            }
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


        //METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST
        [WebMethod()]
        public static string AppendAndDisplayMessage()
        {
            StringBuilder sb = new StringBuilder();
            List<Message> tmpMessageList = new List<Message>();
            if (msgStaticList != null)
            {
                if (msgStaticList.Count != 0)
                {
                    if (msgStaticList.Count < NoOfMsgToDisplay)
                    {
                        tmpMessageList = msgStaticList.ToList();
                        msgStaticList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpMessageList = msgStaticList.GetRange(0, NoOfMsgToDisplay);
                        msgStaticList.RemoveRange(0, NoOfMsgToDisplay);
                    }
                    string img;
                    Student stdt = new Student();
                    StudentBLL studentbll = new StudentBLL();
                    MessageBLL messageBLL = new MessageBLL();
                    for (int i = 0; i < tmpMessageList.Count; i++)
                    {
                        int msgCount = tmpMessageList.Where(x => x.ParentId == tmpMessageList[i].MessageId || x.MessageId == tmpMessageList[i].MessageId).ToList().Count;
                        int n = tmpMessageList[i].MessageId;
                        //for Admin
                        if (tmpMessageList[i].FromUserName == "admin@admin.com" || tmpMessageList[i].FromUserName == "admin@gmail.com")
                        {
                            List<Message> msgalertStudent = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username && x.ParentId == n && x.IsRead == false && x.FromUserName == tmpMessageList[i].FromUserName && x.IsApproved == true) ||

                          (x.ToUserName == CookieHelper.Username && x.MessageId == n && x.IsRead == false && x.FromUserName == tmpMessageList[i].FromUserName && x.IsApproved == true)
                          ).ToList();

                            img = @"Images\no_image.jpg";
                            sb.Append("<span><li><div class='user_img'><img src='" + img + "' /></div>");
                            sb.Append("<div class='user_name'><label><a href='#'>");
                            sb.Append("<span> (" + msgCount + ")</span>");
                            if (msgalertStudent.Count != 0)
                                sb.Append("<span style='color:Green;'>Administrator</span></a></label><br>");
                            else
                                sb.Append("<span>Administrator</span></a></label><br>");
                            sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                            sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                            sb.Append("<p>" + tmpMessageList[i].Description);
                            sb.Append("<a id='" + tmpMessageList[i].MessageId + "' class='msg_replay_open_click button' style='float:right' href='#'>View message and Respond</a></p></div></li></span>");
                        }

                        List<Message> msgalert = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username && x.ParentId == n && x.IsRead == false && x.FromUserName == tmpMessageList[i].FromUserName && x.IsApproved == true) ||

                         (x.ToUserName == CookieHelper.Username && x.MessageId == n && x.IsRead == false && x.FromUserName == tmpMessageList[i].FromUserName && x.IsApproved == true)
                         ).ToList();
                        if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                        {
                            if (tmpMessageList[i].ToUserName != "admin@gmail.com")
                            {
                                stdt = studentbll.GetByUserName(tmpMessageList[i].ToUserName);
                                if (stdt != null)
                                {
                                    if (stdt.Photo == null)
                                        img = @"Images\no_image.jpg";
                                    else
                                        img = @"Images\Profile\" + stdt.Photo;

                                    sb.Append("<span><li><div class='user_img'><img src='" + img + "' /></div>");
                                    sb.Append("<div class='user_name'><label><a href='#'>");
                                    sb.Append("<span> (" + msgCount + ")</span>");
                                    if (msgalert.Count != 0)
                                        sb.Append("<span style='color:Green;'>" + stdt.FirstName + " " + stdt.LastName + "</span></a></label><br>");
                                    else
                                        sb.Append("<span>" + stdt.FirstName + " " + stdt.LastName + "</span></a></label><br>");
                                    sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                                    sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                                    sb.Append("<p>" + tmpMessageList[i].Description);
                                    sb.Append("<a id='" + tmpMessageList[i].MessageId + "' class='msg_replay_open_click button' style='float:right' href='#'>View message and Respond</a></p></div></li></span>");
                                }
                            }
                        }
                        else
                        {

                            stdt = studentbll.GetByUserName(tmpMessageList[i].FromUserName);
                            if (stdt != null)
                            {
                                if (stdt.Photo == null)
                                    img = @"Images\no_image.jpg";
                                else
                                    img = @"Images\Profile\" + stdt.Photo;

                                sb.Append("<span><li><div class='user_img'><img src='" + img + "' /></div>");
                                sb.Append("<div class='user_name'><label><a href='#'>");
                                sb.Append("<span> (" + msgCount + ")</span>");
                                if (msgalert.Count != 0)
                                    sb.Append("<span style='color:Green;'>" + stdt.FirstName + " " + stdt.LastName + "</span></a></label><br>");
                                else
                                    sb.Append("<span>" + stdt.FirstName + " " + stdt.LastName + "</span></a></label><br>");
                                sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                                sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                                sb.Append("<p>" + tmpMessageList[i].Description);
                                sb.Append("<a id='" + tmpMessageList[i].MessageId + "' class='msg_replay_open_click button' style='float:right' href='#'>View message and Respond</a></p></div></li></span>");
                            }
                            else
                            {
                                UniversityBLL universityBLL = new UniversityBLL();
                                University uni = universityBLL.GetByUserName(tmpMessageList[i].FromUserName);
                                if (uni != null)
                                {
                                    if (uni.Image != null)
                                        img = @"Images\Profile\" + uni.Image;
                                    else
                                        img = @"Images\no_image.jpg";

                                    sb.Append("<span><li><div class='user_img'><img src='" + img + "' /></div>");
                                    sb.Append("<div class='user_name'><label><a href='#'>");
                                    sb.Append("<span> (" + msgCount + ")</span>");
                                    if (msgalert.Count != 0)
                                        sb.Append("<span style='color:Green;'>" + uni.UniversityName + "</span></a></label><br>");
                                    else
                                        sb.Append("<span>" + uni.UniversityName + "</span></a></label><br>");
                                    sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                                    sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                                    sb.Append("<p>" + tmpMessageList[i].Description);
                                }
                            }
                        }
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

        protected void lnkFromStudents_Click(object sender, EventArgs e)
        {
            BindStudentShownList("students");
        }

        protected void lnkFromUniversity_Click(object sender, EventArgs e)
        {
            BindStudentShownList("university");
        }

        protected void lnkFromAdmin_Click(object sender, EventArgs e)
        {
            BindStudentShownList("admin");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindStudentShownList(txtSearch.Text);
        }
    }
}