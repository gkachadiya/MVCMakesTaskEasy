using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
  public  class AlertBLL
    {
        public List<Alert> GetAll()
        {
            //for list
            return AlertDAL.FetchAll().ToList();
        }

        public Alert Get(int AlertId)
        {
            //for single record
            return AlertDAL.Get(x => x.AlertId == AlertId);
        }



        public void Save(Alert alert)
        {
            Alert entity = Get(alert.AlertId);
            if (entity != null)
            {              
                //update
                entity.CreatedBy = alert.CreatedBy;
                entity.UserName = alert.UserName;
                entity.Message = alert.UserName;
                entity.CreatedDate = alert.CreatedDate;

                AlertDAL.Update(entity);
            }
            else
            {
                //save record
                AlertDAL.Add(alert);
            }
        }

        public bool delete(int StudentAcademicsId)
        {
            Alert entity = Get(StudentAcademicsId);
            if (entity != null)
            {
                //delete
                AlertDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteByUser(string UserName)
        {
            IEnumerable<Alert> entity = GetAll().Where(x=>x.UserName==UserName).ToList();

            foreach (Alert sw in entity)
            {
                //delete
                AlertDAL.Delete(sw);
            }

            entity = GetAll().Where(x => x.CreatedBy == UserName).ToList();
            foreach (Alert sw in entity)
            {
                //delete
                AlertDAL.Delete(sw);
            }
            return true;
        }
    }
}
