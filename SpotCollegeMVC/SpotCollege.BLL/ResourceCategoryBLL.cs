using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
    public class ResourceCategoryBLL
    {
        public List<ResourceCategory> GetAll()
        {
            //for list
            return ResourceCategoryDAL.FetchAll().ToList();
        }

        public ResourceCategory Get(int CategoryId)
        {
            //for single record
            return ResourceCategoryDAL.Get(x => x.CategoryId == CategoryId);
        }



        public void Save(ResourceCategory view)
        {
            ResourceCategory entity = Get(view.CategoryId);
            if (entity != null)
            {                //update


                entity.Name = view.Name;
                entity.SortingIndex = view.SortingIndex;

                ResourceCategoryDAL.Update(entity);
            }
            else
            {
                //save record
                ResourceCategoryDAL.Add(view);
            }
        }

        public bool delete(int CategoryId)
        {
            ResourceCategory entity = Get(CategoryId);
            if (entity != null)
            {
                //delete
                ResourceCategoryDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
      
    }
}
