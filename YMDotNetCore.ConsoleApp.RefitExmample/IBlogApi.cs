using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMDotNetCore.ConsoleApp.RefitExmample
{
    public interface IBlogApi
    {
        [Get("/api/Blog")]
        Task<List<BlogModel>> Get();
    }
    public class BlogModel
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
}
