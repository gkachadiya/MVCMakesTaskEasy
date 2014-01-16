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

namespace SpotCollege
{
    public partial class ViewAllAlert : System.Web.UI.Page
    {
        public static List<Alert> StaticAlertList = new List<Alert>();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindAlertList();
        }

        void BindAlertList()
        {
             StringBuilder builder = new StringBuilder();
            AlertBLL alertBll = new AlertBLL();
            List<Alert> alertlist = alertBll.GetAll().Where(x=>x.UserName==CookieHelper.Username).OrderByDescending(x => x.CreatedDate).ToList();

             StaticAlertList = alertlist.ToList(); //msgStaticList will be used for displaying record
             if (alertlist.Count < 20)
                StaticAlertList.RemoveAll(x => 1 == 1);
            else
                StaticAlertList.RemoveRange(0, 20);

              if (alertlist.Count != 0)
                    {
                        foreach (Alert alert in alertlist.Take(20))
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
                        if (alertlist.Count == 0)
                        builder.Append("No Alerts Found");
                    }

            AlertListAll.InnerHtml=builder.ToString();
        }


       // METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST
        [WebMethod()]
        public static string AppendAndDisplayAlert()
        {
            StringBuilder sb = new StringBuilder();
            StudentBLL studentBll = new StudentBLL();
            UniversityBLL universityBLL = new UniversityBLL();

            List<Alert> tmpAlertList = new List<Alert>();
            if (StaticAlertList != null)
            {
                if (StaticAlertList.Count != 0)
                {
                    if (StaticAlertList.Count < 20)
                    {
                        tmpAlertList = StaticAlertList.ToList();
                        StaticAlertList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpAlertList = StaticAlertList.GetRange(0, 20);
                        StaticAlertList.RemoveRange(0, 20);
                    }



                    string img;
                    for (int i = 0; i < tmpAlertList.Count; i++)
                    {
                            sb.Append("<li>");
                            sb.Append("<div class='list_3_date'>");
                            sb.Append("<span>" + tmpAlertList[i].CreatedDate.ToShortDateString() + "</span>");
                            sb.Append("</div>");
                            sb.Append("<div class='list_3_detail'>");
                            sb.Append("<p>" + tmpAlertList[i].Message + "</p>");
                            sb.Append("</div>");
                            sb.Append("</li>");
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