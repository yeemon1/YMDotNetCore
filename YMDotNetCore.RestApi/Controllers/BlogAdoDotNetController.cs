using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using YMDotNetCore.RestApi.Model;
using YMDotNetCore.Share;

namespace YMDotNetCore.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : Controller
    {
        public SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = "SELECT * from Tbl_Blog";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
            List<BlogModel> lst = new List<BlogModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                BlogModel blog = new BlogModel();
                blog.BlogId = Convert.ToInt32(item["BlogId"]);
                blog.BlogTitle = Convert.ToString(item["BlogId"]);
                blog.BlogAuthor = Convert.ToString(item["BlogId"]);
                blog.BlogContent = Convert.ToString(item["BlogId"]);
                lst.Add(blog);
            }
            return Ok(lst);
        }
        [HttpGet("{Id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = "SELECT * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
            if (dataTable.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }
            DataRow item = dataTable.Rows[0];
            var itemresult = new BlogModel
            {
                BlogId = Convert.ToInt32(item["BlogId"]),
                BlogTitle = Convert.ToString(item["BlogId"]),
                BlogAuthor = Convert.ToString(item["BlogId"]),
                BlogContent = Convert.ToString(item["BlogId"])
            };            
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
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
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle) ;
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);   
        }
        [HttpPut]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item == null)
            {
                return NotFound("Data Not Found");
            }
            string query = @"UPDATE [dbo].[Tbl_Blog]
                                SET [BlogTitle] = @BlogTitle
                                    ,[BlogAuthor] = @BlogAuthor
                                    ,[BlogContent] = @BlogContent
                                WHERE BlogId = @BlogId";

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item == null)
            {
                return NotFound("Data Not Found");
            }
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle,";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor,";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent,";
            }
            if (conditions.Length == 0)
            {
                return NotFound("No Data To Update");
            }
            conditions = conditions.Substring(0, conditions.Length - 1);
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                                SET {conditions}
                                WHERE BlogId = @BlogId";

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = FindById(id);
            if (item == null)
            {
                return NotFound("Data Not Found");
            }
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId = @BlogId;";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", id);
            
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            string message = result > 0 ? "Delete Successful" : "Sa Failed";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            sqlConnection.Open();

            string query = "SELECT * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
    
            DataRow item = dataTable.Rows[0];
            var itemresult = new BlogModel
            {
                BlogId = Convert.ToInt32(item["BlogId"]),
                BlogTitle = Convert.ToString(item["BlogTitle"]),
                BlogAuthor = Convert.ToString(item["BlogAuthor"]),
                BlogContent = Convert.ToString(item["BlogContent"])
            };
            return itemresult;
        }


    }
}
