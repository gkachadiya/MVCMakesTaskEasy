using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.Mvc;



namespace SpotCollege.Common
{
    public class CommonHelper
    {
       public static String ReadEmailTextFile(String fileName)
       {
           
         // return File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "\\EmailTextFile\\" + fileName));
           return File.ReadAllText(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/" + fileName);
           
       }
    }
}
