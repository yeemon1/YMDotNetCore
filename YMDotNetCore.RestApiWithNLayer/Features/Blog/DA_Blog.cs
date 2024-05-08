using System.Reflection.Metadata;
using YMDotNetCore.RestApiWithNLayer.Db;

namespace YMDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        private readonly AppDbContext _context;
        public DA_Blog(AppDbContext context)
        {
            _context = context;
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _context.Blogs.ToList();
            return lst; 
        }
        public BlogModel GetBlog(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x=>x.BlogId==id);
            return item;
        }
        public int CreateBlog(BlogModel requestmodel)
        {
            _context.Blogs.Add(requestmodel);
            var result = _context.SaveChanges();
            return result;
        }

        public int UpdateBlog(int id, BlogModel resquestmodel)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;

            item.BlogTitle = resquestmodel.BlogTitle;
            item.BlogAuthor = resquestmodel.BlogAuthor;
            item.BlogContent = resquestmodel.BlogContent;

            var result = _context.SaveChanges();
            return result;
        }

        public int PatchBlog(int id,BlogModel requestmodel)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;
            if (!string.IsNullOrEmpty(requestmodel.BlogTitle))
            {
                item.BlogTitle = requestmodel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestmodel.BlogAuthor))
            {
                item.BlogAuthor = requestmodel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(requestmodel.BlogContent))
            {
                item.BlogContent = requestmodel.BlogContent;
            }
            var result = _context.SaveChanges();
            return result;
        }
        public int DeleteBlog(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;
            _context.Blogs.Remove(item);
            var result = _context.SaveChanges();
            return result;
        }
    } 
}
