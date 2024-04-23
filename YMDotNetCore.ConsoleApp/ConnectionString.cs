using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMDotNetCore.ConsoleApp
{
    internal static  class ConnectionString
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "YMDotNetCore",
            IntegratedSecurity = true // Use Windows Authentication


        };

    }
}
