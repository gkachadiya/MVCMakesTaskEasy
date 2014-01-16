using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpotCollege.BLL;
using SpotCollege.Common;
using SpotCollege.DAL.DataModels;
using SpotCollege.DAL;
using System.Data.Objects.DataClasses;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Configuration;


namespace Spot_College_MVC.Controllers
{
    [ValidateInput(false)]
    public class UniversityController : BaseController
    {
        #region Declaration
        //<summary>
        //Declaration section
        //</summary>
        UniversityBLL universityBll = new UniversityBLL();
        StudentBLL studentBll = new StudentBLL();
        StudentInterestBLL studentInterestBll = new StudentInterestBLL();
        UserBLL userBll = new UserBLL();
        Student std = null;
        AlertBLL alertBll = new AlertBLL();
        ViewProfileBLL viewProfileBLL = new ViewProfileBLL();
        static string universityName = "";
        static string universityImage = "";

        string[] privacyStudent = { };
        SettingBLL settingBLL = new SettingBLL();
        Settings settings = new Settings();
        List<Settings> settingList = new List<Settings>();

        public static int StaticUniversityId = 0;
        University university = new University();
        #endregion

        #region Methods Division

        // Get University Dashboard
        public ActionResult index()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("Index", "Authentication");

            University uni = universityBll.GetByUserName(CookieHelper.Username);
            if (uni != null)
            {
                ViewBag.UserName = uni.UniversityName;
                ViewBag.UserImage = uni.UniversityImage;
            }

            UniversityDashboardModel udm = new UniversityDashboardModel();
            var ShownInterestList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Applied).OrderByDescending(x => x.StudentInterestId).Take(5).Select(x => x.StudentUserName).ToArray();
            //var JoinedList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Approved).OrderByDescending(x => x.StudentInterestId).Take(5).Select(x => x.StudentUserName).ToArray();

            List<Student> studentlist = studentBll.GetAll();
            //this is use for privacy settings
            privacyStudent = settingBLL.GetAll().Where(x => x.University == true).Select(y => y.UserName).ToArray();
            studentlist = studentlist.Where(x => !privacyStudent.Contains(x.UserName)).ToList();  // used to remove name of student from search list
            udm.studentInterestList = studentlist.Where(x => ShownInterestList.Contains(x.UserName)).ToList();
            udm.studentRecentJoinList = studentlist.OrderByDescending(x => x.CreatedDate).Take(5).ToList();   //x => JoinedList.Contains(x.UserName)
            udm.viewPropfileList = viewProfileBLL.GetAll().Where(x => x.UniversityUsername == CookieHelper.Username).ToList();

            return View(udm);
        }

        //Get Student Profile for Approving or Decline Student Request
        public ActionResult StudentProfile()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("Index", "Authentication");

            University uni = universityBll.GetByUserName(CookieHelper.Username);
            ViewBag.UserName = uni.UniversityName;
            ViewBag.UserImage = uni.UniversityImage;



            string data = "";
            if (!string.IsNullOrEmpty(Request.Form["btnDecline"]))
            {
                //for decline student
                int uid2 = 0;
                if (int.TryParse(Request.Form["userid"], out uid2))
                {
                    Student student = studentBll.Get(uid2);
                    if (student != null)
                    {
                        StudentInterest std = studentInterestBll.GetAll().Where(x => x.StudentUserName == student.UserName && x.UniversityUserName == CookieHelper.Username).FirstOrDefault();
                        if (std != null)
                        {
                            std.StudentUserName = std.StudentUserName;
                            std.StudentInterestId = std.StudentInterestId;
                            std.Approved = 3;
                            studentInterestBll.Save(std);
                        }
                        string universityname = universityBll.GetByUserName(CookieHelper.Username).UniversityName.ToString();
                        Alert alert = new Alert();
                        alert.Message = "" + universityname + "  has Decline your request for further communication";
                        alert.CreatedDate = DateTime.Now;
                        alert.CreatedBy = CookieHelper.Username;
                        alert.UserName = student.UserName;
                        alertBll.Save(alert);

                        data = uid2.ToString();

                        return RedirectToAction("index");
                        //this is used to make decline button   hide
                        // ViewBag.Decline = false;
                    }
                }
            }
            if (string.IsNullOrEmpty(data))
                data = RouteData.Values["id"].ToString();

            int uid;
            if (int.TryParse(data, out uid))
            {
                ViewProfile viewProfile = new ViewProfile();
                std = studentBll.Get(uid);
                List<ViewProfile> viewProfileList = viewProfileBLL.GetAll().Where(x => x.UniversityUsername == CookieHelper.Username && x.StudentUsername == std.UserName).ToList();
                if (viewProfileList.Count == 0)
                {
                    viewProfile.UniversityUsername = CookieHelper.Username;
                    viewProfile.StudentUsername = std.UserName;
                    viewProfileBLL.Save(viewProfile);
                }

                StudentProfileModel spm = new StudentProfileModel();

                spm.student = FixUp(std);
                spm.studentAcademicList = std.StudentAcademics.ToList();
                spm.testList.testList = std.StudentTests.ToList();
                spm.studentWorkHistoryList = std.StudentWorkHistories.ToList();
                return View(spm);
            }
            else
                return RedirectToAction("index");
        }

        //Get University Profile Overview page
        public ActionResult Profile()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("Index", "Authentication");


            University um = universityBll.GetByUserName(CookieHelper.Username);
            ViewBag.UserName = um.UniversityName;
            ViewBag.UserImage = um.UniversityImage;
            return View(um);
        }

        //Get University Profile Edit
        public ActionResult Edit()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("Index", "Authentication");

            UniversityEditModel uem = new UniversityEditModel();
            string data = Convert.ToString(RouteData.Values["id"]);

            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString() || CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            {
                if (!string.IsNullOrEmpty(CookieHelper.adminUniversity))
                {
                    uem.university = new UniversityModel();
                    User user = userBll.Get(CookieHelper.adminUniversity);
                    if (User != null)
                    {
                        uem.registerModel.UserName = user.UserName;
                        uem.registerModel.Password = EncryptionHelper.Decrypt(user.Password);
                        uem.registerModel.ConfirmPassword = EncryptionHelper.Decrypt(user.Password);
                        uem.university = FixUp(universityBll.GetByUserName(CookieHelper.adminUniversity));
                    }
                }
                else
                {
                    uem.university = new UniversityModel();
                }
            }
            else
                uem.university = FixUp(universityBll.GetByUserName(CookieHelper.Username));



            if (data == "basic" || string.IsNullOrEmpty(data))
            {
                //load basic details
                ViewBag.Current = "basic";
            }
            else if (data == "cost")
            {
                ViewBag.Current = "cost";
            }
            else if (data == "enrollment")
            {
                ViewBag.Current = "enrollment";
            }
            else if (data == "programs")
            {
                ViewBag.Current = "programs";
            }
            else if (data == "faqs")
            {
                ViewBag.Current = "faqs";
            }
            return View(uem);
        }

        //Post University Edit Profile
        [HttpPost]
        public ActionResult Edit(UniversityEditModel uem)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("Index", "Authentication");



            if (!string.IsNullOrEmpty(Request.Form["btnSaveBasic"]))
            {
                if (ModelState.IsValid)
                {
                    University university = null;
                    if (uem.university.UniversityId == 0)
                    {
                        university = new University();
                        //Other Fields Entry

                        if (uem.registerModel != null)
                            university.UserName = uem.registerModel.UserName;
                        else
                            university.UserName = CookieHelper.Username;
                        university.Degree = "";
                        university.Courses = "";
                    }
                    else
                    {
                        university = universityBll.Get(uem.university.UniversityId);
                        if (university == null)
                        {
                            university = new University();
                            //Other Fields Entry

                            if (uem.registerModel != null)
                                university.UserName = uem.registerModel.UserName;
                            else
                                university.UserName = CookieHelper.Username;


                            university.Degree = "";
                            university.Courses = "";
                        }
                    }
                    university.UniversityName = uem.university.UniversityName;
                    university.Address = uem.university.Address;
                    university.AdminName = uem.university.AdminName;
                    university.City = uem.university.City;
                    university.Country = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(uem.university.Country)).ToString();
                    university.EstablishedYear = uem.university.EstablishedYear;
                    university.ContactNo = uem.university.ContactNo;
                    university.CountryCode = uem.university.CountryCode;
                    university.Description = uem.university.Description;
                    if (uem.university.logo_image != null)
                    {
                       
                        string Imagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(uem.university.logo_image.FileName);
                        // uem.university.logo_image.SaveAs(Server.MapPath("\\Images\\Profile\\") + Imagename);
                        uem.university.logo_image.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"]) + "/Images/Profile/" + Imagename);
                        university.Image = Imagename;
                    }

                    if (uem.university.large_image != null)
                    {
                        string UniversityImagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(uem.university.large_image.FileName);
                        // uem.university.large_image.SaveAs(Server.MapPath("\\Images\\Profile\\") + UniversityImagename);
                        uem.university.large_image.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"]) + "/Images/Profile/" + UniversityImagename);
                        university.UniversityImage = UniversityImagename;
                    }
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString() || CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
                    {
                        User user = new User();
                        if (uem.registerModel != null && string.IsNullOrEmpty(CookieHelper.adminUniversity))
                        {
                            User us = userBll.Get(uem.registerModel.UserName);
                            if (us == null)
                            {
                                user.UserName = uem.registerModel.UserName;
                                user.Password = EncryptionHelper.Encrypt(uem.registerModel.Password);
                                user.LoginTypeId = (int)LoginTypes.University;
                                user.IsActive = true;
                                userBll.Save(user);
                                CookieHelper.SetCookie(CookieKeys.adminUniversityId, uem.registerModel.UserName);
                            }
                            else
                            {
                                //if admin entered UserName that is already exist then add Model error
                                ViewBag.Current = "basic";
                                ModelState.AddModelError("registerModel.UserName", "Username already exist. Please use another.");
                                return View(uem);
                            }
                        }
                        else
                        {
                            user.UserName = uem.registerModel.UserName;
                            user.Password = EncryptionHelper.Encrypt(uem.registerModel.Password);
                            userBll.Save(user);
                        }
                    }
                    int universityId = universityBll.Save(university);

                    CookieHelper.SetCookie(CookieKeys.Fullname, university.UniversityName);
                    CookieHelper.SetCookie(CookieKeys.Photo, university.Image);


                    uem.university = FixUp(university);
                    StaticUniversityId = universityId;
                    ViewBag.Current = "cost";
                    return View(uem);
                }
                else
                {
                    ViewBag.Current = "basic";
                    return View(uem);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form["btnSaveCost"]))
            {
                if (ModelState["university.UnderGraduateFee"].Errors.Count == 0 && ModelState["university.GraduateFee"].Errors.Count == 0 && ModelState["university.BoardFee"].Errors.Count == 0 && ModelState["university.BookFee"].Errors.Count == 0)
                {
                    if (uem.university.UniversityId != 0)
                        university = universityBll.Get(uem.university.UniversityId);
                    else
                        university = universityBll.Get(StaticUniversityId);

                    if (university != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(uem.university.UnderGraduateFee)))
                            university.UnderGraduateFee = uem.university.UnderGraduateFee + "/" + uem.university.UnderGraduateFeeCurrency + "/" + uem.university.UnderGraduateFeeUnit;
                        else
                            university.UnderGraduateFee = null;

                        if (!string.IsNullOrEmpty(Convert.ToString(uem.university.GraduateFee)))
                            university.GraduateFee = uem.university.GraduateFee + "/" + uem.university.GraduateFeeCurrency + "/" + uem.university.GraduateFeeUnit;
                        else
                            university.GraduateFee = null;

                        if (!string.IsNullOrEmpty(Convert.ToString(uem.university.BookFee)))
                            university.BookFee = uem.university.BookFee + "/" + uem.university.BookFeeCurrency + "/" + uem.university.BookFeeUnit;
                        else
                            university.BookFee = null;

                        if (!string.IsNullOrEmpty(Convert.ToString(uem.university.BoardFee)))
                            university.BoardFee = uem.university.BoardFee + "/" + uem.university.BoardFeeCurrency;
                        else
                            university.BoardFee = null;

                        if (!string.IsNullOrEmpty(Convert.ToString(uem.university.ApplicationFee)))
                            university.ApplicationFee = uem.university.ApplicationFee + "/" + uem.university.ApplicationFeeCurrency;
                        else
                            university.ApplicationFee = null;

                        universityBll.Save(university);
                        ViewBag.Current = "enrollment";
                        uem.university = FixUp(university);
                        return View(uem);
                    }
                }
                else
                {
                    ViewBag.Current = "cost";
                    return View(uem);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form["btnSaveEnrollNumber"]))
            {
                if (ModelState["university.InternationalGraduate"].Errors.Count == 0 && ModelState["university.Graduates"].Errors.Count == 0 && ModelState["university.UnderGraduates"].Errors.Count == 0)
                {
                    if (uem.university.UniversityId != 0)
                        university = universityBll.Get(uem.university.UniversityId);
                    else
                        university = universityBll.Get(StaticUniversityId);
                    if (university != null)
                    {
                        if (uem.university.UnderGraduates.HasValue)
                            university.UnderGraduates = uem.university.UnderGraduates;
                        else
                            university.UnderGraduates = null;

                        if (uem.university.Graduates.HasValue)
                            university.Graduates = uem.university.Graduates;
                        else
                            university.Graduates = null;

                        if (uem.university.InternationalGraduate.HasValue)
                            university.InternationalGraduate = uem.university.InternationalGraduate;
                        else
                            university.InternationalGraduate = null;

                        if (uem.university.PersuingAssociateDegree.HasValue)
                            university.PersuingAssociateDegree = uem.university.PersuingAssociateDegree;
                        else
                            university.PersuingAssociateDegree = null;

                        if (uem.university.EnroledPerYearChina.HasValue)
                            university.EnroledPerYearChina = uem.university.EnroledPerYearChina;
                        else
                            university.EnroledPerYearChina = null;

                        if (uem.university.EnroledPerYearIndianSub.HasValue)
                            university.EnroledPerYearIndianSub = uem.university.EnroledPerYearIndianSub;
                        else
                            university.EnroledPerYearIndianSub = null;

                        if (uem.university.EnroledPerYearAfrica.HasValue)
                            university.EnroledPerYearAfrica = uem.university.EnroledPerYearAfrica;
                        else
                            university.EnroledPerYearAfrica = null;

                        if (uem.university.EnroledPerYearMidEast.HasValue)
                            university.EnroledPerYearMidEast = uem.university.EnroledPerYearMidEast;
                        else
                            university.EnroledPerYearMidEast = null;

                        if (uem.university.EnroledPerYearSouthAmerica.HasValue)
                            university.EnroledPerYearSouthAmerica = uem.university.EnroledPerYearSouthAmerica;
                        else
                            university.EnroledPerYearSouthAmerica = null;

                        if (uem.university.EnroledPerYearEurope.HasValue)
                            university.EnroledPerYearEurope = uem.university.EnroledPerYearEurope;
                        else
                            university.EnroledPerYearEurope = null;

                        universityBll.Save(university);
                        ViewBag.Current = "programs";
                        uem.university = FixUp(university);
                        return View(uem);
                    }
                }
                else
                {
                    ViewBag.Current = "enrollment";
                    return View(uem);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form["btnSavePrograms"]))
            {
                if (uem.university.UniversityId != 0)
                    university = universityBll.Get(uem.university.UniversityId);
                else
                    university = universityBll.Get(StaticUniversityId);
                if (university != null)
                {
                    string offerdegree = Request.Form["chkCourses"];
                    string CoursesOffered = Request.Form["chkDegree"];
                    university.Courses = CoursesOffered;
                    university.Degree = offerdegree;
                    universityBll.Save(university);
                    ViewBag.Current = "faqs";
                    uem.university = FixUp(university);
                    return View(uem);
                }
                else
                {
                    ViewBag.Current = "programs";
                    return View(uem);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form["btnSaveFaq"]))
            {
                if (uem.university.UniversityId != 0)
                    university = universityBll.Get(uem.university.UniversityId);
                else
                    university = universityBll.Get(StaticUniversityId);
                university.Faqs = uem.university.Faqs;
                universityBll.Save(university);

                if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString() || CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
                {
                    return RedirectToAction("UniversityInformation", "AdminSec");
                }
                else
                    return RedirectToAction("index");
            }

            return View(uem);
        }
        #endregion

        #region Web Methods Division

        //for sending approval message to student
        [WebMethod()]
        public string _SendMessage(string Title, string Description, string sendToUserName, string ParentId)
        {
            UniversityBLL universityBLL = new UniversityBLL();
            AlertBLL alertBll = new AlertBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            string str = string.Empty;

            StudentInterest std = studentInterestBLL.GetAll().Where(x => x.StudentUserName == sendToUserName && x.UniversityUserName == CookieHelper.Username).FirstOrDefault();
            if (std.Approved == 1)
            {
                if (std != null)
                {
                    std.StudentUserName = std.StudentUserName;
                    std.StudentInterestId = std.StudentInterestId;
                    std.Approved = 2;
                    studentInterestBLL.Save(std);
                }
                string universityname = universityBLL.GetByUserName(CookieHelper.Username).UniversityName.ToString();
                Alert alert = new Alert();
                alert.Message = "" + universityname + "  has approved your request for further communication ";
                alert.CreatedDate = DateTime.Now;
                alert.CreatedBy = CookieHelper.Username;
                alert.UserName = sendToUserName;
                alertBll.Save(alert);
            }
            MessageBLL messageBLL = new MessageBLL();
            SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
            msg.Title = Title;
            msg.Description = Description;
            msg.ParentId = Convert.ToInt32(ParentId);
            msg.FromUserName = CookieHelper.Username;
            msg.ToUserName = sendToUserName;
            msg.IsRead = false;
            msg.IsApproved = false;
            msg.Flag = true;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);

            return msg.MessageId.ToString();
        }

        #endregion

    }
}

