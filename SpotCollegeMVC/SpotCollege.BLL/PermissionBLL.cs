using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
   public class PermissionBLL
    {
        public List<Permission> GetAll()
        {
            //for list
            return PermissionDAL.FetchAll().ToList();
        }

        public Permission Get(int PermissionId)
        {
            //for single record
            return PermissionDAL.Get(x => x.PermissionId == PermissionId);
        }
        public Permission GetbyUsername(string Username)
        {
            //for single record
            return PermissionDAL.Get(x => x.UserName == Username);
        }

        public void Save(Permission view)
        {
            Permission entity = Get(view.PermissionId);
            if (entity != null)
            {
                //update
                entity.UserName = view.UserName;
                entity.FirstName = view.FirstName;
                entity.LastName = view.LastName;
                entity.Phone = view.Phone;
                entity.StudentMessage = view.StudentMessage;
                entity.CollegeProfile = view.CollegeProfile;
                entity.CollegeMessage = view.CollegeMessage;
                entity.Article = view.Article;
                entity.Review = view.Review;
                entity.CreatedDate = view.CreatedDate;

                PermissionDAL.Update(entity);
            }
            else
            {
                //save record
                PermissionDAL.Add(view);
            }
        }

        public bool delete(int PermissionId)
        {
            Permission entity = Get(PermissionId);
            if (entity != null)
            {
                //delete
                PermissionDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

      
    }
}
