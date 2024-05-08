namespace YMDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class Bl_Blog
    {
        private readonly DA_Blog _daBlog;
        public Bl_Blog(DA_Blog daBlog)
        {
            _daBlog = daBlog;
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _daBlog.GetBlogs();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _daBlog.GetBlog(id); 
            return item;
        }
        public int CreateBlog(BlogModel requestmodel)
        {
            var result = _daBlog.CreateBlog(requestmodel);   
            return result;
        }

        public int UpdateBlog(int id, BlogModel resquestmodel)
        {
            var item = _daBlog.UpdateBlog(id,resquestmodel);
            return item;
        }

        public int PatchBlog(int id, BlogModel resquestmodel)
        {
            var item = _daBlog.PatchBlog(id, resquestmodel);
            return item;
        }
        public int DeleteBlog(int id)
        {
            var item = _daBlog.DeleteBlog(id);
            return item;
        }
    }
}
