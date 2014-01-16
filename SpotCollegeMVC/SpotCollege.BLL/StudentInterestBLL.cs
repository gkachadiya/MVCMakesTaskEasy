using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
    public class StudentInterestBLL
    {
        public List<StudentInterest> GetAll()
        {
            //for list
            return StudentInterestDAL.FetchAll().ToList();
        }

        public StudentInterest Get(int StudentInterestId)
        {
            //for single record
            return StudentInterestDAL.Get(x => x.StudentInterestId == StudentInterestId);
        }



        public void Save(StudentInterest student)
        {
            StudentInterest entity = Get(student.StudentInterestId);
            if (entity != null)
            {                //update


                entity.UniversityUserName = student.UniversityUserName;
                entity.StudentUserName = student.StudentUserName;
                entity.Approved = student.Approved;

                StudentInterestDAL.Update(entity);
            }
            else
            {
                //save record
                StudentInterestDAL.Add(student);
            }
        }

        public bool delete(int StudentAcademicsId)
        {
            StudentInterest entity = Get(StudentAcademicsId);
            if (entity != null)
            {
                //delete
                StudentInterestDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteByUsername(string studentUserName)
        {
            IEnumerable<StudentInterest> entity = GetAll().Where(x => x.StudentUserName == studentUserName);
            foreach (StudentInterest sw in entity)
            {
                //delete
                StudentInterestDAL.Delete(sw);
            }
            return true;
        }

        public bool deleteByUniversityUsername(string studentUserName)
        {
            IEnumerable<StudentInterest> entity = GetAll().Where(x=>x.UniversityUserName == studentUserName);
            foreach (StudentInterest sw in entity)
            {
                //delete
                StudentInterestDAL.Delete(sw);
            }
            return true;
        }

        public StudentInterest GetByuniversityName(string UserName)
        {
            //for single record
            return StudentInterestDAL.Get(x => x.UniversityUserName == UserName);
        }
    }
}
