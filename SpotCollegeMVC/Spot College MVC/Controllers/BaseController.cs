using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpotCollege.DAL.DataModels;
using SpotCollege.DAL;
using SpotCollege.Common;

namespace Spot_College_MVC.Controllers
{
    public class BaseController : Controller
    {

        #region Student Mapping's

        //Automapper used to map StudentAcademic type to StudentAcademicModel
        public StudentAcademicModel FixUp(StudentAcademic student)
        {
            StudentAcademicModel std = new StudentAcademicModel();
            AutoMapper.Mapper.CreateMap<StudentAcademic, StudentAcademicModel>();
            AutoMapper.Mapper.Map(student, std);
            return std;
        }

        //Automapper used to map StudentWorkHistory type to StudentWorkHistoryModel
        public StudentWorkHistoryModel FixUp(StudentWorkHistory student)
        {
            StudentWorkHistoryModel std = new StudentWorkHistoryModel();
            AutoMapper.Mapper.CreateMap<StudentWorkHistory, StudentWorkHistoryModel>();
            AutoMapper.Mapper.Map(student, std);
            return std;
        }

        //Automapper used to map Student type to StudentEditModel
        public StudentEditModel FixUp(Student student)
        {
            StudentEditModel std = new StudentEditModel();
            AutoMapper.Mapper.CreateMap<Student, StudentEditModel>();
            AutoMapper.Mapper.Map(student, std);
            return std;
        }

        //Automapper used to map StudentEditModel type to Student
        public Student FixUp(StudentEditModel studentmodel)
        {
            Student student = new Student();

            if (studentmodel.Address1 != null)
                student.Address1 = studentmodel.Address1;
            else
                student.Address1 = "";

            student.Address2 = studentmodel.Address2;
            student.BirthDate = studentmodel.BirthDate;
            student.Citizenship = studentmodel.Citizenship;
            if (studentmodel.Address1 != null)
                student.City = studentmodel.City;
            else
                student.City = "";

            student.Country = studentmodel.Country;
            student.FirstName = studentmodel.FirstName;
            student.LastName = studentmodel.LastName;
            if (studentmodel.MiddleName != null)
                student.MiddleName = studentmodel.MiddleName;
            else
                student.MiddleName = "";
            student.PrimaryEmail = studentmodel.PrimaryEmail;
            student.PrimaryNumber = studentmodel.PrimaryNumber;
            student.PrimaryType = studentmodel.PrimaryType;
            student.SecondaryNumber = studentmodel.SecondaryNumber;
            student.SecondaryType = studentmodel.SecondaryType;
            student.StudentId = studentmodel.StudentId;
            return student;
        } 
        #endregion

        #region University Mapping's
        //Map University Object to UniversityModel object
        public UniversityModel FixUp(University university)
        {
            AutoMapper.Mapper.CreateMap<University, UniversityModel>();
            return AutoMapper.Mapper.Map<UniversityModel>(university);
        }
        #endregion

        //public void convert()
        //{
        //    List<Student> stdlist = new List<Student>();
        //    stdlist = StudentDAL.FetchAll().ToList();
        //    foreach (Student s in stdlist)
        //    {
        //        Random random = new Random();
        //        int randomNumber = random.Next(1, 200);

        //        string name = s.Country.ToString();
        //        s.Country = randomNumber.ToString();
        //        s.LookingForCountry = random.Next(1, 200).ToString();
        //        s.Citizenship = random.Next(1, 200).ToString();

        //        StudentDAL.Update(s);
        //    }
        //}
    }
}
