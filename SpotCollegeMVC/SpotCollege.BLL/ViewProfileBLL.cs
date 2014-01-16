using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
   public class ViewProfileBLL
    {
       public List<ViewProfile> GetAll()
        {
            //for list
            return ViewProfileDAL.FetchAll().ToList();
        }

       public ViewProfile Get(int ViewId)
        {
            //for single record
            return ViewProfileDAL.Get(x => x.ViewId == ViewId);
        }



       public void Save(ViewProfile view)
        {
            ViewProfile entity = Get(view.ViewId);
            if (entity != null)
            {                //update


                entity.UniversityUsername = view.UniversityUsername;
                entity.StudentUsername = view.StudentUsername;

                ViewProfileDAL.Update(entity);
            }
            else
            {
                //save record
                ViewProfileDAL.Add(view);
            }
        }

       public bool delete(int ViewId)
       {
           ViewProfile entity = Get(ViewId);
           if (entity != null)
           {
               //delete
               ViewProfileDAL.Delete(entity);
               return true;
           }
           else
           {
               return false;
           }
       }

       public bool deleteByUsername(string studentUserName)
       {
           IEnumerable<ViewProfile> entity = GetAll().Where(x => x.StudentUsername == studentUserName);
           foreach (ViewProfile sw in entity)
           {
               //delete
               ViewProfileDAL.Delete(sw);
           }
           return true;
       }

       public bool deleteByUniversityUsername(string studentUserName)
       {
           IEnumerable<ViewProfile> entity = GetAll().Where(x => x.UniversityUsername == studentUserName);
           foreach (ViewProfile sw in entity)
           {
               //delete
               ViewProfileDAL.Delete(sw);
           }
           return true;
       }
    }
}
