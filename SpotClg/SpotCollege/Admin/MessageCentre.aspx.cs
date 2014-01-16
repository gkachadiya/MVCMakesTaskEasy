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
using System.Data.SqlClient;
using System.Data;

namespace SpotCollege.Admin
{
    public partial class MessageCentre : System.Web.UI.Page
    {
        public static List<Message> StaticMessageList = new List<Message>();
        public static List<University> StaticUniversityList = new List<University>();
        StudentBLL studentBll = new StudentBLL();
        UniversityBLL universityBLL = new UniversityBLL();
        MessageBLL messageBLL = new MessageBLL();
        UserBLL userBLL = new UserBLL();
        User user = new User();

        static List<University> staticUniList = new List<University>();
        static List<Student> staticStudList = new List<Student>();
        static int msgToDisplay = 13;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUniversityList();
            }
        }


        void BindUniversityList()
        {
            staticUniList = null;
            dlUniversitylist.Visible = true;
            dlStudentList.Visible = false;

            IList<sp_NoOFunApprovedMsg_Result> result = universityBLL.getUnApprovedMsg().ToList();

            IList<sp_NoOFunApprovedMsg_Result> tmplist = result.ToList();
            if (result.Count > msgToDisplay)
            {
                for (int i = 0; i < msgToDisplay; i++)
                {
                    tmplist.Remove(result[i]);
                }
            }
            else
            {
                for (int i = 0; i < result.Count; i++)
                {
                    tmplist.Remove(result[i]);
                }
            }

            List<University> uniList = universityBLL.GetAll().Where(x => x.User.IsActive == true && tmplist.Select(y => y.uname).ToArray().Contains(x.UserName)).ToList();

            staticUniList = uniList.ToList();

            dlUniversitylist.DataSource = result.Take(msgToDisplay);
            dlUniversitylist.DataBind();

            if (result.Count == 0)
                divNoUniversity.InnerHtml = "No University Found";
            else
                divNoUniversity.InnerHtml = "";
        }

        void BindStudentList()
        {
            staticStudList = null;

            dlStudentList.Visible = true;
            dlUniversitylist.Visible = false;

            IList<sp_StudentUnApprovedMsg_Result> stdUnapprovedList = studentBll.getUnApprovedMsg();

            IList<sp_StudentUnApprovedMsg_Result> tmpUnapprovedlist = stdUnapprovedList.ToList();

            if (stdUnapprovedList.Count > msgToDisplay)
            {
                for (int i = 0; i < msgToDisplay; i++)
                {
                    tmpUnapprovedlist.Remove(stdUnapprovedList[i]);
                }
            }
            else
            {
                for (int i = 0; i < stdUnapprovedList.Count; i++)
                {
                    tmpUnapprovedlist.Remove(stdUnapprovedList[i]);
                }
            }

            List<Student> stdList = studentBll.GetAll().Where(x => x.User.IsActive == true && tmpUnapprovedlist.Select(y => y.uname).ToArray().Contains(x.UserName)).ToList();

            staticStudList = stdList.ToList();
            
            dlStudentList.DataSource = stdUnapprovedList.Take(msgToDisplay);
            dlStudentList.DataBind();

            if (stdUnapprovedList.Count == 0)
                divNoUniversity.InnerHtml = "No Student Found";
            else
                divNoUniversity.InnerHtml = "";
        }

        public static Control PreviousControl(Control control)
        {
            ControlCollection siblings = control.Parent.Controls;
            for (int i = siblings.IndexOf(control) - 1; i >= 0; i--)
            {
                if (siblings[i].GetType() != typeof(LiteralControl) && siblings[i].GetType().BaseType != typeof(LiteralControl))
                {
                    return siblings[i];
                }
            }
            return null;
        }

        protected void lnkBtnUniversityName_Click(object sender, EventArgs e)
        {
            string UserName = ((LinkButton)sender).CommandArgument.ToString();
            Control str = PreviousControl(((LinkButton)sender));
            if (!string.IsNullOrEmpty(UserName))
            {
                lblmsg.Visible = false;
                List<Message> mli = messageBLL.GetAll().Where(x => x.ToUserName == UserName && x.FromUserName != CookieHelper.Username && x.IsApproved == false || x.FromUserName == UserName && x.ToUserName != CookieHelper.Username && x.IsApproved == false).ToList();
                dlMessageList.DataSource = mli;
                dlMessageList.DataBind();

                if (mli.Count == 0)
                {
                    lblmsg.Visible = true;
                }
            }
        }

        protected void dlMessageList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName1");
                Label lblStudentname = (Label)e.Item.FindControl("lblStudentname");
                Image img = (Image)e.Item.FindControl("ImgBtnPhoto1");

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    object row_items = e.Item.DataItem;
                    object items = e.Item.DataItem;
                    string FromUserName = Convert.ToString(DataBinder.Eval(row_items, "FromUserName"));

                    //check for student data.
                    Student std = studentBll.GetByUserName(FromUserName);
                    if (std != null)
                    {
                        if (std.Photo != null)
                        {
                            img.ImageUrl = "~/Images/Profile/" + std.Photo;
                        }
                        else
                        {
                            img.ImageUrl = "~/Images/no_image.jpg";
                        }

                        lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();

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
                                img.ImageUrl = "~/Images/Profile/" + uni.Image;
                            }
                            else
                            {
                                img.ImageUrl = "~/Images/no_image.jpg";
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

        [WebMethod()]
        public static string SaveApproving(string MessageId)
        {
            UserBLL userBLL = new UserBLL();
            MessageBLL messageBLL = new MessageBLL();
            string str = string.Empty;
            if (!string.IsNullOrEmpty(MessageId))
            {
                Message mRead = messageBLL.Get(Convert.ToInt32(MessageId));
                if (mRead != null)
                {
                    mRead.MessageId = mRead.MessageId;
                    mRead.Title = mRead.Title;
                    mRead.Description = mRead.Description;
                    mRead.ParentId = mRead.ParentId;
                    mRead.FromUserName = mRead.FromUserName;
                    mRead.ToUserName = mRead.ToUserName;
                    mRead.IsRead = false;
                    mRead.IsApproved = true;
                    mRead.CreatedDate = mRead.CreatedDate;
                    messageBLL.Save(mRead);

                    int usType = userBLL.Get(mRead.FromUserName).LoginTypeId;
                    if (usType == 2)
                    {
                        AlertBLL alertBLL = new AlertBLL();
                        UniversityBLL universityBLL = new UniversityBLL();
                        string universityname = universityBLL.GetByUserName(mRead.FromUserName).UniversityName.ToString();
                        Alert alert = new Alert();
                        alert.Message = universityname + " has Send Message";
                        alert.CreatedDate = DateTime.Now;
                        alert.CreatedBy = mRead.FromUserName;
                        alert.UserName = mRead.ToUserName;
                        alertBLL.Save(alert);
                    }
                }
            }
            return str;
        }

        [WebMethod()]
        public static object MsgDelete(string MessageId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            int parent=messageBLL.Get(Convert.ToInt32(MessageId)).ParentId;
            if (parent == 0)
            {
                List<Message> msgLi = messageBLL.GetAll().Where(x => x.MessageId == Convert.ToInt32(MessageId) || x.ParentId == Convert.ToInt32(MessageId)).ToList();
                foreach (var li in msgLi)
                {
                    messageBLL.delete(li.MessageId);
                }
            }
            else
            {
                messageBLL.delete(Convert.ToInt32(MessageId));
            }

            return JsonConvert.SerializeObject(str);
        }

        [WebMethod()]
        public static string SendMessage(string Title, string Description, string ParentId, string MessageId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
            Message message = messageBLL.Get(Convert.ToInt32(MessageId));
            msg.Title = Title;
            msg.Description =message.Description + " was rejected because "+ Description;
            msg.ParentId = Convert.ToInt32(ParentId);
            msg.FromUserName = CookieHelper.Username;
            msg.ToUserName = message.FromUserName;
            msg.IsRead = false;
            msg.IsApproved = true;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);


            messageBLL.delete(Convert.ToInt32(MessageId));
            return str;
        }


        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserType.SelectedItem.Text == "Students")
            {
                BindStudentList();
            }
            else
            {

                BindUniversityList();
            }

        }


        [WebMethod]
        public static string appendToUniversityList()
        {
            List<University> tmpUniversity = new List<University>();
            StringBuilder sb = new StringBuilder();

            MessageBLL messageBLL = new MessageBLL();

            if (staticUniList.Count == 0)
                return "no";

            if (staticUniList.Count < msgToDisplay)
            {
                tmpUniversity = staticUniList.ToList();
                staticUniList.RemoveAll(x => 1 == 1);
            }
            else
            {
                tmpUniversity = staticUniList.Take(msgToDisplay).ToList();
                staticUniList.RemoveRange(0, msgToDisplay);
            }
            string img;
            List<Message> tmpNofmsg = messageBLL.GetAll();
            for (int i = 0; i < tmpUniversity.Count; i++)
            {
                List<Message> NoOfUnApprovedMsgList = tmpNofmsg.Where(x => x.ToUserName == tmpUniversity[i].UserName && x.IsApproved == false).ToList(); //  || x.FromUserName == tmpUniversity[i].UserName && x.IsApproved == false


                if (tmpUniversity[i].Image != null)
                    img = "..//Images//Profile//" + tmpUniversity[i].Image;
                else
                    img = "..//Images//no_image.jpg";
                sb.Append("<span>");
                sb.Append("<li>");
                sb.Append("<div class='user_img'><img src='" + img + "' /></div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<a onclick=\"javascript:openUnApprovesMsgs('" + tmpUniversity[i].UserName + "')\" style='cursor:pointer'>" + tmpUniversity[i].UniversityName + "</a>");
                sb.Append("<span class='user_count_mg img-polaroid'>" + NoOfUnApprovedMsgList.Count + "</span>");
                sb.Append("</div>");
                sb.Append("</li>");
                sb.Append("</span><br/>");
            }
            return sb.ToString();
        }

        [WebMethod]
        public static string appendToStudentList()
        {
            List<Student> tmpStudent = new List<Student>();
            StringBuilder sb = new StringBuilder();
            MessageBLL messageBLL = new MessageBLL();

            if (staticStudList.Count == 0)
                return "no";

            if (staticStudList.Count < msgToDisplay)
            {
                tmpStudent = staticStudList.ToList();
                staticStudList.RemoveAll(x => 1 == 1);
            }
            else
            {
                tmpStudent = staticStudList.Take(msgToDisplay).ToList();
                staticStudList.RemoveRange(0, msgToDisplay);
            }
            List<Message> msgtmp = messageBLL.GetAll();
            string img;
            for (int i = 0; i < tmpStudent.Count; i++)
            {
                List<Message> NoOfUnApprovedMsgList = msgtmp.Where(x => x.ToUserName == tmpStudent[i].UserName && x.IsApproved == false).ToList(); //  || x.FromUserName == tmpStudent[i].UserName && x.IsApproved == false

                if (tmpStudent[i].Photo != null)
                    img = "..//Images//Profile//" + tmpStudent[i].Photo;
                else
                    img = "..//Images//no_image.jpg";
                sb.Append("<span>");
                sb.Append("<li>");
                sb.Append("<div class='user_img'><img src='" + img + "' /></div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<a onclick=\"javascript:openUnApprovesMsgs('" + tmpStudent[i].UserName + "')\" style='cursor:pointer'>" + tmpStudent[i].FirstName + " " + tmpStudent[i].LastName + "</a>");
                sb.Append("<span class='user_count_mg img-polaroid'>" + NoOfUnApprovedMsgList.Count + "</span>");
                sb.Append("</div>");
                sb.Append("</li>");
                sb.Append("</span><br/>");
            }
            return sb.ToString();
        }


        static List<Message> staticMessageList = null;
        [WebMethod]
        public static string displayUnapprovedMessage(string username)
        {
            StringBuilder sb = new StringBuilder();
            MessageBLL msgBll = new MessageBLL();
            List<Message> mli = msgBll.GetAll().Where(x => x.FromUserName == username && x.ToUserName != CookieHelper.Username && x.IsApproved == false).ToList(); //  || x.FromUserName == username && x.ToUserName != CookieHelper.Username && x.IsApproved == false
            StudentBLL studentBll = new StudentBLL();
            UniversityBLL universityBll = new UniversityBLL();

            List<Student> tmpStud = studentBll.GetAll();
            List<University> tmpUniList = universityBll.GetAll();

            staticMessageList = mli.ToList();
            if (mli.Count < 10)
                staticMessageList.RemoveAll(x => 1 == 1);
            else
                staticMessageList.RemoveRange(0, 10);

            //if (mli.Count > 10)
            //{
            //    staticMessageList = mli.GetRange(10, mli.Count);
            //}

            string img = "", name = "";

            int cnt = 0;
            if (mli.Count < 10)
                cnt = mli.Count;
            else
                cnt = 10;


            for (int i = 0; i < cnt; i++)
            {
                Student std = tmpStud.Where(x => x.UserName == mli[i].FromUserName).FirstOrDefault();
                if (std != null)
                {
                    if (std.Photo != null)
                    {
                        img = "/Images/Profile/" + std.Photo;
                    }
                    else
                    {
                        img = "/Images/no_image.jpg";
                    }

                    name = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                }
                else
                {
                    University Uni = tmpUniList.Where(x => x.UserName == mli[i].FromUserName).FirstOrDefault();
                    if (Uni != null)
                    {
                        if (Uni.Image != null)
                        {
                            img = "/Images/Profile/" + Uni.Image;
                        }
                        else
                        {
                            img = "/Images/no_image.jpg";
                        }
                        name = Uni.UniversityName.ToString();
                    }
                }

                sb.Append("<span><li>");
                sb.Append("<div class='user_img'><img class='img-polaroid' src='" + img + "' /></div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<div class='width100per'>");
                sb.Append("<h4>" + name + "</h4>");
                sb.Append("<h5 class='fright'>");
                sb.Append("<span>" + mli[i].CreatedDate.ToString("dd/MM/yyyy") + "</span><br />");
                sb.Append("");
                sb.Append("</h5>");
                sb.Append("</div>");
                sb.Append("<div class='width100per'>");
                sb.Append(mli[i].Description);
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='user_msg'>");
                sb.Append("</div>");

                sb.Append("<div class='width100per'>");
                sb.Append("<div class='fleft'>");

                sb.Append("<a id='msgApproved" + mli[i].MessageId + "' href=\"javascript:MsgApproving('" + mli[i].MessageId + "')\" class='button'>Approved</a>");
                sb.Append("</div>");

                sb.Append("<div class='fright'>");
                sb.Append("<a id='msgRejectwithmsg" + mli[i].MessageId + "' href=\"javascript:MsgDialogOpen('" + mli[i].MessageId + "')\" class='button'>Reject With Message</a>&nbsp;&nbsp;");
                sb.Append("<a id='msgReject" + mli[i].MessageId + "' href=\"javascript:MsgDelete('" + mli[i].MessageId + "')\" class='button'>Reject</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</li></span><br/>");
            }
            return sb.ToString();
        }


        [WebMethod]
        public static string appendToMessageList()
        {
            List<Message> tmpMessage = new List<Message>();
            StringBuilder sb = new StringBuilder();
            StudentBLL studentBll = new StudentBLL();
            UniversityBLL universityBll = new UniversityBLL();

            List<Student> tmpStud = studentBll.GetAll();
            List<University> tmpUniList = universityBll.GetAll();

            string img = "", name = "";
            if (staticMessageList != null)
            {
                if (staticMessageList.Count == 0)
                    return "no";

                if (staticMessageList.Count < 5)
                {
                    tmpMessage = staticMessageList.ToList();
                    staticMessageList.RemoveAll(x => 1 == 1);
                }
                else
                {
                    tmpMessage = staticMessageList.GetRange(0, 5);
                    staticMessageList.RemoveRange(0, 5);
                }
                for (int i = 0; i < tmpMessage.Count; i++)
                {
                    Student std = tmpStud.Where(x => x.UserName == tmpMessage[i].FromUserName).FirstOrDefault();
                    if (std != null)
                    {
                        if (std.Photo != null)
                        {
                            img = "/Images/Profile/" + std.Photo;
                        }
                        else
                        {
                            img = "/Images/no_image.jpg";
                        }

                        name = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                    }
                    else
                    {
                        University Uni = tmpUniList.Where(x => x.UserName == tmpMessage[i].FromUserName).FirstOrDefault();
                        if (Uni != null)
                        {
                            if (Uni.Image != null)
                            {
                                img = "/Images/Profile/" + Uni.Image;
                            }
                            else
                            {
                                img = "/Images/no_image.jpg";
                            }
                            name = Uni.UniversityName.ToString();
                        }
                    }

                    sb.Append("<span><li>");
                    sb.Append("<div class='user_img'><img class='img-polaroid' src='" + img + "' /></div>");
                    sb.Append("<div class='user_name'>");
                    sb.Append("<div class='width100per'>");
                    sb.Append("<h4>" + name + "</h4>");
                    sb.Append("<h5 class='fright'>");
                    sb.Append("<span>" + tmpMessage[i].CreatedDate.ToString("dd/MM/yyyy") + "</span><br />");
                    sb.Append("");
                    sb.Append("</h5>");
                    sb.Append("</div>");
                    sb.Append("<div class='width100per'>");
                    sb.Append(tmpMessage[i].Description);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("<div class='user_msg'>");
                    sb.Append("</div>");

                    sb.Append("<div class='width100per'>");
                    sb.Append("<div class='fleft'>");

                    sb.Append("<a id='msgApproved" + tmpMessage[i].MessageId + "' href=\"javascript:MsgApproving('" + tmpMessage[i].MessageId + "')\" class='button'>Approved</a>");
                    sb.Append("</div>");

                    sb.Append("<div class='fright'>");
                    sb.Append("<a id='msgRejectwithmsg" + tmpMessage[i].MessageId + "' href=\"javascript:MsgDialogOpen('" + tmpMessage[i].MessageId + "')\" class='button'>Reject With Message</a>&nbsp;&nbsp;");
                    sb.Append("<a id='msgReject" + tmpMessage[i].MessageId + "' href=\"javascript:MsgDelete('" + tmpMessage[i].MessageId + "')\" class='button'>Reject</a>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</li></span><br/>");
                }

                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        //protected void dlUniversitylist_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField username = (HiddenField)e.Item.FindControl("HndUniversityUserName");
        //        Label lblNoOfUnApprovedMsg = (Label)e.Item.FindControl("lblNoOfUnApprovedMsg");
        //        List<Message> NoOfUnApprovedMsgList = messageBLL.GetAll().Where(x => x.ToUserName == username.Value && x.IsApproved == false).ToList(); //  || x.FromUserName == username.Value && x.IsApproved == false
        //        lblNoOfUnApprovedMsg.Text = NoOfUnApprovedMsgList.Count.ToString();
        //    }
        //}

        //protected void dlStudentList_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField username = (HiddenField)e.Item.FindControl("HndstudentUserName");
        //        Label lblNoOfUnApprovedMsg = (Label)e.Item.FindControl("lblNoOfUnApprovedMsg");
        //        List<Message> NoOfUnApprovedMsgList = messageBLL.GetAll().Where(x => x.ToUserName == username.Value && x.IsApproved == false).ToList(); // || x.FromUserName == username.Value && x.IsApproved == false
        //        lblNoOfUnApprovedMsg.Text = NoOfUnApprovedMsgList.Count.ToString();
        //    }
        //}



    }
}