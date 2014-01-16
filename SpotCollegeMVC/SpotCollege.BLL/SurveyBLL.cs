using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.DAL;

namespace SpotCollege.BLL
{
   public class SurveyBLL
    {
        public List<SurveyDetail> GetAll()
        {
            //for list
            return SurveyDAL.FetchAll().ToList();
        }

        public SurveyDetail Get(int SurveyId)
        {
            //for single record
            return SurveyDAL.Get(x => x.SurveyId == SurveyId);
        }
        public SurveyDetail GetbyUsername(string Username)
        {
            //for single record
            return SurveyDAL.Get(x => x.EmailId == Username);
        }

        public int Save(SurveyDetail view)
        {
            SurveyDetail entity = Get(view.SurveyId);
            if (entity != null)
            {
                //update
                entity.Name = view.Name;
                entity.Country = view.Country;
                entity.City = view.City;
                entity.University = view.University;
                entity.GraduationYear = view.GraduationYear;
                entity.Course = view.Course;
                entity.Degree = view.Degree;
                entity.ApplyOwnOrAgent = view.ApplyOwnOrAgent;
                entity.LocalAgentCharge = view.LocalAgentCharge;
                entity.AgentName = view.AgentName;
                entity.FirstLive = view.FirstLive;
                entity.LookingForHousing = view.LookingForHousing;
                entity.RoomShared = view.RoomShared;
                entity.MonthlyRent = view.MonthlyRent;
                entity.WhereLive = view.WhereLive;
                entity.Address = view.Address;
                entity.RateYourBuilding = view.RateYourBuilding;
                entity.LandlordFeedback = view.LandlordFeedback;
                entity.ReturnDeposite = view.ReturnDeposite;
                entity.HousingSuggestion = view.HousingSuggestion;
                entity.LookForJob = view.LookForJob;
                entity.FindJob = view.FindJob;
                entity.GetOne = view.GetOne;
                entity.Department = view.Department;
                entity.HourlyPay = view.HourlyPay;
                entity.JobSuggestion = view.JobSuggestion;
                entity.SafeOnCampus = view.SafeOnCampus;
                entity.SafeonOutside = view.SafeonOutside;
                entity.HappenCampus = view.HappenCampus;
                entity.CampusPolice = view.CampusPolice;
                entity.StolenThings = view.StolenThings;
                entity.StolenThingDescription= view.StolenThingDescription;
                entity.RetriveItBack = view.RetriveItBack;
                entity.Scholarship = view.Scholarship;
                entity.OneAfterwards = view.OneAfterwards;
                entity.AfterJoining = view.AfterJoining;
                entity.ScholarshipSuggestion = view.ScholarshipSuggestion;
                entity.SufficientEating = view.SufficientEating;
                entity.SufficientMarkets = view.SufficientMarkets;
                entity.TestScore = view.TestScore;
                entity.ApplyOtherUniversity = view.ApplyOtherUniversity;
                entity.GetAllUniversity = view.GetAllUniversity;
                entity.JobsorInternships = view.JobsorInternships;
                entity.SuggestionstoUniversity = view.SuggestionstoUniversity;
                entity.SuggestionstoStudent = view.SuggestionstoStudent;
                entity.EmailId = view.EmailId;
                entity.ForwardQuestions = view.ForwardQuestions;
                entity.IsApproved = view.IsApproved;
              

                SurveyDAL.Update(entity);
                return view.SurveyId;
            }
            else
            {
                //save record
                SurveyDAL.Add(view);
                return view.SurveyId;
            }
        }

        public bool delete(int SurveyId)
        {
            SurveyDetail entity = Get(SurveyId);
            if (entity != null)
            {
                //delete
                SurveyDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
