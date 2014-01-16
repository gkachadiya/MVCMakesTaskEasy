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


namespace SpotCollege.Admin
{
    public partial class AdminMessage : System.Web.UI.Page
    {
        StudentBLL studentBll = new StudentBLL();
        MessageBLL messageBLL = new MessageBLL();
        UniversityBLL universityBLL = new UniversityBLL();
        AlertBLL alertBLL = new AlertBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Message ParentMessage = messageBLL.Get(Convert.ToInt32(Request.QueryString["id"]));
                if (ParentMessage != null)
                {
                    PageLoad(ParentMessage.FromUserName, ParentMessage.ToUserName);
                    lblmsgTitle.Text = ParentMessage.Title.ToString();
                    lblParentmsgDescription.Text = ParentMessage.Description.ToString();
                    BindStudentShownList(ParentMessage.MessageId);
                }
            }
        }

        void BindStudentShownList(int MessageId)
        {
            lblrec.Visible = false;
            List<Message> msgList = messageBLL.GetAll().Where(x => x.ParentId == MessageId).OrderByDescending(x => x.MessageId).ToList(); // || x.MessageId == MessageId

            if (msgList.Count == 0)
            {
                Response.Redirect("StudentInfo.aspx");
            }
            else
            {
                dlStudentlist.DataSource = msgList;
                dlStudentlist.DataBind();
                if (dlStudentlist.Items.Count == 0)
                {
                    lblrec.Visible = true;
                }
            }
        }

        void PageLoad(string FromUserName, string ToUserName)
        {

            #region Check To - student/University
            //check for student data.
            Student tostd = studentBll.GetByUserName(ToUserName);
            if (tostd != null)
            {
                if (tostd.Photo != null)
                {
                    imgmsgstudentphoto.ImageUrl = "~/Images/Profile/" + tostd.Photo;
                }
                else
                {
                    imgmsgstudentphoto.ImageUrl = "~/Images/no_image.jpg";
                }

                lblmsgstudentname.Text = tostd.FirstName.ToString() + " " + tostd.MiddleName.ToString() + " " + tostd.LastName.ToString();
                lblmsgstudentcountry.Text = "Its in " + tostd.City.ToString() + "," + tostd.Country.ToString();
            }
            else
            {
                //check for university data
                University uni = universityBLL.GetByUserName(ToUserName);
                if (uni != null)
                {
                    if (uni.Image != null)
                    {
                        imgmsgstudentphoto.ImageUrl = "~/Images/Profile/" + uni.Image;
                    }
                    else
                    {
                        imgmsgstudentphoto.ImageUrl = "~/Images/no_image.jpg";
                    }
                    lblmsgstudentname.Text = uni.UniversityName.ToString();
                    lblmsgstudentcountry.Text = "Its in " + uni.City.ToString() + "," + uni.Country.ToString();
                }
            }
            #endregion

            #region Check From - Admin

            imgmsguniversityphoto.ImageUrl = "~/Images/no_image.jpg";
            lblmsgUniversityname.Text = FromUserName;

            #endregion

            #region check for sender and reciever
            if (FromUserName == CookieHelper.Username)
            {
                hdnsender.Value = FromUserName;
                hdnreceiver.Value = ToUserName;
            }
            else
            {
                hdnsender.Value = ToUserName;
                hdnreceiver.Value = FromUserName;
            }
            #endregion
            //  }
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

                    // Check For Admin
                    if (FromUserName == "admin@gmail.com")
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
                        lblStudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                    }
                    else
                    {
                        //check for university data
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
                            lblStudentname.Text = uni.UniversityName;
                        }
                    }
                }
            }
        }

        protected void btnMsgSend_Click(object sender, EventArgs e)
        {
            Message msgdata = messageBLL.Get(Convert.ToInt32(Request.QueryString["id"]));
            if (msgdata != null)
            {
                Message msg = new Message();
                msg.Title = msgdata.Title; ;
                msg.Description = txtMessage.Text;
                msg.ParentId = Convert.ToInt32(msgdata.MessageId);
                msg.FromUserName = hdnsender.Value;
                msg.ToUserName = hdnreceiver.Value;
                msg.IsRead = false;
                msg.IsApproved = true;
                messageBLL.Save(msg);
                txtMessage.Text = "";
                BindStudentShownList(msgdata.MessageId);

                if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                {
                    //string universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
                    Alert alert = new Alert();
                    alert.Message = "Admin has Send Message";
                    alert.CreatedDate = DateTime.Now;
                    alert.CreatedBy = msgdata.FromUserName;
                    alert.UserName = msgdata.ToUserName;
                    alertBLL.Save(alert);
                }
            }
        }

        protected void lnkBtnDeleteAll_Click(object sender, EventArgs e)
        {
            int msgId = Convert.ToInt32(Request.QueryString["id"]);
            Message msgDel = messageBLL.Get(msgId);
            if (msgDel != null)
            {
                List<Message> msgDelList = messageBLL.GetAll().Where(x => x.ParentId == msgDel.MessageId || x.MessageId == msgDel.MessageId).ToList();

                foreach (Message mDel in msgDelList)
                {
                    messageBLL.delete(mDel.MessageId);
                }
            }
            BindStudentShownList(msgId);
        }

        [WebMethod()]
        public static object MsgDelete(string MessageId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            messageBLL.delete(Convert.ToInt32(MessageId));
            return JsonConvert.SerializeObject(str);
        }
    }
}