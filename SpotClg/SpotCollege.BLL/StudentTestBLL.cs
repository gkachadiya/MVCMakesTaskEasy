using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpotCollege.DAL;
using System.Threading.Tasks;

namespace SpotCollege.BLL
{
   public class StudentTestBLL
    {
        public List<StudentTest> GetAll()
        {
            //for list
            return StudentTestDAL.FetchAll().ToList();
        }

        public StudentTest Get(int StudentTestId)
        {
            //for single record
            return StudentTestDAL.Get(x => x.StudentTestId == StudentTestId);
        }

        public void Save(StudentTest user)
        {
            StudentTest entity = Get(user.StudentTestId);
            if (entity != null)
            {
                //update

                entity.StudentId = user.StudentId;
                entity.TestId = user.TestId;
                entity.SectorId = user.SectorId;
                entity.SchoolName = user.SchoolName;
                entity.StartYear = user.StartYear;
                entity.EndYear = user.EndYear;
                entity.Composite = user.Composite;
                entity.English = user.English;
                entity.Math = user.Math;
                entity.Reading = user.Reading;
                entity.Science = user.Science;
                entity.Writing = user.Writing;
                entity.Date = user.Date;
                entity.Subject = user.Subject;
                entity.Score = user.Score;
                entity.VerbalReasoning = user.VerbalReasoning;
                entity.QuantitativeReasoning = user.QuantitativeReasoning;
                entity.AnalyticalWriting = user.AnalyticalWriting;
                entity.Verbal = user.Verbal;
                entity.Total = user.Total;
                entity.Listening = user.Listening;
                entity.Speaking = user.Speaking;
                entity.Biology = user.Biology;
                entity.Physics = user.Physics;
                StudentTestDAL.Update(entity);
            }
            else
            {
                //save record
                StudentTestDAL.Add(user);
            }
        }

        public bool delete(int StudentId)
        {
            StudentTest entity = Get(StudentId);
            if (entity != null)
            {
                //delete
                StudentTestDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteByStudentId(int StudentId)
        {
            IEnumerable<StudentTest> entity = GetAll().Where(x=>x.StudentId==StudentId);
            foreach (StudentTest st in entity)
            {
                //delete
                StudentTestDAL.Delete(st);
            }
            return true;
        }
    }
}
