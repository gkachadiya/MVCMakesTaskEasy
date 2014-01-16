using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
  public  class StudentWorkHistoryBLL
    {
        public List<StudentWorkHistory> GetAll()
        {
            //for list
            return StudentWorkHistoryDAL.FetchAll().ToList();
        }

        public StudentWorkHistory Get(int StudentWorkHistoryId)
        {
            //for single record
            return StudentWorkHistoryDAL.Get(x => x.StudentWorkHistoryId == StudentWorkHistoryId);
        }

        public void Save(StudentWorkHistory user)
        {
            StudentWorkHistory entity = Get(user.StudentWorkHistoryId);
            if (entity != null)
            {
                //update

                entity.StudentId = user.StudentId;
                entity.CompanyName = user.CompanyName;
                entity.Position = user.Position;
                entity.StartDate = user.StartDate;
                entity.EndDate = user.EndDate;
                entity.Responsibilities = user.Responsibilities;
                StudentWorkHistoryDAL.Update(entity);
            }
            else
            {
                //save record
                StudentWorkHistoryDAL.Add(user);
            }
        }

        public bool delete(int StudentWorkHistoryId)
        {
            StudentWorkHistory entity = Get(StudentWorkHistoryId);
            if (entity != null)
            {
                //delete
                StudentWorkHistoryDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteByStudentId(int StudentId)
        {
            IEnumerable<StudentWorkHistory> entity = GetAll().Where(x => x.StudentId == StudentId).ToList();
           foreach(StudentWorkHistory sw in entity)
           {
                //delete
                StudentWorkHistoryDAL.Delete(sw);
           }
           return true;
        }
    }
}
