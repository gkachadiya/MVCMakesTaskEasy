using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
    public class StudentAcademicsBLL
    {
        public List<StudentAcademic> GetAll()
        {
            //for list
            return StudentAcademicsDAL.FetchAll().ToList();
        }

        public StudentAcademic Get(int StudentAcademicsId)
        {
            //for single record
            return StudentAcademicsDAL.Get(x => x.StudentAcademicsId == StudentAcademicsId);
        }

        public void Save(StudentAcademic user)
        {
            StudentAcademic entity = Get(user.StudentAcademicsId);
            if (entity != null)
            {
                //update

                entity.StudentId = user.StudentId;
                entity.SchoolName = user.SchoolName;
                entity.SchoolCity = user.SchoolCity;
                entity.SchoolCountry = user.SchoolCountry;
                entity.Graduate = user.Graduate;
                entity.GraduationYear = user.GraduationYear;
                entity.GraduationLevel = user.GraduationLevel;
                entity.Rank = user.Rank;
                entity.CertificatePath = user.CertificatePath;

                StudentAcademicsDAL.Update(entity);
            }
            else
            {
                //save record
                StudentAcademicsDAL.Add(user);
            }
        }

        public bool delete(int StudentAcademicsId)
        {
            StudentAcademic entity = Get(StudentAcademicsId);
            if (entity != null)
            {
                //delete
                StudentAcademicsDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool deleteByStudentId(int StudentId)
        {
            List<StudentAcademic> entity = StudentAcademicsDAL.FetchAll().Where(x => x.StudentId == StudentId).ToList();
            foreach (StudentAcademic sw in entity)
            {
                //delete
                StudentAcademicsDAL.Delete(sw);
            }
            return true;
        }
    }
}
