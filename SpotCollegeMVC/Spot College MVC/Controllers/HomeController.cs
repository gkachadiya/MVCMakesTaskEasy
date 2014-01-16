using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spot_College_MVC.Models;
using System.Web.Services;
using SpotCollege.Common;
using SpotCollege.DAL;
using SpotCollege.BLL;
using System.Text;
using SpotCollege.DAL.DataModels;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace Spot_College_MVC.Controllers
{
    public class HomeController : Controller
    {
        #region Declaration
        UserBLL userBll = new UserBLL();
        UniversityBLL universityBll = new UniversityBLL();
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        StudentInterestBLL studentInterestBll = new StudentInterestBLL();
        University university = new University();

        static List<Student> StaticStudentList = new List<Student>();
        static List<StudentInterest> StaticStudentInterestList = new List<StudentInterest>();
        List<Student> studentlist = new List<Student>();
        List<StudentInterest> InterestList = new List<StudentInterest>();
        SettingBLL settingBLL = new SettingBLL();
        Settings settings = null;
        SurveyDetail surveyDetail = new SurveyDetail();
        SurveyBLL surveyBLL = new SurveyBLL();
        #endregion

        #region Methods Division
        //Get About us page
        public ActionResult Aboutus()
        {
            return View();
        }

        //Get Privacy and Policy page
        public ActionResult PrivacyAndPolicy()
        {
            return View();
        }

        //Get term and Condition page
        public ActionResult TermToUse()
        {
            return View();
        }

        //Get Careers page
        public ActionResult Careers()
        {
            return View();
        }

        //Get Setting page
        public ActionResult Setting()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            Student current_student = studentBll.GetByUserName(CookieHelper.Username);
            if (current_student == null)
            {
                return RedirectToAction("EditProfile/fl", "Student");
            }
            else if (current_student.StudentAcademics.ToList().Count == 0)
            {
                //return RedirectToAction("EditProfile", "Student", new { id = "CurrentAcademic", chk = "hiiii" });
                return RedirectToAction("EditProfile/CurrentAcademic/fl", "Student");
            }
            else if (string.IsNullOrEmpty(current_student.DesiredFieldofStudy))
            {
                return RedirectToAction("EditProfile/EduPref/fl", "Student");
            }


            SettingsModel sm = new SettingsModel();
            string data = Convert.ToString(RouteData.Values["id"]);

            int editId = Convert.ToInt32(RouteData.Values["chk"]);

            settings = settingBLL.GetByUsername(CookieHelper.Username);
            if (settings != null)
            {
                if (data == "Com")
                {
                    sm.commnicationSetting.SettingId = settings.SettingId;
                    sm.commnicationSetting.DailyEmail = settings.DailyEmail;
                    sm.commnicationSetting.StudentsEmail = settings.StudentsEmail;
                    sm.commnicationSetting.UniversityEmail = settings.UniversityEmail;
                    sm.commnicationSetting.PromotionalEmail = settings.PromotionalEmail;
                }
                else if (data == "Del")
                {

                }
                else
                {
                    sm.privacySetting.SettingId = settings.SettingId;
                    sm.privacySetting.Students = settings.Students;
                    sm.privacySetting.University = settings.University;

                }
            }
            return View(sm);
        }

        //Post Setting page
        [HttpPost]
        public ActionResult Setting(SettingsModel sm)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            #region Comminication Setting
            if (!string.IsNullOrEmpty(Request.Form["btnSaveComminicationSettings"]))
            {
                if (ModelState.IsValid)
                {
                    SaveCommunicationSetting(sm.commnicationSetting);
                    ViewBag.Current = "Com";
                    sm.commnicationSetting.SettingId = settings.SettingId;
                    sm.commnicationSetting.DailyEmail = settings.DailyEmail;
                    sm.commnicationSetting.StudentsEmail = settings.StudentsEmail;
                    sm.commnicationSetting.UniversityEmail = settings.UniversityEmail;
                    sm.commnicationSetting.PromotionalEmail = settings.PromotionalEmail;
                    ModelState.Clear();
                    return View(sm);
                }
                else
                {
                    ViewBag.Current = "Com";
                    return View(sm);
                }
            }
            #endregion

            #region Privacy Setting
            if (!string.IsNullOrEmpty(Request.Form["btnSavePrivacySettings"]))
            {
                if (ModelState.IsValid)
                {
                    SavePrivacySettings(sm.privacySetting);
                    ViewBag.Current = "Pri";
                    sm.privacySetting.SettingId = settings.SettingId;
                    sm.privacySetting.Students = settings.Students;
                    sm.privacySetting.University = settings.University;
                    ModelState.Clear();
                    return View(sm);
                }
                else
                {
                    ViewBag.Current = "Pri";
                    return View(sm);
                }
            }
            return View(sm);
            #endregion
        }

        private bool SaveCommunicationSetting(CommnicationSettingModel comModel)
        {

            if (comModel.SettingId != 0)
            {
                settings = settingBLL.Get(comModel.SettingId);
            }
            else
            {
                settings = new Settings();
            }

            settings.UserName = CookieHelper.Username;
            settings.DailyEmail = comModel.DailyEmail;
            settings.StudentsEmail = comModel.StudentsEmail;
            settings.UniversityEmail = comModel.UniversityEmail;
            settings.PromotionalEmail = comModel.PromotionalEmail;

            settingBLL.Save(settings);
            return true;
        }
        private bool SavePrivacySettings(PrivacySettingModel priModel)
        {
            if (priModel.SettingId != 0)
            {
                settings = settingBLL.Get(priModel.SettingId);
            }
            else
            {
                settings = new Settings();
            }

            settings.UserName = CookieHelper.Username;
            settings.Students = priModel.Students;
            settings.University = priModel.University;

            settingBLL.Save(settings);
            return true;
        }

        //Get FAQ page
        public ActionResult FAQ()
        {
            return View();
        }

        //Get Login Page
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
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

        //Get View all Student list
        [HttpGet]
        public ActionResult ViewAllList(string id)
        {
            StudentProfileModel spm = new StudentProfileModel();

            //for get all student list for student login
            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                Student student = studentBll.GetByUserName(CookieHelper.Username);

                if (id == "Country")
                    studentlist = studentBll.GetAll().Where(x => x.Country == student.Country && x.UserName != CookieHelper.Username).ToList();
                else if (id == "Course")
                    studentlist = studentBll.GetAll().Where(x => x.LookingFor == student.LookingFor && x.UserName != CookieHelper.Username).ToList();

                StaticStudentList = studentlist.ToList(); //StaticStudentList will be used for displaying record
                if (studentlist.Count < 6)
                    StaticStudentList.RemoveAll(x => 1 == 1);
                else
                    StaticStudentList.RemoveRange(0, 6);

                spm.studentList = studentlist.Take(6).ToList();
            }

            //for get all Interested student list for university login
            if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                if (id == "Applied")
                    InterestList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Applied).ToList();
                else if (id == "Approved")
                    InterestList = studentInterestBll.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Approved).ToList();

                StaticStudentInterestList = InterestList.ToList(); //StaticStudentInterestedList will be used for displaying record
                if (InterestList.Count < 6)
                    StaticStudentInterestList.RemoveAll(x => 1 == 1);
                else
                    StaticStudentInterestList.RemoveRange(0, 6);

                spm.studentInterestList = InterestList.Take(6).ToList();
            }

            return View(spm);
        }

        //Get register university
        public ActionResult RegisterUniversity()
        {
            UniversityRegistrationModel um = new UniversityRegistrationModel();
            return View(um);
        }

        //post register university (for send application to admin register university)
        [HttpPost]
        public ActionResult RegisterUniversity(UniversityRegistrationModel um)
        {
            if (ModelState.IsValid)
            {


                User us = userBll.Get(um.UserName);
                if (us != null)
                {
                    ModelState.AddModelError("", "Email already exist in Record. Please Choose Any other..!");
                    return View(um);
                }
                else
                {
                    //User Data
                    SpotCollege.DAL.User user = new SpotCollege.DAL.User();
                    user.UserName = um.UserName;
                    string password = Guid.NewGuid().ToString().Substring(6, 10);
                    user.Password = EncryptionHelper.Encrypt(password);
                    user.LoginTypeId = (int)LoginTypes.University;
                    user.IsActive = false;
                    userBll.Save(user);

                    //University Data

                    university.UniversityName = um.UniversityName;
                    university.UserName = um.UserName;
                    university.Address = um.Address;
                    university.City = um.City;
                    university.Country = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(um.Country)).ToString();
                    university.ContactNo = um.ContactNo;
                    university.CountryCode = um.CountryCode;
                    university.AdminName = um.AdminName;
                    universityBll.Save(university);

                    string adminusername = userBll.GetAll().Where(x => x.LoginTypeId == (int)LoginTypes.Admin).FirstOrDefault().UserName;

                    try
                    {
                        //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendNewUniversityDetail.htm"));
                        StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendNewUniversityDetail.htm")));
                        emailBody.Replace(EmailConstantHelper.UniversityName, um.UniversityName);
                        emailBody.Replace(EmailConstantHelper.UniversityEmail, um.UserName);
                        emailBody.Replace(EmailConstantHelper.UniversityAddress, um.Address);
                        emailBody.Replace(EmailConstantHelper.UniversityCity, um.City);
                        emailBody.Replace(EmailConstantHelper.UniversityCountry, EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(um.Country)).ToString());
                        emailBody.Replace(EmailConstantHelper.UniversityContact, um.ContactNo);
                        MailHelper.sendMail(um.UserName, "Thanks to create Account : SpotCollege", emailBody.ToString());
                        MailHelper.sendMail(adminusername, "Intrested University : SpotCollege", emailBody.ToString());

                        //StringBuilder mailmsg = new StringBuilder(string.Empty);
                        //mailmsg.Append("<h3>" + "Hi " + um.UniversityName + "</h3>");
                        //mailmsg.Append("<br/>Your account has been created and spotcollege admin will contact you soon");
                        //mailmsg.Append("<br/><b>University Name :</b>" + um.UniversityName);
                        //mailmsg.Append("<br/><b>University Email :</b>" + um.UserName);
                        //mailmsg.Append("<br/><b>University Address :</b>" + um.Address);
                        //mailmsg.Append("<br/><b>University City :</b>" + um.City);
                        //mailmsg.Append("<br/><b>University Country :</b>" + EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(um.Country)).ToString());
                        //mailmsg.Append("<br/><b>University Contact No :</b>" + um.ContactNo);

                        //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                        //MailHelper.sendMail(um.UserName, "Thanks to create Account : SpotCollege", mailmsg.ToString());

                        //MailHelper.sendMail(adminusername, "Intrested University : SpotCollege", mailmsg.ToString());

                        ViewBag.lblMsg = "Thank You! Our representative will get in touch with you soon";
                    }
                    catch (Exception ex)
                    {

                    }

                }
                um.UniversityName = "";
                um.UserName = "";
                um.Address = "";
                um.ContactNo = "";
                um.City = "";
                um.AdminName = "";
                um.Country = "0";
                um.CountryCode = "0";
                ModelState.Clear();
                return View(um);
            }
            um.UniversityName = "";
            um.UserName = "";
            um.Address = "";
            um.ContactNo = "";
            um.City = "";
            um.AdminName = "";
            um.Country = "0";
            um.CountryCode = "0";
            ModelState.Clear();
            return View(um);
        }

        #endregion

        #region Web Methods Division
        //for register new Student
        [WebMethod()]
        public string _RegisterUser()
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

        //for check the username valid or not
        [WebMethod()]
        public JsonResult _ValidateUser(string UserName)
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
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //For delete student record 
        [WebMethod()]
        public JsonResult _StudentDelete(string usernamedel)
        {
            #region declaration
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            AlertBLL alertBLL = new AlertBLL();
            UserBLL userBll = new UserBLL();
            StudentTestBLL studentTestbll = new StudentTestBLL();
            StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
            StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();
            StudentBLL studentBll = new StudentBLL();
            ViewProfileBLL viewProfileBLL = new ViewProfileBLL();
            #endregion

            if (!string.IsNullOrEmpty(usernamedel))
            {
                settings = settingBLL.GetByUsername(usernamedel);
                if (settings != null)
                {
                    settingBLL.delete(settings.SettingId);
                }

                int StudentId = studentBll.GetByUserName(usernamedel).StudentId;

                List<StudentTest> studentTest = studentTestbll.GetAll().Where(x => x.StudentId == StudentId).ToList();
                foreach (var li in studentTest)
                {
                    studentTestbll.delete(li.StudentTestId);
                }

                List<StudentAcademic> studentAcademics = academicBll.GetAll().Where(x => x.StudentId == StudentId).ToList();
                foreach (var li in studentAcademics)
                {
                    academicBll.delete(li.StudentAcademicsId);
                }
                List<StudentWorkHistory> StudentWorkhistory = workHistoryBll.GetAll().Where(x => x.StudentId == StudentId).ToList();
                foreach (var lis in StudentWorkhistory)
                {
                    workHistoryBll.delete(lis.StudentWorkHistoryId);
                }

                List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.StudentUserName == usernamedel).ToList();
                foreach (var lia in studentInterest)
                {
                    studentInterestBLL.delete(lia.StudentInterestId);
                }

                List<ViewProfile> viewProfile = viewProfileBLL.GetAll().Where(x => x.StudentUsername == usernamedel).ToList();
                foreach (var liv in viewProfile)
                {
                    viewProfileBLL.delete(liv.ViewId);
                }

                List<Alert> Alert = alertBLL.GetAll().Where(x => x.UserName == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }
                List<Message> message = messageBLL.GetAll().Where(x => x.FromUserName == usernamedel).ToList();
                foreach (var lim in message)
                {
                    messageBLL.delete(lim.MessageId);
                }

                List<Message> messaget = messageBLL.GetAll().Where(x => x.ToUserName == usernamedel).ToList();
                foreach (var limt in messaget)
                {
                    messageBLL.delete(limt.MessageId);
                }

                if (studentBll.delete(StudentId))
                {
                    userBll.delete(usernamedel);
                }
                else
                {
                    return Json(str, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF DATA TO LIST (For Scrolling)
        [WebMethod()]
        public JsonResult _AppendAndDisplayStudents()
        {
            StringBuilder sb = new StringBuilder();

            List<Student> tmpStudentList = new List<Student>();
            List<StudentInterest> tmpStudentInterestList = new List<StudentInterest>();
            StudentBLL studentBll = new StudentBLL();

            if (StaticStudentInterestList.Count != 0)
            {
                #region studInterest
                if (StaticStudentInterestList.Count < 6)
                {
                    tmpStudentInterestList = StaticStudentInterestList.ToList();
                    StaticStudentInterestList.RemoveAll(x => 1 == 1);
                }
                else
                {
                    tmpStudentInterestList = StaticStudentInterestList.GetRange(0, 6);
                    StaticStudentInterestList.RemoveRange(0, 6);
                }

                string img;
                string tmpurl = "";
                string field;

                for (int i = 0; i < tmpStudentInterestList.Count; i++)
                {

                    #region MyRegion

                    string username = "";
                    Student std = studentBll.GetByUserName(tmpStudentInterestList[i].StudentUserName);
                    if (std != null)
                        username = std.StudentId.ToString();
                    else
                        username = "0";

                    //  tmpurl = "~/Student/Profile?intu=" + username;


                    if (std.DesiredFieldofStudy != null)
                        field = " in " + std.DesiredFieldofStudy;
                    else
                        field = "";

                    if (std.Photo != null)
                        img = "/Images/Profile/" + std.Photo;
                    else
                        img = "/Images/no_image.jpg";
                    sb.Append("<li><div class='clg_img' style='width:8%; border:0px'> <img src='" + img + "' /> </div>");
                    sb.Append(" <div class='clg_descripition'><p><b>" + std.FirstName + " " + std.LastName + "</b><br/> Applying from " + std.City + " for " + Enum.Parse(typeof(ProgramLookingFor), std.LookingFor.ToString()) + " " + field + " in " + std.LookingForCountry + ". <br/> Desired joining in date " + Enum.Parse(typeof(expectedStart), std.StartDate.ToString()) + ". </p></div>");
                    sb.Append("<div class='clg_apply'> <a href=\"/University/StudentProfile/'" + std.StudentId + "'\" class='button fright'>Profile</a> </div></li>");
                    #endregion

                }
                return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
                #endregion
            }
            else if (StaticStudentList.Count != 0)
            {
                #region StudentJoined
                if (StaticStudentList.Count < 5)
                {
                    tmpStudentList = StaticStudentList.ToList();
                    StaticStudentList.RemoveAll(x => 1 == 1);
                }
                else
                {
                    tmpStudentList = StaticStudentList.GetRange(0, 5);
                    StaticStudentList.RemoveRange(0, 5);
                }

                string img;
                string tmpurl = "";
                string field;

                for (int i = 0; i < tmpStudentList.Count; i++)
                {

                    #region MyRegion

                    tmpurl = "/Student/Profile/" + tmpStudentList[i].StudentId;

                    if (tmpStudentList[i].DesiredFieldofStudy != null)
                        field = " in " + tmpStudentList[i].DesiredFieldofStudy;
                    else
                        field = "";

                    if (tmpStudentList[i].Photo != null)
                        img = "/Images/Profile/" + tmpStudentList[i].Photo;
                    else
                        img = "/Images/no_image.jpg";
                    sb.Append("<li><div class='clg_img'> <img src='" + img + "' /> </div>");
                    sb.Append(" <div class='clg_descripition'><p><b><a href='" + tmpurl + "'>" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "</a></b><br/> Applying from " + tmpStudentList[i].City + " for " + Enum.Parse(typeof(ProgramLookingFor), tmpStudentList[i].LookingFor.ToString()) + " " + field + " in " + tmpStudentList[i].LookingForCountry + ". <br/> Desired joining in date " + Enum.Parse(typeof(expectedStart), tmpStudentList[i].StartDate.ToString()) + ". </p></div>");
                    sb.Append("<div class='clg_apply'><a id='lnkMessage' class='msgbtn fright' href='javascript:void(0);' onclick=\"javascript:OpenMsgPopup('" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "','" + tmpStudentList[i].UserName + "')\">Message</a> </div></li>");


                    #endregion
                }

                return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
                #endregion
            }
            else
            {
                return Json("no", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        [WebMethod()]
        public JsonResult _SaveFirstScreenAnswer(string SurveyId, string Name, string Country, string City, string University, string GraduationYear, string Course, string Degree, string IsApproved)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.Name = Name;
            surveyDetail.Country = Country;
            surveyDetail.City = City;
            surveyDetail.University = University;
            surveyDetail.GraduationYear = Convert.ToInt32(GraduationYear);
            surveyDetail.Course = Course;
            surveyDetail.Degree = Degree;
            if (IsApproved == "" || IsApproved == null)
                surveyDetail.IsApproved = false;
            else
                surveyDetail.IsApproved = Convert.ToBoolean(IsApproved);

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _SaveSecondScreenAnswer(string SurveyId, string ApplyOwnOrAgent, string LocalAgentCharge, string AgentName)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.ApplyOwnOrAgent = ApplyOwnOrAgent;
            surveyDetail.LocalAgentCharge = LocalAgentCharge;
            surveyDetail.AgentName = AgentName;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _SaveThirdScreenAnswer(string SurveyId, string FirstLive, string LookingForHousing, string RoomShared, string MonthlyRent, string WhereLive, string Address, string RateYourBuilding, string LandlordFeedback, string ReturnDeposite, string HousingSuggestion)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.FirstLive = FirstLive;
            surveyDetail.LookingForHousing = LookingForHousing;
            surveyDetail.RoomShared = RoomShared;
            surveyDetail.MonthlyRent = MonthlyRent;
            surveyDetail.WhereLive = WhereLive;
            surveyDetail.Address = Address;
            surveyDetail.RateYourBuilding = RateYourBuilding;
            surveyDetail.LandlordFeedback = LandlordFeedback;
            surveyDetail.ReturnDeposite = ReturnDeposite;
            surveyDetail.HousingSuggestion = HousingSuggestion;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveFourthScreenAnswer(string SurveyId, string LookForJob, string FindJob, string GetOne, string Department, string HourlyPay, string JobSuggestion)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.LookForJob = LookForJob;
            surveyDetail.FindJob = FindJob;
            surveyDetail.GetOne = GetOne;
            surveyDetail.Department = Department;
            surveyDetail.HourlyPay = HourlyPay;
            surveyDetail.JobSuggestion = JobSuggestion;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveFifthScreenAnswer(string SurveyId, string SafeOnCampus, string SafeonOutside, string HappenCampus, string CampusPolice, string StolenThings, string StolenThingDescription, string RetriveItBack)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.SafeOnCampus = SafeOnCampus;
            surveyDetail.SafeonOutside = SafeonOutside;
            surveyDetail.HappenCampus = HappenCampus;
            surveyDetail.CampusPolice = CampusPolice;
            surveyDetail.StolenThings = StolenThings;
            surveyDetail.StolenThingDescription = StolenThingDescription;
            surveyDetail.RetriveItBack = RetriveItBack;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveSixthScreenAnswer(string SurveyId, string Scholarship, string OneAfterwards, string AfterJoining, string ScholarshipSuggestion)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.Scholarship = Scholarship;
            surveyDetail.OneAfterwards = OneAfterwards;
            surveyDetail.AfterJoining = AfterJoining;
            surveyDetail.ScholarshipSuggestion = ScholarshipSuggestion;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveSeventhScreenAnswer(string SurveyId, string SufficientEating, string SufficientMarkets)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.SufficientEating = SufficientEating;
            surveyDetail.SufficientMarkets = SufficientMarkets;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveEightScreenAnswer(string SurveyId, string TestScore, string ApplyOtherUniversity, string GetAllUniversity)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.TestScore = TestScore;
            surveyDetail.ApplyOtherUniversity = ApplyOtherUniversity;
            surveyDetail.GetAllUniversity = GetAllUniversity;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveNineScreenAnswer(string SurveyId, string JobsorInternships, string HealthInsuranceUni, string PruchaseHealthInsu)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.JobsorInternships = JobsorInternships;
            surveyDetail.HealthInsuranceUni = HealthInsuranceUni;
            surveyDetail.PruchaseHealthInsu = PruchaseHealthInsu;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveTenScreenAnswer(string SurveyId, string SuggestionstoUniversity, string SuggestionstoStudent)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.SuggestionstoUniversity = SuggestionstoUniversity;
            surveyDetail.SuggestionstoStudent = SuggestionstoStudent;

            int id = surveyBLL.Save(surveyDetail);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _SaveElevenScreenAnswer(string SurveyId, string EmailId, string ForwardQuestions)
        {
            string str = string.Empty;

            if (SurveyId != string.Empty)
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            else
                surveyDetail = new SurveyDetail();

            surveyDetail.EmailId = EmailId;
            surveyDetail.ForwardQuestions = Convert.ToBoolean(ForwardQuestions);

            int id = surveyBLL.Save(surveyDetail);
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                str = "sur";

            return Json(str, JsonRequestBehavior.AllowGet);
        }


        [WebMethod()]
        public JsonResult _SendMessageEmailToStudent(string EmailId, string Message)
        {
            Student std = new Student();
            string str = string.Empty;
            if (EmailId != null && EmailId != "")
            {
                std = studentBll.GetByUserName(CookieHelper.Username);
               
               // StringBuilder emailBody = new StringBuilder(ReadEmailTextFile("SendEmailToSurveyor.htm"));
                StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendEmailToSurveyor.htm")));
                emailBody.Replace(EmailConstantHelper.Name, std.FirstName + " " + std.LastName);
                emailBody.Replace(EmailConstantHelper.UserName, CookieHelper.Username);
                emailBody.Replace(EmailConstantHelper.MessageDescription, Message);
                MailHelper.sendMail(EmailId, "Received Message : SpotCollege", emailBody.ToString());

                //StringBuilder mailmsg = new StringBuilder(string.Empty);
                //mailmsg.Append("<h3>You Received Message from SpotCollege User</h3>");
                //mailmsg.Append("<br/><b>Message Description :</b>" + Message);
                //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                //MailHelper.sendMail(EmailId, "Received Message : SpotCollege", mailmsg.ToString());
            }
            str = "stud";
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //public static String ReadEmailTextFile(String fileName)
        //{
        //    return File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "\\EmailTextFile\\" + fileName));
        //    //return File.ReadAllText(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/" + fileName);
        //}
    }
}
