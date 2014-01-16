using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;
namespace SpotCollege.BLL
{
   public class UserBLL
    {
        public List<User> GetAll()
        {
            //for list
            return UserDAL.FetchAll().ToList();
        }

        public User Get(string UserName)
        {
            //for single record
            return UserDAL.Get(x => x.UserName == UserName);
        }

        public void Save(User user)
        {
            User entity = Get(user.UserName);
            if (entity != null)
            {
                //update
                entity.UserName = entity.UserName;
                entity.Password = user.Password;
                UserDAL.Update(entity);
            }
            else
            {
                //save record
                UserDAL.Add(user);
            }
        }

        public bool delete(string UserName)
        {
            User entity = Get(UserName);
            if (entity != null)
            {
                //delete
                UserDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
