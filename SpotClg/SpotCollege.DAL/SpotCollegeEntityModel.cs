using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using SpotCollege.Common;

namespace SpotCollege.DAL
{
    public class SpotCollegeEntityModel
    {
        private static SpotCollegeEntities _entityModel = null;

        static SpotCollegeEntityModel()
        {
        }

        public static void Dispose()
        {
            _entityModel = null;
        }


        public static SpotCollegeEntities EntityModel
        {
            get
            {
                String key = "DBContext";
                object context;
                if (HttpContext.Current == null)
                {
                    if (_entityModel == null)
                    {
                        _entityModel = new SpotCollegeEntities(ConfigHelper.ConnectionString);
                    }
                    return _entityModel;
                }

                // *** Create a unique Key for the Web Request/Context 

                key = key + HttpContext.Current.GetHashCode().ToString("x") + Thread.CurrentContext.ContextID.ToString();
                context = HttpContext.Current.Items[key];

                if (context == null)
                {
                    context = new SpotCollegeEntities(ConfigHelper.ConnectionString);

                    if (context != null)

                        HttpContext.Current.Items[key] = context;

                }
                return (SpotCollegeEntities)context;
            }



        }
    }
}
