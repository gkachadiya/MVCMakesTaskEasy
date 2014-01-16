using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotCollege.BLL;
using SpotCollege.DAL;
using SpotCollege.DAL.DataModels;
using SpotCollege.Common;
using System.Web;


namespace EmailComponent
{
   public class GetEmailIdFromDB
    {
        UserBLL userBLL = new UserBLL();
        List<User> EmailIdList = new List<User>();
        public IList<User> GetMailIds()
        {
            EmailIdList = userBLL.GetAll().Where(x => x.LoginTypeId == 1).ToList();
            return EmailIdList;
        }
    }
}
