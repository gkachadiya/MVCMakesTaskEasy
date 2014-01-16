using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpotCollege.Common
{
    public class CookieKeys
    {
        public const String UserType = "UserType";
        public const String UserID = "UserID";
        public const String SubscriptionReminderFlag = "SubscriptionReminderFlag";
        public const String Username = "UserName";
        public const String EncUserID = "EncUserID";
        public const String Fullname = "Fullname";
        public const String IsResetPassword = "IsResetPassword";
    }

    public class CookieHelper
    {


        public static void SetCookie(String key, String value, DateTime expiryDate)
        {
            HttpCookie cookieRequest;
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                cookieRequest = HttpContext.Current.Request.Cookies[key];
            }
            else
            {
                cookieRequest = new HttpCookie(key);
            }
            cookieRequest.Expires = expiryDate;
            cookieRequest.Domain = ConfigHelper.CookieDomain;
            cookieRequest.Value = key;

            if (HttpContext.Current.Response.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies[key].Value = value;
            }

        }

        public static void SetCookie(String key, String value)
        {
            HttpCookie cookieRequest;
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                cookieRequest = HttpContext.Current.Request.Cookies[key];
            }
            else
            {
                cookieRequest = new HttpCookie(key);
            }
            cookieRequest.Domain = ConfigHelper.CookieDomain;
            cookieRequest.Value = value;

            if (HttpContext.Current.Response.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies[key].Value = value;
            }

        }

        public static String GetCookieValue(String key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                return HttpContext.Current.Request.Cookies[key].Value;
            }
            else
            {
                return String.Empty;
            }
        }

        public static void SetEncryptedCookie(string key, string value)
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            HttpRequestBase request = httpContext.Request;
            HttpResponseBase response = httpContext.Response;
            DateTime dtmCookieExpires = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0).AddDays(1);
            HttpCookie cookie = new HttpCookie(key, EncryptionHelper.Encrypt(value)) { Expires = dtmCookieExpires };
            if (!HttpContext.Current.Request.Url.Host.Contains("192.168.17"))
            {
                cookie.Domain = ConfigHelper.CookieDomain;
            }
            response.Cookies.Add(cookie);
        }

        public static string GetEncryptedCookieValue(string cookieName)
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            HttpRequestBase request = httpContext.Request;
            HttpResponseBase response = httpContext.Response;
            if (request.Cookies[cookieName] != null)
            {
                return EncryptionHelper.Decrypt(request.Cookies[cookieName].Value);
            }
            else
            {
                return null;
            }
        }

        public static void Clear(string key)
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            HttpResponseBase response = httpContext.Response;

            HttpCookie cookie = new HttpCookie(key)
            {
                Expires = DateTime.Now.AddDays(-1), // or any other time in the past

            };
            if (!HttpContext.Current.Request.Url.Host.Contains("192.168.17"))
            {
                cookie.Domain = ConfigHelper.CookieDomain;
            }
            response.Cookies.Add(cookie);
        }


        public static void RemoveCookie(string cookieName)
        {
            CookieHelper.Clear(cookieName);
        }

        public static void ClearLoginCookie()
        {
            CookieHelper.Clear(CookieKeys.UserID);
            CookieHelper.Clear(CookieKeys.Username);
            CookieHelper.Clear(CookieKeys.UserType);
            CookieHelper.Clear(CookieKeys.Fullname);
            CookieHelper.Clear(CookieKeys.IsResetPassword);
        }

        public static void CreateLoginCookie(string UserName, string FullName, string LoginType)
        {
            CookieHelper.SetCookie(CookieKeys.Username, UserName);
            CookieHelper.SetCookie(CookieKeys.Fullname, FullName);
            CookieHelper.SetCookie(CookieKeys.UserType, LoginType.ToString());
        }

        public static bool Exists(string key)
        {
            try
            {
                var httpContext = new HttpContextWrapper(HttpContext.Current);
                HttpRequestBase request = httpContext.Request;
                return request.Cookies[key] != null;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsLoginCookieExists()
        {
            if (CookieHelper.Exists(CookieKeys.EncUserID) && CookieHelper.Exists(CookieKeys.UserID) && CookieHelper.Exists(CookieKeys.Username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsLoggedInUser()
        {
            if (CookieHelper.Exists(CookieKeys.UserID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Int64 UserID
        {
            get
            {
                if (CookieHelper.Exists(CookieKeys.UserID))
                {
                    return Convert.ToInt64(CookieHelper.GetCookieValue(CookieKeys.UserID));
                }
                else
                {
                    return 0;
                }
            }

        }

        public static String Username
        {
            get
            {
                if (CookieHelper.Exists(CookieKeys.Username))
                {
                    return Convert.ToString(CookieHelper.GetCookieValue(CookieKeys.Username));
                }
                else
                {
                    return String.Empty;
                }
            }

        }

        public static String Fullname
        {
            get
            {
                if (CookieHelper.Exists(CookieKeys.Username))
                {
                    return Convert.ToString(CookieHelper.GetCookieValue(CookieKeys.Fullname));
                }
                else
                {
                    return String.Empty;
                }
            }

        }

        public static String UserType
        {
            get
            {
                if (CookieHelper.Exists(CookieKeys.Username))
                {
                    return Convert.ToString(CookieHelper.GetCookieValue(CookieKeys.UserType));
                }
                else
                {
                    return String.Empty;
                }
            }

        }
    }
}