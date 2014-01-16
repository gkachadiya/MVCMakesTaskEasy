using Newtonsoft.Json;
using SpotCollege.BLL;
using SpotCollege.Common;
using SpotCollege.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Spot_College_MVC.Controllers
{
    public class MethodController : Controller
    {
        #region ACT

        [WebMethod()]
        public JsonResult ACTSave(string TestId, string Composite, string English, string Math, string Reading, string Science, string Writing, string Date, string StudentTestId)
        {
            string str = string.Empty;

            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();


            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId= studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId; 
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId; 
            }


            StudentTest school = null;
            if (StudentTestId != "0")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Composite = Composite;
                stdTest.English = English;
                stdTest.Math = Math;
                stdTest.Reading = Reading;
                stdTest.Science = Science;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 2).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Composite = Composite;
                stdTest.English = English;
                stdTest.Math = Math;
                stdTest.Reading = Reading;
                stdTest.Science = Science;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        [WebMethod()]
        public string GetACT(string StudentTestId)
        {
            StudentTest schoolACT = new StudentTest();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            schoolACT = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

            return getJSON(schoolACT);
        }

        [WebMethod()]
        public string ACTDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return str;
        }

        
        #endregion

        #region SAT
        
        [WebMethod()]
        public JsonResult SATSave(string TestId, string Reading, string Math, string Writing, string Composite, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Composite = Composite;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 3).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Composite = Composite;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        [WebMethod()]
        public string GetSAT(string StudentTestId)
        {
            StudentTest schoolSAT = new StudentTest();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            schoolSAT = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            return getJSON(schoolSAT);
        }

        [WebMethod()]
        public string SATDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return str;
        }
        #endregion

        #region AP

        [WebMethod()]
        public JsonResult APSave(string TestId, string Subject, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();

           
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 4).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        [WebMethod()]
        public string GetAP(string StudentTestId)
        {
            StudentTest schoolAP = new StudentTest();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            schoolAP = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            return getJSON(schoolAP);
        }

        [WebMethod()]
        public string APDelete(string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            studentTestBLL.delete(Convert.ToInt32(StudentTestId));
            return str;
        }

        #endregion

        //this method is use to Get Test Record
        [WebMethod()]
        public string GetTest(string StudentTestId)
        {
            StudentTest schoolTest = new StudentTest();
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            schoolTest = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            return getJSON(schoolTest);
        }

        #region Division Save Test 

        //for Save GRE record
        [WebMethod()]
        public JsonResult GRESave(string TestId, string VerbalReasoning, string QuantitativeReasoning, string AnalyticalWriting, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.VerbalReasoning = VerbalReasoning;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 5).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.VerbalReasoning = VerbalReasoning;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save GMAT record
        [WebMethod()]
        public JsonResult GMATSave(string TestId, string Verbal, string QuantitativeReasoning, string Total, string AnalyticalWriting, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Verbal = Verbal;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.Total = Total;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 6).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Verbal = Verbal;
                stdTest.QuantitativeReasoning = QuantitativeReasoning;
                stdTest.Total = Total;
                stdTest.AnalyticalWriting = AnalyticalWriting;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save IB record
        [WebMethod()]
        public JsonResult IBSave(string TestId, string Subject, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 7).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save IELTS record
        [WebMethod()]
        public JsonResult IELTSSave(string TestId, string Listening, string Reading, string Writing, string Speaking, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Listening = Listening;
                stdTest.Reading = Reading;
                stdTest.Writing = Writing;
                stdTest.Speaking = Speaking;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 8).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Listening = Listening;
                stdTest.Reading = Reading;
                stdTest.Writing = Writing;
                stdTest.Speaking = Speaking;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save LSAT record
        [WebMethod()]
        public JsonResult LSATSave(string TestId, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 9).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save MCAT record
        [WebMethod()]
        public JsonResult MCATSave(string TestId, string Biology, string Physics, string Verbal, string Writing, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Biology = Biology;
                stdTest.Physics = Physics;
                stdTest.Verbal = Verbal;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);
                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 10).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Biology = Biology;
                stdTest.Physics = Physics;
                stdTest.Verbal = Verbal;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);


                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save PSAT record
        [WebMethod()]
        public JsonResult PSATSave(string TestId, string Reading, string Math, string Writing, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 11).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Math = Math;
                stdTest.Writing = Writing;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save SAT-II record
        [WebMethod()]
        public JsonResult SAT2Save(string TestId, string Subject, string Score, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 12).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Subject = Subject;
                stdTest.Score = Score;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save TOEFL Internet based record
        [WebMethod()]
        public JsonResult TOEFLInternetbasedSave(string TestId, string Reading, string Listening, string Speaking, string Writing, string Total, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 13).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);

            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        //for Save TOEFL Paper based record
        [WebMethod()]
        public JsonResult TOEFLPaperbasedSave(string TestId, string Reading, string Listening, string Speaking, string Writing, string Total, string Date, string StudentTestId)
        {
            string str = string.Empty;
            StudentTestBLL studentTestBLL = new StudentTestBLL();
            StudentBLL studentBll = new StudentBLL();
            StudentTest stdTest = new StudentTest();
            int studentId = 0;
            if (CookieHelper.UserType == ((int)LoginTypes.Admin).ToString())
            {
                studentId = studentBll.GetByUserName(CookieHelper.GetCookieValue(CookieKeys.adminStudentId)).StudentId;
            }
            else
            {
                studentId = studentBll.GetByUserName(CookieHelper.Username).StudentId;
            }
            StudentTest school = new StudentTest();
            if (StudentTestId != "")
            {
                school = studentTestBLL.Get(Convert.ToInt32(StudentTestId));
            }
            if (school == null)
            {
                stdTest = new StudentTest();
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest lis = studentTestBLL.GetAll().Where(x => x.StudentId == studentId && x.TestId == 14).OrderByDescending(x => x.StudentTestId).FirstOrDefault();

                String[] arr = new String[4];
                arr[0] = lis.StudentTestId.ToString();
                arr[1] = lis.Date;
                arr[2] = "add";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                stdTest.StudentTestId = Convert.ToInt32(StudentTestId);
                stdTest.StudentId = studentId;
                stdTest.TestId = Convert.ToInt32(TestId);
                stdTest.Reading = Reading;
                stdTest.Listening = Listening;
                stdTest.Speaking = Speaking;
                stdTest.Writing = Writing;
                stdTest.Total = Total;
                stdTest.Date = Date;
                studentTestBLL.Save(stdTest);

                StudentTest sch = studentTestBLL.Get(Convert.ToInt32(StudentTestId));

                String[] arr = new String[4];
                arr[0] = sch.StudentTestId.ToString();
                arr[1] = sch.Date;
                arr[2] = "edit";
                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        // This method is used to get JSON equivalennt of obj
        private string getJSON(object obj)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            string[] proptype = { "String", "Int32" };
            string[] propertyNames = obj.GetType().GetProperties().Where(x => proptype.Contains(x.PropertyType.Name)).Select(p => p.Name).ToArray();


            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();

                foreach (var prop in propertyNames)
                {
                    object propValue = obj.GetType().GetProperty(prop).GetValue(obj, null);
                    writer.WritePropertyName(prop);
                    writer.WriteValue(propValue);
                }
                //writer.WriteStartArray();
                //writer.WriteValue("DVD read/writer");
                //writer.WriteComment("(broken)");
                //writer.WriteValue("500 gigabyte hard drive");
                //writer.WriteValue("200 gigabype hard drive");
                writer.WriteEnd();
            }

            return sw.ToString();
        }
    }

}
