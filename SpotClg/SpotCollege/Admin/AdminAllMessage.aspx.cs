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

namespace SpotCollege.Admin
{
    public partial class AdminAllMessage : System.Web.UI.Page
    {
        public static List<Message> StaticMessageList = new List<Message>();
        StudentBLL studentBll = new StudentBLL();
        MessageBLL messageBLL = new MessageBLL();
        UserBLL userBLL = new UserBLL();
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStudentShownList();
            }
        }

        void BindStudentShownList()
        {
            lblrec.Visible = false;
            // List<Message> msgList = messageBLL.GetAll().Where(x => x.FromUserName == CookieHelper.Username && x.ParentId == 0 || x.ToUserName == CookieHelper.Username && x.ParentId == 0).OrderByDescending(x => x.MessageId).ToList();
            List<Message> msgList = messageBLL.GetAll().Where(x => x.ParentId == 0).OrderByDescending(x => x.MessageId).ToList();

            StaticMessageList = msgList.ToList(); //msgStaticList will be used for displaying record
            if (msgList.Count < 5)
                StaticMessageList.RemoveAll(x => 1 == 1);
            else
                StaticMessageList.RemoveRange(0, 6);

            dlStudentlist.DataSource = msgList.Take(6);
            dlStudentlist.DataBind();
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
                Image img = (Image)e.Item.FindControl("ImgBtnPhoto1");

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
                    messageBLL.Save(msgRead);
                }
                Response.Redirect("AdminMessage.aspx?id=" + MessageId);
            }
        }

        //For Filtering
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblrec.Visible = false;
            List<Message> msgList = new List<Message>();
            msgList = messageBLL.GetAll().Where(x => x.FromUserName == txtFilterByUserName.Text && x.ParentId == 0 && x.CreatedDate >= Convert.ToDateTime(txtFilterByFromDate.Text) && x.CreatedDate <= Convert.ToDateTime(txtFilterByToDate.Text) || x.ToUserName == txtFilterByUserName.Text && x.ParentId == 0 && x.CreatedDate >= Convert.ToDateTime(txtFilterByFromDate.Text) && x.CreatedDate <= Convert.ToDateTime(txtFilterByToDate.Text)).OrderByDescending(x => x.MessageId).ToList();
            dlStudentlist.DataSource = msgList;
            dlStudentlist.DataBind();
            txtFilterByUserName.Text = "";
            txtFilterByFromDate.Text = "";
            txtFilterByToDate.Text = "";
            if (dlStudentlist.Items.Count == 0)
            {
                lblrec.Visible = true;
            }
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST
        [WebMethod()]
        public static string AppendAndDisplayMessage()
        {
            StringBuilder sb = new StringBuilder();
            StudentBLL studentBll = new StudentBLL();
            UniversityBLL universityBLL = new UniversityBLL();

            List<Message> tmpMessageList = new List<Message>();
            if (StaticMessageList != null)
            {
                if (StaticMessageList.Count != 0)
                {
                    if (StaticMessageList.Count < 5)
                    {
                        tmpMessageList = StaticMessageList.ToList();
                        StaticMessageList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpMessageList = StaticMessageList.GetRange(0, 5);
                        StaticMessageList.RemoveRange(0, 5);
                    }



                    string img;
                    for (int i = 0; i < tmpMessageList.Count; i++)
                    {

                        if (tmpMessageList[i].FromUserName == "admin@admin.com" || tmpMessageList[i].FromUserName == "admin@gmail.com")
                        {
                            img = @"../Images/no_image.jpg";

                            sb.Append("<span><li><div class='user_img'> <img src='" + img + "' /> </div>");
                            sb.Append(" <div class='user_name'><span class='fleft'>Admin</span></div>");
                            sb.Append("<div class='user_msg'><a href=\"AdminMessage.aspx?id=" + tmpMessageList[i].MessageId + "\" >" + tmpMessageList[i].Title + "</a><br/><span>" + tmpMessageList[i].Description + "</span></div> </li></span>");
                        }

                        Student std = studentBll.GetByUserName(tmpMessageList[i].FromUserName);

                        if (std != null)
                        {
                            if (std.Photo != null)
                                img = @"../Images/Profile/" + std.Photo;
                            else
                                img = @"../Images/no_image.jpg";

                            sb.Append("<span><li><div class='user_img'> <img src='" + img + "' /> </div>");
                            sb.Append(" <div class='user_name'><span class='fleft'>" + std.FirstName + " " + std.LastName + "</span></div>");
                            sb.Append("<div class='user_msg'><a href=\"AdminMessage.aspx?id=" + tmpMessageList[i].MessageId + "\" >" + tmpMessageList[i].Title + "</a><br/><span>" + tmpMessageList[i].Description + "</span></div> </li></span>");
                        }
                        else
                        {
                            University uni = universityBLL.GetByUserName(tmpMessageList[i].FromUserName);

                            if (uni.Image != null)
                            {
                                img = @"../Images/Profile/" + uni.Image;
                            }
                            else
                            {
                                img = @"../Images/no_image.jpg";
                            }

                            sb.Append("<span><li><div class='user_img'> <img src='" + img + "' /> </div>");
                            sb.Append(" <div class='user_name'><span class='fleft'>" + uni.UniversityName + "</span></div>");
                            sb.Append("<div class='user_msg'><a href=\"AdminMessage.aspx?id=" + tmpMessageList[i].MessageId + "\" >" + tmpMessageList[i].Title + "</a><br/><span>" + tmpMessageList[i].Description + "</span></div> </li></span>");

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
    }
}