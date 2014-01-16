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
using System.Configuration;


namespace Spot_College_MVC.Controllers
{
    public class AdminSecController : Controller
    {
        #region Declaration
        public static List<Student> staticStudentList = new List<Student>();
        public static List<University> staticUniversityList = new List<University>();
        StudentBLL studentBll = new StudentBLL();
        StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
        StudentTestBLL studentTestBll = new StudentTestBLL();
        StudentInterestBLL StudentInterestBLL = new StudentInterestBLL();
        UniversityBLL universityBll = new UniversityBLL();
        UserBLL userBll = new UserBLL();
        MessageBLL messageBLL = new MessageBLL();
        ResourceCategoryBLL resourceCategoryBLL = new ResourceCategoryBLL();
        ResourceBLL resourceBLL = new ResourceBLL();
        SettingBLL settingBLL = new SettingBLL();
        PermissionBLL permissionBLL = new PermissionBLL();

        List<University> universityList = null;
        List<Student> studentlist = null;
        User user = new User();
        Permission permission = new Permission();

        static List<University> staticUniList = new List<University>();
        static List<Student> staticStudList = new List<Student>();
        static int msgToDisplay = 13;

        //This list is used for soting purpose. Final Student list after searching is stored here.
        static List<Student> staticFinalResultStudent = new List<Student>();
        static List<University> staticFinalResultUniversity = new List<University>();
        //this variable is used to store current sorting type. This variable must have one of two values 'asc' and 'desc'
        static string sortType = "asc";

        static string[] msgTitle = { };

        public static List<MessageModel> StaticSearchMessageList = new List<MessageModel>();
        List<ResourceCategory> resourceCategoryList = new List<ResourceCategory>();
        SurveyBLL surveyBLL = new SurveyBLL();
        SurveyDetail surveyDetail = new SurveyDetail();
        List<SurveyDetail> surveyDetailList = new List<SurveyDetail>();

        string[] privacyStudent = { };

        Settings settings = new Settings();
        List<Settings> settingList = new List<Settings>();
        #endregion

        #region Methods Division
        // Get Admin dashboard data
        public ActionResult Index()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            return View();
        }

        // set cookie value for add new student data from admin site
        public ActionResult AddStudent(string id)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (string.IsNullOrEmpty(id))
                CookieHelper.RemoveCookie(CookieKeys.adminStudentId);
            else
                CookieHelper.SetCookie(CookieKeys.adminStudentId, id);
            return RedirectToAction("EditProfile", "Student");
        }

        //set cookie value for add new student data from admin site
        public ActionResult AddUniversity(string id)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (string.IsNullOrEmpty(id))
                CookieHelper.RemoveCookie(CookieKeys.adminUniversityId);
            else
                CookieHelper.SetCookie(CookieKeys.adminUniversityId, id);
            return RedirectToAction("Edit", "University");
        }

        // Get Student Information 
        public ActionResult StudentInformation()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            {
                return RedirectToAction("index", "Authentication");
            }

            StudentSearchModel spm = new StudentSearchModel();
            staticFinalResultStudent = new List<Student>();
            studentlist = studentBll.GetAll().OrderByDescending(x => x.StudentId).ToList();
            spm.studentList = studentlist.Take(15).ToList();
            ViewBag.lblTotalResult = studentlist.Count();


            //Assiging all UserName to Hiddenfield to send message to all Users
            StringBuilder builder = new StringBuilder();
            foreach (string value in studentlist.Select(x => x.UserName).ToArray())
            {
                builder.Append(value);
                builder.Append(',');
            }
            ViewBag.hdnAllRecipients = builder.ToString();

            staticStudentList = studentlist.ToList();
            //static Final list is seperatly managed because element which is displayed is removed from 
            // staticStudentList.
            staticFinalResultStudent = studentlist.ToList();

            if (studentlist.Count < 15)
                staticStudentList.RemoveAll(x => 1 == 1);
            else
                staticStudentList.RemoveRange(0, 15);

            return View(spm);
        }

        // Post student info for Sorting and Searching
        [HttpPost]
        public ActionResult StudentInformation(StudentSearchModel ssm)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            //For sorting of student list by Name
            if (!string.IsNullOrEmpty(Request.Form["btnSort"]))
            {
                if (sortType == "asc")
                {
                    staticStudentList = staticFinalResultStudent.OrderBy(m => m.FirstName).ToList();
                    sortType = "desc";
                }
                else
                {
                    staticStudentList = staticFinalResultStudent.OrderByDescending(m => m.FirstName).ToList();
                    sortType = "asc";
                }
                ssm.studentList = staticStudentList.Take(15).ToList();

                if (staticFinalResultStudent.Count < 15)
                    staticStudentList.RemoveAll(x => 1 == 1);
                else
                    staticStudentList.RemoveRange(0, 15);
                ViewBag.lblTotalResult = staticFinalResultStudent.Count();
                return View(ssm);
            }
            // For Searching Specific Student from list
            #region RgnSearching
            List<Student> finalStudentSearch = new List<Student>();
            if (!string.IsNullOrEmpty(Request.Form["btnSearch"]))
            {
                if (ModelState.IsValid)
                {
                    finalStudentSearch = null;
                    List<Student> studentList = new List<Student>();
                    studentList = studentBll.GetAll().OrderByDescending(x => x.StudentId).ToList();

                    if (ssm.IsActive || ssm.IsNotActive)
                    {
                        string[] activeUserList = userBll.GetAll().Where(x => x.LoginTypeId == 1 && (x.IsActive == true || x.IsActive == !ssm.IsNotActive)).Select(y => y.UserName).ToArray();
                        finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => activeUserList.Contains(x.UserName)).ToList());
                    }

                    if (ssm.NotEnteredIntScore)
                    {
                        int[] tmp = studentTestBll.GetAll().Select(x => x.StudentId).Distinct().ToArray();
                        finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => !tmp.Contains(x.StudentId)).Distinct().ToList());
                    }

                    if (ssm.NotEnteredEduInfo)
                    {
                        int[] tmp = academicBll.GetAll().Select(x => x.StudentId).Distinct().ToArray();
                        finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => !tmp.Contains(x.StudentId)).Distinct().ToList());
                    }

                    if (ssm.NotUploadCertificate)
                    {
                        int[] cerificate = academicBll.GetAll().Where(x => string.IsNullOrEmpty(x.CertificatePath) && x.Graduate == 1).Select(y => y.StudentId).Distinct().ToArray();
                        finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => cerificate.Contains(x.StudentId)).ToList());
                    }

                    if (ssm.NotUploadSelfImg)
                    {
                        finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => string.IsNullOrEmpty(x.Photo)).ToList());
                    }

                    if (ssm.NotComplEduPreferences)
                    {
                        finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => string.IsNullOrEmpty(x.LookingForCountry)).ToList());
                    }
                    ViewBag.msgErr = "";
                    if (ssm.FromDateMonth != "" && ssm.FromDateYear != "" && ssm.ToDateMonth != "" && ssm.ToDateYear != "" && ssm.FromDateMonth != null && ssm.FromDateYear != null && ssm.ToDateMonth != null && ssm.ToDateYear != null)
                    {
                        try
                        {
                            DateTime todate = new DateTime(Convert.ToInt16(ssm.ToDateYear), Convert.ToInt16(ssm.ToDateMonth), 1);
                            DateTime fromdate = new DateTime(Convert.ToInt16(ssm.FromDateYear), Convert.ToInt16(ssm.FromDateMonth), 1);

                            finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => x.CreatedDate <= todate && x.CreatedDate >= fromdate).ToList());
                        }
                        catch (Exception ex)
                        {
                            ViewBag.msgErr = "please enter correct date";
                            return View(ssm);
                        }
                    }
                    if (ssm.FromDateMonth != "" && ssm.FromDateYear != "" && ssm.ToDateMonth == "" && ssm.ToDateYear == "" && ssm.FromDateMonth != null && ssm.FromDateYear != null && ssm.ToDateMonth == null && ssm.ToDateYear == null)
                    {
                        try
                        {
                            DateTime fromdate = new DateTime(Convert.ToInt16(ssm.FromDateYear), Convert.ToInt16(ssm.FromDateMonth), 1);
                            finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => x.CreatedDate >= fromdate).ToList());
                        }
                        catch (Exception ex)
                        {
                            ViewBag.msgErr = "please enter correct date";
                            return View(ssm);
                        }
                    }
                    if (ssm.FromDateMonth == "" && ssm.FromDateYear == "" && ssm.ToDateMonth != "" && ssm.ToDateYear != "" && ssm.FromDateMonth == null && ssm.FromDateYear == null && ssm.ToDateMonth != null && ssm.ToDateYear != null)
                    {
                        try
                        {
                            DateTime todate = new DateTime(Convert.ToInt16(ssm.ToDateYear), Convert.ToInt16(ssm.ToDateMonth), 1);
                            finalStudentSearch = getCommonElements(finalStudentSearch, studentList.Where(x => x.CreatedDate <= todate).ToList());
                        }
                        catch (Exception ex)
                        {
                            ViewBag.msgErr = "please enter correct date";
                            return View(ssm);
                        }
                    }

                    ViewBag.msgErr = "";

                    string country = "", lookingforcountry = "", programs = "", lookingfor = "", term = "";
                    if (ssm.StudentFrom != "0")
                        country = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(ssm.StudentFrom)).ToString();

                    if (ssm.DesiredCountry != "0")
                        lookingforcountry = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(ssm.DesiredCountry)).ToString();

                    if (ssm.DesiredProgram != "0")
                        programs = EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Convert.ToInt16(ssm.DesiredProgram)).ToString();

                    if (ssm.DesiredTermStart != "0")
                        term = ssm.DesiredTermStart;

                    if (ssm.DesiredLevel != "0")
                        lookingfor = ssm.DesiredLevel;


                    //x.DesiredFieldofStudy!=null &&
                    List<Student> tmpStudents = studentBll.GetAll()
                                                     .Where(x =>
                                                         x.Country.Contains(country) &&
                                                                 x.LookingForCountry.Contains(lookingforcountry) &&
                                                                 x.DesiredFieldofStudy.Contains(programs) &&
                                                                 x.LookingFor.ToString().Contains(lookingfor) &&
                                                                 x.StartDate.ToString().Contains(term)).OrderByDescending(x => x.StudentId).ToList();

                    if (finalStudentSearch != null)
                        finalStudentSearch = getCommonElements(finalStudentSearch, tmpStudents);
                    else
                        finalStudentSearch = tmpStudents;

                    string[] strarr = finalStudentSearch.Select(x => x.UserName).ToArray();

                    StringBuilder builder = new StringBuilder();
                    foreach (string value in strarr)
                    {
                        builder.Append(value);
                        builder.Append(',');
                    }
                    ViewBag.hdnAllRecipients = builder.ToString();

                    ViewBag.lblTotalResult = finalStudentSearch.Count.ToString();

                    staticStudentList = finalStudentSearch.OrderByDescending(x => x.StudentId).ToList();
                    //static Final list is seperatly managed because element which is displayed is removed from 
                    // staticStudentList.
                    staticFinalResultStudent = staticStudentList.ToList();


                    if (finalStudentSearch.Count < 15)
                        staticStudentList.RemoveAll(x => 1 == 1);
                    else
                        staticStudentList.RemoveRange(0, 15);

                    ssm.studentList = finalStudentSearch.Take(15).ToList();
                    return View(ssm);
                }
                return View(ssm);
            }
            #endregion
            return View(ssm);
        }

        //this method is use to get common student data for searching(Private method)
        private List<Student> getCommonElements(List<Student> list1, List<Student> list2)
        {
            List<Student> tmplist = new List<Student>();
            if (list1 != null)
                tmplist = list1.Where(x => list2.Select(y => y.UserName).ToArray().Contains(x.UserName)).ToList();
            else
                tmplist = list2;
            return tmplist;
        }

        // Get University Information 
        public ActionResult UniversityInformation()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            {
                Permission permission = permissionBLL.GetbyUsername(CookieHelper.Username);
                if (permission.CollegeProfile == false)
                { return RedirectToAction("index", "Authentication"); }
            }

            UniversitySearchModel usm = new UniversitySearchModel();
            List<University> universityList = universityBll.GetAll().OrderByDescending(x => x.UniversityId).ToList();
            staticFinalResultUniversity = new List<University>();
            StringBuilder builder = new StringBuilder();
            foreach (string value in universityList.Select(x => x.UserName).ToArray())
            {
                builder.Append(value);
                builder.Append(',');
            }

            ViewBag.hdnAllRecipients = builder.ToString();
            ViewBag.lblNoOfResult = universityList.Count.ToString() + " Record Found";

            staticUniversityList = universityList.OrderByDescending(x => x.UniversityId).ToList();
            staticFinalResultUniversity = staticUniversityList.ToList();

            if (universityList.Count < 15)
                staticUniversityList.RemoveAll(x => 1 == 1);
            else
                staticUniversityList.RemoveRange(0, 15);

            usm.UniversityList = universityList.OrderByDescending(x => x.UniversityId).Take(15).ToList();
            return View(usm);
        }

        // Post University info for Sorting and Searching
        [HttpPost]
        public ActionResult UniversityInformation(UniversitySearchModel usm)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            // For sorting university by Name
            if (!string.IsNullOrEmpty(Request.Form["btnSort"]))
            {
                if (sortType == "asc")
                {
                    staticUniversityList = staticFinalResultUniversity.OrderBy(x => x.UniversityName).ToList();
                    sortType = "desc";
                }
                else
                {
                    staticUniversityList = staticFinalResultUniversity.OrderByDescending(m => m.UniversityName).ToList();
                    sortType = "asc";
                }
                usm.UniversityList = staticUniversityList.Take(15).ToList();

                if (staticFinalResultUniversity.Count < 15)
                    staticUniversityList.RemoveAll(x => 1 == 1);
                else
                    staticUniversityList.RemoveRange(0, 15);
                ViewBag.lblTotalResult = staticFinalResultUniversity.Count();
                return View(usm);
            }
            // For Search Specific universities from list
            #region UniversitySearch
            if (!string.IsNullOrEmpty(Request.Form["btnSearchUniversity"]))
            {
                universityList = null;

                if (usm.Incompletecost)
                    universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => string.IsNullOrEmpty(x.BookFee) || string.IsNullOrEmpty(x.GraduateFee) || string.IsNullOrEmpty(x.BoardFee) || string.IsNullOrEmpty(x.UnderGraduateFee)).ToList());

                if (usm.Nolargeimage)
                    universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => string.IsNullOrEmpty(x.UniversityImage)).ToList());

                if (usm.Nologoimage)
                    universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => string.IsNullOrEmpty(x.Image)).ToList());


                if (usm.Isactive || usm.IsNotActive)
                {
                    string[] activeUserList = userBll.GetAll().Where(x => x.LoginTypeId == 2 && (x.IsActive == usm.Isactive || x.IsActive == !usm.IsNotActive)).Select(y => y.UserName).ToArray();
                    universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => activeUserList.Contains(x.UserName)).ToList());
                }
                if (usm.IncompleteEnrollment)
                {
                    universityList = getCommonElements(universityList, universityBll.GetAll().Where(x => x.Graduates == null || x.UnderGraduates == null || x.InternationalGraduate == null).ToList());
                }


                string fromcntry = "";
                if (usm.CountryFrom != "0")
                    fromcntry = EnumHelper.GetDescriptionFromEnumValue((CountryList)Convert.ToInt16(usm.CountryFrom)).ToString();
                if (universityList != null)
                    universityList = universityList.Where(x => x.Country.Contains(fromcntry)).ToList();
                else
                    universityList = universityBll.GetAll().Where(x => x.Country.Contains(fromcntry)).ToList();

                StringBuilder builder = new StringBuilder();
                foreach (string value in universityList.Select(x => x.UserName).ToArray())
                {
                    builder.Append(value);
                    builder.Append(',');
                }
                ViewBag.hdnAllRecipients = builder.ToString();
                ViewBag.lblNoOfResult = universityList.Count.ToString() + " Records Found";

                staticUniversityList = universityList.OrderByDescending(x => x.UniversityId).ToList();
                staticFinalResultUniversity = staticUniversityList.ToList();


                if (universityList.Count < 15)
                    staticUniversityList.RemoveAll(x => 1 == 1);
                else
                    staticUniversityList.RemoveRange(0, 15);

                usm.UniversityList = universityList.OrderByDescending(x => x.UniversityId).Take(15).ToList();

                return View(usm);
            }
            #endregion

            return View(usm);
        }

        //this method is use to get common University data for searching(Private method)
        private List<University> getCommonElements(List<University> list1, List<University> list2)
        {
            List<University> tmplist = new List<University>();
            if (list1 != null)
                tmplist = list1.Where(x => list2.Select(y => y.UserName).ToArray().Contains(x.UserName)).ToList();
            else
                tmplist = list2;
            return tmplist;
        }

        //Get Intersted University List
        public ActionResult InterestedUniversity()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            UniversitySearchModel usm = new UniversitySearchModel();

            List<User> user = userBll.GetAll().Where(x => x.IsActive == false).ToList();

            List<University> universityList = new List<University>();
            foreach (var intrestuniversity in user)
            {
                University u = universityBll.GetByUserName(intrestuniversity.UserName);
                if (u != null)
                {
                    universityList.Add(u);
                }
                else
                {
                    universityList.Remove(u);
                }
            }

            usm.UniversityList = universityList.OrderByDescending(x => x.UniversityId).ToList();
            return View(usm);
        }

        //Get Messages from University for approving
        public ActionResult MessageCenter()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            //if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            //{
            //    Permission permission = permissionBLL.GetbyUsername(CookieHelper.Username);
            //    if (permission.StudentMessage == false || permission.CollegeMessage == false)
            //    { return RedirectToAction("index", "Authentication"); }
            //}

            MessageCenterModel mcm = new MessageCenterModel();

            staticUniList = null;
            IList<sp_NoOFunApprovedMsg_Result> result = universityBll.getUnApprovedMsg().ToList();
            IList<sp_NoOFunApprovedMsg_Result> tmplist = result.ToList();
            if (result.Count > msgToDisplay)
            {
                for (int i = 0; i < msgToDisplay; i++)
                {
                    tmplist.Remove(result[i]);
                }
            }
            else
            {
                for (int i = 0; i < result.Count; i++)
                {
                    tmplist.Remove(result[i]);
                }
            }

            List<University> uniList = universityBll.GetAll().Where(x => x.User.IsActive == true && tmplist.Select(y => y.uname).ToArray().Contains(x.UserName)).ToList();

            staticUniList = uniList.ToList();
            //this is used for when sub admin is login
            if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            {
                PermissionBLL permissionBLL = new PermissionBLL();
                Permission permission = permissionBLL.GetbyUsername(CookieHelper.Username);

                if (permission.CollegeMessage == true && permission.StudentMessage == false)
                {
                    mcm.universityList = result.Take(msgToDisplay).ToList();
                }
                else if (permission.CollegeMessage == false && permission.StudentMessage == true)
                {
                    staticStudList = null;

                    IList<sp_StudentUnApprovedMsg_Result> stdUnapprovedList = studentBll.getUnApprovedMsg();

                    IList<sp_StudentUnApprovedMsg_Result> tmpUnapprovedlist = stdUnapprovedList.ToList();

                    if (stdUnapprovedList.Count > msgToDisplay)
                    {
                        for (int i = 0; i < msgToDisplay; i++)
                        {
                            tmpUnapprovedlist.Remove(stdUnapprovedList[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < stdUnapprovedList.Count; i++)
                        {
                            tmpUnapprovedlist.Remove(stdUnapprovedList[i]);
                        }
                    }

                    List<Student> stdList = studentBll.GetAll().Where(x => x.User.IsActive == true && tmpUnapprovedlist.Select(y => y.uname).ToArray().Contains(x.UserName)).ToList();

                    staticStudList = stdList.ToList();

                    mcm.studentList = stdUnapprovedList.Take(msgToDisplay).ToList();

                }
                else
                {
                    mcm.universityList = result.Take(msgToDisplay).ToList();
                    mcm.studentList = null;
                }
            }
            else
            {
                mcm.universityList = result.Take(msgToDisplay).ToList();
                mcm.studentList = null;
            }

            return View(mcm);

        }

        //this is use for to get Student messges for approving
        [HttpPost]
        public ActionResult MessageCenter(MessageCenterModel mcm)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (mcm.userType == "Student")
            {
                staticStudList = null;

                IList<sp_StudentUnApprovedMsg_Result> stdUnapprovedList = studentBll.getUnApprovedMsg();

                IList<sp_StudentUnApprovedMsg_Result> tmpUnapprovedlist = stdUnapprovedList.ToList();

                if (stdUnapprovedList.Count > msgToDisplay)
                {
                    for (int i = 0; i < msgToDisplay; i++)
                    {
                        tmpUnapprovedlist.Remove(stdUnapprovedList[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < stdUnapprovedList.Count; i++)
                    {
                        tmpUnapprovedlist.Remove(stdUnapprovedList[i]);
                    }
                }

                List<Student> stdList = studentBll.GetAll().Where(x => x.User.IsActive == true && tmpUnapprovedlist.Select(y => y.uname).ToArray().Contains(x.UserName)).ToList();

                staticStudList = stdList.ToList();

                mcm.studentList = stdUnapprovedList.Take(msgToDisplay).ToList();
            }
            else
            {
                staticUniList = null;
                IList<sp_NoOFunApprovedMsg_Result> result = universityBll.getUnApprovedMsg().ToList();
                IList<sp_NoOFunApprovedMsg_Result> tmplist = result.ToList();
                if (result.Count > msgToDisplay)
                {
                    for (int i = 0; i < msgToDisplay; i++)
                    {
                        tmplist.Remove(result[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        tmplist.Remove(result[i]);
                    }
                }

                List<University> uniList = universityBll.GetAll().Where(x => x.User.IsActive == true && tmplist.Select(y => y.uname).ToArray().Contains(x.UserName)).ToList();

                staticUniList = uniList.ToList();

                mcm.universityList = result.Take(msgToDisplay).ToList();

            }
            return View(mcm);
        }

        // this is use for getting indivisual conversation between user and admin
        public ActionResult MessageDetail(string id)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            List<MessageModel> msgModelList = new List<MessageModel>();
            string StudentUsername = string.Empty;
            Student std = new Student();
            string name = string.Empty;
            string imgPath = string.Empty;
            if (id != null)
            {
                std = studentBll.Get(Convert.ToInt32(id));
                StudentUsername = std.UserName;
                name = std.FirstName + " " + std.LastName;
                ViewBag.Name = name;
                ViewBag.ToUserName = std.UserName;
                if (std.Photo != null)
                {
                    imgPath = "/Images/Profile/" + std.Photo;
                    ViewBag.ImagePath = imgPath;
                }
                else
                {
                    imgPath = "/Images/no_image.jpg";
                    ViewBag.ImagePath = imgPath;
                }

                List<Message> msgList = messageBLL.GetAll().Where(x => x.FromUserName == CookieHelper.Username && x.ToUserName == StudentUsername
                                                                  && x.ParentId == 0 && x.IsApproved == true

                                                                  ||

                                                                  x.ToUserName == CookieHelper.Username && x.FromUserName == StudentUsername
                                                                  && x.ParentId == 0 && x.IsApproved == true
                                                                 ||
                                                                 x.FromUserName == CookieHelper.Username && x.ToUserName == StudentUsername
                                                                  )

                                                                  .OrderByDescending(x => x.MessageId).ThenByDescending(y => y.CreatedDate).ToList();

                MessageModel msgModel;
                Student stdnt = new Student();
                foreach (var m in msgList)
                {
                    msgModel = new MessageModel();
                    List<Message> messagelist = messageBLL.GetAll().Where(x => x.ParentId == m.MessageId && x.IsApproved == true || x.MessageId == m.MessageId && x.IsApproved == true).ToList();
                    msgModel.NoofChildMessage = messagelist.Count;
                    int co = messagelist.Where(x => x.IsRead == false && x.ToUserName == CookieHelper.Username).Count();
                    if (co != 0)
                        msgModel.NameColor = "#AFC0CD";

                    msgModel.MessageId = m.MessageId;
                    msgModel.ParentId = m.ParentId;
                    msgModel.Title = m.Title;
                    msgModel.Description = m.Description;
                    msgModel.CreatedDate = m.CreatedDate;
                    msgModel.Name = name;
                    msgModel.Photo = imgPath;
                    msgModelList.Add(msgModel);
                }

                msgTitle = msgModelList.Select(x => x.Title).Distinct().ToArray();
                StaticSearchMessageList = msgModelList.ToList();
                return View(msgModelList);
            }
            return View(msgModelList);
        }
        public ActionResult MessageDetails(string id)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            List<MessageModel> msgModelList = new List<MessageModel>();
            string UniversityUsername = string.Empty;
            University uni = new University();
            string name = string.Empty;
            string imgPath = string.Empty;
            if (id != null)
            {
                uni = universityBll.Get(Convert.ToInt32(id));
                UniversityUsername = uni.UserName;
                name = uni.UniversityName;
                ViewBag.Name = name;
                ViewBag.ToUserName = uni.UserName;
                if (uni.Image != null)
                {
                    imgPath = "/Images/Profile/" + uni.Image;
                    ViewBag.ImagePath = imgPath;
                }
                else
                {
                    imgPath = "/Images/no_image.jpg";
                    ViewBag.ImagePath = imgPath;
                }

                List<Message> msgList = messageBLL.GetAll().Where(x => x.FromUserName == CookieHelper.Username && x.ToUserName == UniversityUsername
                                                                  && x.ParentId == 0 && x.IsApproved == true

                                                                  ||

                                                                  x.ToUserName == CookieHelper.Username && x.FromUserName == UniversityUsername
                                                                  && x.ParentId == 0
                                                                  && x.IsApproved == true
                                                                    ||
                                                                 x.FromUserName == CookieHelper.Username && x.ToUserName == UniversityUsername)


                                                                  .OrderByDescending(x => x.MessageId).ThenByDescending(y => y.CreatedDate).ToList();

                MessageModel msgModel;
                Student stdnt = new Student();
                foreach (var m in msgList)
                {
                    msgModel = new MessageModel();
                    List<Message> messagelist = messageBLL.GetAll().Where(x => x.ParentId == m.MessageId && x.IsApproved == true || x.MessageId == m.MessageId && x.IsApproved == true).ToList();
                    msgModel.NoofChildMessage = messagelist.Count;
                    int co = messagelist.Where(x => x.IsRead == false && x.ToUserName == CookieHelper.Username).Count();
                    if (co != 0)
                        msgModel.NameColor = "#AFC0CD";

                    msgModel.MessageId = m.MessageId;
                    msgModel.ParentId = m.ParentId;
                    msgModel.Title = m.Title;
                    msgModel.Description = m.Description;
                    msgModel.CreatedDate = m.CreatedDate;
                    msgModel.Name = name;
                    msgModel.Photo = imgPath;
                    msgModelList.Add(msgModel);
                }

                msgTitle = msgModelList.Select(x => x.Title).Distinct().ToArray();
                StaticSearchMessageList = msgModelList.ToList();
                return View("MessageDetail", msgModelList);
            }
            return View("MessageDetail", msgModelList);
        }

        //Get Resources
        public ActionResult Resources()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            {
                Permission permission = permissionBLL.GetbyUsername(CookieHelper.Username);
                if (permission.Article == false)
                { return RedirectToAction("index", "Authentication"); }
            }

            resourceCategoryList = resourceCategoryBLL.GetAll().OrderByDescending(x => x.CategoryId).ToList();

            AdminResourceModel arm = new AdminResourceModel();
            string data = Convert.ToString(RouteData.Values["id"]);

            int editId = Convert.ToInt32(RouteData.Values["chk"]);

            if (data == "Cat" || data == "CatAdd")
            {
                if (editId != 0)
                {
                    ResourceCategory rcat = resourceCategoryBLL.Get(editId);
                    if (rcat != null)
                    {
                        arm.resourceCategory.CategoryId = rcat.CategoryId;
                        arm.resourceCategory.Name = rcat.Name;
                        arm.resourceCategory.SortingIndex = rcat.SortingIndex.ToString();
                    }
                    arm.categoryList = resourceCategoryList;
                }
                else
                    arm.categoryList = resourceCategoryList;
            }
            else
            {
                if (editId != 0)
                {
                    Resource res = resourceBLL.Get(editId);
                    if (res != null)
                    {
                        arm.resource.ResourceId = res.ResourceId;
                        arm.resource.CategoryId = res.CategoryId.ToString();
                        arm.resource.Title = res.Title;
                        arm.resource.Description = res.Description;
                        arm.resource.CreatedBy = res.CreatedBy;
                        arm.resource.ImagePath = res.ImagePath;
                    }
                    List<Resource> resourceList = resourceBLL.GetAll().OrderByDescending(x => x.ResourceId).ToList();
                    arm.resourceList = resourceList;

                    arm.categoryList = resourceCategoryList;
                }
                else
                {
                    List<Resource> resourceList = resourceBLL.GetAll().OrderByDescending(x => x.ResourceId).ToList();
                    arm.resourceList = resourceList;
                    arm.categoryList = resourceCategoryList;
                }
            }
            return View(arm);
        }

        //Post Resources
        [HttpPost, ValidateInput(false)]
        public ActionResult Resources(AdminResourceModel arm)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            #region Resource
            if (!string.IsNullOrEmpty(Request.Form["btnSaveResources"]))
            {
                if (ModelState.IsValid)
                {
                    SaveResources(arm.resource);
                    ViewBag.Current = "Res";
                    arm.resourceList = resourceBLL.GetAll().OrderByDescending(x => x.ResourceId).ToList();
                    arm.resource = new ResourceModel();
                    ModelState.Clear();
                    arm.categoryList = resourceCategoryBLL.GetAll().OrderByDescending(x => x.CategoryId).ToList();
                    return View(arm);
                }
                else
                {
                    ViewBag.Current = "Res";
                    arm.resourceList = resourceBLL.GetAll().OrderByDescending(x => x.ResourceId).ToList();
                    return View(arm);
                }
            }
            #endregion

            #region Resource Category
            if (!string.IsNullOrEmpty(Request.Form["btnSaveResourcesCategory"]))
            {
                if (ModelState.IsValid)
                {
                    SaveResourcesCategory(arm.resourceCategory);
                    ViewBag.Current = "Cat";
                    //int id = sem.studentWork.StudentId;
                    arm.categoryList = resourceCategoryBLL.GetAll().OrderByDescending(x => x.CategoryId).ToList();
                    arm.resourceCategory = new ResourceCategoryModel();
                    ModelState.Clear();
                    return View(arm);
                }
                else
                {
                    ViewBag.Current = "Cat";
                    arm.categoryList = resourceCategoryBLL.GetAll().OrderByDescending(x => x.CategoryId).ToList();
                    return View(arm);
                }
            }
            return View(arm);
            #endregion
        }

        //this private method is use for save details of resources and its category
        private bool SaveResources(ResourceModel resModel)
        {
            Resource resource = null;
            if (resModel.ResourceId != 0)
            {
                resource = resourceBLL.Get(resModel.ResourceId);
                resource.CreatedDate = resource.CreatedDate;
                resource.CreatedBy = resource.CreatedBy;
            }
            else
            {
                resource = new Resource();
                resource.CreatedDate = DateTime.Now;
                resource.CreatedBy = CookieHelper.Username;
            }

            if (resModel.ImageResource != null)
            {
                string Imagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(resModel.ImageResource.FileName);
                //resModel.ImageResource.SaveAs((ConfigurationManager.AppSettings["ServerPath"]) + "/Images/Profile/" + Imagename);
                resModel.ImageResource.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"]) + "/Images/Profile/" + Imagename);
                resource.ImagePath = Imagename;
            }

            resource.CategoryId = Convert.ToInt32(resModel.CategoryId);
            resource.Title = resModel.Title;
            resource.Description = resModel.Description;

            resourceBLL.Save(resource);
            return true;
        }
        private bool SaveResourcesCategory(ResourceCategoryModel resCatModel)
        {
            ResourceCategory resourceCategory = null;
            if (resCatModel.CategoryId != 0)
            {
                resourceCategory = resourceCategoryBLL.Get(resCatModel.CategoryId);
            }
            else
            {
                resourceCategory = new ResourceCategory();
            }

            resourceCategory.Name = resCatModel.Name;
            resourceCategory.SortingIndex = Convert.ToInt32(resCatModel.SortingIndex);

            resourceCategoryBLL.Save(resourceCategory);
            return true;
        }

        public ActionResult SubUserManagement()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            SubUserManagementModel sum = new SubUserManagementModel();

            sum.permissionList = permissionBLL.GetAll().ToList();

            int editId = Convert.ToInt32(RouteData.Values["id"]);

            if (editId != 0)
            {
                ViewBag.Current = "add";

                Permission per = permissionBLL.Get(editId);

                sum.permissionModel.PermissionId = per.PermissionId;
                sum.permissionModel.UserName = per.UserName;
                sum.permissionModel.Password = EncryptionHelper.Decrypt(per.User.Password);
                sum.permissionModel.FirstName = per.FirstName;
                sum.permissionModel.LastName = per.LastName;
                sum.permissionModel.Phone = per.Phone;
                sum.permissionModel.StudentMessage = per.StudentMessage;
                sum.permissionModel.CollegeProfile = per.CollegeProfile;
                sum.permissionModel.CollegeMessage = per.CollegeMessage;
                sum.permissionModel.Article = per.Article;
                sum.permissionModel.Review = per.Review;
            }


            return View(sum);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SubUserManagement(SubUserManagementModel sum)
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");


            if (!string.IsNullOrEmpty(Request.Form["btnSaveSubUserPermissionDetail"]))
            {
                if (ModelState.IsValid)
                {
                    if (sum.permissionModel.PermissionId == 0)
                    {
                        User us = userBll.Get(sum.permissionModel.UserName);
                        if (us == null)
                        {
                            //if no user exist then create entry in User table
                            user.UserName = sum.permissionModel.UserName;
                            user.Password = EncryptionHelper.Encrypt(sum.permissionModel.Password);
                            user.LoginTypeId = (int)LoginTypes.SubAdmin;
                            user.IsActive = true;
                            userBll.Save(user);
                        }
                        else
                        {
                            //if admin entered UserName that is already exist then add Model error
                            ModelState.AddModelError("permissionModel.UserName", "Username already exist. Please use another.");
                            sum.permissionModel.UserName = null;
                            return View(sum);
                        }
                    }

                    SaveSubUserPermission(sum.permissionModel);
                    sum.permissionList = permissionBLL.GetAll().OrderByDescending(x => x.PermissionId).ToList();
                    sum.permissionModel = new PermissionModel();
                    ModelState.Clear();
                    return RedirectToAction("SubUserManagement", new { Id = "" });
                    //return View(sum);
                }
                else
                {
                    //ViewBag.Current = "Res";
                    sum.permissionList = permissionBLL.GetAll().OrderByDescending(x => x.PermissionId).ToList();
                    return View(sum);
                }
            }
            return View(sum);
        }

        private bool SaveSubUserPermission(PermissionModel perModel)
        {
            Permission permission = null;
            if (perModel.PermissionId != 0)
            {
                permission = permissionBLL.Get(perModel.PermissionId);
                permission.CreatedDate = permission.CreatedDate;

                user.UserName = perModel.UserName;
                user.Password = EncryptionHelper.Encrypt(perModel.Password);
                userBll.Save(user);
            }
            else
            {
                permission = new Permission();
                permission.CreatedDate = DateTime.Now;
            }

            permission.UserName = perModel.UserName;
            permission.FirstName = perModel.FirstName;
            permission.LastName = perModel.LastName;
            permission.Phone = perModel.Phone;
            permission.StudentMessage = perModel.StudentMessage;
            permission.CollegeProfile = perModel.CollegeProfile;
            permission.CollegeMessage = perModel.CollegeMessage;
            permission.Article = perModel.Article;
            permission.Review = perModel.Review;

            permissionBLL.Save(permission);

            try
            {
                StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendUserNameandPassword.htm")));
                emailBody.Replace(EmailConstantHelper.UserName, perModel.UserName);
                emailBody.Replace(EmailConstantHelper.Password, perModel.Password);
                MailHelper.sendMail(perModel.UserName, "SpotCollege", emailBody.ToString());

                //StringBuilder mailmsg = new StringBuilder(string.Empty);
                //mailmsg.Append("<h3>" + "Hi " + perModel.FirstName + " " + perModel.LastName + "</h3>");
                //mailmsg.Append("<br/>Your account has been created as a spotcollege SubAdmin ");
                //mailmsg.Append("<br/><b>UserName :</b>" + perModel.UserName);
                //mailmsg.Append("<br/><b>Password :</b>" + perModel.Password);
                //mailmsg.Append("<br/><b>Contact No. :</b>" + perModel.Phone);
                //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                //MailHelper.sendMail(perModel.UserName, "Thanks to create Account : SpotCollege", mailmsg.ToString());
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        public ActionResult Review()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            return View();
        }

        public ActionResult Survey()
        {
            if (Convert.ToString(CookieHelper.Username) == "")
                return RedirectToAction("index", "Authentication");

            if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            {
                Permission permission = permissionBLL.GetbyUsername(CookieHelper.Username);
                if (permission.Review == false)
                { return RedirectToAction("index", "Authentication"); }
            }

            SurveyModel sm = new SurveyModel();
            sm.surveyList = surveyBLL.GetAll().ToList();
            sm.universityList = universityBll.GetAll().ToList();
            return View(sm);
        }
        #endregion

        #region Web Method Division

        //Get student Information in popup
        [WebMethod()]
        public JsonResult _GetStudentData(string UserName)
        {
            //string str=string.Empty;
            Student student = new Student();
            StudentBLL studentBLL = new StudentBLL();
            string[] strArr = new string[27];

            student = studentBLL.GetByUserName(UserName);

            #region personal basic information

            if (student.FirstName != null)
                strArr[0] = student.FirstName;
            if (student.MiddleName != null)
                strArr[1] = student.MiddleName;
            if (student.LastName != null)
                strArr[2] = student.LastName;
            if (student.Citizenship != null)
                strArr[3] = student.Citizenship;
            if (student.Address1 != null)
                strArr[4] = student.Address1;
            if (student.Photo != null)
                strArr[5] = student.Photo;
            if (student.Address2 != null)
                strArr[6] = student.Address2;
            if (student.ZipCode != null)
                strArr[7] = student.ZipCode;
            if (student.PrimaryNumber != null)
                strArr[8] = student.PrimaryNumber;
            if (student.BirthDate != null)
            {
                strArr[9] = Convert.ToDateTime(student.BirthDate).ToString("MM/dd/yyyy");
            }
            if (student.City != null)
                strArr[10] = student.City;
            if (student.Country != null)
                strArr[11] = student.Country;
            if (student.PrimaryEmail != null)
                strArr[12] = student.PrimaryEmail;

            #endregion

            #region Educational Preference

            if (student.StudyingIn != null)
            {
                string strStudingIn = (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CurrentlyIn)student.StudyingIn)).ToString();
                strArr[13] = strStudingIn;
            }
            if (student.LookingFor != null)
            {
                string strLookingFor = (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.ProgramLookingFor)student.LookingFor)).ToString();
                strArr[14] = strLookingFor;
            }
            if (student.LookingForCountry != null)
            {
                strArr[15] = student.LookingForCountry;
            }
            if (student.StartDate != null)
            {
                string strStudingIn = (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.expectedStart)student.StartDate)).ToString();
                strArr[16] = strStudingIn;
            }
            if (student.Payout != null)
            {
                string pay = "";
                if (student.Payout == 1)
                    pay = "$3000-5000";
                if (student.Payout == 2)
                    pay = "$5000-10000";
                if (student.Payout == 3)
                    pay = "$10000-15000";
                if (student.Payout == 4)
                    pay = "$15000+";

                strArr[17] = pay;
            }
            if (student.DesiredFieldofStudy != null)
            {
                strArr[18] = student.DesiredFieldofStudy;
            }
            #endregion

            StringBuilder sb = new StringBuilder();

            #region Current Academics
            try
            {
                StudentAcademicsBLL academicBll = new StudentAcademicsBLL();
                List<StudentAcademic> AcademicList = academicBll.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
                sb.Append("<table class='box simple_table' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'><tbody>");
                sb.Append("<tr><th scope='col' style='width:10%;'>SchoolName</th><th scope='col' style='width:10%;'>Graduate?</th><th scope='col' style='width:8%;'>Rank</th><th scope='col' style='width:8%;'>Certificate</th></tr>");
                for (int i = 0; i < AcademicList.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='width:10%;'>");
                    sb.Append("<span>" + AcademicList[i].SchoolName + "</span></td>");
                    sb.Append("<td style='width:10%;'><span>" + EnumHelper.GetDescriptionFromEnumValue((GraduateStatus)AcademicList[i].Graduate) + "</span></td>");

                    if (AcademicList[i].Rank != null)
                        sb.Append("<td style='width:8%;'>" + (int)AcademicList[i].Rank + "</td>");
                    else
                        sb.Append("<td style='width:8%;'>Not Available</td>");

                    sb.Append("<td style='width:8%;'>");
                    sb.Append(@"<a href='../Images/Certificate/" + AcademicList[i].CertificatePath + "' target='_blank' class='preview'>");
                    sb.Append(@"<img class='large_images' src='../Images/Certificate/" + AcademicList[i].CertificatePath + "' height='30' width='30'></a>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                strArr[19] = sb.ToString();
            }
            catch (Exception ex) { }
            #endregion

            #region International Test
            try
            {
                sb = new StringBuilder();

                StudentTestBLL studentTestBLL = new StudentTestBLL();

                List<StudentTest> TestList = studentTestBLL.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
                sb.Append("<table class='box simple_table' cellspacing='0' border='1' style='border-collapse:collapse;'><tbody>");
                sb.Append("<tr><th scope='col' style='width:10%;'>Test Name</th></tr>");
                for (int i = 0; i < TestList.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='width:10%;'>");
                    sb.Append("<span>" + (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.InternationalTestRecord)TestList[i].TestId)).ToString() + "-" + TestList[i].Date + "</span></td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                strArr[20] = sb.ToString();
            }
            catch (Exception ex) { }
            #endregion

            #region Work History
            try
            {
                sb = new StringBuilder();

                StudentWorkHistoryBLL workHistoryBll = new StudentWorkHistoryBLL();
                List<StudentWorkHistory> WorkHistoryList = workHistoryBll.GetAll().Where(x => x.StudentId == student.StudentId).ToList();

                sb.Append("<table class='box simple_table' cellspacing='0' border='1' style='border-collapse:collapse;'><tbody>");

                sb.Append("<tr><th scope='col' style='width:10%;'>Company Name</th><th scope='col' style='width:10%;'>Start Date</th><th scope='col' style='width:8%;'>End Date</th></tr>");

                for (int i = 0; i < WorkHistoryList.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='width:10%;'>");
                    sb.Append("<span>" + WorkHistoryList[i].CompanyName + "</span></td>");
                    sb.Append("<td style='width:8%;'>" + WorkHistoryList[i].StartDate.ToString("dd/MM/yyyy") + "</td>");
                    sb.Append("<td style='width:8%;'>" + WorkHistoryList[i].EndDate.ToString("dd/MM/yyyy"));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                strArr[21] = sb.ToString();
            }
            catch (Exception ex) { }
            #endregion

            #region Extra Activity

            strArr[22] = student.SportActivities;
            strArr[23] = student.LeadershipActivies;
            strArr[24] = student.OtherActivities;

            #endregion

            #region Message Button
            sb = new StringBuilder();
            sb.Append("<a id='lnkMessage' onclick=\"javascript:OpenMsgPopup('" + student.FirstName + " " + student.LastName + "','" + student.UserName + "')\"  class='button'>Send Message</a>");
            strArr[25] = sb.ToString();
            sb = new StringBuilder();
            sb.Append("<a id='lnkMessage' onclick=\"javascript:OpenMsgPopupApprove('" + student.FirstName + " " + student.LastName + "','" + student.UserName + "')\"  class='button'>Send Message</a>&nbsp;");
            sb.Append("<a id='btnDecline' onclick=\"javascript:InterestedStudentDecline('" + student.UserName + "')\"  class='button fright'>Ignore</a>");
            strArr[26] = sb.ToString();
            #endregion

            return Json(strArr, JsonRequestBehavior.AllowGet);
        }

        //For delete student record 
        [WebMethod()]
        public JsonResult _StudentDelete(string StudentId)
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

            if (!string.IsNullOrEmpty(StudentId))
            {
                staticFinalResultStudent.RemoveAll(m => m.StudentId == Convert.ToInt32(StudentId));
                List<StudentTest> studentTest = studentTestbll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                foreach (var li in studentTest)
                {
                    studentTestbll.delete(li.StudentTestId);
                }

                List<StudentAcademic> studentAcademics = academicBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                foreach (var li in studentAcademics)
                {
                    academicBll.delete(li.StudentAcademicsId);
                }
                List<StudentWorkHistory> StudentWorkhistory = workHistoryBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                foreach (var lis in StudentWorkhistory)
                {
                    workHistoryBll.delete(lis.StudentWorkHistoryId);
                }

                Student student = studentBll.Get(Convert.ToInt32(StudentId));
                string usernamedel = student.UserName;

                List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.StudentUserName == usernamedel).ToList();
                foreach (var lia in studentInterest)
                {
                    studentInterestBLL.delete(lia.StudentInterestId);
                }

                Settings settings = settingBLL.GetByUsername(usernamedel);
                if (settings != null)
                {
                    settingBLL.delete(settings.SettingId);
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

                if (studentBll.delete(Convert.ToInt32(StudentId)))
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

        //For delete Selected Student Record 
        [WebMethod()]
        public JsonResult _SelectedStudentDelete(string users)
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

            string[] strarr = users.Split(',');
            string u1 = "";
            for (int i = 0; i < strarr.Length; i++)
            {
                u1 = strarr[i];
                if (!string.IsNullOrEmpty(u1))
                {
                    int StudentId = studentBll.GetByUserName(u1).StudentId;

                    staticFinalResultStudent.RemoveAll(m => m.StudentId == StudentId);
                    #region RgnDeleteRecord
                    if (StudentId != null && StudentId != 0)
                    {
                        List<StudentTest> studentTest = studentTestbll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                        foreach (var li in studentTest)
                        {
                            studentTestbll.delete(li.StudentTestId);
                        }

                        List<StudentAcademic> studentAcademics = academicBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                        foreach (var li in studentAcademics)
                        {
                            academicBll.delete(li.StudentAcademicsId);
                        }
                        List<StudentWorkHistory> StudentWorkhistory = workHistoryBll.GetAll().Where(x => x.StudentId == Convert.ToInt32(StudentId)).ToList();
                        foreach (var lis in StudentWorkhistory)
                        {
                            workHistoryBll.delete(lis.StudentWorkHistoryId);
                        }

                        Student student = studentBll.Get(Convert.ToInt32(StudentId));
                        string usernamedel = student.UserName;

                        Settings settings = settingBLL.GetByUsername(usernamedel);
                        if (settings != null)
                        {
                            settingBLL.delete(settings.SettingId);
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

                        if (studentBll.delete(Convert.ToInt32(StudentId)))
                        {
                            userBll.delete(usernamedel);
                        }
                        else
                        {
                            return Json(str, JsonRequestBehavior.AllowGet);
                        }
                    }
                    #endregion
                }
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //For Activate Student and University from information page
        [WebMethod()]
        public JsonResult _StatusActive(string UserName)
        {
            string str = string.Empty;
            User us = userBll.Get(UserName);
            if (us != null)
            {
                us.UserName = UserName;
                us.IsActive = true;
                userBll.Save(us);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //For Deactivate Student and University from information page
        [WebMethod()]
        public JsonResult _StatusInActive(string UserName)
        {
            string str = string.Empty;
            User us = userBll.Get(UserName);
            if (us != null)
            {
                us.UserName = UserName;
                us.IsActive = false;
                userBll.Save(us);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //for activate interested university with mail
        [WebMethod()]
        public JsonResult _InterestedUniversityActive(string UserName)
        {
            string str = string.Empty;
            User us = userBll.Get(UserName);
            University univer = universityBll.GetByUserName(UserName);

            if (us != null)
            {
                us.UserName = UserName;
                us.IsActive = true;
                userBll.Save(us);

                string password = EncryptionHelper.Decrypt(us.Password);

                //for send password to new activate university through Mail
                #region Sending mail
                try
                {
                    if (univer != null)
                    {
                        // StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendUserNameandPassword.htm"));
                        StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendUserNameandPassword.htm")));
                        emailBody.Replace(EmailConstantHelper.UserName, univer.UserName);
                        emailBody.Replace(EmailConstantHelper.Password, password);
                        MailHelper.sendMail(univer.UserName, "SpotCollege", emailBody.ToString());

                        //StringBuilder mailmsg = new StringBuilder(string.Empty);
                        //mailmsg.Append("<h3>" + "Hi " + univer.UniversityName + "</h3>");
                        //mailmsg.Append("<br/><b>Your account has been created.</b> ");
                        //mailmsg.Append("<br/><b>University Name : </b>" + univer.UniversityName);
                        //mailmsg.Append("<br/><b>University Email : </b>" + univer.UserName);
                        //mailmsg.Append("<br/><b>your Password : </b>" + password);
                        //mailmsg.Append("<br/><b>University City : </b>" + univer.City);
                        //mailmsg.Append("<br/><b>University Country : </b>" + univer.Country);
                        //mailmsg.Append("<br/><b>University Contact No : </b>" + univer.ContactNo);

                        //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                        //MailHelper.sendMail(univer.UserName, "Thanks to create Account : SpotCollege", mailmsg.ToString());
                    }

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                #endregion

                return Json(str, JsonRequestBehavior.AllowGet);
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //for sending message to all selected user
        [WebMethod]
        public JsonResult _sendGroupMessage(string users, string Title, string Description)
        {
            string str = string.Empty;
            string[] strarr = users.Split(',');
            string u1 = "";
            for (int i = 0; i < strarr.Length; i++)
            {
                u1 = strarr[i];
                if (!string.IsNullOrEmpty(u1))
                {
                    MessageBLL messageBLL = new MessageBLL();
                    SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
                    msg.Title = Title;
                    msg.Description = Description;
                    msg.ParentId = 0;
                    msg.FromUserName = CookieHelper.Username;
                    msg.ToUserName = u1;
                    msg.IsRead = false;
                    msg.IsApproved = true;
                    msg.Flag = true;
                    msg.CreatedDate = DateTime.Now;

                    messageBLL.Save(msg);

                    AlertBLL alertBLL = new AlertBLL();
                    Alert alert = new Alert();
                    alert.Message = "Message received from Admin";
                    alert.CreatedDate = DateTime.Now;
                    alert.CreatedBy = CookieHelper.Username;
                    alert.UserName = u1;
                    alertBLL.Save(alert);

                    //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
                    StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendMessageEmail.htm")));
                    emailBody.Replace(EmailConstantHelper.MessageTitle, Title);
                    emailBody.Replace(EmailConstantHelper.MessageDescription, Description);
                    MailHelper.sendMail(u1, "Received New Message from Admin : SpotCollege", emailBody.ToString());

                    //StringBuilder mailmsg = new StringBuilder(string.Empty);
                    //mailmsg.Append("<h3>You Received New Message from Admin</h3>");
                    //mailmsg.Append("<br/><b>Message Title :</b>" + Title);
                    //mailmsg.Append("<br/><b>Message Description :</b>" + Description);
                    //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                    //MailHelper.sendMail(u1, "Received Message : SpotCollege", mailmsg.ToString());
                }
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        // For Geting All messages between User and admin
        [WebMethod]
        public JsonResult _GetAllConversation(string messageId)
        {
            String[] strarr = new String[2];
            if (messageId != string.Empty)
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

                List<Message> tmpMsgList = new List<Message>();
                tmpMsgList = messageBLL.GetAll();
                if (ParentMessage != null)
                {
                    Message msgforup;
                    List<Message> msg = tmpMsgList.Where(x => x.ParentId == ParentMessage.MessageId && x.ToUserName == CookieHelper.Username && x.IsApproved == true).ToList();
                    for (int i = 0; i < msg.Count; i++)
                    {
                        msgforup = msg[i];
                        msgforup.IsRead = true;
                        messageBLL.Save(msgforup);
                    }
                    Message msgtmp = tmpMsgList.Where(x => x.ParentId == ParentMessage.ParentId && x.IsRead == false).FirstOrDefault();
                    if (msgtmp != null)
                    {
                        msgtmp.IsRead = true;
                        messageBLL.Save(msgtmp);
                    }


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

                    builder.Append("<div class='h2_heading'><h2>" + ParentMessage.Title + "</h2><a id='msgdel' onclick=\"MsgDeleteAll('" + ParentMessage.MessageId + "')\" class='delete' style='cursor:pointer'>Delete All Messages</a></div>");
                    builder.Append("<ul class='list_4'><li><label>Reply</label><textarea id='txtReplyDesc' placeholder='type your message here'></textarea></li>");
                    builder.Append("<li><label></label><input type='button' id='btnReply' class='button' value='Send' />");
                    builder.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='btnBack' class='button' value='Back' /></li></ul>");
                    builder.Append("<script type='text/javascript'>$('#btnReply').click(function () {SaveMessageReply('" + ParentMessage.MessageId + "','" + ParentMessage.Title + "','" + CookieHelper.Username + "','" + strMsgToSens + "')});</script>");
                    builder.Append("<script type='text/javascript'>$('#btnBack').click(function(){$('.msg_replay_hide').show(); $('.msg_replay_show').hide();});</script>");


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
                                builder.Append("<span class='date fright'>" + msgList[i].CreatedDate.ToShortDateString() + "</span>");
                                builder.Append(" <div class='list_2_detail'><span>" + res.FirstName + " " + res.LastName + "</span>");
                                builder.Append("<div><a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright button smallbutton' style='cursor:pointer;>Delete</a></div>");

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
                                builder.Append("<span class='date fright'>" + msgList[i].CreatedDate.ToShortDateString() + "</span>");
                                builder.Append(" <div class='list_2_detail'><span>" + res.UniversityName + "</span>");
                                builder.Append("<div><a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright button smallbutton' style='cursor:pointer;>Delete</a></div>");

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
                            builder.Append("<span class='date fright'>" + msgList[i].CreatedDate.ToShortDateString() + "</span>");
                            builder.Append("<div><a id='msgdel" + msgList[i].MessageId + "' onclick=\"MsgDelete('" + msgList[i].MessageId + "')\" class='fright button smallbutton' style='cursor:pointer;'>Delete</a></div>");
                            builder.Append("<p>" + msgList[i].Description + "</p></div></div></li>");
                        }
                    }
                    builder.Append("</ul>");


                    if (msgList.Count > 6)
                        builder.Append("<div class='openclose_collapsible'> <a id='openAll' title='Open All'>" + (msgList.Count - 5) + " Message</a></div>");



                }

                strarr[0] = builder.ToString();
                strarr[1] = builder_1.ToString();
                return Json(strarr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(strarr, JsonRequestBehavior.AllowGet);
            }
        }

        // For Delete Message
        [WebMethod()]
        public JsonResult _MsgDelete(string MessageId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            messageBLL.delete(Convert.ToInt32(MessageId));
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        // For Delete All Message
        [WebMethod]
        public JsonResult _DeleteAllConversation(string parentId)
        {
            int msgId = Convert.ToInt32(parentId);
            MessageBLL msgbll = new MessageBLL();
            Message msgDel = msgbll.Get(msgId);
            if (msgDel != null)
            {
                List<Message> msgDelList = msgbll.GetAll().Where(x => x.ParentId == msgDel.MessageId || x.MessageId == msgDel.MessageId).ToList();

                foreach (Message mDel in msgDelList)
                {
                    msgbll.delete(mDel.MessageId);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF Student DATA TO LIST
        [WebMethod()]
        public JsonResult _AppendAndDisplayStudentList()
        {
            StringBuilder sb = new StringBuilder();

            List<Student> tmpStudentList = new List<Student>();
            if (staticStudentList != null)
            {
                if (staticStudentList.Count != 0)
                {
                    if (staticStudentList.Count < 15)
                    {
                        tmpStudentList = staticStudentList.ToList();
                        staticStudentList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpStudentList = staticStudentList.GetRange(0, 15);
                        staticStudentList.RemoveRange(0, 15);
                    }

                    // string img;
                    for (int i = 0; i < tmpStudentList.Count; i++)
                    {
                        sb.Append("<tr><td style='width: 4%'><input id='chkSelect' class='chkSelectClass' type='checkbox' value='" + tmpStudentList[i].UserName + "' /></td>");
                        sb.Append("<td style='width: 12%'><a id='" + tmpStudentList[i].UserName + "' onclick=\"javascript:OpenFullInfoPopup('" + tmpStudentList[i].UserName + "')\" href='javascript:void(0);'>" + tmpStudentList[i].UserName + "</a></td>");
                        sb.Append("<td style='width: 10%'>" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "</td>");
                        sb.Append("<td style='width: 10%'><a href='/AdminSec/AddStudent?id=" + tmpStudentList[i].UserName + "'>Edit</a></td>");
                        if (tmpStudentList[i].User != null)
                            sb.Append("<td style='width: 7%'><input id='chkStatus' class='chkStatusClass' type='checkbox' name='chkStatus' checked='" + tmpStudentList[i].User.IsActive + "' value='" + tmpStudentList[i].UserName + "' /></td>");
                        //sb.Append("<td style='width: 10%'><a onclick=\"javascript:OpenMsgPopup('" + tmpStudentList[i].FirstName + " " + tmpStudentList[i].LastName + "','" + tmpStudentList[i].UserName + "')\" href='javascript:void(0);'>Message</a></td>");
                        sb.Append("<td style='width: 10%'><a href='/AdminSec/MessageDetail/" + tmpStudentList[i].StudentId + "'>Message</a></td>");
                        sb.Append("<td style='width: 5%'><a id='lnkBtnDelete" + tmpStudentList[i].StudentId + "' href=\"javascript:StudentDelete('" + tmpStudentList[i].StudentId + "')\">Delete</a></td></tr>");
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


        //For Full information of University
        [WebMethod()]
        public JsonResult _GetUniversityData(string UniversityUserName)
        {
            //string str=string.Empty;
            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            StringBuilder sb = new StringBuilder();
            string[] strArr = new string[24];
            string[] tmp;
            string[] tmpCourseArr;
            university = universityBll.GetByUserName(UniversityUserName);
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
                    strArr[23] = university.ContactNo;

                if (university.ApplicationFee != null)
                {
                    tmp = university.ApplicationFee.ToString().Split('/');
                    strArr[22] = tmp[0] + '-' + EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)Convert.ToInt32(tmp[1])).Split('(')[1].TrimEnd(')');
                }
                return Json(strArr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(strArr, JsonRequestBehavior.AllowGet);
            }
        }

        //For Getting Student (University And Student) Conversation
        private static string getStudentListtag(List<Student> student)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < student.Count; i++)
            {
                sb.Append("<li>");
                sb.Append("<div class='user_img'>");
                if (student[i].Photo == null)
                    sb.Append("<img src='../Images/no_image.jpg' />");
                else
                    sb.Append("<img src='../Images/Profile/" + student[i].Photo + "' />");

                sb.Append("</div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<label>");
                sb.Append("<a id='" + student[i].UserName + "' class='msg_conversation_open_click'>");
                sb.Append(student[i].FirstName + " " + student[i].LastName);
                sb.Append("</a>");
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            return sb.ToString();
        }
        [WebMethod()]
        public JsonResult _retriveStudentList(string university)
        {
            List<Student> tmpList = new List<Student>();
            staticStudentList = new List<Student>();


            StudentBLL studentBll = new StudentBLL();
            StudentInterestBLL studentInterest = new StudentInterestBLL();
            MessageBLL msgbll = new MessageBLL();
            List<Message> tmpmsgList = new List<Message>();
            tmpmsgList = msgbll.GetAll();
            string[] recentMsgUser = tmpmsgList
                                          .Where(
                                           x => (x.ToUserName == university)
                                               &&
                                               !(x.FromUserName.Contains(CookieHelper.Username))).OrderBy(x => x.MessageId).Select(y => y.FromUserName).ToArray();



            string[] usernames = studentInterest.GetAll().Where(x => x.UniversityUserName == university && x.Approved == 2).Select(y => y.StudentUserName).ToArray();

            usernames = recentMsgUser.Concat(usernames).Distinct().ToArray();


            List<Student> tmpstud = new List<Student>();
            tmpstud = studentBll.GetAll();
            Student stmp = new Student();

            foreach (string user in usernames)
            {
                stmp = tmpstud.Where(x => x.UserName == user).FirstOrDefault();
                if (stmp != null)
                    staticStudentList.Add(stmp);
            }
            //staticStudentList = tmpstud.Where(x => usernames.Contains(x.UserName)).ToList();

            if (staticStudentList.Count == 0)
                return Json("no", JsonRequestBehavior.AllowGet);

            //if (staticStudentList.Count < 8)
            //{
            //    tmpList = staticStudentList.ToList();
            //    staticStudentList.RemoveAll(x => 1 == 1);
            //}
            //else
            //{
            //    tmpList = staticStudentList.GetRange(0, 8);
            //    staticStudentList.RemoveRange(0, 8);
            //}
            tmpList = staticStudentList.ToList();
            // return getStudentListtag(tmpList);
            return Json(getStudentListtag(tmpList), JsonRequestBehavior.AllowGet);
        }
        [WebMethod()]
        public JsonResult _retriveConversationBetweenUni(string studentusername, string university)
        {
            MessageBLL msgBll = new MessageBLL();

            List<Message> msgList = msgBll.GetAll()
                                          .Where(
                                           x => (x.ToUserName == studentusername && x.FromUserName == university && x.IsApproved == true) ||
                                           (x.FromUserName == studentusername && x.ToUserName == university && x.IsApproved == true)).OrderBy(x => x.MessageId).ToList();

            StringBuilder sb = new StringBuilder();
            UniversityBLL uniBll = new UniversityBLL();
            StudentBLL studBll = new StudentBLL();

            List<University> universityList = uniBll.GetAll();
            List<Student> studList = studBll.GetAll();
            Student tmpStudent = new Student();
            University tmpUni = new University();



            string date = "", name = "";
            for (int i = 0; i < msgList.Count; i++)
            {
                if (studList.Select(x => x.UserName).ToArray().Contains(msgList[i].FromUserName))
                {
                    tmpStudent = studList.Where(x => x.UserName == msgList[i].FromUserName).FirstOrDefault();
                    if (tmpStudent != null)
                    {
                        name = tmpStudent.FirstName + " " + tmpStudent.LastName;
                    }
                }
                else
                {
                    tmpUni = universityList.Where(x => x.UserName == msgList[i].FromUserName).FirstOrDefault();
                    if (tmpUni != null)
                    {
                        name = tmpUni.UniversityName;
                    }
                }

                sb.Append("<li>");
                sb.Append("<div class='user_name'>");
                sb.Append("<label>");
                sb.Append("<a>");
                sb.Append(name + " says:");
                sb.Append("</a>");
                sb.Append("</label>");
                sb.Append("<i>");
                sb.Append(msgList[i].CreatedDate.ToString("dd/MM/yyyy"));
                sb.Append("</i>");
                sb.Append("<br/><br/><p>");
                sb.Append(msgList[i].Description);
                sb.Append("</p>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            //return sb.ToString();
            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }

        //For Sending Message
        [WebMethod()]
        public JsonResult _SendMessage(string Title, string Description, string sendToUserName, string ParentId)
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
            msg.IsApproved = true;
            msg.Flag = true;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);

            AlertBLL alertBLL = new AlertBLL();
            Alert alert = new Alert();
            alert.Message = "Message received from Admin";
            alert.CreatedDate = DateTime.Now;
            alert.CreatedBy = CookieHelper.Username;
            alert.UserName = sendToUserName;
            alertBLL.Save(alert);

            //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
            StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendMessageEmail.htm")));
            emailBody.Replace(EmailConstantHelper.MessageTitle, Title);
            emailBody.Replace(EmailConstantHelper.MessageDescription, Description);
            MailHelper.sendMail(sendToUserName, "Received New Message From Admin : SpotCollege", emailBody.ToString());

            //StringBuilder mailmsg = new StringBuilder(string.Empty);
            //mailmsg.Append("<h3>You Received New Message from Admin</h3>");
            //mailmsg.Append("<br/><b>Message Title :</b>" + Title);
            //mailmsg.Append("<br/><b>Message Description :</b>" + Description);
            //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
            //MailHelper.sendMail(sendToUserName, "Received Message : SpotCollege", mailmsg.ToString());

            return Json(msg.MessageId.ToString(), JsonRequestBehavior.AllowGet);
        }

        //For Delete University Record
        [WebMethod()]
        public JsonResult _UniversityDelete(string UniversityId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            UniversityBLL universityBll = new UniversityBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            AlertBLL alertBLL = new AlertBLL();
            UserBLL userBll = new UserBLL();
            ViewProfileBLL viewProfileBLL = new ViewProfileBLL();

            if (!string.IsNullOrEmpty(UniversityId))
            {
                staticFinalResultUniversity.RemoveAll(m => m.UniversityId == Convert.ToInt32(UniversityId));
                University univ = universityBll.Get(Convert.ToInt32(UniversityId));
                string usernamedel = univ.UserName;

                List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == usernamedel).ToList();
                foreach (var lia in studentInterest)
                {
                    studentInterestBLL.delete(lia.StudentInterestId);
                }

                List<ViewProfile> viewProfile = viewProfileBLL.GetAll().Where(x => x.UniversityUsername == usernamedel).ToList();
                foreach (var liv in viewProfile)
                {
                    viewProfileBLL.delete(liv.ViewId);
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



                List<Alert> Alert = alertBLL.GetAll().Where(x => x.CreatedBy == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }

                Alert = alertBLL.GetAll().Where(x => x.UserName == usernamedel).ToList();
                foreach (var liaa in Alert)
                {
                    alertBLL.delete(liaa.AlertId);
                }

                if (universityBll.delete(Convert.ToInt32(UniversityId)))
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

        [WebMethod()]
        public JsonResult _SelectedUniversityDelete(string users)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            UniversityBLL universityBll = new UniversityBLL();
            StudentInterestBLL studentInterestBLL = new StudentInterestBLL();
            AlertBLL alertBLL = new AlertBLL();
            UserBLL userBll = new UserBLL();
            ViewProfileBLL viewProfileBLL = new ViewProfileBLL();

            string[] strarr = users.Split(',');
            string u1 = "";
            for (int i = 0; i < strarr.Length; i++)
            {
                u1 = strarr[i];


                if (!string.IsNullOrEmpty(u1))
                {
                    int UniversityId = universityBll.GetByUserName(u1).UniversityId;
                    staticFinalResultUniversity.RemoveAll(m => m.UniversityId == UniversityId);
                    #region RgnUniversityRecordDelete
                    if (UniversityId != null && UniversityId != 0)
                    {

                        University univ = universityBll.Get(Convert.ToInt32(UniversityId));
                        string usernamedel = univ.UserName;

                        List<StudentInterest> studentInterest = studentInterestBLL.GetAll().Where(x => x.UniversityUserName == usernamedel).ToList();
                        foreach (var lia in studentInterest)
                        {
                            studentInterestBLL.delete(lia.StudentInterestId);
                        }

                        List<ViewProfile> viewProfile = viewProfileBLL.GetAll().Where(x => x.UniversityUsername == usernamedel).ToList();
                        foreach (var liv in viewProfile)
                        {
                            viewProfileBLL.delete(liv.ViewId);
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



                        List<Alert> Alert = alertBLL.GetAll().Where(x => x.CreatedBy == usernamedel).ToList();
                        foreach (var liaa in Alert)
                        {
                            alertBLL.delete(liaa.AlertId);
                        }

                        Alert = alertBLL.GetAll().Where(x => x.UserName == usernamedel).ToList();
                        foreach (var liaa in Alert)
                        {
                            alertBLL.delete(liaa.AlertId);
                        }

                        if (universityBll.delete(Convert.ToInt32(UniversityId)))
                        {
                            userBll.delete(usernamedel);
                        }
                        else
                        {
                            return Json(str, JsonRequestBehavior.AllowGet);
                        }
                    }
                    #endregion
                }
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF University DATA TO LIST (for message center scrolling)
        [WebMethod()]
        public JsonResult _AppendAndDisplayUniversityList()
        {
            StringBuilder sb = new StringBuilder();

            List<University> tmpUniversityList = new List<University>();
            if (staticUniversityList != null)
            {
                if (staticUniversityList.Count != 0)
                {
                    if (staticUniversityList.Count < 15)
                    {
                        tmpUniversityList = staticUniversityList.ToList();
                        staticUniversityList.RemoveAll(x => 1 == 1);
                    }
                    else
                    {
                        tmpUniversityList = staticUniversityList.GetRange(0, 15);
                        staticUniversityList.RemoveRange(0, 15);
                    }

                    // string img;
                    for (int i = 0; i < tmpUniversityList.Count; i++)
                    {
                        sb.Append("<tr><td style='width: 4%'><input id='chkSelect' class='chkSelectClass' type='checkbox' value='" + tmpUniversityList[i].UserName + "' /></td>");
                        sb.Append("<td style='width: 10%'><a id='" + tmpUniversityList[i].UserName + "' onclick=\"javascript:OpenFullInfoPopup('" + tmpUniversityList[i].UserName + "')\" href='javascript:void(0);'>" + tmpUniversityList[i].UserName + "</a></td>");
                        sb.Append("<td style='width: 10%'>" + tmpUniversityList[i].UniversityName + "</td>");
                        sb.Append("<td style='width: 10%'>" + tmpUniversityList[i].AdminName + "</td>");
                        if (tmpUniversityList[i].User != null)
                        {
                            if (tmpUniversityList[i].User.IsActive == true)
                                sb.Append("<td style='width: 6%'><input id='chkStatus' class='chkStatusClass' type='checkbox' name='chkStatus' checked='checked' value='" + tmpUniversityList[i].UserName + "' /></td>");
                            else
                                sb.Append("<td style='width: 6%'><input id='chkStatus' class='chkStatusClass' type='checkbox' name='chkStatus' value='" + tmpUniversityList[i].UserName + "' /></td>");
                        }
                        sb.Append("<td style='width: 5%'><a href='/AdminSec/AddUniversity?id=" + tmpUniversityList[i].UserName + "'>Edit</a></td>");
                        sb.Append("<td style='width: 7%'><a onclick=\"javascript:openStudentDetail('" + tmpUniversityList[i].UserName + "')\" href='javascript:void(0);'>Student</a></td>");
                        //sb.Append("<td style='width: 7%'><a onclick=\"javascript:OpenMsgPopup('" + tmpUniversityList[i].UniversityName + "','" + tmpUniversityList[i].UserName + "')\" href='javascript:void(0);'>Message</a></td>");
                        sb.Append("<td style='width: 8%'><a  href='/AdminSec/MessageDetails/" + tmpUniversityList[i].UniversityId + "'>Message</a></td>");
                        sb.Append("<td style='width: 6%'><a id='lnkBtnDelete" + tmpUniversityList[i].UniversityId + "' href=\"javascript:UniversityDelete('" + tmpUniversityList[i].UniversityId + "')\">Delete</a></td></tr>");
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

        //Method for Message Center
        [WebMethod]
        public JsonResult _appendToUniversityListMessageCenter()
        {
            List<University> tmpUniversity = new List<University>();
            StringBuilder sb = new StringBuilder();

            MessageBLL messageBLL = new MessageBLL();

            if (staticUniList.Count == 0)
                return Json("no", JsonRequestBehavior.AllowGet);

            if (staticUniList.Count < msgToDisplay)
            {
                tmpUniversity = staticUniList.ToList();
                staticUniList.RemoveAll(x => 1 == 1);
            }
            else
            {
                tmpUniversity = staticUniList.Take(msgToDisplay).ToList();
                staticUniList.RemoveRange(0, msgToDisplay);
            }
            string img;
            List<Message> tmpNofmsg = messageBLL.GetAll();
            for (int i = 0; i < tmpUniversity.Count; i++)
            {
                List<Message> NoOfUnApprovedMsgList = tmpNofmsg.Where(x => x.ToUserName == tmpUniversity[i].UserName && x.IsApproved == false).ToList(); //  || x.FromUserName == tmpUniversity[i].UserName && x.IsApproved == false


                if (tmpUniversity[i].Image != null)
                    img = "..//Images//Profile//" + tmpUniversity[i].Image;
                else
                    img = "..//Images//no_image.jpg";
                sb.Append("<span>");
                sb.Append("<li>");
                sb.Append("<div class='user_img'><img src='" + img + "' /></div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<a onclick=\"javascript:openUnApprovesMsgs('" + tmpUniversity[i].UserName + "')\" style='cursor:pointer'>" + tmpUniversity[i].UniversityName + "</a>");
                sb.Append("<span class='user_count_mg img-polaroid'>" + NoOfUnApprovedMsgList.Count + "</span>");
                sb.Append("</div>");
                sb.Append("</li>");
                sb.Append("</span><br/>");
            }
            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF Student DATA TO LIST (for message center scrolling)
        [WebMethod]
        public JsonResult _appendToStudentListMessageCenter()
        {
            List<Student> tmpStudent = new List<Student>();
            StringBuilder sb = new StringBuilder();
            MessageBLL messageBLL = new MessageBLL();

            if (staticStudList.Count == 0)
                return Json("no", JsonRequestBehavior.AllowGet);

            if (staticStudList.Count < msgToDisplay)
            {
                tmpStudent = staticStudList.ToList();
                staticStudList.RemoveAll(x => 1 == 1);
            }
            else
            {
                tmpStudent = staticStudList.Take(msgToDisplay).ToList();
                staticStudList.RemoveRange(0, msgToDisplay);
            }
            List<Message> msgtmp = messageBLL.GetAll();
            string img;
            for (int i = 0; i < tmpStudent.Count; i++)
            {
                List<Message> NoOfUnApprovedMsgList = msgtmp.Where(x => x.ToUserName == tmpStudent[i].UserName && x.IsApproved == false).ToList(); //  || x.FromUserName == tmpStudent[i].UserName && x.IsApproved == false

                if (tmpStudent[i].Photo != null)
                    img = "..//Images//Profile//" + tmpStudent[i].Photo;
                else
                    img = "..//Images//no_image.jpg";
                sb.Append("<span>");
                sb.Append("<li>");
                sb.Append("<div class='user_img'><img src='" + img + "' /></div>");
                sb.Append("<div class='user_name'>");
                //sb.Append("<a onclick=\"javascript:openUnApprovesMsgs('" + tmpStudent[i].UserName + "')\" style='cursor:pointer'>" + tmpStudent[i].FirstName + " " + tmpStudent[i].LastName + "<span class='user_count_mg img-polaroid'>" + NoOfUnApprovedMsgList.Count + "</span></a>");
                sb.Append("<a onclick=\"javascript:openUnApprovesMsgs('" + tmpStudent[i].UserName + "')\" style='cursor:pointer'>" + tmpStudent[i].FirstName + " " + tmpStudent[i].LastName + "</a>");
                sb.Append("<span class='user_count_mg img-polaroid'>" + NoOfUnApprovedMsgList.Count + "</span>");
                sb.Append("</div>");
                sb.Append("</li>");
                sb.Append("</span><br/>");
            }
            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }

        //for displaying unapproved message list of students and universities
        static List<Message> staticMessageList = null;
        [WebMethod]
        public JsonResult _displayUnapprovedMessage(string username)
        {
            StringBuilder sb = new StringBuilder();
            MessageBLL msgBll = new MessageBLL();
            List<Message> mli = msgBll.GetAll().Where(x => x.FromUserName == username && x.ToUserName != CookieHelper.Username && x.IsApproved == false).ToList(); //  || x.FromUserName == username && x.ToUserName != CookieHelper.Username && x.IsApproved == false
            StudentBLL studentBll = new StudentBLL();
            UniversityBLL universityBll = new UniversityBLL();

            List<Student> tmpStud = studentBll.GetAll();
            List<University> tmpUniList = universityBll.GetAll();

            staticMessageList = mli.ToList();
            if (mli.Count < 10)
                staticMessageList.RemoveAll(x => 1 == 1);
            else
                staticMessageList.RemoveRange(0, 10);

            //if (mli.Count > 10)
            //{
            //    staticMessageList = mli.GetRange(10, mli.Count);
            //}

            string img = "", name = "";

            int cnt = 0;
            if (mli.Count < 10)
                cnt = mli.Count;
            else
                cnt = 10;


            for (int i = 0; i < cnt; i++)
            {
                Student std = tmpStud.Where(x => x.UserName == mli[i].ToUserName).FirstOrDefault();
                if (std != null)
                {
                    if (std.Photo != null)
                    {
                        img = "/Images/Profile/" + std.Photo;
                    }
                    else
                    {
                        img = "/Images/no_image.jpg";
                    }

                    name = std.FirstName.ToString() + " " + std.LastName.ToString();
                }
                else
                {
                    University Uni = tmpUniList.Where(x => x.UserName == mli[i].ToUserName).FirstOrDefault();
                    if (Uni != null)
                    {
                        if (Uni.Image != null)
                        {
                            img = "/Images/Profile/" + Uni.Image;
                        }
                        else
                        {
                            img = "/Images/no_image.jpg";
                        }
                        name = Uni.UniversityName.ToString();
                    }
                }

                sb.Append("<span><li>");
                sb.Append("<div class='user_img'><img class='img-polaroid' src='" + img + "' /></div>");
                sb.Append("<div class='user_name'>");
                sb.Append("<div class='width100per'>");
                sb.Append("<h4>" + name + "</h4>");
                sb.Append("<h5 class='fright'>");
                sb.Append("<span>" + mli[i].CreatedDate.ToString("dd/MM/yyyy") + "</span><br />");
                sb.Append("");
                sb.Append("</h5>");
                sb.Append("</div>");
                sb.Append("<div class='width100per'>");
                sb.Append(mli[i].Description);
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='user_msg'>");
                sb.Append("</div>");

                sb.Append("<div class='width100per'>");
                sb.Append("<div class='fleft'>");

                sb.Append("<a id='msgApproved" + mli[i].MessageId + "' href=\"javascript:MsgApproving('" + mli[i].MessageId + "')\" class='button'>Approved</a>");
                sb.Append("</div>");

                sb.Append("<div class='fright'>");
                sb.Append("<a id='msgRejectwithmsg" + mli[i].MessageId + "' href=\"javascript:MsgDialogOpen('" + name + "','" + mli[i].MessageId + "')\" class='button'>Reject With Message</a>&nbsp;&nbsp;");
                sb.Append("<a id='msgReject" + mli[i].MessageId + "' href=\"javascript:MsgDelete('" + mli[i].MessageId + "')\" class='button'>Reject</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</li></span><br/>");
            }
            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);

        }

        //METHOD USED TO APPEND SPECIFIC RANGE OF unapproved Message DATA TO LIST (for message center scrolling)
        [WebMethod]
        public JsonResult _appendToMessageList()
        {
            List<Message> tmpMessage = new List<Message>();
            StringBuilder sb = new StringBuilder();
            StudentBLL studentBll = new StudentBLL();
            UniversityBLL universityBll = new UniversityBLL();

            List<Student> tmpStud = studentBll.GetAll();
            List<University> tmpUniList = universityBll.GetAll();

            string img = "", name = "";
            if (staticMessageList != null)
            {
                if (staticMessageList.Count == 0)
                    return Json("no", JsonRequestBehavior.AllowGet);

                if (staticMessageList.Count < 10)
                {
                    tmpMessage = staticMessageList.ToList();
                    staticMessageList.RemoveAll(x => 1 == 1);
                }
                else
                {
                    tmpMessage = staticMessageList.GetRange(0, 10);
                    staticMessageList.RemoveRange(0, 10);
                }
                for (int i = 0; i < tmpMessage.Count; i++)
                {
                    Student std = tmpStud.Where(x => x.UserName == tmpMessage[i].ToUserName).FirstOrDefault();
                    if (std != null)
                    {
                        if (std.Photo != null)
                        {
                            img = "/Images/Profile/" + std.Photo;
                        }
                        else
                        {
                            img = "/Images/no_image.jpg";
                        }

                        name = std.FirstName.ToString() + " " + std.LastName.ToString();
                    }
                    else
                    {
                        University Uni = tmpUniList.Where(x => x.UserName == tmpMessage[i].ToUserName).FirstOrDefault();
                        if (Uni != null)
                        {
                            if (Uni.Image != null)
                            {
                                img = "/Images/Profile/" + Uni.Image;
                            }
                            else
                            {
                                img = "/Images/no_image.jpg";
                            }
                            name = Uni.UniversityName.ToString();
                        }
                    }

                    sb.Append("<span><li>");
                    sb.Append("<div class='user_img'><img class='img-polaroid' src='" + img + "' /></div>");
                    sb.Append("<div class='user_name'>");
                    sb.Append("<div class='width100per'>");
                    sb.Append("<h4>" + name + "</h4>");
                    sb.Append("<h5 class='fright'>");
                    sb.Append("<span>" + tmpMessage[i].CreatedDate.ToString("dd/MM/yyyy") + "</span><br />");
                    sb.Append("");
                    sb.Append("</h5>");
                    sb.Append("</div>");
                    sb.Append("<div class='width100per'>");
                    sb.Append(tmpMessage[i].Description);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("<div class='user_msg'>");
                    sb.Append("</div>");

                    sb.Append("<div class='width100per'>");
                    sb.Append("<div class='fleft'>");

                    sb.Append("<a id='msgApproved" + tmpMessage[i].MessageId + "' href=\"javascript:MsgApproving('" + tmpMessage[i].MessageId + "')\" class='button'>Approved</a>");
                    sb.Append("</div>");

                    sb.Append("<div class='fright'>");
                    sb.Append("<a id='msgRejectwithmsg" + tmpMessage[i].MessageId + "' href=\"javascript:MsgDialogOpen('" + name + "','" + tmpMessage[i].MessageId + "')\" class='button'>Reject With Message</a>&nbsp;&nbsp;");
                    sb.Append("<a id='msgReject" + tmpMessage[i].MessageId + "' href=\"javascript:MsgDelete('" + tmpMessage[i].MessageId + "')\" class='button'>Reject</a>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</li></span><br/>");
                }

                return Json(sb.ToString(), JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(" ", JsonRequestBehavior.AllowGet);

            }
        }

        //for updating the approved message
        [WebMethod()]
        public JsonResult _SaveApproving(string MessageId)
        {
            UserBLL userBLL = new UserBLL();
            MessageBLL messageBLL = new MessageBLL();
            SettingBLL settingBLL = new SettingBLL();
            Settings comSettings = new Settings();
            string str = string.Empty;
            if (!string.IsNullOrEmpty(MessageId))
            {
                Message mRead = messageBLL.Get(Convert.ToInt32(MessageId));
                if (mRead != null)
                {
                    mRead.MessageId = mRead.MessageId;
                    mRead.Title = mRead.Title;
                    mRead.Description = mRead.Description;
                    mRead.ParentId = mRead.ParentId;
                    mRead.FromUserName = mRead.FromUserName;
                    mRead.ToUserName = mRead.ToUserName;
                    mRead.IsRead = false;
                    mRead.IsApproved = true;
                    mRead.Flag = true;
                    mRead.CreatedDate = mRead.CreatedDate;
                    messageBLL.Save(mRead);

                    string firstname = studentBll.GetByUserName(mRead.ToUserName).FirstName;
                    Student std = studentBll.GetByUserName(mRead.FromUserName);
                    string name = std.FirstName + " " + std.LastName;

                    int usType = userBLL.Get(mRead.FromUserName).LoginTypeId;
                    if (usType == 2)
                    {
                        AlertBLL alertBLL = new AlertBLL();
                        UniversityBLL universityBLL = new UniversityBLL();
                        string universityname = universityBLL.GetByUserName(mRead.FromUserName).UniversityName.ToString();
                        Alert alert = new Alert();
                        alert.Message = "Message received from " + universityname;
                        alert.CreatedDate = DateTime.Now;
                        alert.CreatedBy = mRead.FromUserName;
                        alert.UserName = mRead.ToUserName;
                        alertBLL.Save(alert);

                        comSettings = settingBLL.GetByUsername(mRead.ToUserName);
                        if (comSettings != null)
                        {
                            //bool sendMailUni = settingBLL.GetByUsername(mRead.ToUserName).UniversityEmail;
                            if (comSettings.UniversityEmail == true)
                            {

                                //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
                                StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendMessageEmail.htm")));
                                emailBody.Replace(EmailConstantHelper.MessageTitle, mRead.Title);
                                emailBody.Replace(EmailConstantHelper.MessageDescription, mRead.Description);
                                MailHelper.sendMail(mRead.ToUserName, "Received New Message : SpotCollege", emailBody.ToString());

                                //StringBuilder mailmsg = new StringBuilder(string.Empty);
                                //mailmsg.Append("<h3>You Received New Message</h3>");
                                //mailmsg.Append("<br/><b>Message Title :</b>" + mRead.Title);
                                //mailmsg.Append("<br/><b>Message Description :</b>" + mRead.Description);
                                //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                                //MailHelper.sendMail(mRead.ToUserName, "Received Message : SpotCollege", mailmsg.ToString());
                            }
                        }

                    }
                    else
                    {
                        comSettings = settingBLL.GetByUsername(mRead.ToUserName);
                        if (comSettings != null)
                        {
                            //  bool sendMailStud = settingBLL.GetByUsername(mRead.ToUserName).StudentsEmail;
                            if (comSettings.StudentsEmail == true)
                            {
                                //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
                                StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/NewMessageMail.htm")));
                                emailBody.Replace(EmailConstantHelper.Name, name);
                                emailBody.Replace(EmailConstantHelper.FirstName, firstname);
                                emailBody.Replace(EmailConstantHelper.MessageTitle, mRead.Title);
                                emailBody.Replace(EmailConstantHelper.MessageDescription, mRead.Description);
                                MailHelper.sendMail(mRead.ToUserName, "Received New Message :SpotCollege", emailBody.ToString());

                                //StringBuilder mailmsg = new StringBuilder(string.Empty);
                                //mailmsg.Append("<h3>You Received New Message</h3>");
                                //mailmsg.Append("<br/><b>Message Title :</b>" + mRead.Title);
                                //mailmsg.Append("<br/><b>Message Description :</b>" + mRead.Description);
                                //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                                //MailHelper.sendMail(mRead.ToUserName, "Received Message : SpotCollege", mailmsg.ToString());
                            }
                        }
                    }
                }
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        //for delete message
        [WebMethod()]
        public JsonResult _MsgDeleteCenter(string MessageId)
        {
            string str = string.Empty;
            Message msg = new Message();
            MessageBLL messageBLL = new MessageBLL();
            Message message = messageBLL.Get(Convert.ToInt32(MessageId));
            msg.Title = message.Title;
            msg.Description = message.Description + " was Rejected By Admin";
            // msg.ParentId = message.ParentId; // its used when Rejected message showing in between series of message
            msg.ParentId = 0;
            msg.FromUserName = CookieHelper.Username;
            msg.ToUserName = message.FromUserName;
            msg.IsRead = false;
            msg.IsApproved = true;
            msg.Flag = true;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);

            AlertBLL alertBLL = new AlertBLL();
            Alert alert = new Alert();
            alert.Message = "Rejected Message received from Admin";
            alert.CreatedDate = DateTime.Now;
            alert.CreatedBy = CookieHelper.Username;
            alert.UserName = message.FromUserName;
            alertBLL.Save(alert);

            //StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
            StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendMessageEmail.htm")));
            emailBody.Replace(EmailConstantHelper.MessageTitle, message.Title);
            emailBody.Replace(EmailConstantHelper.MessageDescription, message.Description);
            MailHelper.sendMail(message.FromUserName, "Received Rejected Message : SpotCollege", emailBody.ToString());

            //StringBuilder mailmsg = new StringBuilder(string.Empty);
            //mailmsg.Append("<h3>You Received Rejected Message</h3>");
            //mailmsg.Append("<br/><b>Message Title :</b>" + message.Title);
            //mailmsg.Append("<br/><b>Message Description :</b>" + message.Description);
            //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
            //MailHelper.sendMail(message.FromUserName, "Received Message : SpotCollege", mailmsg.ToString());

            int parent = message.ParentId;

            if (parent == 0)
            {
                List<Message> msgLi = messageBLL.GetAll().Where(x => x.MessageId == Convert.ToInt32(MessageId) || x.ParentId == Convert.ToInt32(MessageId)).ToList();
                foreach (var li in msgLi)
                {
                    messageBLL.delete(li.MessageId);
                }
            }
            else
            {
                messageBLL.delete(Convert.ToInt32(MessageId));
            }

            return Json(str, JsonRequestBehavior.AllowGet);

        }

        //for sending message to user with rejected message 
        [WebMethod()]
        public JsonResult _SendMessageRejected(string Title, string Description, string ParentId, string MessageId)
        {
            string str = string.Empty;
            MessageBLL messageBLL = new MessageBLL();
            SpotCollege.DAL.Message msg = new SpotCollege.DAL.Message();
            Message message = messageBLL.Get(Convert.ToInt32(MessageId));
            //msg.Title = Title;
            msg.Title = message.Title;
            msg.Description = message.Description + " was Rejected By Admin because " + Description;
            msg.ParentId = Convert.ToInt32(ParentId);
            // msg.ParentId = message.ParentId;
            msg.FromUserName = CookieHelper.Username;
            msg.ToUserName = message.FromUserName;
            msg.IsRead = false;
            msg.IsApproved = true;
            msg.Flag = true;
            msg.CreatedDate = DateTime.Now;
            messageBLL.Save(msg);


            messageBLL.delete(Convert.ToInt32(MessageId));

            AlertBLL alertBLL = new AlertBLL();
            Alert alert = new Alert();
            alert.Message = "Rejected Message received from Admin";
            alert.CreatedDate = DateTime.Now;
            alert.CreatedBy = CookieHelper.Username;
            alert.UserName = message.FromUserName;
            alertBLL.Save(alert);

            // StringBuilder emailBody = new StringBuilder(CommonHelper.ReadEmailTextFile("SendMessageEmail.htm"));
            StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServerPath"] + "/EmailTextFile/SendMessageEmail.htm")));
            emailBody.Replace(EmailConstantHelper.MessageTitle, Title);
            emailBody.Replace(EmailConstantHelper.MessageDescription, Description);
            MailHelper.sendMail(message.FromUserName, "Received Rejected Message : SpotCollege", emailBody.ToString());

            //StringBuilder mailmsg = new StringBuilder(string.Empty);
            //mailmsg.Append("<h3>You Received Rejected Message</h3>");
            //mailmsg.Append("<br/><b>Message Title :</b>" + Title);
            //mailmsg.Append("<br/><b>Message Description :</b>" + Description);
            //mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
            //MailHelper.sendMail(message.FromUserName, "Received Message : SpotCollege", mailmsg.ToString());

            return Json(str, JsonRequestBehavior.AllowGet);

        }

        //this is use for Get all Title of Parent Message for searching the message by Message title
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
                    sb.Append("<span><li class='background-color:" + tmpSerchMessageList[i].NameColor + ";'><div class='user_img'><img src='" + tmpSerchMessageList[i].Photo + "' /></div>");
                    sb.Append("<div class='user_name'><label>" + tmpSerchMessageList[i].Name + "(" + tmpSerchMessageList[i].NoofChildMessage + ")</label><br/>");
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

        //This method is use to delete resources Category
        [WebMethod()]
        public JsonResult _ResourceCategoryDelete(int CategoryId)
        {
            List<Resource> resList = resourceBLL.GetAll().Where(x => x.CategoryId == CategoryId).ToList();
            foreach (var rl in resList)
            {
                resourceBLL.delete(rl.ResourceId);
            }
            resourceCategoryBLL.delete(CategoryId);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        //This method is use to delete resources 
        [WebMethod()]
        public JsonResult _ResourceDelete(int ResourceId)
        {
            string del = string.Empty;
            //if (CookieHelper.UserType == ((int)LoginTypes.SubAdmin).ToString())
            //{
            //    Resource res = resourceBLL.Get(ResourceId);
            //    if (res != null)
            //    {
            //        if (res.CreatedBy != null)
            //        {
            //            if (res.CreatedBy == CookieHelper.Username)
            //            {
            //                resourceBLL.delete(ResourceId);
            //            }
            //            else
            //                del = "no";
            //        }
            //        else
            //            del = "no";
            //    }
            //    else
            //        del = "no";
            //}
            //else
            //{
            //    resourceBLL.delete(ResourceId);
            //}
            resourceBLL.delete(ResourceId);
            return Json(del, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _SubUserDetailDelete(int PermissionId, string Username)
        {
            if (PermissionId != 0)
            {
                List<Resource> ResList = resourceBLL.GetAll().Where(x => x.CreatedBy == Username).ToList();
                if (ResList.Count != 0)
                {
                    foreach (var r in ResList)
                    {
                        resourceBLL.delete(r.ResourceId);
                    }
                }

                permissionBLL.delete(PermissionId);

                userBll.delete(Username);
            }
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }


        [WebMethod()]
        public JsonResult _SurveyDetailDelete(int surveyId)
        {
            if (surveyId != 0)
            {
                surveyBLL.delete(surveyId);
            }
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _getSurveyDetail(int surveyId)
        {
            if (surveyId != 0)
            {
                surveyDetail = surveyBLL.Get(surveyId);

                // if (CookieHelper.UserType == ((int)LoginTypes.Student).ToString())

            }
            return Json(surveyDetail, JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        public JsonResult _GetResourceDescription(string ResourceId)
        {
            string des = string.Empty;
            Resource res = resourceBLL.Get(Convert.ToInt32(ResourceId));
            if (res != null)
            {
                if (res.Description != null)
                    des = res.Description;

                return Json(des, JsonRequestBehavior.AllowGet);
            }
            return Json(des, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
