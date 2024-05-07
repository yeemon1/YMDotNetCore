using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using YMDotNetCore.RestApi.Model;
using YMDotNetCore.Share;

namespace YMDotNetCore.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController2 : ControllerBase
    {

        public readonly DappperService _dapperService = new DappperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_blog";
            var lst2 = _dapperService.Query<BlogModel>(query);
            return Ok(lst2);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindbyId(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
            ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES 
             (@BlogTitle,@BlogAuthor,@BlogContent)";
            var result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindbyId(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
                             SET [BlogTitle] = @BlogTitle
                             ,[BlogAuthor] = @BlogAuthor
                             ,[BlogContent] = @BlogContent
                             WHERE BlogId = @BlogId";
            var result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Updating Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindbyId(id);
            if (item is null)
            {
                return NotFound("No Data Found");
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
            var result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Updating Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindbyId(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            string query = @"DELETE FROM  [dbo].[Tbl_Blog]
                                WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogModel { BlogId = id });
            string message = result > 0 ? "Deleting  Successful" : "Saving Failed";
            return Ok(message);
        }

        private BlogModel? FindbyId(int id)
        {
            string query = "select * from Tbl_blog where blogId = @BlogId";
            var item = _dapperService.QueryFirstorDefault<BlogModel>(query, new BlogModel { BlogId = id });
            return item;
        }

    }
}
