using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
   public class SettingBLL
    {
        public List<Settings> GetAll()
        {
            //for list
            return SettingDAL.FetchAll().ToList();
        }

        public Settings Get(int SettingId)
        {
            //for single record
            return SettingDAL.Get(x => x.SettingId == SettingId);
        }

        public Settings GetByUsername(string Username)
        {
            //for list
            return SettingDAL.Get(x => x.UserName == Username);
        }

        public void Save(Settings view)
        {
            Settings entity = Get(view.SettingId);
            if (entity != null)
            {
                //update
                entity.UserName = view.UserName;
                entity.Students = view.Students;
                entity.University = view.University;
                entity.DailyEmail = view.DailyEmail;
                entity.StudentsEmail = view.StudentsEmail;
                entity.UniversityEmail = view.UniversityEmail;
                entity.PromotionalEmail = view.PromotionalEmail;

                SettingDAL.Update(entity);
            }
            else
            {
                //save record
                SettingDAL.Add(view);
            }
        }

        public bool delete(int SettingId)
        {
            Settings entity = Get(SettingId);
            if (entity != null)
            {
                //delete
                SettingDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
