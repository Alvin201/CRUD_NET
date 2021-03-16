using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Image_System
{
    public class GlobalConnection
    {
        public static string objcon => ConfigurationManager.ConnectionStrings["ImageSystem"].ConnectionString;
    }
}