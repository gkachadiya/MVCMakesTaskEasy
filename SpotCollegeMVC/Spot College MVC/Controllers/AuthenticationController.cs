using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Spot_College_MVC.Filters;
using Spot_College_MVC.Models;
using SpotCollege.BLL;
using SpotCollege.Common;
using SpotCollege.DAL;
using System.Web.Services;
using System.Text;
using System.Dynamic;
using System.Configuration;
namespace Spot_College_MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Declaration
        UserBLL userBll = new UserBLL();
        UniversityBLL universityBll = new UniversityBLL();
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        #endregion

        #region Methods Division

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult index(string returnUrl)
        {
            if (CookieHelper.Username != "")
            {
                SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
                if (user != null)
                {
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                        return RedirectToAction("index", "AdminSec");

                    // this is for sub admin user
                    if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
                        return RedirectToAction("index", "AdminSec");

                    else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                    {
                        University university = universityBll.GetByUserName(CookieHelper.Username);
                        if (university != null)
                            return RedirectToAction("index", "University");
                        else
                            return RedirectToAction("Edit", "University");
                    }


                    else if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                    {
                        SpotCollege.DAL.Student student = studentBll.GetByUserName(CookieHelper.Username);
                        StudentAcademic stdAca = new StudentAcademic();
                        if (student != null)
                            stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();

                        if (student != null && stdAca != null)
                            return RedirectToAction("EditProfile/CurrentAcademic", "Student");

                        if (student != null && string.IsNullOrEmpty(student.DesiredFieldofStudy))
                            return RedirectToAction("EditProfile/EduPref", "Student");

                        return RedirectToAction("index", "Student");

                    }
                    else
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        return View();
                    }
                }
                else
                {
                    ViewBag.ReturnUrl = returnUrl;
                    return View();
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult index(HomaPageModel lm, string returnUrl)
        {
            string adminusername = userBll.GetAll().Where(x => x.LoginTypeId == (int)LoginTypes.Admin).FirstOrDefault().UserName;
            CookieHelper.SetAdminUserName(EncryptionHelper.Encrypt(adminusername));

            #region Login
            if (lm.registerModel != null)
            {
                string str = string.Empty;
                SpotCollege.DAL.User user = null;

                user = userBll.Get(lm.registerModel.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("duplicateUser", "UserName already exist in Record. Please Choose Any other..!");
                    return View(lm);
                }
                user = new SpotCollege.DAL.User();

                user.UserName = lm.registerModel.UserName;
                user.Password = EncryptionHelper.Encrypt(lm.registerModel.Password);
                user.LoginTypeId = (int)LoginTypes.Student;
                user.IsActive = true;
                userBll.Save(user);

                //fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/file.txt"));

                // send mail to the User

                //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendUserNameandPassword.htm"));
                StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendUserNameandPassword.htm")));
                emailBody.Replace(EmailConstantHelper.UserName, lm.registerModel.UserName);
                emailBody.Replace(EmailConstantHelper.Password, lm.registerModel.Password);
                MailHelper.sendMail(lm.registerModel.UserName, "SpotCollege", emailBody.ToString());

                //StringBuilder mailmsg = new StringBuilder(string.Empty);
                //mailmsg.Append("<h3>Your account has been created.</h3>");
                //mailmsg.Append("<br/><b>Your UserName : </b>" + lm.registerModel.UserName);
                //mailmsg.Append("<br/><b>Your Password : </b>" + lm.registerModel.Password);
                //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                //MailHelper.sendMail(lm.registerModel.UserName, "Thanks to create Account : SpotCollege", mailmsg.ToString());

                CookieHelper.CreateLoginCookie(user.UserName, "", ((int)LoginTypes.Student).ToString());

                return RedirectToAction("EditProfile", "Student");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string pass = string.Empty;
                    SpotCollege.DAL.User user = userBll.Get(lm.loginModel.UserName);
                    if (user != null)
                    {
                        if (user.LoginTypeId == (int)LoginTypes.Admin) //it's check bcoz der is no encrypt password for admin user
                            pass = user.Password;
                        else
                            pass = EncryptionHelper.Decrypt(user.Password);

                        if (user != null && pass == lm.loginModel.Password && user.IsActive == true)
                        {
                            CookieHelper.CreateLoginCookie(user.UserName, "", user.LoginTypeId.ToString());

                            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                                return RedirectToAction("index", "AdminSec");

                            if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
                                return RedirectToAction("index", "AdminSec");

                            else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                            {
                                University university = universityBll.GetByUserName(CookieHelper.Username);
                                CookieHelper.SetCookie(CookieKeys.Fullname, university.UniversityName);
                                CookieHelper.SetCookie(CookieKeys.Photo, university.Image);
                                if (university != null)
                                    return RedirectToAction("index", "University");
                                else
                                    return RedirectToAction("Edit", "University");
                            }
                            else if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                            {
                                SpotCollege.DAL.Student student = studentBll.GetByUserName(CookieHelper.Username);

                                if (student != null)
                                {
                                    CookieHelper.SetCookie(CookieKeys.Fullname, student.FirstName + " " + student.LastName);
                                    CookieHelper.SetCookie(CookieKeys.Photo, student.Photo);
                                }
                                else
                                {
                                    CookieHelper.SetCookie(CookieKeys.Fullname, "[no-name]");
                                    CookieHelper.RemoveCookie(CookieKeys.Photo);
                                }

                                StudentAcademic stdAca = new StudentAcademic();
                                if (student != null)
                                    stdAca = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).FirstOrDefault();

                                if (student != null && stdAca != null)
                                    return RedirectToAction("index", "Student");

                                else
                                    return RedirectToAction("EditProfile", "Student");
                            }
                            else
                                return RedirectToAction("index", "Account");
                        }
                        else
                            ModelState.AddModelError("", "Username or Password is Wrong.Please Try Again..!");
                    }
                    else
                        ModelState.AddModelError("", "Username or Password is Wrong.Please Try Again..!");
                }
                else
                    return View(lm);
            }
            #endregion
            return View(lm);
        }

        //[HttpGet]
        //public ActionResult FacebookLogin()
        //{
        //    if (CookieHelper.Username != null && CookieHelper.Username != "")
        //    {
        //        return RedirectToAction("EditProfile", "Student");
        //    }
        //    else
        //        return View();
        //}

        // for logout
        [HttpGet]
        public ActionResult LogOff()
        {
            CookieHelper.ClearLoginCookie();
            return RedirectToAction("index", "Authentication");
        }

        //Get for forgot password
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //Post for forgot password
        [HttpPost]
        public ActionResult ForgotPassword(ForgetPasswordModel fpm)
        {
            string pass = string.Empty;
            try
            {
                SpotCollege.DAL.User user = userBll.Get(fpm.UserName);
                if (user != null)
                {
                    if (user.LoginTypeId == (int)LoginTypes.Admin)
                        pass = user.Password;
                    else
                        pass = EncryptionHelper.Decrypt(user.Password);

                    //string password = Guid.NewGuid().ToString().Substring(0, 6);
                    //user.UserName = user.UserName;
                    //user.Password = password;
                    //userBll.Save(user);

                    // send mail to the User

                    //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendForgotPassword.htm"));
                    StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendForgotPassword.htm")));
                    emailBody.Replace(EmailConstantHelper.Password, pass);
                    MailHelper.sendMail(fpm.UserName, "SpotCollege", emailBody.ToString());

                    //StringBuilder mailmsg = new StringBuilder(string.Empty);
                    //mailmsg.Append("<h3>" + " Your Password is : " + pass + "</h3>");
                    //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");`
                    //MailHelper.sendMail(fpm.UserName, "Changed Password : SpotCollege", mailmsg.ToString());


                    ViewBag.msg = "Your Password has been Successfully Changed and New Password sent on your Email Address";

                }
                else
                {
                    ModelState.AddModelError("errMsg", "Invalid Username");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ModelState.AddModelError("errMsg", "Invalid Username");
            }
            return View(fpm);
        }

        #endregion

        #region Web Methods Division
        //for check the username is valid or not
        [WebMethod()]
        public string ValidateUser(string UserName)
        {
            UserBLL userBll = new UserBLL();
            string str = string.Empty;
            SpotCollege.DAL.User user = userBll.Get(UserName);

            if (user != null)
            {
                str = "true";
            }
            else
            {
                str = "false";
            }
            return str;
        }
        #endregion
    }

}
