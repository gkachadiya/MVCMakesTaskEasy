using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
   public class AnswerBLL
    {
       public List<FeedBackAnswer> GetAll()
        {
            //for list
            return AnswerDAL.FetchAll().ToList();
        }

       public FeedBackAnswer Get(int AnswerId)
        {
            //for single record
            return AnswerDAL.Get(x => x.AnswerId == AnswerId);
        }
        //public AnswerDetail GetbyUsername(string Username)
        //{
        //    //for single record
        //    return AnswerDAL.Get(x => x.UserName == Username);
        //}

       public void Save(FeedBackAnswer view)
        {
            FeedBackAnswer entity = Get(view.AnswerId);
            if (entity != null)
            {
                //update
                entity.QuestionId = view.QuestionId;
                entity.Answer = view.Answer;
                entity.SectionId = view.SectionId;
                entity.IsApproved = view.IsApproved;

                AnswerDAL.Update(entity);
            }
            else
            {
                //save record
                AnswerDAL.Add(view);
            }
        }

        public bool delete(int AnswerId)
        {
            FeedBackAnswer entity = Get(AnswerId);
            if (entity != null)
            {
                //delete
                AnswerDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
