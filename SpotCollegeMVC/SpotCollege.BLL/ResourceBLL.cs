using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
   public class ResourceBLL
    {
        public List<Resource> GetAll()
        {
            //for list
            return ResourceDAL.FetchAll().ToList();
        }

        public Resource Get(int ResourceId)
        {
            //for single record
            return ResourceDAL.Get(x => x.ResourceId == ResourceId);
        }

        public void Save(Resource view)
        {
            Resource entity = Get(view.ResourceId);
            if (entity != null)
            {                
                //update
                entity.CategoryId = view.CategoryId;
                entity.Title = view.Title;
                entity.Description = view.Description;
                entity.CreatedDate = view.CreatedDate;
                entity.CreatedBy = view.CreatedBy;
                entity.ImagePath = view.ImagePath;

                ResourceDAL.Update(entity);
            }
            else
            {
                //save record
                ResourceDAL.Add(view);
            }
        }

        public bool delete(int ResourceId)
        {
            Resource entity = Get(ResourceId);
            if (entity != null)
            {
                //delete
                ResourceDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

      
    }
}
