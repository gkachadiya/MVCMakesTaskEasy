using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
    public class UniversityBLL
    {
        public List<University> GetAll()
        {
            //for list
            return UniversityDAL.FetchAll().ToList();
        }

        public University Get(int UniversityId)
        {
            //for single record
            return UniversityDAL.Get(x => x.UniversityId == UniversityId);
        }

        public University GetByUserName(string UserName)
        {
            //for single record
            return UniversityDAL.Get(x => x.UserName == UserName);
        }

        public IList<sp_NoOFunApprovedMsg_Result> getUnApprovedMsg()
        {
            return SpotCollegeEntityModel.EntityModel.sp_NoOFunApprovedMsg().ToList();
        }

        public int Save(University user)
        {
            University entity = Get(user.UniversityId);
            if (entity != null)
            {
                //update

                entity.UserName = user.UserName;
                entity.UniversityName = user.UniversityName;
                entity.Address = user.Address;
                entity.AdminName = user.AdminName;
                entity.Image = user.Image;
                entity.Degree = user.Degree;
                entity.City = user.City;
                entity.Country = user.Country;
                entity.CountryCode = user.CountryCode;
                entity.ContactNo = user.ContactNo;
                entity.EstablishedYear = user.EstablishedYear;
                entity.Description = user.Description;
                entity.UnderGraduateFee = user.UnderGraduateFee;
                entity.GraduateFee = user.GraduateFee;
                entity.BookFee = user.BookFee;
                entity.BoardFee = user.BoardFee;
                entity.ApplicationFee = user.ApplicationFee;
                entity.Graduates = user.Graduates;
                entity.UnderGraduates = user.UnderGraduates;
                entity.InternationalGraduate = user.InternationalGraduate;
                entity.Faqs = user.Faqs;
                entity.EnroledPerYearAfrica = user.EnroledPerYearAfrica;
                entity.EnroledPerYearChina = user.EnroledPerYearChina;
                entity.EnroledPerYearEurope = user.EnroledPerYearEurope;
                entity.EnroledPerYearIndianSub = user.EnroledPerYearIndianSub;
                entity.EnroledPerYearMidEast = user.EnroledPerYearMidEast;
                entity.EnroledPerYearSouthAmerica = user.EnroledPerYearSouthAmerica;
                UniversityDAL.Update(entity);
                return user.UniversityId;
            }
            else
            {
                //save record
                UniversityDAL.Add(user);
                return user.UniversityId;
            }
        }

        public bool delete(int UniversityId)
        {
            University entity = Get(UniversityId);
            if (entity != null)
            {
                //delete
                UniversityDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteByUserName(string UserName)
        {
            StudentInterestBLL interestbbll = new StudentInterestBLL();
            AlertBLL alert = new AlertBLL();
            MessageBLL msgBll = new MessageBLL();
            UniversityBLL universityBll = new UniversityBLL();
            UserBLL usserBll = new UserBLL();

            List<Message> message = msgBll.GetAll().Where(x => x.FromUserName == UserName).ToList();
            foreach (var lim in message)
            {
                msgBll.delete(lim.MessageId);
            }

            List<Message> messaget = msgBll.GetAll().Where(x => x.ToUserName == UserName).ToList();
            foreach (var limt in messaget)
            {
                msgBll.delete(limt.MessageId);
            }


            interestbbll.deleteByUniversityUsername(UserName);

            List<University> entity = GetAll().Where(x => x.UserName == UserName).ToList();
            foreach (University sw in entity)
            {
                //delete
                UniversityDAL.Delete(sw);
            }
            alert.deleteByUser(UserName);

            usserBll.delete(UserName);

            return true;
        }


        public University GetByUniversityName(string UniversityName)
        {
            return UniversityDAL.Get(x => x.UniversityName == UniversityName);
        }


        public University GetByUniversityCountry(string Country)
        {
            return UniversityDAL.Get(x => x.Country == Country);
        }

    }
}
