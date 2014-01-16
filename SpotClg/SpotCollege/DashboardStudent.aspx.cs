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
using System.Text;
using System.Data;

namespace SpotCollege
{
    public partial class DashboardStudent : System.Web.UI.Page
    {

        #region PageMethod
        protected void Page_Load(object sender, EventArgs e)
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

            }

            if (!IsPostBack)
            {
                if (student != null)
                {
                    Setlable();
                    bindUniversityDetail();
                    bindcountrydetail();
                    bindapplystudentdetail();
                }
                else
                {
                    Response.Redirect("Account/EditProfile.aspx");
                }
            }
        }

        #endregion

        #region Private Method

        //University per user preference
        private void bindUniversityDetail()
        {

            StudentBLL studentBll = new StudentBLL();
            Student student = studentBll.GetByUserName(CookieHelper.Username);
            UniversityBLL universityBll = new UniversityBLL();

            StudentInterestBLL StudentInterestBLL = new StudentInterestBLL();
            List<StudentInterest> studentintrestlist = StudentInterestBLL.GetAll().Where(x => x.StudentUserName == CookieHelper.Username).ToList();

            University university = universityBll.GetByUniversityCountry(student.Country);

            string cource="";
            Student  std= studentBll.GetByUserName(CookieHelper.Username);
            if (std != null)
            {
                cource = Enum.Parse(typeof(ProgramLookingFor), std.LookingFor.ToString()).ToString().ToUpper(); 
            }
            List<University> universityList = universityBll.GetAll().Where(x =>x.Degree!=null &&  x.Degree.Contains(cource)).ToList();
            
            

            
            foreach (var intrest in studentintrestlist)
            {
                University u = universityBll.GetByUserName(intrest.UniversityUserName);
                if (u != null)
                {
                    universityList.Remove(u);
                }
            }




            dlUniversityList.DataSource = universityList.Take(15).OrderByDescending(x => x.UniversityId);
            dlUniversityList.DataBind();

            if (universityList.Count == 0)
                divNoUniversity.InnerHtml = "No University Found";
            else
                divNoUniversity.InnerHtml = "";

        }
        //Student per applying same Country

        private void bindcountrydetail()
        {
            StudentBLL studentBll = new StudentBLL();
            UserBLL userBll = new UserBLL();
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            Student student = studentBll.GetByUserName(user.UserName);
            List<Student> studentlist = studentBll.GetAll().Where(x => x.Country == student.Country).ToList();
            studentlist.Remove(student);
            
            
            studentlist = studentlist.Take(5).OrderByDescending(x => x.StudentId).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var br in studentlist)
            {
                try
                {
                    if (br.Photo == null)
                        br.Photo = "login_email.png";
                    sb.Append("<li><input type='hidden' id='" + br.StudentId + "' value='" + br.UserName + "' /><div class='list_2_img'><a href='#'><img id='Imgcountry' src='Images/Profile/" + br.Photo + "' alt='' /></a></div>");
                    sb.Append("<div class='list_2_detail'>");
                    if (br.StartDate != 0)
                        sb.Append("<p align='justify'><b>" + br.FirstName + "</b> from " + br.City + " applying for " + EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)student.LookingFor).ToString() + " in " + br.DesiredFieldofStudy + " to " + br.LookingForCountry + " desire date of joining : " + EnumHelper.GetDescriptionFromEnumValue((expectedStart)br.StartDate).ToString() + "<br />");
                    else
                        sb.Append("<p align='justify'><b>" + br.FirstName + "</b> from " + br.City + " applying for " + EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)student.LookingFor).ToString() + "<br />");
                    sb.Append("</p><a id='lnkMessage' href=\"javascript:OpenMsgPopup('" + br.FirstName + " " + br.LastName + "','" + br.StudentId + "')\" class='msgbtn'>Message</a></div></li>");
                }
                catch (Exception ex) { }
            }

            ddlapplycountry.InnerHtml = sb.ToString();
            ddlapplycountry2.InnerHtml = sb.ToString();

            if (studentlist.Count == 0)
                errorMsgDiv.InnerHtml = "No Student Found";
            else
                errorMsgDiv.InnerHtml = "";

            //ddlaplycountry.DataSource = studentlist;
            //ddlaplycountry.DataBind();
        }
        //Student per applying same looking for 
        private void bindapplystudentdetail()
        {
            StudentBLL studentBll = new StudentBLL();
            UserBLL userBll = new UserBLL();
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            Student student = studentBll.GetByUserName(user.UserName);
            List<Student> studentlist = studentBll.GetAll().Where(x => x.LookingFor == student.LookingFor).ToList();
            studentlist.Remove(student);
            studentlist = studentlist.Take(5).OrderByDescending(x => x.StudentId).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var br in studentlist)
            {
                if (br.Photo == null)
                    br.Photo = "login_email.png";
                sb.Append("<li><input type='hidden' id='" + br.StudentId + "' value='" + br.UserName + "' /><div class='list_2_img'><a href='#'><img id='Imgcountry' src='Images/Profile/" + br.Photo + "' alt='' /></a></div>");
                sb.Append("<div class='list_2_detail'>");

                if (br.StartDate != 0)
                    sb.Append("<p align='justify'><b>" + br.FirstName + "</b> from " + br.City + " applying for " + EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)student.LookingFor).ToString() + " in " + br.DesiredFieldofStudy + " to " + br.LookingForCountry + " desire date of joining : " + EnumHelper.GetDescriptionFromEnumValue((expectedStart)br.StartDate).ToString() + "<br />");
                else
                    sb.Append("<p align='justify'><b>" + br.FirstName + "</b> from " + br.City + " applying for " + EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)student.LookingFor).ToString() + "<br />");
     
                sb.Append("</p><a id='lnkMessage' href=\"javascript:OpenMsgPopup('" + br.FirstName + " " + br.LastName + "','" + br.StudentId + "')\" class='msgbtn'>Message</a></div></li>");
            }

            ddlapplystudent.InnerHtml = sb.ToString();

            if (studentlist.Count == 0)
                divNostudent.InnerHtml = "No Student Found";
            else
                divNostudent.InnerHtml = "";

            //ddlapplystudent.DataSource = studentlist.Take(5).OrderByDescending(x => x.StudentId);
            //ddlapplystudent.DataBind();
        }


        //set all lable in the student dashboard
        private void Setlable()
        {
            UserBLL userBll = new UserBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            if (user != null)
            {
                Student student = studentBll.GetByUserName(user.UserName);
                if (student != null)
                {
                    headingcountry.InnerHtml = "Make friends with other students applying from " + student.Country + "</br>";
                    if (student.LookingFor != 0)
                    {
                        headercuurentlylooking.InnerHtml = "Make friends with other students applying for  " + Enum.Parse(typeof(ProgramLookingFor), student.LookingFor.ToString()).ToString() + "</br>";
                        headingcollege.InnerText = "College that offer in  " + Enum.Parse(typeof(ProgramLookingFor), student.LookingFor.ToString()).ToString();
                    }
                    else
                    {
                        headercuurentlylooking.InnerText = "Other Student applying from ";
                        headingcollege.InnerText = "College that offer in";
                    }
                }
            }
        }

        [WebMethod()]
        public static object GetUnivercityData(string UniversityName)
        {
            //string str=string.Empty;
            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            Student std = new Student();
            StudentBLL studentBll = new StudentBLL();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                //std = studentBll.GetByUserName(CookieHelper.Username);
                //List<StudentTest> stdTest = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                //if (stdTest.Count == 0)
                //{
                //    //return str = "Please input atleast one test scores in order to contact universities";
                //    university = null;
                //    return JsonConvert.SerializeObject(university);
                //}
                //else
                //{
                    university = universityBll.GetByUniversityName(UniversityName);
                    return JsonConvert.SerializeObject(university);
               // }
            }
            else
            {
                university = null;
                return JsonConvert.SerializeObject(university);
            }


        }

        [WebMethod()]
        public static string SaveStudentIntrest(string UniversityName)
        {

            UserBLL userBll = new UserBLL();
            string str = string.Empty;
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            if (user != null)
            {
                StudentBLL studentBll = new StudentBLL();
                Student student = studentBll.GetByUserName(CookieHelper.Username);
                StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
                StudentTestBLL studentTestBll = new StudentTestBLL();
                if (student != null)
                {
                    StudentAcademic stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                    List<StudentTest> stdTest = studentTestBll.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
                    if (stdAca == null)
                    {
                        str = "Please Enter Atleast One Student Academic Record inorder to show Interest in University.";
                    }
                    else if (stdTest == null || stdTest.Count == 0)
                    {
                        str = "Please Enter Atleast One International Test Record inorder to show Interest in University.";
                    }
                    else
                    {

                        StudentInterest studentintrest = new StudentInterest();
                        StudentInterestBLL studentintrestbll = new StudentInterestBLL();
                        University university = new University();
                        UniversityBLL universityBll = new UniversityBLL();

                        university = universityBll.GetByUniversityName(UniversityName);

                        studentintrest.UniversityUserName = university.UserName;
                        studentintrest.StudentUserName = user.UserName;
                        studentintrest.Approved = (int)StudentInterestApproved.Applied;
                        studentintrestbll.Save(studentintrest);
                        str = "true";
                    }
                }

            }

            return str;
        }

        //for Message
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
            msg.IsApproved = false;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);
            return msg.MessageId.ToString();
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

        [WebMethod()]
        public static string SaveIsRead(string MessageId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
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
            return str;
        }

        #endregion

        protected void dlUniversityList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item ||
             e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image img = (Image)e.Item.FindControl("ImgUniversity");
                object items = e.Item.DataItem;
                string imgstr = ((University)e.Item.DataItem).Image;
                if (string.IsNullOrEmpty(imgstr))
                    img.ImageUrl = "\\Images\\no_image.jpg";
                else
                    img.ImageUrl = "\\Images\\Profile\\" + imgstr;
            }

        }
    }
}