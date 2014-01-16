using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.DAL;
using SpotCollege.Common;
using SpotCollege.BLL;

namespace SpotCollege
{
    public partial class Student_Message : System.Web.UI.Page
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
                    BindStudentShownList(ParentMessage.MessageId);
                }
            }
        }
        void BindStudentShownList(int MessageId)
        {
            lblrec.Visible = false;
            List<Message> msgList = messageBLL.GetAll().Where(x => x.ParentId == MessageId || x.MessageId == MessageId).OrderByDescending(x => x.MessageId).ToList();
            dlStudentlist.DataSource = msgList;
            dlStudentlist.DataBind();
            if (dlStudentlist.Items.Count == 0)
            {
                lblrec.Visible = true;
            }
        }
        void PageLoad(string FromUserName, string ToUserName)
        {
            //Parent message
            //Message msgdata = messageBLL.GetAll().Where(x => x.FromUserName == CookieHelper.Username || x.ToUserName == Request.QueryString["touser"] || x.FromUserName == Request.QueryString["touser"] || x.ToUserName == CookieHelper.Username && x.MessageId == 0).FirstOrDefault();
            //if (msgdata != null)
            //{
            //    lblmsgTitle.Text = msgdata.Title.ToString();

            #region Check From - student/University
            //check for student data.
            Student std = studentBll.GetByUserName(FromUserName);
            if (std != null)
            {
                if (std.Photo != null)
                {
                    imgmsgstudentphoto.ImageUrl = "Images/Profile/" + std.Photo;
                }
                else
                {
                    imgmsgstudentphoto.ImageUrl = "Images/no_image.jpg";
                }

                lblmsgstudentname.Text = std.FirstName.ToString() + " " + std.MiddleName.ToString() + " " + std.LastName.ToString();
                lblmsgstudentcountry.Text = "Its in " + std.City.ToString() + "," + std.Country.ToString();
            }
            else
            {
                //check for university data
                University uni = universityBLL.GetByUserName(FromUserName);
                if (uni != null)
                {
                    if (uni.Image != null)
                    {
                        imgmsgstudentphoto.ImageUrl = "Images/Profile/" + uni.Image;
                    }
                    else
                    {
                        imgmsgstudentphoto.ImageUrl = "Images/no_image.jpg";
                    }
                    lblmsgstudentname.Text = uni.UniversityName.ToString();
                    lblmsgstudentcountry.Text = "Its in " + uni.City.ToString() + "," + uni.Country.ToString();
                }
            }
            #endregion

            #region Check To - student/University
            //check for student data.
            Student tostd = studentBll.GetByUserName(ToUserName);
            if (tostd != null)
            {
                if (tostd.Photo != null)
                {
                    imgmsguniversityphoto.ImageUrl = "Images/Profile/" + tostd.Photo;
                }
                else
                {
                    imgmsguniversityphoto.ImageUrl = "Images/no_image.jpg";
                }

                lblmsgUniversityname.Text = tostd.FirstName.ToString() + " " + tostd.MiddleName.ToString() + " " + tostd.LastName.ToString();
                lblmsguniversitycountry.Text = "Its in " + tostd.City.ToString() + "," + tostd.Country.ToString();
            }
            else
            {
                //check for university data
                University uni = universityBLL.GetByUserName(ToUserName);
                if (uni != null)
                {
                    if (uni.Image != null)
                    {
                        imgmsgstudentphoto.ImageUrl = "Images/Profile/" + uni.Image;
                    }
                    else
                    {
                        imgmsgstudentphoto.ImageUrl = "Images/no_image.jpg";
                    }
                    lblmsgUniversityname.Text = uni.UniversityName.ToString();
                    lblmsguniversitycountry.Text = "Its in " + uni.City.ToString() + "," + uni.Country.ToString();
                }
            }
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
                    // If you have a column called customerId,
                    object items = e.Item.DataItem;
                    string FromUserName = Convert.ToString(DataBinder.Eval(row_items, "FromUserName"));
                    //Check for Admin
                    if (FromUserName == "admin@admin.com")
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
                            img.ImageUrl = "Images/Profile/" + std.Photo;
                        }
                        else
                        {
                            img.ImageUrl = "Images/no_image.jpg";
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
                                img.ImageUrl = "Images/Profile/" + uni.Image;
                            }
                            else
                            {
                                img.ImageUrl = "Images/no_image.jpg";
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
                msg.IsApproved = false;
                messageBLL.Save(msg);
                txtMessage.Text = "";
                BindStudentShownList(msgdata.MessageId);

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
        }
    }
}