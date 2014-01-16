//------------------------------------------------------------------------------
// <remark>
//     Student controller
//
//     This controller is used to manage details reagarding Student. Details like
//     profile Edit, View etc.
// </remark>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpotCollege.BLL;
using SpotCollege.DAL;
using SpotCollege.DAL.DataModels;
using SpotCollege.Common;
using System.Text;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using Spot_College_MVC.Filters;
using System.Globalization;
using System.Configuration;
using System.IO;

namespace Spot_College_MVC.Controllers
{
    public class StudentController : BaseController
    {
        #region Declaration
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        StudentTestBLL studentTestBll = new StudentTestBLL();
        StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
        UniversityBLL universityBll = new UniversityBLL();
        UserBLL userBll = new UserBLL();
        StudentWorkHistoryBLL studentWorkHistoryBLL = new StudentWorkHistoryBLL();
        AlertBLL alertBll = new AlertBLL();
        MessageBLL messageBLL = new MessageBLL();
        UniversityBLL universityBLL = new UniversityBLL();
        ViewProfileBLL viewProfileBLL = new ViewProfileBLL();
        ResourceCategoryBLL resourceCategoryBLL = new ResourceCategoryBLL();
        ResourceBLL resourceBLL = new ResourceBLL();


        List<University> universityList = null;
        List<Student> studentlist = null;
        List<StudentInterest> InterestList = new List<StudentInterest>();

        static List<Student> staticStudentList = new List<Student>();
        static List<University> StaticUniversityList = new List<University>();
        static List<Alert> StaticAlertList = new List<Alert>();
        public static List<Message> msgStaticList = new List<Message>();
        Student std = new Student();
        static int NoOfMsgToDisplay = 12;
        List<User> tmpUserList = new List<User>();
        static string[] universityUser = { };
        static string[] students = { };
        static string[] adminUser = { };
        static string[] newmsguserList = { };
        static string[] msgTitle = { };
        string[] privacyStudent = { };
        public static List<MessageModel> StaticSearchMessageList = new List<MessageModel>();
        public static List<StudentInterest> StaticStudentInterestList = new List<StudentInterest>();

        static string loginname = "";
        static string loginimage = "";

        SurveyBLL surveyBLL = new SurveyBLL();
        SurveyDetail surveyDetail = new SurveyDetail();
        List<SurveyDetail> surveyDetailList = new List<SurveyDetail>();

        SettingBLL settingBLL = new SettingBLL();
        Settings settings = new Settings();
        List<Settings> settingList = new List<Settings>();
        #endregion

        #region Methods Division
        //Get Student Dashboard
        public ActionResult Index()
        {
            /// <summary>
            /// Redirect to login page if not authorized
            /// </summary>
            if (Convert.ToString(CookieHelper.Username) == "")
            {
                return RedirectToAction("index", "Authentication");
            }

            StudentDashboardModel stdm = new StudentDashboardModel();

            /// <summary>
            /// Display University that supports students desired fieled of Study
            /// </summary>
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

            //List<StudentInterest> studentintrestlist = studentInterestBLL.GetAll().Where(x => x.StudentUserName == CookieHelper.Username).ToList();

            //University university = universityBll.GetByUniversityCountry(current_student.Country);

            string cource = "";
            cource = Enum.Parse(typeof(ProgramLookingFor), current_student.LookingFor.ToString()).ToString();

            List<University> universityList = universityBll.GetAll().Where(x => x.Degree != null && x.Courses != null && x.Degree.Contains(Convert.ToString(cource)) && x.Courses.Contains(Convert.ToString(current_student.DesiredFieldofStudy))).ToList();

            //universityList = universityList.Where(x => !studentintrestlist.Select(y => y.UniversityUserName).ToArray().Contains(x.UserName)).ToList();
            stdm.universityList = universityList;


            //this is use for privacy settings
            privacyStudent = settingBLL.GetAll().Where(x => x.Students == true).Select(y => y.UserName).ToArray();

            /// <summary>
            /// Display students that are from students coutry
            /// </summary>

            studentlist = studentBll.GetAll().Where(x => x.Country == current_student.Country && x.UserName != CookieHelper.Username && x.StudyingIn != 0).ToList();
            studentlist = studentlist.Where(x => !privacyStudent.Contains(x.UserName)).ToList();  // used to remove name of student from search list
            studentlist.Remove(current_student);
            studentlist = studentlist.OrderByDescending(x => x.StudentId).Take(5).ToList();
            stdm.studentlist = studentlist;


            /// <summary>
            /// Display students that are applygin for students desired program
            /// </summary>

            studentlist = studentBll.GetAll().Where(x => x.LookingFor == current_student.LookingFor && x.UserName != CookieHelper.Username && x.StudyingIn != 0).ToList();
            studentlist = studentlist.Where(x => !privacyStudent.Contains(x.UserName)).ToList();  // used to remove name of student from search list
            studentlist.Remove(current_student);
            studentlist = studentlist.OrderByDescending(x => x.StudentId).Take(5).ToList();
            stdm.studentlist2 = studentlist;

            /// <summary>
            /// Set Labels dynamically
            /// </summary>
            ViewBag.DesiredField = current_student.DesiredFieldofStudy;
            ViewBag.StudentApplyFor = EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)current_student.LookingFor);
            ViewBag.StudentCountry = current_student.Country;
            ViewBag.UserName = current_student.FirstName + ' ' + current_student.LastName;
            if (string.IsNullOrEmpty(current_student.Photo))
                ViewBag.UserImage = "/Images/no_image.jpg";
            else
                ViewBag.UserImage = "/Images/Profile/" + current_student.Photo;

            return View(stdm);
        }

        //Get Student Profile OverView
        public ActionResult Profile(string id)
        {
            if (CookieHelper.Username == null)
                return RedirectToAction("index", "Authentication");

            StudentProfileModel spm = new StudentProfileModel();
            if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                if (id != null)
                {
                    std = studentBll.Get(Convert.ToInt32(id));
                }
                else
                {
                    std = studentBll.GetByUserName(CookieHelper.Username);
                }

                spm.student = FixUp(std);
                spm.studentAcademicList = academicBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.testList.testList = studentTestBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.studentWorkHistoryList = studentWorkHistoryBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();

                ViewProfile viewProfile = new ViewProfile();
                List<ViewProfile> viewProfileList = viewProfileBLL.GetAll().Where(x => x.UniversityUsername == CookieHelper.Username && x.StudentUsername == std.UserName).ToList();
                if (viewProfileList.Count == 0)
                {
                    viewProfile.UniversityUsername = CookieHelper.Username;
                    viewProfile.StudentUsername = std.UserName;
                    viewProfileBLL.Save(viewProfile);
                }
            }
            else
            {
                Student current_student = studentBll.GetByUserName(CookieHelper.Username);
                if (current_student == null)
                {
                    return RedirectToAction("EditProfile", "Student");
                }
                else if (current_student.StudentAcademics.ToList().Count == 0)
                {
                    return RedirectToAction("EditProfile/CurrentAcademic", "Student");
                }
                else if (string.IsNullOrEmpty(current_student.DesiredFieldofStudy))
                {
                    return RedirectToAction("EditProfile/EduPref", "Student");
                }

                if (id != null)
                {
                    std = studentBll.Get(Convert.ToInt32(id));
                }
                else
                {
                    std = studentBll.GetByUserName(CookieHelper.Username);
                }

                ViewBag.UserName = std.FirstName + " " + std.LastName;
                ViewBag.UserImage = std.Photo;

                spm.student = FixUp(std);
                spm.studentAcademicList = academicBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.testList.testList = studentTestBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.studentWorkHistoryList = studentWorkHistoryBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
            }
            return View(spm);
        }

        //Get Student Edit Profile
        public ActionResult EditProfile()
        {

            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            StudentProfileModel spm = new StudentProfileModel();

            Student std = null;
            //Student registration by administrator
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)))
                {
                    std = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId));
                    spm.registerModel.UserName = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                    spm.registerModel.Password = EncryptionHelper.Decrypt(userBll.Get(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).Password);
                    spm.registerModel.ConfirmPassword = spm.registerModel.Password;
                }
            }
            else
            {
                std = studentBll.GetByUserName(CookieHelper.Username);
            }



            if (std == null)
            {
                ViewBag.isBasicDetailComplete = false;
                ViewBag.IntTestStudentName = "User";
                spm.student = new StudentEditModel();
                spm.studentAcademic = new StudentAcademicModel();
                spm.studentAcademicList = new List<StudentAcademic>();
                spm.studentEduPreference = new StudentEducationalPreferenceModel();
                spm.studentExtraActivity = new StudentExtraActivityModel();

                return View(spm);
            }
            else
            {
                if (std.FirstName == null)
                {
                    ViewBag.isBasicDetailComplete = false;
                    ViewBag.IntTestStudentName = "User";
                }
                else
                {
                    ViewBag.isBasicDetailComplete = true;
                    ViewBag.IntTestStudentName = std.FirstName + " " + std.LastName;
                }
            }

            ViewBag.UserName = loginname;
            ViewBag.UserImage = loginimage;

            #region Region to edit existing records

            string academic = Convert.ToString(Request.QueryString["academic"]);
            string work = Convert.ToString(Request.QueryString["work"]);
            string data = Convert.ToString(RouteData.Values["id"]);

            //this code is used to display specific academic record
            int academicId;
            if (int.TryParse(academic, out academicId))
            {
                spm.studentAcademicList = academicBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.studentAcademic = FixUp(academicBll.Get(academicId));
                ViewBag.Current = "CurrentAcademic";
                return View(spm);
            }

            //this code is used to display specific work record
            int workid;
            if (int.TryParse(work, out workid))
            {
                spm.studentWorkHistoryList = studentWorkHistoryBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.studentWork = FixUp(studentWorkHistoryBLL.Get(workid));
                ViewBag.Current = "WorkHistory";
                return View(spm);
            }
            #endregion


            //this code is used to display record specific to tab selected
            if (data == "CurrentAcademic")
            {
                spm.studentAcademicList = academicBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.studentAcademic = new StudentAcademicModel();
                spm.studentAcademic.StudentId = std.StudentId;
            }
            else if (data == "Test")
            {
                spm.testList = new StudentTestModel();
                spm.testList.testList = studentTestBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.testList.testAct = new testACT();
                spm.testList.testSat = new testSAT();

            }
            else if (data == "EduPref")
            {
                spm.studentEduPreference = new StudentEducationalPreferenceModel();
                spm.studentEduPreference.LookingFor = std.LookingFor;

                //This condition is important because from EnumHelper.GetEnumValueFromDescription value is returned if Enum has
                //associated description with it
                if (std.LookingForCountry != "")
                {
                    string tmp = Convert.ToString((int)EnumHelper.GetEnumValueFromDescription<CountryList>(std.LookingForCountry));
                    if (tmp == "0")
                    {
                        //To get value of enum if it does not contain description
                        CountryList cl = (CountryList)Enum.Parse(typeof(CountryList), std.LookingForCountry);
                        spm.studentEduPreference.LookingForCountry = Convert.ToString((int)cl);
                    }
                    else
                        spm.studentEduPreference.LookingForCountry = tmp;
                }

                //This condition is important because from EnumHelper.GetEnumValueFromDescription value is returned if Enum has associated description with it
                if (std.DesiredFieldofStudy != "")
                {
                    string desfield = Convert.ToString((int)EnumHelper.GetEnumValueFromDescription<CoursesOffered>(std.DesiredFieldofStudy));
                    if (desfield == "0")
                    {
                        //To get value of enum if it does not contain description
                        CoursesOffered co = (CoursesOffered)Enum.Parse(typeof(CoursesOffered), std.DesiredFieldofStudy);
                        spm.studentEduPreference.DesiredFieldofStudy = Convert.ToString((int)co);
                    }
                    else
                        spm.studentEduPreference.DesiredFieldofStudy = desfield;
                }
                spm.studentEduPreference.OtherStudy = std.OtherStudy;
                spm.studentEduPreference.Payout = std.Payout;
                spm.studentEduPreference.StartDate = std.StartDate;
                spm.studentEduPreference.StudentId = std.StudentId;
                spm.studentEduPreference.StudyingIn = std.StudyingIn;
            }
            else if (data == "Photo")
            {
                if (string.IsNullOrEmpty(std.Photo))
                    ViewBag.Photo = "/Images/no_image.jpg";
                else
                    ViewBag.Photo = "/Images/Profile/" + std.Photo;
            }
            else if (data == "WorkHistory")
            {
                spm.studentWorkHistoryList = studentWorkHistoryBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                spm.studentWork = new StudentWorkHistoryModel();
                spm.studentWork.StudentId = std.StudentId;
            }
            else if (data == "ExtraActivity")
            {
                spm.studentExtraActivity = new StudentExtraActivityModel();
                spm.studentExtraActivity.OtherActivities = std.OtherActivities;
                spm.studentExtraActivity.LeadershipActivies = std.LeadershipActivies;
                spm.studentExtraActivity.SportActivities = std.SportActivities;
                spm.studentExtraActivity.StudentId = std.StudentId;
            }
            else
            {
                spm.student = FixUp(std);
            }
            return View(spm);
        }

        //Post Student Edit Profile
        [HttpPost]
        public ActionResult EditProfile(StudentProfileModel sem)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            #region Basic details
            //<summary>
            //Used to save basic student details
            //</summary>
            if (!string.IsNullOrEmpty(Request.Form["btnSave"]))
            {
                StudentProfileModel spm = new StudentProfileModel();
                if (ModelState.IsValid)
                {
                    DateTime fromDateValue;
                    string s = sem.student.day + "/" + sem.student.month + "/" + sem.student.year;
                    var formats = new[] { "d/MM/yyyy", "yyyy-MM-d" };
                    if (DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                    {

                    }
                    else
                    {
                        ViewBag.Current = "basic";
                        ModelState.AddModelError("errInvalidDate", "Please enter proper date.");
                        return View(sem);
                    }

                    //This is used when Admin is inserting new student entry
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        User user = new User();
                        if (sem.registerModel != null && string.IsNullOrEmpty(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)))
                        {
                            //check if user exist or not
                            User us = userBll.Get(sem.registerModel.UserName);
                            if (us == null)
                            {
                                //if no user exist then create entry in User table
                                user.UserName = sem.registerModel.UserName;
                                user.Password = EncryptionHelper.Encrypt(sem.registerModel.Password);
                                user.LoginTypeId = (int)LoginTypes.Student;
                                user.IsActive = true;
                                userBll.Save(user);
                                CookieHelper.SetCookie(CookieKeys.adminStudentId, sem.registerModel.UserName);
                            }
                            else
                            {
                                //if admin entered UserName that is already exist then add Model error
                                ViewBag.Current = "basic";
                                ModelState.AddModelError("errDuplicateUser", "Username already exist. Please use another.");
                                return View(sem);
                            }
                        }
                        else
                        {
                            user.UserName = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                            user.Password = EncryptionHelper.Encrypt(sem.registerModel.Password);
                            userBll.Save(user);
                        }
                    }
                    SaveBasicDetails(sem.student);
                    ViewBag.Current = "CurrentAcademic";

                    ViewBag.CurrnentAcadEnabled = true;
                    ViewBag.WorkHistEnabled = true;

                    CookieHelper.SetCookie(CookieKeys.Fullname, sem.student.FirstName + " " + sem.student.LastName);

                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        std = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId));
                    }
                    else
                    {
                        std = studentBll.GetByUserName(CookieHelper.Username);
                    }

                    //display student academic list
                    spm.studentAcademicList = academicBll.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                    spm.studentAcademic = new StudentAcademicModel();
                    spm.studentAcademic.StudentId = std.StudentId;


                    ViewBag.isBasicDetailComplete = true;
                    return View(spm);
                }
                else
                {
                    ViewBag.Current = "basic";
                    return View(sem);
                }
            }
            #endregion
            #region Academic details
            //<summary>
            //Used to save Current Academic details
            //</summary>
            if (!string.IsNullOrEmpty(Request.Form["btnSaveAcademic"]))
            {
                if (ModelState.IsValid)
                {
                    int studid = sem.studentAcademic.StudentId;
                    ViewBag.Current = "CurrentAcademic";
                    SaveAcademics(sem.studentAcademic);
                    sem.studentAcademicList = academicBll.GetAll().Where(x => x.StudentId == sem.studentAcademic.StudentId).ToList();
                    sem.studentAcademic = new StudentAcademicModel();
                    ModelState.Clear();
                    sem.studentAcademic.StudentId = studid;
                    return View(sem);
                }
                else
                {
                    ViewBag.Current = "CurrentAcademic";
                    sem.studentAcademicList = academicBll.GetAll().Where(x => x.StudentId == sem.studentAcademic.StudentId).ToList();
                    sem.studentAcademic.StudentId = sem.studentAcademic.StudentId;
                    return View(sem);
                }
            }
            #endregion
            #region Current academics
            //<summary>
            //Used to save current academics
            //</summary>
            if (!string.IsNullOrEmpty(Request.Form["btnSaveEduPref"]))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Current = "Photo";
                    SaveEducationalPrefrence(sem.studentEduPreference);

                    return View(sem);
                }
                else
                {
                    ViewBag.Current = "EduPref";
                    return View(sem);
                }
            }
            #endregion
            #region Photo
            if (!string.IsNullOrEmpty(Request.Form["btnSavePhoto"]))
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase htp = Request.Files["fileup"];
                    SavePhoto(htp);
                    ViewBag.Current = "WorkHistory";


                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        std = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId));
                    }
                    else
                    {
                        std = studentBll.GetByUserName(CookieHelper.Username);
                    }
                    sem.studentWorkHistoryList = studentWorkHistoryBLL.GetAll().Where(x => x.StudentId == std.StudentId).ToList();
                    sem.studentWork = new StudentWorkHistoryModel();
                    sem.studentWork.StudentId = std.StudentId;
                    return View(sem);
                }
            }
            #endregion
            #region Work history
            if (!string.IsNullOrEmpty(Request.Form["btnSaveWorkHistory"]))
            {
                if (ModelState.IsValid)
                {
                    SaveWorkHistory(sem.studentWork);
                    ViewBag.Current = "WorkHistory";
                    int id = sem.studentWork.StudentId;
                    sem.studentWorkHistoryList = studentWorkHistoryBLL.GetAll().Where(x => x.StudentId == sem.studentWork.StudentId).ToList();
                    sem.studentWork = new StudentWorkHistoryModel();
                    sem.studentWork.StudentId = id;
                    ModelState.Clear();
                    return View(sem);
                }
                else
                {
                    ViewBag.Current = "WorkHistory";
                    sem.studentWorkHistoryList = studentWorkHistoryBLL.GetAll().Where(x => x.StudentId == sem.studentWork.StudentId).ToList();
                    return View(sem);
                }
            }
            #endregion
            #region Extra activity
            if (!string.IsNullOrEmpty(Request.Form["btnSaveActivities"]))
            {
                if (ModelState.IsValid)
                {
                    SaveExtra(sem.studentExtraActivity);
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        return RedirectToAction("StudentInformation", "AdminSec");
                    }
                    else
                    {
                        ViewBag.Current = "ExtraActivity";
                        //return RedirectToAction("Profile");
                    }
                }
            }
            #endregion
            #region International Test

            if (!string.IsNullOrEmpty(Request.Form["btnACTSave"]))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Current = "Test";
                    return View(sem);
                }
                else
                {
                    ViewBag.Current = "Test";
                    return View(sem);
                }
            }
            if (!string.IsNullOrEmpty(Request.Form["btnSATSave"]))
            {
                if (ModelState.IsValid)
                {
                    return View(sem);
                }
            }
            return View(sem);

            #endregion
        }

        // For Display All Student(Student Portal)
        public ActionResult Portal(string id)
        {
            StudentProfileModel spm = new StudentProfileModel();

            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                Student current_student = studentBll.GetByUserName(CookieHelper.Username);
                if (current_student == null)
                {
                    return RedirectToAction("EditProfile/fl", "Student");
                }
                else if (current_student.StudentAcademics.ToList().Count == 0)
                {
                    return RedirectToAction("EditProfile/CurrentAcademic/fl", "Student");
                }
                else if (string.IsNullOrEmpty(current_student.DesiredFieldofStudy))
                {
                    return RedirectToAction("EditProfile/EduPref/fl", "Student");
                }

                //For Displaying Student List
                // For Searching Of Student
                if (!string.IsNullOrEmpty(Request.Form["btnSearch"]))
                {

                    #region When Searching is enable
                    Student std = studentBll.GetByUserName(CookieHelper.Username);
                    spm.student = FixUp(std);
                    List<Student> studentlist = new List<Student>();

                    studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username && x.StudyingIn != null && x.StudyingIn != 0).OrderBy(y => y.Country == std.Country).ToList();



                    string dllStudentCountry = Request.Form["dllStudentCountry"];
                    string ddlSearchByLookingFor = Request.Form["ddlSearchByLookingFor"];
                    string ddlSearchByCountry = Request.Form["ddlSearchByCountry"];

                    string Country = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(dllStudentCountry));
                    string LookingForCountry = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(ddlSearchByCountry));

                    if (dllStudentCountry == "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.LookingForCountry == LookingForCountry && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor == "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingForCountry == LookingForCountry && x.Country == Country && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry == "0")
                        studentlist = studentlist.Where(x => x.Country == Country && x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.UserName != CookieHelper.Username).ToList();



                    else if (dllStudentCountry == "0" && ddlSearchByLookingFor == "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingForCountry == LookingForCountry && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor == "0" && ddlSearchByCountry == "0")
                        studentlist = studentlist.Where(x => x.Country == Country && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry == "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry == "0")
                        studentlist = studentlist.Where(x => x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.UserName != CookieHelper.Username).ToList();



                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingForCountry == LookingForCountry && x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.Country == Country && x.UserName != CookieHelper.Username).ToList();
                    else { studentlist = studentlist; }

                    //this is for privacy settings
                    privacyStudent = settingBLL.GetAll().Where(x => x.Students == true).Select(y => y.UserName).ToArray();
                    studentlist = studentlist.Where(x => !privacyStudent.Contains(x.UserName)).ToList();

                    ViewBag.lblTotalRecBtn = studentlist.Count().ToString() + " record found";
                    staticStudentList = studentlist.ToList();
                    if (studentlist.Count < 8)
                        staticStudentList.RemoveAll(x => 1 == 1);
                    else
                        staticStudentList.RemoveRange(0, 8);

                    StaticStudentInterestList = null;
                    spm.studentList = studentlist.Take(8).ToList();

                    spm.student.Country = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(dllStudentCountry));
                    spm.student.LookingFor = Convert.ToInt32(ddlSearchByLookingFor);
                    spm.student.LookingForCountry = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(ddlSearchByCountry));
                    return View(spm);
                    #endregion
                }
                else
                {

                    Student std = studentBll.GetByUserName(CookieHelper.Username);

                    if (id != null)
                    {
                        if (id == "Country")
                        {
                            studentlist = studentBll.GetAll().Where(x => x.Country == std.Country && x.UserName != CookieHelper.Username && x.StudyingIn != null && x.StudyingIn != 0).ToList();
                            spm.student.Country = std.Country;
                        }
                        else if (id == "Course")
                        {
                            studentlist = studentBll.GetAll().Where(x => x.LookingFor == std.LookingFor && x.UserName != CookieHelper.Username && x.StudyingIn != null && x.StudyingIn != 0).ToList();
                            spm.student.LookingFor = std.LookingFor;
                        }
                    }
                    else
                    {
                        studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username && x.StudyingIn != null && x.StudyingIn != 0).OrderBy(y => y.Country == std.Country).ToList();
                        spm.student = FixUp(std);
                    }

                    //this is for privacy settings
                    privacyStudent = settingBLL.GetAll().Where(x => x.Students == true).Select(y => y.UserName).ToArray();
                    studentlist = studentlist.Where(x => !privacyStudent.Contains(x.UserName)).ToList();

                    staticStudentList = studentlist.ToList();
                    ViewBag.lblStudRecPageLd = studentlist.Count().ToString() + " record found";

                    if (studentlist.Count < 8)
                        staticStudentList.RemoveAll(x => 1 == 1);
                    else
                        staticStudentList.RemoveRange(0, 8);

                    StaticStudentInterestList = null;
                    spm.studentList = studentlist.Take(8).ToList();

                    return View(spm);
                }
            }
            if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                if (!string.IsNullOrEmpty(Request.Form["btnSearch"]))
                {
                    #region When Searching is enable
                    University uni = universityBLL.GetByUserName(CookieHelper.Username);

                    List<Student> studentlist = new List<Student>();

                    studentlist = studentBll.GetAll().Where(x => x.StudyingIn != null && x.StudyingIn != 0).OrderBy(y => y.Country == uni.Country).ToList();

                    string dllStudentCountry = Request.Form["dllStudentCountry"];
                    string ddlSearchByLookingFor = Request.Form["ddlSearchByLookingFor"];
                    string ddlSearchByCountry = Request.Form["ddlSearchByCountry"];

                    string Country = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(dllStudentCountry));
                    string LookingForCountry = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(ddlSearchByCountry));

                    if (dllStudentCountry == "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.LookingForCountry == LookingForCountry && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor == "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingForCountry == LookingForCountry && x.Country == Country && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry == "0")
                        studentlist = studentlist.Where(x => x.Country == Country && x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.UserName != CookieHelper.Username).ToList();



                    else if (dllStudentCountry == "0" && ddlSearchByLookingFor == "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingForCountry == LookingForCountry && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor == "0" && ddlSearchByCountry == "0")
                        studentlist = studentlist.Where(x => x.Country == Country && x.UserName != CookieHelper.Username).ToList();

                    else if (dllStudentCountry == "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry == "0")
                        studentlist = studentlist.Where(x => x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.UserName != CookieHelper.Username).ToList();



                    else if (dllStudentCountry != "0" && ddlSearchByLookingFor != "0" && ddlSearchByCountry != "0")
                        studentlist = studentlist.Where(x => x.LookingForCountry == LookingForCountry && x.LookingFor == Convert.ToInt32(ddlSearchByLookingFor) && x.Country == Country && x.UserName != CookieHelper.Username).ToList();
                    else { studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username && x.StudyingIn != null && x.StudyingIn != 0).OrderBy(y => y.Country).ToList(); }

                    //this is use for privacy settings
                    privacyStudent = settingBLL.GetAll().Where(x => x.University == true).Select(y => y.UserName).ToArray();
                    studentlist = studentlist.Where(x => !privacyStudent.Contains(x.UserName)).ToList();  // used to remove name of student from search list

                    ViewBag.lblTotalRecBtn = studentlist.Count().ToString() + " record found";
                    staticStudentList = studentlist.ToList();
                    if (studentlist.Count < 8)
                        staticStudentList.RemoveAll(x => 1 == 1);
                    else
                        staticStudentList.RemoveRange(0, 8);

                    StaticStudentInterestList = null;
                    spm.studentList = studentlist.Take(8).ToList();

                    spm.student.Country = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(dllStudentCountry));
                    spm.student.LookingFor = Convert.ToInt32(ddlSearchByLookingFor);
                    spm.student.LookingForCountry = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(ddlSearchByCountry));

                    return View(spm);
                    #endregion
                }
                else
                {
                    University uni = universityBLL.GetByUserName(CookieHelper.Username);
                    if (uni != null)
                    {
                        if (id != null)
                        {
                            if (id == "Applied")
                                InterestList = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Applied).ToList();
                            //else if (id == "Approved")
                            //    InterestList = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == CookieHelper.Username && x.Approved == (int)StudentInterestApproved.Approved).ToList();

                            StaticStudentInterestList = InterestList.ToList(); //StaticStudentInterestedList will be used for displaying record
                            ViewBag.lblUniRecPageLd = InterestList.Count().ToString() + " record found";
                            if (InterestList.Count < 6)
                                StaticStudentInterestList.RemoveAll(x => 1 == 1);
                            else
                                StaticStudentInterestList.RemoveRange(0, 6);

                            staticStudentList = null;
                            spm.studentInterestList = InterestList.Take(6).ToList();
                            return View(spm);
                        }
                        else
                        {
                            studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username && x.StudyingIn != null && x.StudyingIn != 0).OrderByDescending(y => y.CreatedDate).ThenBy(y => y.Country == uni.Country).ToList();

                            //this is for privacy settings
                            privacyStudent = settingBLL.GetAll().Where(x => x.University == true).Select(y => y.UserName).ToArray();
                            studentlist = studentlist.Where(x => !privacyStudent.Contains(x.UserName)).ToList();

                            staticStudentList = studentlist.ToList();

                            ViewBag.lblUniRecPageLd = studentlist.Count().ToString() + " record found";


                            if (studentlist.Count < 8)
                                staticStudentList.RemoveAll(x => 1 == 1);
                            else
                                staticStudentList.RemoveRange(0, 8);

                            StaticStudentInterestList = null;
                            spm.studentList = studentlist.Take(8).ToList();
                            return View(spm);
                        }

                    }
                }
            }
            return View(spm);
        }

        //For Display List Of All Active University(University List)
        public ActionResult UniversityList()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("Index", "Authentication");


            Student current_student = studentBll.GetByUserName(CookieHelper.Username);
            if (current_student == null)
            {
                return RedirectToAction("EditProfile/fl", "Student");
            }
            else if (current_student.StudentAcademics.ToList().Count == 0)
            {
                return RedirectToAction("EditProfile/CurrentAcademic/fl", "Student");
            }
            else if (string.IsNullOrEmpty(current_student.DesiredFieldofStudy))
            {
                return RedirectToAction("EditProfile/EduPref/fl", "Student");
            }


            List<University> universityList2 = new List<University>();
            List<University> universityList3 = new List<University>();
            List<University> result = new List<University>();
            StudentProfileModel spm = new StudentProfileModel();

            spm.reviewList = surveyBLL.GetAll().Where(x => x.IsApproved == true).OrderByDescending(x => x.SurveyId).ToList();


            // For Displaying University List And..
            // For Searching of University (By Country, By Desired Field Of Study)
            if (!string.IsNullOrEmpty(Request.Form["btnSearch"]))
            {

                StaticUniversityList.RemoveAll(x => 1 == 1);

                universityList = universityBll.GetAll();

                string ddlCountryList = Request.Form["ddlCountryList"];
                string ddlLookingFor = Request.Form["ddlLookingFor"];
                string ddlCourses = Request.Form["ddlCourses"];

                string Country = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(ddlCountryList));
                string Degree = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.ProgramLookingFor)Convert.ToInt32(ddlLookingFor));
                string Courses = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CoursesOffered)Convert.ToInt32(ddlCourses));

                if (universityList.Count != 0)
                {
                    string lookingFr = Degree;
                    if (lookingFr == "Bachelor's program")
                        lookingFr = "Bachelors";
                    else if (lookingFr == "Master's program")
                        lookingFr = "Masters";
                    else if (lookingFr == "Phd Program")
                        lookingFr = "PHD";
                    else if (lookingFr == "Other")
                        lookingFr = "Associates";
                    else
                        lookingFr = "";


                    #region Conditions for searching results

                    if (ddlCountryList != "0" && ddlLookingFor == "0" && ddlCourses == "0")
                    {
                        universityList = universityList.Where(x => x.Country == Country).ToList();
                    }
                    else if (ddlCountryList == "0" && ddlLookingFor != "0" && ddlCourses == "0")
                    {
                        universityList = universityList.Where(x => x.Degree != null && x.Degree.Contains(lookingFr)).ToList();
                    }
                    else if (ddlCountryList == "0" && ddlLookingFor == "0" && ddlCourses != "0")
                    {
                        universityList = universityList.Where(x => x.Courses != null && x.Courses.Contains(Courses)).ToList();
                    }
                    else if (ddlCountryList != "0" && ddlLookingFor != "0" && ddlCourses == "0")
                    {
                        universityList = universityList.Where(x => x.Country == Country && x.Degree != null && x.Degree.Contains(lookingFr)).ToList();
                    }

                    else if (ddlCountryList != "0" && ddlLookingFor == "0" && ddlCourses != "0")
                    {
                        universityList = universityList.Where(x => x.Country == Country && x.Courses != null && x.Courses.Contains(Courses)).ToList();
                    }
                    else if (ddlCountryList == "0" && ddlLookingFor != "0" && ddlCourses != "0")
                    {
                        universityList = universityList.Where(x => x.Degree != null && x.Degree.Contains(lookingFr) && x.Courses != null && x.Courses.Contains(Courses)).ToList();
                    }
                    else if (ddlCountryList != "0" && ddlLookingFor != "0" && ddlCourses != "0")
                    {
                        universityList = universityList.Where(x => x.Country == Country && x.Degree != null && x.Degree.Contains(lookingFr) && x.Courses != null && x.Courses.Contains(Courses)).ToList();
                    }
                    else
                    {
                        universityList = universityList.Where(x => (x.Degree != null || x.Courses != null)).ToList();
                    }
                    #endregion

                    UserBLL user = new UserBLL();
                    List<string> userlist = user.GetAll().Where(x => x.LoginTypeId == 2 && x.IsActive == true).Select(y => y.UserName).ToList();
                    string[] strarr = (string[])userlist.ToArray();

                    universityList = universityList.Where(x => strarr.Contains(x.UserName)).ToList();

                    ViewBag.lblTotalUni = universityList.Count().ToString() + " record found";
                    string str = string.Join(",", strarr);
                    StaticUniversityList = universityList.ToList();
                    if (universityList.Count < 6)
                        StaticUniversityList.RemoveAll(x => 1 == 1);
                    else
                        StaticUniversityList.RemoveRange(0, 6);

                    spm.universityList = universityList.Take(6).ToList();
                    Student std = studentBll.GetByUserName(CookieHelper.Username);

                    ViewBag.UserName = std.FirstName + " " + std.LastName;
                    ViewBag.UserImage = std.Photo;

                    spm.student = FixUp(std);

                    //for UniversitySearch dropdown selected value
                    //spm.student.Country = Enum.GetName(typeof(CountryList), Convert.ToInt32(ddlCountryList));
                    //spm.student.LookingFor = Convert.ToInt32(ddlLookingFor);
                    //spm.student.DesiredFieldofStudy = Enum.GetName(typeof(CoursesOffered), Convert.ToInt32(ddlCourses));

                    spm.student.LookingForCountry = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)Convert.ToInt32(ddlCountryList));
                    spm.student.LookingFor = Convert.ToInt32(ddlLookingFor);
                    spm.student.DesiredFieldofStudy = SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CoursesOffered)Convert.ToInt32(ddlCourses));


                    return View(spm);
                }
                return View(spm);
            }
            else
            {
                Student std = studentBll.GetByUserName(CookieHelper.Username);
                ViewBag.UserName = std.FirstName + " " + std.LastName;
                ViewBag.UserImage = std.Photo;
                spm.student = FixUp(std);
                if (std != null)
                {
                    string lookingFr = EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)std.LookingFor);
                    if (lookingFr == "Bachelor's program")
                    {
                        lookingFr = "Bachelors";
                    }
                    else if (lookingFr == "Master's program")
                    {
                        lookingFr = "Masters";
                    }
                    else if (lookingFr == "Phd Program")
                    {
                        lookingFr = "PHD";
                    }
                    else if (lookingFr == "Other")
                    {
                        lookingFr = "Associates";
                    }
                    else
                    {
                        lookingFr = "";
                    }

                    universityList = universityBll.GetAll().Where(x => x.Degree != null && x.Courses != null && x.Country == std.LookingForCountry && x.Degree.Contains(lookingFr) && x.Courses.Contains(std.DesiredFieldofStudy)).ToList();
                    universityList2 = universityBll.GetAll().Where(x => !(string.IsNullOrEmpty(x.Degree)) & x.Country == std.LookingForCountry && x.Degree.Contains(lookingFr)).ToList();
                    universityList3 = universityBll.GetAll().Where(x => x.Country == std.LookingForCountry).ToList();

                    universityList2 = universityList2.Except(universityList).ToList();
                    universityList3 = universityList3.Except(universityList2).Except(universityList).ToList();

                    universityList.AddRange(universityList2);
                    universityList.AddRange(universityList3);
                    universityList.AddRange(universityBll.GetAll().ToList().Except(universityList));

                    UserBLL user = new UserBLL();
                    List<string> userlist = user.GetAll().Where(x => x.LoginTypeId == 2 && x.IsActive == true).Select(y => y.UserName).ToList();
                    string[] strarr = (string[])userlist.ToArray();

                    universityList = universityList.Where(x => strarr.Contains(x.UserName)).ToList();



                    StaticUniversityList = universityList.ToList(); //StaticUniversityList will be used for displaying record
                    ViewBag.lblUniPageLd = universityList.Count().ToString() + " record found";
                    if (universityList.Count < 6)
                        StaticUniversityList.RemoveAll(x => 1 == 1);
                    else
                        StaticUniversityList.RemoveRange(0, 6);

                    spm.universityList = universityList.Take(6).ToList();

                    return View(spm);

                }
                return View(spm);
            }
        }

        //Get Messages from student,university,admin
        public ActionResult Message()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            #region For Check User Profile Completed or not
            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                Student current_student = studentBll.GetByUserName(CookieHelper.Username);
                if (current_student == null)
                {
                    return RedirectToAction("EditProfile/fl", "Student");
                }
                else if (current_student.StudentAcademics.ToList().Count == 0)
                {
                    return RedirectToAction("EditProfile/CurrentAcademic/fl", "Student");
                }
                else if (string.IsNullOrEmpty(current_student.DesiredFieldofStudy))
                {
                    return RedirectToAction("EditProfile/EduPref/fl", "Student");
                }
            }
            #endregion
            int noOfUnread = 0;
            string adminUsername = CookieHelper.AdminUsername; //"admin@gmail.com"; //for static admin username
            List<MessageModel> msgModelList = new List<MessageModel>();

            tmpUserList = userBll.GetAll();
            universityUser = tmpUserList.Where(x => x.LoginTypeId == 2).Select(y => y.UserName).ToArray();
            students = tmpUserList.Where(x => x.LoginTypeId == 1).Select(y => y.UserName).ToArray();
            adminUser = tmpUserList.Where(x => x.LoginTypeId == 3 || x.LoginTypeId == 4).Select(y => y.UserName).ToArray();
            #region Region Message tab Selection
            string fromuser = "";
            if (!string.IsNullOrEmpty(Request.Form["lnkFromStudents"]))
            {
                fromuser = "students";
                ViewBag.Activestudents = "active";
            }
            else if (!string.IsNullOrEmpty(Request.Form["lnkFromUniversity"]))
            {
                fromuser = "university";
                ViewBag.Activeuniversity = "active";
            }
            else if (!string.IsNullOrEmpty(Request.Form["lnkFromAdmin"]))
            {
                fromuser = "admin";
                ViewBag.Activeadmin = "active";
            }
            else
            {
                fromuser = "students";
                ViewBag.Activestudents = "active";
            }
            #endregion

            List<Message> msgList = messageBLL.GetAll().Where(x => x.FromUserName == CookieHelper.Username
                                                                && x.ParentId == 0 && x.IsApproved == true

                                                                ||

                                                                x.ToUserName == CookieHelper.Username
                                                                && x.ParentId == 0
                                                                && x.IsApproved == true)

                                                                .OrderByDescending(x => x.MessageId).ThenByDescending(y => y.CreatedDate).ToList();

            List<Message> msg = messageBLL.GetAll().Where(x => !string.IsNullOrEmpty(x.IsApproved.ToString())
                && x.ToUserName == CookieHelper.Username && x.IsRead == false && x.IsApproved == true).ToList();

            int std, uni, admin;
            std = msg.Where(x => students.Contains(x.FromUserName) && x.IsRead == false).ToList().Count;
            uni = msg.Where(x => universityUser.Contains(x.FromUserName) && x.IsRead == false).ToList().Count;
            admin = msg.Where(x => adminUser.Contains(x.FromUserName) && x.IsRead == false).ToList().Count;

            if (std != 0)
                ViewBag.lnkFromStudents = "Messages From Students (" + std.ToString() + ")";
            else
                ViewBag.lnkFromStudents = "Messages From Students";

            if (uni != 0)
                ViewBag.lnkFromUniversity = "Messages From University (" + uni.ToString() + ")";
            else
                ViewBag.lnkFromUniversity = "Messages From University";

            if (admin != 0)
                ViewBag.lnkFromAdmin = "Messages From Admin (" + admin.ToString() + ")";
            else
                ViewBag.lnkFromAdmin = "Messages From Admin";

            if (fromuser == "students")
            {
                if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    msgList = msgList.Where(x => students.Contains(x.ToUserName) && x.ToUserName != adminUsername || students.Contains(x.FromUserName) && x.ToUserName != adminUsername).ToList();
                }
                else
                    msgList = msgList.Where(x => students.Contains(x.FromUserName) && !universityUser.Contains(x.ToUserName) && x.ToUserName != adminUsername).ToList();
            }
            else if (fromuser == "university")
            {
                msgList = msgList.Where(x => universityUser.Contains(x.FromUserName) && x.ToUserName != adminUsername || universityUser.Contains(x.ToUserName) && x.ToUserName != adminUsername).ToList();
            }
            else if (fromuser == "admin")
            {
                msgList = msgList.Where(x => adminUser.Contains(x.FromUserName) || adminUser.Contains(x.ToUserName)).ToList();
                //msgList = msgList.Where(x => (x.FromUserName == adminUsername)).ToList();
            }

            msgStaticList = msgList.ToList(); //StaticUniversityList will be used for displaying record
            if (msgList.Count < NoOfMsgToDisplay)
                msgStaticList.RemoveAll(x => 1 == 1);
            else
                msgStaticList.RemoveRange(0, NoOfMsgToDisplay);


            MessageModel msgModel;
            Student stdnt = new Student();
            foreach (var m in msgList)
            {
                msgModel = new MessageModel();
                List<Message> messagelist = messageBLL.GetAll().Where(x => x.ParentId == m.MessageId && x.IsApproved == true || x.MessageId == m.MessageId && x.IsApproved == true).ToList();
               
                //messagelist = messagelist.Where(x => x.Flag != false && x.FromUserName != CookieHelper.Username).ToList();

                msgModel.NoofChildMessage = messagelist.Count;
                int co = messagelist.Where(x => x.IsRead == false && x.ToUserName == CookieHelper.Username).Count();
                if (co != 0)
                    msgModel.NameColor = "#AFC0CD";



                #region admin
                //Check for Admin
                if (m.FromUserName == adminUsername || m.ToUserName == adminUsername || adminUser.Contains(m.FromUserName) || adminUser.Contains(m.ToUserName))
                {
                    msgModel.Photo = "/Images/no_image.jpg";
                    msgModel.Name = "Administrator";
                }

                #endregion

                if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                {
                    Student stdData = new Student();
                    if (m.ToUserName == CookieHelper.Username)
                        stdData = studentBll.GetByUserName(m.FromUserName);
                    else
                        stdData = studentBll.GetByUserName(m.ToUserName);

                    if (stdData != null)
                    {
                        #region student
                        if (stdData.Photo != null)
                        {
                            msgModel.Photo = "/Images/Profile/" + stdData.Photo;
                        }
                        else
                        {
                            msgModel.Photo = "/Images/no_image.jpg";
                        }
                        msgModel.Name = stdData.FirstName.ToString() + " " + stdData.LastName.ToString();

                        #region  Not used (Commented)
                        // List<Message> msgalertStudent = messageBLL.GetAll().Where(x =>
                        //  (x.ToUserName == CookieHelper.Username && x.ParentId == m.MessageId && x.IsRead == false && x.FromUserName == m.FromUserName && x.IsApproved == true)
                        //  ||
                        //(x.ToUserName == CookieHelper.Username && x.MessageId == m.MessageId && x.IsRead == false && x.FromUserName == m.FromUserName && x.IsApproved == true
                        //||
                        //x.FromUserName == m.ToUserName && x.ParentId == m.MessageId && x.IsRead == false && x.ToUserName == CookieHelper.Username && x.IsApproved == true)
                        //).ToList();

                        // if (msgalertStudent.Count != 0)
                        // {
                        //     msgModel.Name = stdData.FirstName.ToString() + " " + stdData.LastName.ToString();
                        //     msgModel.NameColor = "Green";
                        // }
                        // else
                        // {
                        //     msgModel.Name = stdData.FirstName.ToString() + " " + stdData.LastName.ToString();
                        //     msgModel.NameColor = "Black";
                        // } 
                        #endregion

                        #endregion
                    }
                }
                else
                {
                    //check for student data.

                    if (m.FromUserName == CookieHelper.Username)
                        stdnt = studentBll.GetByUserName(m.ToUserName);
                    else
                        stdnt = studentBll.GetByUserName(m.FromUserName);

                    if (stdnt != null)
                    {
                        #region stdRgn
                        if (stdnt.Photo != null)
                        {
                            msgModel.Photo = "/Images/Profile/" + stdnt.Photo;
                        }
                        else
                        {
                            msgModel.Photo = "/Images/no_image.jpg";
                        }
                        msgModel.Name = stdnt.FirstName.ToString() + " " + stdnt.LastName.ToString();

                        #region not used (Commented)
                        //  List<Message> msgalertStudent = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username && x.ParentId == m.MessageId
                        //      && x.IsRead == false && x.FromUserName == m.FromUserName && x.IsApproved == true) ||
                        //(x.ToUserName == CookieHelper.Username && x.MessageId == m.MessageId && x.IsRead == false
                        //&& x.FromUserName == m.FromUserName && x.IsApproved == true) ||
                        //(x.FromUserName == m.FromUserName && x.ParentId == m.MessageId && x.IsRead == false
                        //&& x.ToUserName == CookieHelper.Username && x.IsApproved == true)
                        //).ToList();

                        //  if (msgalertStudent.Count != 0)
                        //  {
                        //      msgModel.Name = stdnt.FirstName.ToString() + " " + stdnt.LastName.ToString();
                        //      msgModel.NameColor = "Green";
                        //  }
                        //  else
                        //  {
                        //      msgModel.Name = stdnt.FirstName.ToString() + " " + stdnt.LastName.ToString();
                        //      msgModel.NameColor = "Black";
                        //} 
                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region UniRgn
                        //check for university data
                        University univr = new University();
                        UniversityBLL universityBLL = new UniversityBLL();
                        if (m.FromUserName == CookieHelper.Username)
                            univr = universityBLL.GetByUserName(m.ToUserName);
                        else
                            univr = universityBll.GetByUserName(m.FromUserName);

                        if (univr != null)
                        {
                            if (univr.Image != null)
                            {
                                msgModel.Photo = "/Images/Profile/" + univr.Image;
                            }
                            else
                            {
                                msgModel.Photo = "/Images/no_image.jpg";
                            }
                            msgModel.Name = univr.UniversityName;

                            #region not used (Commented)
                            //List<Message> msgalertStudent = messageBLL.GetAll().Where(x => (x.ToUserName == CookieHelper.Username
                            //                                                                    && x.ParentId == m.MessageId && x.IsRead == false
                            //                                                                    && x.FromUserName == m.FromUserName && x.IsApproved == true) ||

                            //                                                                    (x.ToUserName == CookieHelper.Username &&
                            //                                                                    x.MessageId == m.MessageId && x.IsRead == false &&
                            //                                                                    x.FromUserName == m.FromUserName && x.IsApproved == true) ||

                            //                                                                   (x.FromUserName == m.FromUserName
                            //                                                                    && x.ParentId == m.MessageId && x.IsRead == false
                            //                                                                    && x.ToUserName == CookieHelper.Username && x.IsApproved == true)).ToList();
                            //if (msgalertStudent.Count != 0)
                            //{
                            //    msgModel.Name = univr.UniversityName;
                            //    msgModel.NameColor = "Green";
                            //}
                            //else
                            //{
                            //    msgModel.Name = univr.UniversityName;
                            //    msgModel.NameColor = "Black";
                            //} 
                            #endregion
                        }
                        #endregion
                    }
                }

                if (m.ToUserName == CookieHelper.Username)
                    msgModel.ToUserName = m.FromUserName;
                else
                    msgModel.ToUserName = m.ToUserName;

                if (m.Flag == false && m.FromUserName == CookieHelper.Username)
                    msgModel.IsShow = "false";
                else
                    msgModel.IsShow = "true";

                msgModel.MessageId = m.MessageId;
                msgModel.ParentId = m.ParentId;
                msgModel.Title = m.Title;
                msgModel.Description = m.Description;
                msgModel.CreatedDate = m.CreatedDate;
                msgModelList.Add(msgModel);
            }

            // newmsguserList = msgModelList.Select(x => x.Name + "," + x.ToUserName).Distinct().ToArray(); //this is use for getting message sender name for sending new message

            msgTitle = msgModelList.Select(x => x.Title).Distinct().ToArray();
            StaticSearchMessageList = msgModelList.ToList();
            return View(msgModelList);
        }

        //Get All Alerts
        public ActionResult ViewAllAlerts()
        {
            StudentProfileModel spm = new StudentProfileModel();
            StringBuilder builder = new StringBuilder();
            AlertBLL alertBll = new AlertBLL();
            List<Alert> alertlist = alertBll.GetAll().Where(x => x.UserName == CookieHelper.Username).OrderByDescending(x => x.CreatedDate).ToList();

            StaticAlertList = alertlist.ToList(); //msgStaticList will be used for displaying record
            if (alertlist.Count < 20)
                StaticAlertList.RemoveAll(x => 1 == 1);
            else
                StaticAlertList.RemoveRange(0, 20);

            spm.alertList = alertlist.Take(20).ToList();

            return View(spm);
        }

        //Get Artical Category wise
        public ActionResult Resource(string id)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            AdminResourceModel arm = new AdminResourceModel();

            if (id != null && id != "")
            {
                arm.resourceList = resourceBLL.GetAll().Where(x => x.CategoryId == Convert.ToInt32(id)).OrderByDescending(x => x.ResourceId).ToList();  //
            }
            else
            {
                int catId = resourceCategoryBLL.GetAll().FirstOrDefault().CategoryId;
                arm.resourceList = resourceBLL.GetAll().Where(x => x.CategoryId == catId).OrderByDescending(x => x.ResourceId).ToList();
            }

            arm.categoryList = resourceCategoryBLL.GetAll().ToList();  //.OrderByDescending(x => x.CategoryId)


            return View(arm);
        }

        #endregion

        #region Private Methods Division for Save Student All Information
        /// <summary>
        /// This method is used to Save student basic Details
        /// </summary>
        /// 
        private bool SaveBasicDetails(StudentEditModel studentedit)
        {
            Student studentDetail = new Student();
            //Check if user already exist or not.


            SpotCollege.DAL.User user = null;

            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                user = userBll.Get(CookieHelper.GetCookieValue(CookieKeys.adminStudentId));
            }
            else
            {
                user = userBll.Get(CookieHelper.Username);
            }

            if (user != null)
            {

                Student student = studentBll.GetByUserName(user.UserName);
                if (student == null)
                {
                    student = new Student();

                    student.FirstName = studentedit.FirstName;
                    if (studentedit.MiddleName != null)
                        student.MiddleName = studentedit.MiddleName;
                    else
                        student.MiddleName = "";

                    student.LastName = studentedit.LastName;
                    student.Address1 = studentedit.Address1;
                    student.Address2 = studentedit.Address2;
                    student.ZipCode = studentedit.ZipCode;
                    student.City = studentedit.City;
                    student.Country = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.Country)).ToString();
                    student.PrimaryNumber = studentedit.CoutryCodePrimary.ToString() + "-" + studentedit.PrimaryAreaCode + "-" + studentedit.PrimaryNumber;

                    student.PrimaryType = studentedit.PrimaryType;
                    if (studentedit.SecondaryNumber != "")
                    {
                        if (studentedit.SecondaryType != "0")
                        {
                            student.SecondaryType = studentedit.SecondaryType;
                            studentedit.SecondaryNumber = studentedit.CountryCodeSecondary.ToString() + "-" + studentedit.SecondaryAreaCode + "-" + studentedit.SecondaryNumber;
                        }
                    }

                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        student.PrimaryEmail = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                    }
                    else
                    {
                        student.PrimaryEmail = CookieHelper.Username;
                    }



                    student.BirthDate = new DateTime(Convert.ToInt16(studentedit.year), Convert.ToInt16(studentedit.month), Convert.ToInt16(studentedit.day));


                    student.Citizenship = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.Citizenship)).ToString();
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        student.UserName = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                    }
                    else
                    {
                        student.UserName = CookieHelper.Username;
                    }

                    //Temporary Values to set
                    student.StudyingIn = 0;
                    student.LookingFor = 0;
                    student.LookingForCountry = "";
                    student.StartDate = 0;
                    student.Payout = 0;
                    student.CreatedDate = DateTime.Now;
                    student.DesiredFieldofStudy = string.Empty;
                    studentBll.Save(student);
                }
                else
                {
                    //For Update student record
                    student.FirstName = studentedit.FirstName;
                    if (studentedit.MiddleName != null)
                        student.MiddleName = studentedit.MiddleName;
                    student.LastName = studentedit.LastName;
                    student.Address1 = studentedit.Address1;
                    student.Address2 = studentedit.Address2;
                    student.ZipCode = studentedit.ZipCode;
                    student.City = studentedit.City;
                    student.Country = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.Country)).ToString();
                    student.PrimaryNumber = studentedit.CoutryCodePrimary.ToString() + "-" + studentedit.PrimaryAreaCode + "-" + studentedit.PrimaryNumber;

                    student.PrimaryType = studentedit.PrimaryType;
                    student.PrimaryType = studentedit.PrimaryType;
                    if (studentedit.SecondaryNumber != "")
                    {
                        if (studentedit.SecondaryType != "0")
                        {
                            student.SecondaryType = studentedit.SecondaryType;
                            student.SecondaryNumber = studentedit.CountryCodeSecondary + "-" + studentedit.SecondaryAreaCode + "-" + studentedit.SecondaryNumber;
                        }
                    }
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        student.PrimaryEmail = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                    }
                    else
                    {
                        student.PrimaryEmail = CookieHelper.Username;
                    }
                    student.BirthDate = new DateTime(Convert.ToInt16(studentedit.year), Convert.ToInt16(studentedit.month), Convert.ToInt16(studentedit.day));
                    student.Citizenship = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.Citizenship)).ToString();
                    studentBll.Save(student);
                }
            }
            return true;
        }

        /// <summary>
        /// This method is used to Save student academics details
        /// </summary>
        /// 
        private bool SaveAcademics(StudentAcademicModel studentacademic)
        {


            StudentProfileModel spm = new StudentProfileModel();

            //Student std = studentBll.GetByUserName(CookieHelper.Username);

            //List<Student> studentlist = new List<Student>();
            //studentlist = studentBll.GetAll().Where(x => x.UserName != CookieHelper.Username).OrderBy(y => y.Country == std.Country).ToList();

            StudentAcademic academic = null;
            if (studentacademic.StudentAcademicsId != 0)
                academic = academicBll.Get(studentacademic.StudentAcademicsId);
            else
                academic = new StudentAcademic();


            string docPath = String.Empty;
            if (studentacademic.fileupload != null)
            {
                string filename = studentacademic.fileupload.FileName;
                filename = filename.Substring(filename.LastIndexOf('.') - 1);
                docPath = Guid.NewGuid().ToString() + filename;
                studentacademic.fileupload.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"]) + "\\Images\\Certificate\\" + docPath);
                academic.CertificatePath = docPath;
            }

            academic.SchoolName = studentacademic.SchoolName;
            academic.SchoolCity = studentacademic.SchoolCity;
            academic.SchoolCountry = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentacademic.SchoolCountry)).ToString();
            academic.Graduate = Convert.ToInt32(studentacademic.Graduate);


            if (academic.CertificatePath == null)
            {
                academic.CertificatePath = "";
            }

            if (academic.Graduate == (int)GraduateStatus.Yes)
            {
                if (studentacademic.GraduationYear != 0)
                    academic.GraduationYear = studentacademic.GraduationYear;
                if (studentacademic.GraduationLevel != 0)
                    academic.GraduationLevel = studentacademic.GraduationLevel;
            }
            else
            {
                academic.GraduationYear = null;
                academic.GraduationLevel = null;
            }
            if (academic.Graduate == (int)GraduateStatus.OnGoing)
            {
                if (studentacademic.DegreebeingPursued != 0)
                    academic.DegreebeingPursued = studentacademic.DegreebeingPursued;
                if (studentacademic.ExpectedYearofGraduation != 0)
                    academic.ExpectedYearofGraduation = studentacademic.ExpectedYearofGraduation;
                academic.FieldofStudy = studentacademic.FieldofStudy;
            }
            else
            {
                academic.DegreebeingPursued = null;
                academic.ExpectedYearofGraduation = null;
                academic.FieldofStudy = null;
            }

            if (studentacademic.Rank != null)
            {
                academic.Rank = Convert.ToDecimal(studentacademic.Rank);
            }
            else
            {
                academic.Rank = null;
            }
            academic.StudentId = studentacademic.StudentId;
            academicBll.Save(academic);
            return true;
        }


        /// <summary>
        /// This method is used to Save student basic Details
        /// </summary>
        /// 
        private bool SaveEducationalPrefrence(StudentEducationalPreferenceModel studentedit)
        {
            Student student = null;

            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                student = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId));
            }
            else
            {
                student = studentBll.GetByUserName(CookieHelper.Username);
            }


            if (student == null)
            {
                student = new Student();

                if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                {
                    student.UserName = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                }
                else
                {
                    student.UserName = CookieHelper.Username;
                }

                student.StudyingIn = studentedit.StudyingIn;
                student.LookingFor = studentedit.LookingFor;
                if (student.LookingForCountry != "233")
                {
                    student.LookingForCountry = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.LookingForCountry)).ToString(); ;
                }
                else
                {
                    student.LookingForCountry = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.LookingForCountry)).ToString(); ;
                    student.OtherStudy = studentedit.OtherStudy;
                }
                student.Payout = studentedit.Payout;
                student.StartDate = studentedit.StartDate;
                student.DesiredFieldofStudy = EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Convert.ToInt16(studentedit.DesiredFieldofStudy)).ToString();

                //Dummy Entries for OtherFields
                student.FirstName = "";
                student.MiddleName = "";
                student.LastName = "";
                student.Photo = "";
                student.Address1 = "";
                student.City = "";
                student.Country = "";
                student.PrimaryNumber = "";
                student.PrimaryType = "";
                student.PrimaryEmail = "";
                student.Citizenship = "";
                studentBll.Save(student);
            }

            else
            {
                student.StudyingIn = studentedit.StudyingIn;
                student.LookingFor = studentedit.LookingFor;
                if (student.LookingForCountry != "233")
                {
                    student.LookingForCountry = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.LookingForCountry)).ToString(); ;
                }
                else
                {
                    student.LookingForCountry = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(studentedit.LookingForCountry)).ToString(); ;
                    student.OtherStudy = studentedit.OtherStudy;
                }
                student.Payout = studentedit.Payout;
                student.StartDate = studentedit.StartDate;
                student.DesiredFieldofStudy = EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Convert.ToInt16(studentedit.DesiredFieldofStudy)).ToString();

                studentBll.Save(student);
            }
            return true;
        }

        /// <summary>
        /// This method is used to Save Photo Details
        /// </summary>
        /// 
        private bool SavePhoto(HttpPostedFileBase hpf)
        {
            if (hpf != null)
            {
                try
                {
                    string docPath = String.Empty;
                    string filename = hpf.FileName;
                    filename = filename.Substring(filename.LastIndexOf('.') - 1);
                    docPath = Guid.NewGuid().ToString() + filename;
                    hpf.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "\\Images\\Profile\\") + docPath);
                    //hpf.SaveAs(ConfigurationManager.AppSettings["ServerPath"] + "/Images/Profile/" + docPath);
                    Student student = null;
                    if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                    {
                        student = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId));
                    }
                    else
                    {
                        student = studentBll.GetByUserName(CookieHelper.Username);
                    }

                    if (student == null)
                    {
                        student = new Student();
                        if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                        {
                            student.UserName = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                        }
                        else
                        {
                            student.UserName = CookieHelper.Username;
                        }
                        student.Photo = docPath;
                        CookieHelper.SetCookie(CookieKeys.Photo, student.Photo);

                        //Null Entries for other fields
                        student.FirstName = "";
                        student.MiddleName = "";
                        student.LastName = "";
                        //student.Photo = "";
                        student.Address1 = "";
                        student.City = "";
                        student.Country = "";
                        student.PrimaryNumber = "";
                        student.PrimaryType = "";
                        student.PrimaryEmail = "";
                        student.Citizenship = "";
                        student.StudyingIn = 0;
                        student.LookingFor = 0;
                        student.LookingForCountry = "";
                        student.StartDate = 0;
                        student.Payout = 0;

                        studentBll.Save(student);
                    }
                    else
                    {
                        student.Photo = docPath;
                        CookieHelper.SetCookie(CookieKeys.Photo, student.Photo);

                        studentBll.Save(student);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return true;
        }


        /// <summary>
        /// This method is used to Save Extra Activity Details
        /// </summary>
        /// 
        private bool SaveExtra(StudentExtraActivityModel studentedit)
        {
            Student student = null;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                student = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId));
            }
            else
            {
                student = studentBll.GetByUserName(CookieHelper.Username);
            }
            if (student == null)
            {
                student = new Student();
                student.SportActivities = studentedit.SportActivities;
                student.LeadershipActivies = studentedit.LeadershipActivies;
                student.OtherActivities = studentedit.LeadershipActivies;
                if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                {
                    student.UserName = CookieHelper.GetCookieValue(CookieKeys.adminStudentId);
                }
                else
                {
                    student.UserName = CookieHelper.Username;
                }

                //Null Entries for other fields
                student.FirstName = "";
                student.MiddleName = "";
                student.LastName = "";
                student.Photo = "";
                student.Address1 = "";
                student.City = "";
                student.Country = "";
                student.PrimaryNumber = "";
                student.PrimaryType = "";
                student.PrimaryEmail = "";
                student.Citizenship = "";
                student.StudyingIn = 0;
                student.LookingFor = 0;
                student.LookingForCountry = "";
                student.StartDate = 0;
                student.Payout = 0;
                studentBll.Save(student);
            }
            else
            {
                student.SportActivities = studentedit.SportActivities;
                student.LeadershipActivies = studentedit.LeadershipActivies;
                student.OtherActivities = studentedit.OtherActivities;
                studentBll.Save(student);
            }
            return true;
        }


        /// <summary>
        /// This method is used to Save Work history
        /// </summary>
        /// 
        private bool SaveWorkHistory(StudentWorkHistoryModel studentedit)
        {
            StudentWorkHistory workHistory = null;
            if (studentedit.StudentWorkHistoryId != 0)
            {
                workHistory = studentWorkHistoryBLL.Get(studentedit.StudentWorkHistoryId);
            }
            else
            {
                workHistory = new StudentWorkHistory();
            }

            workHistory.StudentId = studentedit.StudentId;
            workHistory.CompanyName = studentedit.CompanyName;
            workHistory.Position = studentedit.Position;

            workHistory.StartDate = new DateTime(int.Parse(studentedit.start_year), int.Parse(studentedit.start_month), int.Parse(studentedit.start_day));
            workHistory.EndDate = new DateTime(int.Parse(studentedit.end_year), int.Parse(studentedit.end_month), int.Parse(studentedit.end_day));
            workHistory.Responsibilities = studentedit.Responsibilities;

            studentWorkHistoryBLL.Save(workHistory);
            return true;
        }


        /// <summary>
        /// This method is used to Save International Test Details
        /// </summary>
        /// 
        private bool SaveInternationalTest(StudentExtraActivityModel studentedit)
        {
            return false;
        }
        #endregion

        #region Web Methods Division
        //This method is use to delete student academic record
        [WebMethod()]
        public JsonResult AcademicDelete(int StudentAcademicId)
        {
            string str = string.Empty;
            StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
            academicBll.delete(Convert.ToInt32(StudentAcademicId));
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //This method is use to delete student work record
        [WebMethod()]
        public JsonResult WorkHistoryDelete(int StudentWorkId)
        {
            studentWorkHistoryBLL.delete(StudentWorkId);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        // For Delete International Test Record in ProfileOverView Page
        [WebMethod()]
        public JsonResult _TestDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        // For Getting Status Of Message Conversation
        [WebMethod()]
        public JsonResult _Getstatus(string ToUserName)
        {
            Message msg = new Message();
            MessageBLL messageBLL = new MessageBLL();
            msg = messageBLL.GetAll().Where(x => x.ToUserName == ToUserName && x.FromUserName == CookieHelper.Username || x.ToUserName == CookieHelper.Username && x.FromUserName == ToUserName && x.ParentId == 0).FirstOrDefault();
            if (msg != null)
                return Json(msg.MessageId.ToString(), JsonRequestBehavior.AllowGet);
            else
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        // For Getting Conversation Of Messages
        [WebMethod]
        public JsonResult _GetAllConversation(string messageId)
        {
            MessageBLL messageBLL = new MessageBLL();
            Message ParentMessage = messageBLL.Get(Convert.ToInt32(messageId));
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            System.Text.StringBuilder builder_1 = new System.Text.StringBuilder();
            Student std = null;
            StudentBLL studentBLL = new StudentBLL();
            UniversityBLL universityBLL = new UniversityBLL();
            University uni = new University();
            String toUser = "", fromUser = "", strMsgToSens = "";
            string ShowDelete = string.Empty;
            string ShowMessage = string.Empty;

            List<Message> tmpMsgList = new List<Message>();
            tmpMsgList = messageBLL.GetAll();
            if (ParentMessage != null)
            {
                Message msgforup;
                List<Message> msg = tmpMsgList.Where(x => x.ParentId == ParentMessage.MessageId && x.ToUserName == CookieHelper.Username && x.IsApproved == true
                    ||
                    x.MessageId == ParentMessage.MessageId).ToList();
                for (int i = 0; i < msg.Count; i++)
                {
                    msgforup = msg[i];
                    msgforup.IsRead = true;
                    messageBLL.Save(msgforup);
                }
                //Message msgtmp = tmpMsgList.Where(x => x.MessageId == ParentMessage.MessageId && x.IsRead == false).FirstOrDefault();
                //if (msgtmp != null)
                //{
                //    msgtmp.IsRead = true;
                //    messageBLL.Save(msgtmp);
                //}


                if (ParentMessage.FromUserName == CookieHelper.Username)
                {
                    fromUser = CookieHelper.Username;
                    toUser = ParentMessage.ToUserName;
                    strMsgToSens = ParentMessage.ToUserName;
                }
                else
                {
                    fromUser = ParentMessage.FromUserName;
                    toUser = CookieHelper.Username;
                    strMsgToSens = ParentMessage.FromUserName;
                }

                UserBLL userBll = new UserBLL();

                string toUserType = userBll.Get(toUser).LoginTypeId.ToString();
                string fromUserType = userBll.Get(fromUser).LoginTypeId.ToString();
                //FOR TOUSER DISPLAY
                #region ToUser
                if (toUserType == "1")
                {
                    //When User is Student
                    std = studentBLL.GetByUserName(toUser);
                    if (!string.IsNullOrEmpty(std.Photo))
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + std.Photo + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + std.FirstName + " " + std.LastName + "</label></div>");
                    builder_1.Append("<div class='std_time_country'><span>In " + std.City + "," + std.Country + "</span></div></li>");
                    //SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CountryList)std.Country).ToString()
                }
                else if (toUserType == "2")
                {
                    //when user is university
                    uni = universityBLL.GetByUserName(toUser);
                    if (!string.IsNullOrEmpty(uni.Image))
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + uni.Image + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + uni.UniversityName + "</label></div>");
                    builder_1.Append("<div class='std_time_country'><span>In " + uni.City + "," + uni.Country + "</span></div></li>");
                }
                else
                {
                    builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>Administrator</label></div>");
                    builder_1.Append("</li>");
                }

                #endregion
                //FOR FROM USER DISPLAY
                #region FromUser
                if (fromUserType == "1")
                {
                    //When User is Student
                    std = studentBLL.GetByUserName(fromUser);
                    if (!string.IsNullOrEmpty(std.Photo))
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + std.Photo + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + std.FirstName + " " + std.LastName + "</label></div>");
                    builder_1.Append("<div class='std_time_country'><span>In " + std.City + "," + std.Country + "</span></div></li>");

                }
                else if (fromUserType == "2")
                {
                    //when user is university
                    uni = universityBLL.GetByUserName(fromUser);
                    if (!string.IsNullOrEmpty(uni.Image))
                        builder_1.Append("<li><div class='std_img'><img src='/Images/Profile/" + uni.Image + "' alt='' /></div>");
                    else
                        builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>" + uni.UniversityName + "</label></div>");
                    builder_1.Append("<div class='std_time_country'><span>In " + uni.City + "," + uni.Country + "</span></div></li>");
                }
                else
                {
                    builder_1.Append("<li><div class='std_img'><img src='/Images/no_image.jpg' alt='' /></div>");
                    builder_1.Append("<div class='std_name'><label>Administrator</label></div>");
                    builder_1.Append("</li>");
                }

                #endregion



                builder.Append("<div class='h2_heading'><h2>" + ParentMessage.Title + "</h2></div>");

                builder.Append("<ul class='list_4'><li><label>Reply</label><textarea id='txtReplyDesc' class='textcounter' maxlength='300' placeholder='type your message here'></textarea>");
                builder.Append(" <input disabled='disabled' maxlength='3' size='3' class='counter fright' value='300' id='counter4'></li>");

                builder.Append("<li><label></label><input type='button' id='btnReply' class='button' value='Send' />");
                builder.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='btnBack' class='button' value='Back' /></li></ul>");
                builder.Append("<script type='text/javascript'>$('#btnReply').click(function () {SaveMessageReply('" + ParentMessage.MessageId + "','" + ParentMessage.Title + "','" + CookieHelper.Username + "','" + strMsgToSens + "')});</script>");
                builder.Append("<script type='text/javascript'>$('#btnBack').click(function(){$('.msg_replay_hide').show(); $('.msg_replay_show').hide();});</script>");
                builder.Append("<script type='text/javascript'>$('.textcounter').bind('keyup blur',function(){var maxLength = $(this).attr('maxlength');var counterid = $(this).siblings('.counter').attr('id');textCounter(this, counterid, maxLength);});</script>");

                builder.Append("<ul id='ulMessage' class='list_2 collapsible_msg'>");

                List<Message> msgList = messageBLL.GetAll()

                    .Where(x => x.ParentId == ParentMessage.MessageId && x.FromUserName == CookieHelper.Username
                        ||
                        (x.ParentId == ParentMessage.MessageId && x.IsApproved == true)

                        ||

                        x.MessageId == ParentMessage.MessageId && x.FromUserName == CookieHelper.Username

                        ||

                       x.MessageId == ParentMessage.MessageId && x.IsApproved == true)

                       .OrderByDescending(x => x.MessageId).ToList();

                for (int i = 0; i < msgList.Count; i++)
                {
                    if (msgList[i].Flag == false && msgList[i].FromUserName == CookieHelper.Username)
                        ShowMessage = "false";
                    else
                        ShowMessage = "true";
                    if (msgList[i].Flag == true && msgList[i].FromUserName == CookieHelper.Username)
                        ShowDelete = "true";
                    else
                        ShowDelete = "false";

                    if (ShowMessage != "false")
                    {

                        string userType = userBll.Get(msgList[i].FromUserName).LoginTypeId.ToString();
                        if (userType == "1")
                        {
                            var res = studentBLL.GetByUserName(msgList[i].FromUserName);
                            if (i < 5)
                                builder.Append("<li><div class='page_collapsible'></div>");
                            else
                                builder.Append("<li class='remainMsg' style='display:none'><div class='page_collapsible'></div>");
                            if (res != null)
                            {
                                if (!string.IsNullOrEmpty(res.Photo))
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + res.Photo + "'/></div>");
                                else
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                                builder.Append(" <div class='list_2_detail'><span>" + res.FirstName + " " + res.LastName + "</span>");
                                builder.Append(" <span class='date'>" + msgList[i].CreatedDate.ToShortDateString() + "</span>");
                                if (ShowDelete == "true")
                                    builder.Append("<div><a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright button smallbutton' style='cursor:pointer'>Delete</a></div>");
                                builder.Append("<p>" + msgList[i].Description + "</p></div></div></li>");
                            }
                        }

                        else if (userType == "2")
                        {
                            var res = universityBLL.GetByUserName(msgList[i].FromUserName);
                            if (i < 5)
                                builder.Append("<li><div class='page_collapsible'></div>");
                            else
                                builder.Append("<li class='remainMsg' style='display:none'><div class='page_collapsible'></div>");
                            if (res != null)
                            {
                                if (!string.IsNullOrEmpty(res.Image))
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + res.Image + "'/></div>");
                                else
                                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                                builder.Append(" <div class='list_2_detail'><span>" + res.UniversityName + "</span>");
                                builder.Append(" <span class='date'>" + msgList[i].CreatedDate.ToShortDateString() + "</span>");
                                if (ShowDelete == "true")
                                    builder.Append("<div><a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright button smallbutton' style='cursor:pointer'>Delete</a></div>");

                                builder.Append("<p>" + msgList[i].Description + "</p></div></div></li>");
                            }
                        }
                        else
                        {
                            if (i < 5)
                                builder.Append("<li><div class='page_collapsible'></div>");
                            else
                                builder.Append("<li class='remainMsg' style='display:none'><div class='page_collapsible'></div>");
                            builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                            builder.Append(" <div class='list_2_detail'><span>Administrator</span>");
                            builder.Append(" <span class='date'>" + msgList[i].CreatedDate.ToShortDateString() + "</span>");
                            if (ShowDelete == "true")
                                builder.Append("<div><a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright button smallbutton' style='cursor:pointer'>Delete</a></div>");

                            builder.Append("<p>" + msgList[i].Description + "</p></div></div></li>");
                        }
                    }
                }
                builder.Append("</ul>");


                if (msgList.Count > 6)
                    builder.Append("<div class='openclose_collapsible'> <a id='openAll' title='Open All'>View More Message</a></div>");
                //builder.Append("<div class='openclose_collapsible'> <a id='openAll' title='Open All'>" + (msgList.Count - 5) + " Message</a></div>");

            }
            String[] strarr = new String[2];
            strarr[0] = builder.ToString();
            strarr[1] = builder_1.ToString();
            return Json(strarr, JsonRequestBehavior.AllowGet);
        }

        // For Delete Message
        [WebMethod()]
        public JsonResult _MsgDelete(string MessageId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            Message msg = new Message();
            msg = messageBLL.Get(Convert.ToInt32(MessageId));
            int parent = msg.ParentId;
            if (parent == 0)
            {
                List<Message> msgLi = messageBLL.GetAll().Where(x => x.MessageId == Convert.ToInt32(MessageId) || x.ParentId == Convert.ToInt32(MessageId)).ToList();
                foreach (var li in msgLi)
                {
                    msg = li;
                    msg.Flag = false;
                    messageBLL.Save(msg);
                }
                str = "parent";
            }
            else
            {
                msg.Flag = false;
                messageBLL.Save(msg);
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        // For Sending the Message
        [WebMethod()]
        public string _SendMessage(string Title, string Description, string sendToUserName, string ParentId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
            msg.Title = Title;
            msg.Description = Description;
            msg.ParentId = Convert.ToInt32(ParentId);
            msg.FromUserName = CookieHelper.Username;
            msg.ToUserName = sendToUserName;
            msg.IsRead = false;
            if (sendToUserName == CookieHelper.AdminUsername)
                msg.IsApproved = true;
            else
                msg.IsApproved = false;
            msg.CreatedDate = DateTime.Now;
            msg.Flag = true;
            messageBLL.Save(msg);
            return msg.MessageId.ToString();
        }

        // For Sending the Message Reply
        [WebMethod]
        public JsonResult _SaveMessageReply(string parentMsgId, string msgDesc, string msgTitle, string fromusername, string tousername)
        {
            MessageBLL messageBLL = new MessageBLL();
            string msgs = null;
            Message msgdata = messageBLL.Get(Convert.ToInt32(parentMsgId));
            if (msgdata != null)
            {
                Message msg = new Message();
                msg.Title = msgTitle;
                msg.Description = msgDesc;
                msg.ParentId = Convert.ToInt32(parentMsgId);
                msg.FromUserName = fromusername;
                msg.ToUserName = tousername;
                msg.IsRead = false;
                msg.CreatedDate = DateTime.Now;

                if (tousername == CookieHelper.AdminUsername || fromusername == CookieHelper.AdminUsername || adminUser.Contains(fromusername) || adminUser.Contains(tousername))
                    msg.IsApproved = true;
                else
                    msg.IsApproved = false;
                msg.CreatedDate = DateTime.Now;
                msg.Flag = true;
                messageBLL.Save(msg);

                if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
                {
                    //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
                    StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendMessageEmail.htm")));
                    emailBody.Replace(EmailConstantHelper.MessageTitle, msgTitle);
                    emailBody.Replace(EmailConstantHelper.MessageDescription, msgDesc);
                    MailHelper.sendMail(tousername, "Received Message From Admin : SpotCollege", emailBody.ToString());

                    //    StringBuilder mailmsg = new StringBuilder(string.Empty);
                    //    mailmsg.Append("<h3>You Received  Message from Admin</h3>");
                    //    mailmsg.Append("<br/><b>Message Title :</b>" + msgTitle);
                    //    mailmsg.Append("<br/><b>Message Description :</b>" + msgDesc);
                    //    mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                    //    MailHelper.sendMail(tousername, "Received Message : SpotCollege", mailmsg.ToString());
                }


                UniversityBLL universityBLL = new UniversityBLL();
                AlertBLL alertBLL = new AlertBLL();
            }

            StudentBLL studentBLL = new StudentBLL();
            UniversityBLL UniversityBLL = new UniversityBLL();
            StringBuilder builder = new StringBuilder();

            if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
            {
                Student std = studentBLL.GetByUserName(fromusername);
                builder.Append("<li class='admin'><div class='page_collapsible' id='first'></div>");

                if (!string.IsNullOrEmpty(std.Photo))
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + std.Photo + "'/></div>");
                else
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                builder.Append(" <div class='list_2_detail'><span>" + std.FirstName + " " + std.LastName + "</span>");
                builder.Append("<span class='date'>" + DateTime.Now.Date.ToShortDateString() + "</span>");
                //builder.Append("<a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright' style='color:red; line-height:35px; cursor:pointer'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");
                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                msgs = builder.ToString();
                return Json(msgs, JsonRequestBehavior.AllowGet);
            }
            else if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            {
                University uni = UniversityBLL.GetByUserName(fromusername);
                builder.Append("<li class='admin'><div class='page_collapsible' id='first'></div>");

                if (!string.IsNullOrEmpty(uni.Image))
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/Profile/" + uni.Image + "'/></div>");
                else
                    builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                builder.Append(" <div class='list_2_detail'><span>" + uni.UniversityName + "</span>");
                builder.Append("<span class='date'>" + DateTime.Now.Date.ToShortDateString() + "</span>");
                // builder.Append("<a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright' style='color:red; line-height:35px; cursor:pointer'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");
                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                msgs = builder.ToString();
                return Json(msgs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                builder.Append("<li class='admin'><div class='page_collapsible' id='first'></div>");
                builder.Append("<div class='collapsible-detail'><div class='list_2_img'><img alt='' src='/Images/no_image.jpg'/></div>");
                builder.Append(" <div class='list_2_detail'><span>Administrator</span>");
                builder.Append("<span class='date'>" + DateTime.Now.Date.ToShortDateString() + "</span>");
                //builder.Append("<a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright' style='color:red; line-height:35px; cursor:pointer'>&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>");
                builder.Append("<p>" + msgDesc + "</p></div></div></li>");
                msgs = builder.ToString();
                return Json(msgs, JsonRequestBehavior.AllowGet);
            }

        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF Student DATA TO LIST (For Scrolling)
        [WebMethod()]
        public JsonResult _AppendAndDisplayStudent()
        {
            StringBuilder sb = new StringBuilder();

            List<Student> tmpStudentList = new List<Student>();
            List<StudentInterest> tmpStudentInterestList = new List<StudentInterest>();

            if (StaticStudentInterestList != null)
            {
                if (StaticStudentInterestList.Count != 0)
                {
                    staticStudentList = null;

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

                        if (std.Photo != null)
                            img = "/Images/Profile/" + std.Photo;
                        else
                            img = "/Images/no_image.jpg";

                        sb.Append("<li><div class='name'>" + std.FirstName + " " + std.LastName + "<div class='country'>" + std.Country + "</div></div>");
                        sb.Append("<div class='img'><img src=" + img + " /></a></div>");
                        sb.Append("<div class='detail'><ul class='list_4'><li><label>Location :</label><span>" + std.City + ", " + std.Country + "</span></li>");
                        if (std.StudyingIn != 0)
                        {
                            sb.Append("<li><label>Current Status :</label><span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CurrentlyIn)(std.StudyingIn))).ToString() + "</span></li>");
                            sb.Append("<li><label>Looking for a :</label><span>" + ((SpotCollege.Common.ProgramLookingFor)(std.LookingFor)).ToString() + " in " + std.DesiredFieldofStudy + "</span></li>");
                            sb.Append("<li><label>Desired Start Date :</label><span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.expectedStart)(std.StartDate))).ToString() + "</span></li>");
                        }
                        else
                        {
                            sb.Append("<li><label>Current Status :</label><span></span></li>");
                            sb.Append("<li><label>Looking for a :</label><span></span></li>");
                            sb.Append("<li><label>Desired Start Date :</label><span></span></li>");
                        }
                        sb.Append(" <li><label>Desired Location :</label><span>" + std.LookingForCountry + "</span>");
                        sb.Append(" <li><label>Profile Created On :</label><span>" + std.CreatedDate.Value.ToShortDateString() + "</span></li></ul></div>");
                        sb.Append(" <div class='morebtn_'><input type='hidden' id='" + std.StudentId + "' value='" + std.UserName + "' />");
                        sb.Append("<a href='javascript:void(0)' onclick=\"javascript:ShowStudentProfile('" + std.UserName + "' ,'true')\" class='button fright'>Profile</a></div></li>");
                        //sb.Append("<a href=\"/University/StudentProfile/" + std.StudentId + "\" class='button fright'>Profile</a></div></li>");
                        #endregion

                    }
                    return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
                    #endregion
                }
                else
                {
                    return Json("no", JsonRequestBehavior.AllowGet);
                    // return "no";
                }
            }

            if (staticStudentList != null)
            {
                if (staticStudentList.Count != 0)
                {
                    StaticStudentInterestList = null;
                    if (staticStudentList.Count < 8)
                    {
                        tmpStudentList = staticStudentList.ToList();
                        staticStudentList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpStudentList = staticStudentList.GetRange(0, 8);
                        staticStudentList.RemoveRange(0, 8);
                    }
                    string img;
                    for (int i = 0; i < tmpStudentList.Count; i++)
                    {
                        if (tmpStudentList[i].Photo != null)
                            img = "/Images/Profile/" + tmpStudentList[i].Photo;
                        else
                            img = "/Images/no_image.jpg";

                        sb.Append("<li><div class='name'>" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "<div class='country'>" + tmpStudentList[i].Country + "</div></div>");
                        sb.Append("<div class='img'><img src=" + img + " /></a></div>");
                        sb.Append("<div class='detail'><ul class='list_4'><li><label>Location :</label><span>" + tmpStudentList[i].City + ", " + tmpStudentList[i].Country + "</span></li>");
                        if (tmpStudentList[i].StudyingIn != 0)
                        {
                            sb.Append("<li><label>Current Status :</label><span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CurrentlyIn)(tmpStudentList[i].StudyingIn))).ToString() + "</span></li>");
                            sb.Append("<li><label>Looking for a :</label><span>" + ((SpotCollege.Common.ProgramLookingFor)(tmpStudentList[i].LookingFor)).ToString() + " in " + tmpStudentList[i].DesiredFieldofStudy + "</span></li>");
                            sb.Append("<li><label>Desired Start Date :</label><span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.expectedStart)(tmpStudentList[i].StartDate))).ToString() + "</span></li>");
                        }
                        else
                        {
                            sb.Append("<li><label>Current Status :</label><span></span></li>");
                            sb.Append("<li><label>Looking for a :</label><span></span></li>");
                            sb.Append("<li><label>Desired Start Date :</label><span></span></li>");
                        }
                        sb.Append(" <li><label>Desired Location :</label><span>" + tmpStudentList[i].LookingForCountry + "</span>");
                        sb.Append(" <li><label>Profile Created On :</label><span>" + tmpStudentList[i].CreatedDate.Value.ToShortDateString() + "</span></li></ul></div>");
                        sb.Append(" <div class='morebtn_'><input type='hidden' id='" + tmpStudentList[i].StudentId + "' value='" + tmpStudentList[i].UserName + "' />");
                        if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())
                            sb.Append("<a href='javascript:void(0)' class='msgbtn' id='" + tmpStudentList[i].UserName + "' onclick=\"javascript:OpenMsgPopup( '" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "','" + tmpStudentList[i].UserName + "' )\">Message</a></div></li>");
                        else
                            sb.Append("<a href='javascript:void(0)' onclick=\"javascript:ShowStudentProfile('" + tmpStudentList[i].UserName + "' )\" class='button fright'>Profile</a></div></li>");

                    }
                    return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
                    //return sb.ToString();
                }
                else
                {
                    return Json("no", JsonRequestBehavior.AllowGet);
                    // return "no";
                }
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
                //return "Error";
            }
        }

        //University Full Information PopUp
        [WebMethod()]
        public JsonResult _GetUnivercityData(string UniversityName)
        {
            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            StringBuilder sb = new StringBuilder();
            string[] strArr = new string[25];
            string[] tmp;
            string[] tmpCourseArr;

            List<StudentInterest> stdint = studentInterestBLL.GetAll().Where(x => x.StudentUserName == CookieHelper.Username && x.UniversityUserName == UniversityName).ToList();
            if (stdint.Count != 0)
                strArr[22] = "hideint";

            university = universityBll.GetByUserName(UniversityName);
            if (university != null)
            {
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
                if (university.PersuingAssociateDegree != null)
                    strArr[14] = university.PersuingAssociateDegree.ToString();
                if (university.EnroledPerYearChina != null)
                    strArr[15] = university.EnroledPerYearChina.ToString();
                if (university.EnroledPerYearIndianSub != null)
                    strArr[16] = university.EnroledPerYearIndianSub.ToString();
                if (university.EnroledPerYearAfrica != null)
                    strArr[17] = university.EnroledPerYearAfrica.ToString();
                if (university.EnroledPerYearMidEast != null)
                    strArr[18] = university.EnroledPerYearMidEast.ToString();
                if (university.EnroledPerYearSouthAmerica != null)
                    strArr[19] = university.EnroledPerYearSouthAmerica.ToString();
                if (university.EnroledPerYearEurope != null)
                    strArr[20] = university.EnroledPerYearEurope.ToString();

                if (university.UniversityName != null)
                    strArr[21] = university.UniversityName;

                if (university.ContactNo != null)
                    strArr[24] = university.ContactNo;

                if (university.ApplicationFee != null)
                {
                    tmp = university.ApplicationFee.ToString().Split('/');
                    strArr[23] = tmp[0] + '-' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')');
                }
                return Json(strArr, JsonRequestBehavior.AllowGet);
            }
            return Json(strArr, JsonRequestBehavior.AllowGet);
        }

        //Save Student Interest
        [WebMethod()]
        public JsonResult _SaveStudentIntrest(string UniversityName)
        {
            UserBLL userBll = new UserBLL();
            string str = string.Empty;
            SpotCollege.DAL.User user = userBll.Get(CookieHelper.Username);
            if (user != null)
            {
                StudentBLL studentBll = new StudentBLL();
                Student student = studentBll.GetByUserName(CookieHelper.Username);
                StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
                StudentTestBLL studentTestBll = new StudentTestBLL();
                if (student != null)
                {
                    List<StudentTest> stdTest = studentTestBll.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
                    if (stdTest == null || stdTest.Count == 0)
                    {
                        str = "Please Enter Atleast One International Test Record inorder to show Interest in University.";
                    }
                    else
                    {

                        StudentInterest studentintrest = new StudentInterest();
                        StudentInterestBLL studentintrestbll = new StudentInterestBLL();
                        University university = new University();
                        UniversityBLL universityBll = new UniversityBLL();

                        university = universityBll.GetByUniversityName(UniversityName);

                        studentintrest.UniversityUserName = university.UserName;
                        studentintrest.StudentUserName = user.UserName;
                        studentintrest.Approved = (int)StudentInterestApproved.Applied;
                        studentintrestbll.Save(studentintrest);
                        str = "true";
                    }
                }

            }

            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF University DATA TO LIST (For Scrolling UniversityList)
        [WebMethod()]
        public JsonResult _AppendAndDisplayUniversity()
        {
            StringBuilder sb = new StringBuilder();
            string[] tmp;
            List<University> tmpUniversityList = new List<University>();
            if (StaticUniversityList != null)
            {
                if (StaticUniversityList.Count != 0)
                {
                    if (StaticUniversityList.Count < 6)
                    {
                        tmpUniversityList = StaticUniversityList.ToList();
                        StaticUniversityList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpUniversityList = StaticUniversityList.GetRange(0, 6);
                        StaticUniversityList.RemoveRange(0, 6);
                    }

                    string img;
                    string undgradFee;
                    string gradFee;
                    string BookFee;
                    string BoardFee;
                    string ApplicationFee;
                    string InternationalGraduate;
                    string Graduates;
                    string UnderGraduates;
                    string PersuingAssociateDegree;
                    for (int i = 0; i < tmpUniversityList.Count; i++)
                    {

                        if (tmpUniversityList[i].UniversityImage != null)
                            img = "/Images/Profile/" + tmpUniversityList[i].UniversityImage;
                        else
                            img = "/Images/no_image.jpg";

                        if (tmpUniversityList[i].UnderGraduateFee != null)
                        {
                            tmp = tmpUniversityList[i].UnderGraduateFee.ToString().Split('/');
                            undgradFee = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                        }
                        else
                            undgradFee = "Not Available";

                        if (tmpUniversityList[i].GraduateFee != null)
                        {
                            tmp = tmpUniversityList[i].GraduateFee.ToString().Split('/');
                            gradFee = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                        }
                        else
                            gradFee = "Not Available";

                        if (tmpUniversityList[i].BookFee != null)
                        {
                            tmp = tmpUniversityList[i].BookFee.ToString().Split('/');
                            BookFee = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + ' ' + tmp[2];
                        }
                        else
                            BookFee = "Not Available";

                        if (tmpUniversityList[i].BoardFee != null)
                        {
                            tmp = tmpUniversityList[i].BoardFee.ToString().Split('/');
                            BoardFee = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')') + " Per Month";
                        }
                        else
                            BoardFee = "Not Available";

                        if (tmpUniversityList[i].ApplicationFee != null)
                        {
                            tmp = tmpUniversityList[i].ApplicationFee.ToString().Split('/');
                            ApplicationFee = tmp[0] + ' ' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')');
                        }
                        else
                            ApplicationFee = "Not Available";

                        if (tmpUniversityList[i].Graduates != null)
                            Graduates = tmpUniversityList[i].Graduates.ToString();
                        else
                            Graduates = "Not Available";
                        if (tmpUniversityList[i].UnderGraduates != null)
                            UnderGraduates = tmpUniversityList[i].UnderGraduates.ToString();
                        else
                            UnderGraduates = "Not Available";
                        if (tmpUniversityList[i].InternationalGraduate != null)
                            InternationalGraduate = tmpUniversityList[i].InternationalGraduate.ToString();
                        else
                            InternationalGraduate = "Not Available";
                        if (tmpUniversityList[i].PersuingAssociateDegree != null)
                            PersuingAssociateDegree = tmpUniversityList[i].PersuingAssociateDegree.ToString();
                        else
                            PersuingAssociateDegree = "Not Available";

                        sb.Append("<br/><span><li><div class='img_padding'> <img src='" + img + "' /> </div>");
                        sb.Append(" <div class='university_detail'><div class='name'><span>" + tmpUniversityList[i].UniversityName + "</span></div>");
                        sb.Append("<div class='establishment'>In " + tmpUniversityList[i].City + ", " + tmpUniversityList[i].Country + ". &nbsp;&nbsp; Year of establishment  " + tmpUniversityList[i].EstablishedYear + " </div>");
                        sb.Append("<ul class='small_university_box'><li><div class='h2_heading'><h2>Cost for International students :</h2></div>");
                        sb.Append("</li><li>&raquo; Bachelor's Program : " + undgradFee + "	</li><li>&raquo; Master's Program : " + gradFee + "</li><li>&raquo; Books and supplies : " + BookFee + "</li><li>&raquo; Off-campus room and Board : " + BoardFee + "</li><li>&raquo; Application Fee : " + ApplicationFee + "</li></ul>");
                        sb.Append("<div class='width100per'></div>");
                        sb.Append("<ul class='small_university_box'><li><div class='h2_heading'><h2>Enrollment Numbers</h2> </div></li>");
                        sb.Append("<li>&raquo; Associate's  : " + PersuingAssociateDegree + " </li><li>&raquo; Bachelor's : " + Graduates + "</li>");
                        sb.Append("<li>&raquo; Master's : " + UnderGraduates + " </li>");
                        sb.Append("<li>&raquo; Phd : " + InternationalGraduate + "</li></ul><div class='moreinformation'>");
                        sb.Append("<div class='width100per'></div>");

                        sb.Append("<div class='moreinformation'> <a href='javascript:void(0);' onclick=\"javascript:OpenFAQsPopup('" + tmpUniversityList[i].UserName + "')\" class='button'>FAQ for Student</a> &nbsp;<a href='javascript:void(0);' onclick=\"javascript:OpenReviewList('" + tmpUniversityList[i].UniversityName + "')\" class='button'>Reviews</a>  <a id='" + tmpUniversityList[i].UserName + "' class=\"anUniversityDetail button fright\" onclick=\"javascript: Openpopup('" + tmpUniversityList[i].UserName + "'); \" >More Information</a></div></div> </li></span>");
                    }

                    return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("no", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        // METHOD USED TO APPEND SPECIFIC RANGE OF Alert DATA TO LIST (For Scrolling Alert)
        [WebMethod()]
        public JsonResult _AppendAndDisplayAlert()
        {
            StringBuilder sb = new StringBuilder();
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

                    return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("no", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF Message DATA TO LIST (For Scrolling Message list)
        [WebMethod()]
        public JsonResult _AppendAndDisplayMessage()
        {
            StringBuilder sb = new StringBuilder();
            List<Message> tmpMessageList = new List<Message>();
            string adminUsername = CookieHelper.AdminUsername;
            if (msgStaticList != null)
            {
                if (msgStaticList.Count != 0)
                {
                    if (msgStaticList.Count < NoOfMsgToDisplay)
                    {
                        tmpMessageList = msgStaticList.ToList();
                        msgStaticList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpMessageList = msgStaticList.GetRange(0, NoOfMsgToDisplay);
                        msgStaticList.RemoveRange(0, NoOfMsgToDisplay);
                    }
                    string img;
                    Student stdt = new Student();
                    StudentBLL studentbll = new StudentBLL();
                    MessageBLL messageBLL = new MessageBLL();
                    string newmsgColor = string.Empty;
                    string IsShow = string.Empty;
                    for (int i = 0; i < tmpMessageList.Count; i++)
                    {
                        if (tmpMessageList[i].Flag == false && tmpMessageList[i].FromUserName == CookieHelper.Username)
                            IsShow = "false";
                        else
                            IsShow = "true";

                        if (IsShow != "false")
                        {
                            List<Message> messagelist = messageBLL.GetAll().Where(x => x.ParentId == tmpMessageList[i].MessageId && x.IsApproved == true || x.MessageId == tmpMessageList[i].MessageId && x.IsApproved == true).ToList();
                            int msgCount = messagelist.Count;
                            int n = tmpMessageList[i].MessageId;
                            int co = messagelist.Where(x => x.IsRead == false && x.ToUserName == CookieHelper.Username).Count();
                            if (co != 0)
                                newmsgColor = "#C8D8E8";
                            else
                                newmsgColor = "transparent";
                            //for Admin
                            #region Admin
                            if (tmpMessageList[i].FromUserName == adminUsername || tmpMessageList[i].ToUserName == adminUsername || adminUser.Contains(tmpMessageList[i].FromUserName) || adminUser.Contains(tmpMessageList[i].ToUserName))
                            {
                                img = "/Images/no_image.jpg";
                                sb.Append("<li id='parentdiv_" + tmpMessageList[i].MessageId + "' style='background-color:" + newmsgColor + ";'><div class='user_img'><img src='" + img + "' /></div>");

                                sb.Append("<div class='user_name'><label >");
                                sb.Append("Administrator (" + msgCount + ")</label><br/>");
                                sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                                sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                                sb.Append("<p>" + tmpMessageList[i].Description);
                                sb.Append("<a id='" + tmpMessageList[i].MessageId + "' class='msg_replay_open_click button' style='float:right' href='#'>View message and Respond</a></p></div></li>");
                            }
                            #endregion

                            if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
                            {
                                #region University
                                if (tmpMessageList[i].ToUserName != CookieHelper.AdminUsername)
                                {
                                    if (tmpMessageList[i].ToUserName == CookieHelper.Username)
                                        stdt = studentbll.GetByUserName(tmpMessageList[i].FromUserName);
                                    else
                                        stdt = studentbll.GetByUserName(tmpMessageList[i].ToUserName);

                                    if (stdt != null)
                                    {
                                        if (stdt.Photo == null)
                                            img = "/Images/no_image.jpg";
                                        else
                                            img = "/Images/Profile/" + stdt.Photo;

                                        sb.Append("<li id='parentdiv_" + tmpMessageList[i].MessageId + "' style='background-color:" + newmsgColor + ";'><div class='user_img'><img src='" + img + "' /></div>");

                                        sb.Append("<div class='user_name'><label>");
                                        sb.Append(stdt.FirstName + " " + stdt.LastName + "(" + msgCount + ")</label><br/>");

                                        //if (msgalert.Count != 0)
                                        //{
                                        //    sb.Append("<div class='user_name'><label style='color:Green;'>");
                                        //    sb.Append(stdt.FirstName + " " + stdt.LastName + "(" + msgCount + ")</label><br/>");
                                        //}
                                        //else
                                        //{
                                        //    sb.Append("<div class='user_name'><label>");
                                        //    sb.Append(stdt.FirstName + " " + stdt.LastName + "(" + msgCount + ")</label><br/>");
                                        //}
                                        sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                                        sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                                        sb.Append("<p>" + tmpMessageList[i].Description);
                                        sb.Append("<a id='" + tmpMessageList[i].MessageId + "' class='msg_replay_open_click button' onclick=\"OpenMsg(this)\" style='float:right' href='#'>View message and Respond</a></p></div></li>");
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                if (tmpMessageList[i].FromUserName == CookieHelper.Username)
                                    stdt = studentbll.GetByUserName(tmpMessageList[i].ToUserName);
                                else
                                    stdt = studentbll.GetByUserName(tmpMessageList[i].FromUserName);

                                #region Student
                                if (stdt != null)
                                {
                                    if (stdt.Photo == null)
                                        img = "/Images/no_image.jpg";
                                    else
                                        img = "/Images/Profile/" + stdt.Photo;

                                    sb.Append("<li id='parentdiv_" + tmpMessageList[i].MessageId + "' style='background-color:" + newmsgColor + ";'><div class='user_img'><img src='" + img + "' /></div>");
                                    sb.Append("<div class='user_name'><label>");
                                    sb.Append(stdt.FirstName + " " + stdt.LastName + "(" + msgCount + ")</label><br/>");
                                    sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                                    sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                                    sb.Append("<p>" + tmpMessageList[i].Description);
                                    sb.Append("<a id='" + tmpMessageList[i].MessageId + "' class='msg_replay_open_click button'  onclick=\"OpenMsg(this)\"  style='float:right' href='#'>View message and Respond</a></p></div></li>");
                                }
                                else
                                {
                                    UniversityBLL universityBLL = new UniversityBLL();
                                    University uni = new University();
                                    if (tmpMessageList[i].FromUserName == CookieHelper.Username)
                                        uni = universityBLL.GetByUserName(tmpMessageList[i].ToUserName);
                                    else
                                        uni = universityBll.GetByUserName(tmpMessageList[i].FromUserName);

                                    if (uni != null)
                                    {
                                        if (uni.Image != null)
                                            img = "/Images/Profile/" + uni.Image;
                                        else
                                            img = "/Images/no_image.jpg";

                                        sb.Append("<li id='parentdiv_" + tmpMessageList[i].MessageId + "' style='background-color:" + newmsgColor + ";'><div class='user_img'><img src='" + img + "' /></div>");

                                        sb.Append("<div class='user_name'><label>");
                                        sb.Append(uni.UniversityName + "(" + msgCount + ")</label><br/>");
                                        sb.Append("<i>" + tmpMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                                        sb.Append("<div class='user_msg'><label>" + tmpMessageList[i].Title + "</label>");
                                        sb.Append("<p>" + tmpMessageList[i].Description);
                                        sb.Append("<a id='" + tmpMessageList[i].MessageId + "' class='msg_replay_open_click button'  onclick=\"OpenMsg(this)\"  style='float:right' href='#'>View message and Respond</a></p></div></li>");

                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("no", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        //For Name and UserName for Searching in Message Center
        [WebMethod()]
        public JsonResult _GetNewMessageSenderList()
        {
            string[] strArr;
            strArr = newmsguserList;
            return Json(strArr, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _GetMessageTitle()
        {
            return Json(msgTitle, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _SearchMessageByTitle(string Title)
        {
            StringBuilder sb = new StringBuilder();
            MessageBLL messageBLL = new MessageBLL();
            List<MessageModel> tmpSerchMessageList = new List<MessageModel>();

            tmpSerchMessageList = StaticSearchMessageList.Where(x => x.Title.Contains(Title.Trim())).ToList();
            if (tmpSerchMessageList.Count != 0)
            {
                for (int i = 0; i < tmpSerchMessageList.Count; i++)
                {
                    sb.Append("<span><li><div class='user_img'><img src='" + tmpSerchMessageList[i].Photo + "' /></div>");
                    sb.Append("<div class='user_name'><label style='" + tmpSerchMessageList[i].NameColor + "'>" + tmpSerchMessageList[i].Name + "(" + tmpSerchMessageList[i].NoofChildMessage + ")</label><br/>");
                    sb.Append("<i>" + tmpSerchMessageList[i].CreatedDate.ToShortDateString() + "</i></div>");
                    sb.Append("<div class='user_msg'><label>" + tmpSerchMessageList[i].Title + "</label>");
                    sb.Append("<p>" + tmpSerchMessageList[i].Description);
                    sb.Append("<a id='" + tmpSerchMessageList[i].MessageId + "' class='msg_replay_open_click button' onclick=\"OpenMsg(this)\" style='float:right' href='#'>View message and Respond</a></p></div></li></span>");
                }
                return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
            }
            else
                return Json("no", JsonRequestBehavior.AllowGet);

        }

        [WebMethod()]
        public JsonResult _GetFAQs(string Username)
        {
            string faqs = string.Empty;
            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            university = universityBll.GetByUserName(Username);
            if (university != null)
            {

                if (university.Faqs != null)
                    faqs = university.Faqs;

                return Json(faqs, JsonRequestBehavior.AllowGet);
            }
            return Json(faqs, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _DeclineInterstedStudent(string Username)
        {
            string str = string.Empty;
            StudentInterestBLL studentInterestBll = new StudentInterestBLL();
            AlertBLL alertBll = new AlertBLL();
            UniversityBLL universityBll = new UniversityBLL();
            StudentInterest std = studentInterestBll.GetAll().Where(x => x.StudentUserName == Username && x.UniversityUserName == CookieHelper.Username).FirstOrDefault();
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
            alert.UserName = Username;
            alertBll.Save(alert);
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _ProfileCount(string Username)
        {
            string str = string.Empty;
            ViewProfile viewProfile = new ViewProfile();
            List<ViewProfile> viewProfileList = viewProfileBLL.GetAll().Where(x => x.UniversityUsername == CookieHelper.Username && x.StudentUsername == Username).ToList();
            if (viewProfileList.Count == 0)
            {
                viewProfile.UniversityUsername = CookieHelper.Username;
                viewProfile.StudentUsername = Username;
                viewProfileBLL.Save(viewProfile);
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _GetResourceDescription(string ResourceId)
        {
            string[] Des = new string[3];
            Resource resource = new Resource();
            resource = resourceBLL.Get(Convert.ToInt32(ResourceId));
            if (resource != null)
            {
                Des[0] = resource.Title;
                if (resource.Description != null)
                    Des[1] = resource.Description;

                return Json(Des, JsonRequestBehavior.AllowGet);
            }
            return Json(Des, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _SendFeedBack(string Description, string sendToUserName)
        {


            string str = string.Empty;

            //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
            StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendMessageEmail.htm")));
            emailBody.Replace(EmailConstantHelper.FeedBackDetails, Description);
            MailHelper.sendMail(sendToUserName, "Received FeedBack : SpotCollege", emailBody.ToString());

            //StringBuilder mailmsg = new StringBuilder(string.Empty);

            //if (CookieHelper.UserType == ((int)LoginTypes.University).ToString())
            //    mailmsg.Append("<h3>You Received  FeedBack from University</h3>");
            //else
            //    mailmsg.Append("<h3>You Received  FeedBack from Student</h3>");
            //mailmsg.Append("<br/><b>FeedBack Details :</b>" + Description);
            //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
            //MailHelper.sendMail(sendToUserName, "Received FeedBack : SpotCollege", mailmsg.ToString());

            return Json(str, JsonRequestBehavior.AllowGet);
        }


        [WebMethod()]
        public JsonResult _getSurveyDetail(string SurveyId)
        {
            if (SurveyId != "" && SurveyId != null)
            {
                //surveyDetail = surveyBLL.GetAll().Where(x => x.University.Contains(UniversityName)).FirstOrDefault();
                surveyDetail = surveyBLL.Get(Convert.ToInt32(SurveyId));
            }
            return Json(surveyDetail, JsonRequestBehavior.AllowGet);
        }

        public static string UniversityName = string.Empty;
        [HttpGet]
        public ActionResult filteredCountry(string Country, string University)
        {
            if (University != null)
                UniversityName = University;

            StudentProfileModel spm = new StudentProfileModel();
            List<SurveyDetail> surveyList = new List<SurveyDetail>();

            if (Country != "0" && Country != null)
                spm.reviewList = surveyBLL.GetAll().Where(x => x.IsApproved == true && x.Country == Country && x.University.Contains(UniversityName)).OrderByDescending(x => x.SurveyId).ToList();
            else
                spm.reviewList = surveyBLL.GetAll().Where(x => x.IsApproved == true && x.University.Contains(UniversityName)).OrderByDescending(x => x.SurveyId).ToList();

            return View(spm);
        }
        #endregion
    }
}

