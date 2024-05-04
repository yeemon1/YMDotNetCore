using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YMDotNetCore.RestApi.Model;

namespace YMDotNetCore.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : Controller
    {
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

    }
}
