using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;

namespace SpotCollege.DAL
{
    public class DALConfigHelper
    {
        public static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[DALConfigKey.ConnectionString].ConnectionString; }
        }
    }
}
