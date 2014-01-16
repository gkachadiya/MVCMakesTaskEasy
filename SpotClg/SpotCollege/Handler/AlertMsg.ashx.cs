using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpotCollege.DAL;
using SpotCollege.Common;
using SpotCollege.BLL;
using System.Text;

namespace SpotCollege.Handler
{
    /// <summary>
    /// Summary description for AlertMsg
    /// </summary>
    public class AlertMsg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string MyWidgets = null;
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                string option = context.Request.QueryString["id"];
                if (option == null)
                {
                    StringBuilder builder = new StringBuilder();
                   

                    AlertBLL alertBll = new AlertBLL();
                    List<Alert> alertlist = alertBll.GetAll().Where(x => x.UserName == CookieHelper.Username).OrderByDescending(x => x.CreatedDate).Take(5).ToList();

                    StudentAcademicsBLL studAcdBLL = new StudentAcademicsBLL();
                    StudentBLL studentBLL = new StudentBLL();
                    List<Student> studentList = studentBLL.GetAll().Where(z => z.UserName == CookieHelper.Username).ToList();

                    List<StudentAcademic> stud = studAcdBLL.GetAll().Where(y => y.StudentId == studentList[0].StudentId && y.Graduate == 1 &&  y.CertificatePath==string.Empty).ToList();
                    if (stud.Count != 0)
                    {
                        builder.Append("<li>");
                        builder.Append("<div class='list_3_date'>");
                        builder.Append("<span>" + DateTime.Now.ToShortDateString() + "</span>");
                        builder.Append("</div>");
                        builder.Append("<div class='list_3_detail'>");
                        builder.Append("<p>" + "In order to contact universities, please upload image of passing/degree certificate from " + stud[0].SchoolName + " insititute" + "</p>");
                        builder.Append("</div>");
                        builder.Append("</li>");
                    }



                    if (alertlist.Count != 0)
                    {
                        foreach (Alert alert in alertlist)
                        {
                            builder.Append("<li>");
                            builder.Append("<div class='list_3_date'>");
                            builder.Append("<span>" + alert.CreatedDate.ToShortDateString() + "</span>");
                            builder.Append("</div>");
                            builder.Append("<div class='list_3_detail'>");
                            builder.Append("<p>" + alert.Message + "</p>");
                            builder.Append("</div>");
                            builder.Append("</li>");
                        }
                    }
                    else
                    {
                        if(stud.Count ==0)
                        builder.Append("No Alerts Found");
                    }
                    MyWidgets = builder.ToString();
                }
                else
                {
                    AlertBLL alertBll = new AlertBLL();
                    StringBuilder builder = new StringBuilder();
                    List<Alert> alertlist = alertBll.GetAll().Where(x => x.UserName == CookieHelper.Username).OrderByDescending(x => x.CreatedDate).ToList();

                    StudentAcademicsBLL studAcdBLL = new StudentAcademicsBLL();
                    StudentBLL studentBLL = new StudentBLL();
                    List<Student> studentList = studentBLL.GetAll().Where(z => z.UserName == CookieHelper.Username).ToList();

                    List<StudentAcademic> stud = studAcdBLL.GetAll().Where(y => y.StudentId == studentList[0].StudentId && y.Graduate == 1 && y.CertificatePath == string.Empty).ToList();
                    if (stud.Count != 0)
                    {
                        builder.Append("<li>");
                        builder.Append("<div class='list_3_date'>");
                        builder.Append("<span>" + DateTime.Now.ToShortDateString() + "</span>");
                        builder.Append("</div>");
                        builder.Append("<div class='list_3_detail'>");
                        builder.Append("<p>" + "In order to contact universities, please upload image of passing/degree certificate from " + stud[0].SchoolName + " insititute" + "</p>");
                        builder.Append("</div>");
                        builder.Append("</li>");
                    }

                    if (alertlist.Count != 0)
                    {
                        foreach (Alert alert in alertlist)
                        {
                            builder.Append("<li>");
                            builder.Append("<div class='list_3_date'>");
                            builder.Append("<span>" + alert.CreatedDate.ToShortDateString() + "</span>");
                            builder.Append("</div>");
                            builder.Append("<div class='list_3_detail'>");
                            builder.Append("<p>" + alert.Message + "</p>");
                            builder.Append("</div>");
                            builder.Append("</li>");
                        }
                    }
                    else
                    {
                        if(stud.Count==0)
                        builder.Append("No Alerts Found");
                    }
                    MyWidgets = builder.ToString();
                }

               
                context.Response.Write(MyWidgets);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}



