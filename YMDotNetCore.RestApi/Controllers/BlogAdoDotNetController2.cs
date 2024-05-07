using Microsoft.AspNetCore.Mvc;
using YMDotNetCore.RestApi.Model;
using YMDotNetCore.Share;

namespace YMDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController2 : ControllerBase
    {
        private readonly AdoDotnetService _service = new AdoDotnetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * from Tbl_Blog";
            var lst = _service.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var result = FindById(id);
            if(result == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                ([BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent])
                            VALUES
                                (@BlogTitle
		                        ,@BlogAuthor
		                        ,@BlogContent)";

            int result = _service.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );
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

            int result = _service.Execute(query,
                new AdoDotNetParameter("@BlogId", id),
               new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
               new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
               new AdoDotNetParameter("@BlogContent", blog.BlogContent)
               );
            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
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

            int result = _service.Execute(query,
                new AdoDotNetParameter("@BlogId", id),
               new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
               new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
               new AdoDotNetParameter("@BlogContent", blog.BlogContent)
               );
            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete]
        public IActionResult Delete (int id)
        {
            var item = FindById(id);
            if (item == null)
            {
                return NotFound("Data Not Found");
            }
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId = @BlogId;";
            var result = _service.Execute(query, new AdoDotNetParameter("@BlogId", id));
            string message = result > 0 ? "Delete Successfully!" : "Delete Failed!";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = "SELECT * from Tbl_Blog where BlogId = @BlogId";
            var lst = _service.QueryFirstorDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
            return lst;
        }
        
    }
}
