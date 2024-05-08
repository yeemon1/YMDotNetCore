using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YMDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly Bl_Blog _blBlog;
        public BlogController(Bl_Blog blBlog)
        {
            _blBlog = blBlog;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var lst = _blBlog.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.GetBlog(id);

            if (item == null)
            {
                return NotFound("No Data Found!.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            var result = _blBlog.CreateBlog(blog);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var  result= _blBlog.UpdateBlog(id, blog);
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var result = _blBlog.PatchBlog(id, blog);
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.GetBlog(id);
            if ( item == null)
            {
                return NotFound("No Data Found!.");
            }
            var result = _blBlog.DeleteBlog(id);
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message); 
        }
    }
}
