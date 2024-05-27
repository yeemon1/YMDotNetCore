using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMDotNetCore.PizzaApi
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "YMDotNetCore",
            IntegratedSecurity = true, // Use Windows Authentication
            TrustServerCertificate = true
        };

    }
}
