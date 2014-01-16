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
namespace Spot_College_MVC.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {

        UserBLL userBll = new UserBLL();
        UniversityBLL universityBll = new UniversityBLL();
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        //
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
                       return RedirectToAction("index", "Admin");


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
                            return RedirectToAction("index", "Student");

                        else
                            return RedirectToAction("Edit", "Student");
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

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult index(LoginModel lm, string returnUrl)
        {

            SpotCollege.DAL.User user = userBll.Get(lm.UserName);
            if (user != null && user.Password == lm.Password && user.IsActive == true)
            {
                CookieHelper.CreateLoginCookie(user.UserName, "", user.LoginTypeId.ToString());
                if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    return RedirectToAction("index", "Admin");

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
                        return RedirectToAction("index", "Student");

                    else
                        return RedirectToAction("Edit", "Student");
                }
                else
                    return RedirectToAction("index", "Account");
            }
            else
                ModelState.AddModelError("", "Username or Password is Wrong.Please Try Again..!");
            return View(lm);
        }

        //
        // POST: /Account/LogOff

        [HttpGet]
        public ActionResult LogOff()
        {
            CookieHelper.ClearLoginCookie();
            return RedirectToAction("index", "Account");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {

                }
                catch (MembershipCreateUserException e)
                {
                    //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [WebMethod()]
        public static string RegisterUser()
        {
            string str = string.Empty;
            UserBLL userBll = new UserBLL();
            SpotCollege.DAL.User user = new SpotCollege.DAL.User();
            

            user.LoginTypeId = (int)LoginTypes.Student;
            user.IsActive = true;
            userBll.Save(user);

            CookieHelper.CreateLoginCookie(user.UserName, "", ((int)LoginTypes.Student).ToString());
            return str;
        }



    }
}
