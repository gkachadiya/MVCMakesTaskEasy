using SpotCollege.BLL;
using SpotCollege.Common;
using SpotCollege.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpotCollege.Handler
{
    /// <summary>
    /// Summary description for GetNoOfUnReadMsg
    /// </summary>
    public class GetNoOfUnReadMsg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            MessageBLL msgBLL = new MessageBLL();
            List<Message> msg = msgBLL.GetAll().Where(x =>!string.IsNullOrEmpty(x.IsApproved.ToString()) && x.ToUserName == CookieHelper.Username && x.IsRead == false && x.IsApproved == true).ToList();
            String str = msg.Count.ToString();
            context.Response.ContentType = "text/plain";
            context.Response.Write(str);
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