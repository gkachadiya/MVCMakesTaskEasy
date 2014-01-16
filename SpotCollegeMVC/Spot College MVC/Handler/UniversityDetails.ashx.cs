using SpotCollege.BLL;
using SpotCollege.Common;
using SpotCollege.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Spot_College_MVC.Handler
{
    /// <summary>
    /// Summary description for UniversityDetails
    /// </summary>
    public class UniversityDetails : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string UniversityName = "NIT";
            //string str=string.Empty;
            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            SpotCollege.DAL.Student std = new SpotCollege.DAL.Student();
            StudentBLL studentBll = new StudentBLL();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            StringBuilder sb = new StringBuilder();
            string[] strArr = new string[15];
            string[] tmp;
            string[] tmpCourseArr;

            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                std = studentBll.GetByUserName(CookieHelper.Username);


                List<StudentTest> stdTest = studentTestBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();

                if (stdTest.Count == 0)
                {
                    //return str = "Please input atleast one test scores in order to contact universities";
                    university = null;
                    context.Response.Write(null);
                }
                else
                {

                    university = universityBll.GetByUniversityName(UniversityName);

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
                        tmp = university.UnderGraduateFee.ToString().Split('/');
                        strArr[8] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                    }
                    if (university.GraduateFee != null)
                    {
                        tmp = university.GraduateFee.ToString().Split('/');
                        strArr[9] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                    }
                    if (university.BookFee != null)
                    {
                        tmp = university.BookFee.ToString().Split('/');
                        strArr[10] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                    }
                    if (university.BoardFee != null)
                    {
                        tmp = university.BoardFee.ToString().Split('/');
                        strArr[11] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + " Per Month";
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

                    StudentInterest stduser = studentInterestBLL.GetAll().Where(x => x.StudentUserName == CookieHelper.Username && x.UniversityUserName == university.UserName).FirstOrDefault();
                    if (stduser != null)
                        strArr[14] = "hideStdInt";
                    else
                        strArr[14] = "ShowStdInt";

                    context.Response.Write(strArr);
                    // return JsonConvert.SerializeObject(university);
                }
            }
            else
            {
                university = universityBll.GetByUniversityName(UniversityName);
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
                    tmp = university.UnderGraduateFee.ToString().Split('/');
                    strArr[8] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                }
                if (university.GraduateFee != null)
                {
                    tmp = university.GraduateFee.ToString().Split('/');
                    strArr[9] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                }
                if (university.BookFee != null)
                {
                    tmp = university.BookFee.ToString().Split('/');
                    strArr[10] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                }
                if (university.BoardFee != null)
                {
                    tmp = university.BoardFee.ToString().Split('/');
                    strArr[11] = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + " Per Month";
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
                context.Response.Write(strArr);
                //return JsonConvert.SerializeObject(university);
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