using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace YMDotNetCore.Share
{
    public class DappperService
    {
        private readonly string _connectionstring;
        public DappperService(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionstring);
            var lst = db.Query<T>(query, param).ToList();
            return lst;
        }
        public T QueryFirstorDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionstring);
            var item = db.Query<T>(query, param).FirstOrDefault();
            return item;
        }
        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionstring);
            var result = db.Execute(query, param);
            return result;
        }
    }
}