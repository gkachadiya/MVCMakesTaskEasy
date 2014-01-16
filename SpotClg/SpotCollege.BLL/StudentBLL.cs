using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;
using System.Linq.Expressions;

namespace SpotCollege.BLL
{
    public class StudentBLL
    {
        public List<Student> GetAll()
        {
            //for list
            return StudentDAL.FetchAll().ToList();
        }

        public Student Get(int StudentId)
        {
            //for single record
            return StudentDAL.Get(x => x.StudentId == StudentId);
        }

        public Student GetByUserName(string UserName)
        {
            //for single record
            return StudentDAL.Get(x => x.UserName == UserName);
        }

        public IList<sp_StudentUnApprovedMsg_Result> getUnApprovedMsg()
        {
            return SpotCollegeEntityModel.EntityModel.sp_StudentUnApprovedMsg().ToList();
        }

      
        public void Save(Student user)
        {
            Student entity = Get(user.StudentId);
            if (entity != null)
            {
                //update

                entity.UserName = user.UserName;
                entity.FirstName = user.FirstName;
                entity.MiddleName = user.MiddleName;
                entity.LastName = user.LastName;
                entity.Photo = user.Photo;
                entity.Address1 = user.Address1;
                entity.Address2 = user.Address2;
                entity.City = user.City;
                entity.Country = user.Country;
                entity.PrimaryNumber = user.PrimaryNumber;
                entity.PrimaryType = user.PrimaryType;
                entity.SecondaryNumber = user.SecondaryNumber;
                entity.SecondaryType = user.SecondaryType;
                entity.PrimaryEmail = user.PrimaryEmail;
                entity.BirthDate = user.BirthDate;
                entity.Citizenship = user.Citizenship;
                entity.StudyingIn = user.StudyingIn;
                entity.LookingFor = user.LookingFor;
                entity.LookingForCountry = user.LookingForCountry;
                entity.StartDate = user.StartDate;
                entity.Payout = user.Payout;
                entity.SportActivities = user.SportActivities;
                entity.LeadershipActivies = user.LeadershipActivies;
                entity.OtherActivities = user.OtherActivities;
                StudentDAL.Update(entity);
            }
            else
            {
                //save record
                StudentDAL.Add(user);
            }
        }

        public bool delete(int StudentId)
        {
            Student entity = Get(StudentId);
            if (entity != null)
            {
                //delete
                StudentDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Student> SearchStudents(Expression< Func<Student, bool>> predicate)
        {
            IEnumerable<Student> std = StudentDAL.FetchAll().Where(predicate);
            return std;
        }
    }
}

