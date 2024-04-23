using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMDotNetCore.ConsoleApp
{
    public class AdoNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "YMDotNetCore",
            IntegratedSecurity = true // Use Windows Authentication
        };

        public void Read()
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = "SELECT * from Tbl_Blog";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Console.WriteLine("Blog Id => " + dataRow["BlogId"]);
                Console.WriteLine("Blog Title => " + dataRow["BlogTitle"]);
                Console.WriteLine("Blog Author => " + dataRow["BlogAuthor"]);
                Console.WriteLine("Blog Content => " + dataRow["BlogContent"]);
                Console.WriteLine("----------------------------------");
            }
        }

        public void Edit(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = "SELECT * from Tbl_Blog WHERE BlogId = @BlogId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            DataRow dataRow = dataTable.Rows[0];
            Console.WriteLine("Blog Id => " + dataRow["BlogId"]);
            Console.WriteLine("Blog Title => " + dataRow["BlogTitle"]);
            Console.WriteLine("Blog Author => " + dataRow["BlogAuthor"]);
            Console.WriteLine("Blog Content => " + dataRow["BlogContent"]);
            Console.WriteLine("----------------------------------");
        }

        public void Create(string title, string author, string content)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                ([BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent])
                            VALUES
                                (@BlogTitle
		                        ,@BlogAuthor
		                        ,@BlogContent)";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", title);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", author);
            sqlCommand.Parameters.AddWithValue("@BlogContent", content);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Created Successfully!" : "Creating Failed!";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                                SET [BlogTitle] = @BlogTitle
                                    ,[BlogAuthor] = @BlogAuthor
                                    ,[BlogContent] = @BlogContent
                                WHERE BlogId = @BlogId";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", title);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", author);
            sqlCommand.Parameters.AddWithValue("@BlogContent", content);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId = @BlogId;";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Deleted Successfully!" : "Deleting Failed!";
            Console.WriteLine(message);
        }
    }

}
