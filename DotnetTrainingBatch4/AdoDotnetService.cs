using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMDotNetCore.Share
{
    public  class AdoDotnetService
    {
        private readonly string _connectionstring;

        public AdoDotnetService(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionstring);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            if (parameters is not null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
            string json = JsonConvert.SerializeObject(dataTable);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);
            return lst;
        }
        public T QueryFirstorDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionstring);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            if (parameters is not null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
            string json = JsonConvert.SerializeObject(dataTable);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;
            return lst[0];
        }
        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionstring);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            if (parameters is not null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            var result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return result;
        }

    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }
        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }

}
