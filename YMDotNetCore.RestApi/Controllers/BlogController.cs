using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using YMDotNetCore.RestApi.Db;
using YMDotNetCore.RestApi.Model;

namespace YMDotNetCore.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lst = _context.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item == null)
            {
                return NotFound("No Data Found!.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound("No Data Found!.");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound("No Data Found!.");
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound("No Data Found!.");
            }
            _context.Blogs.Remove(item);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
    }
}
