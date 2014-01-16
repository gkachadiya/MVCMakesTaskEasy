using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpotCollege.Common
{
    public class ConfigKey
    {
        public const String Version = "Version";
        public const String AdoConnstring = "AdoConnstring";
        public const String ConnectionString = "SpotCollegeEntities";
        public const String SiteImagePath = "SiteImagePath";
        public const String JavaScriptPath = "JavaScriptPath";
        public const String SiteCssPath = "SiteCssPath";
        public const String SiteUrlPath = "SiteUrlPath";
        public const String CookieDomain = "CookieDomain";
        public const String LogType = "LogType";
        public const String LogFilesPath = "LogFilesPath";
        public const String PageSize = "PageSize";
        public const String SMTP_Server = "SMTP_Server";
        public const String SMTP_Username = "SMTP_Username";
        public const String SMTP_Password = "SMTP_Password";
        public const String SMTP_Port = "SMTP_Port";
        public const string EmailTemplatePath = "EmailTemplatePath";
        public const String AdminEmailID = "Admin_EmailID";
    }

    public class ConfigHelper
    {
        public static String AdoConnstring
        {
            get { return ConfigurationManager.ConnectionStrings[ConfigKey.AdoConnstring].ConnectionString; }
        }

        public static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[ConfigKey.ConnectionString].ConnectionString; }
        }

        public static String SiteImagePath
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.SiteImagePath]; }
        }

        public static String JavaScriptPath
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.JavaScriptPath]; }
        }

        public static String SiteCssPath
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.SiteCssPath]; }
        }

        public static String SiteUrlPath
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.SiteUrlPath]; }
        }

        public static String CookieDomain
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.CookieDomain]; }
        }

        public static String LogType
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.LogType]; }
        }

        public static String LogFilesPath
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.LogFilesPath]; }
        }

        public static Int32 PageSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings[ConfigKey.PageSize]); }
        }

        public static String SMTP_Server
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.SMTP_Server]; }
        }

        public static String SMTP_Username
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.SMTP_Username]; }
        }

        public static String SMTP_Password
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.SMTP_Password]; }
        }

        public static Int32 SMTP_Port
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings[ConfigKey.SMTP_Port]); }
        }

        public static String Version
        {
            get { return ConfigurationManager.AppSettings[ConfigKey.Version]; }
        }
        public static String EmailTemplatePath
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigKey.EmailTemplatePath];
            }
        }

        public static String AdminEmailID
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigKey.AdminEmailID];
            }
        }

       
    }


}
