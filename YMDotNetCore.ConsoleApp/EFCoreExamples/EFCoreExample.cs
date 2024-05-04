using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMDotNetCore.ConsoleApp.Dtos;

namespace YMDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        private readonly AppDbContext db = new AppDbContext();

        public void Run()
        {
            //Read();
            //Edit(4);
            //Edit(5);
            //Create("title", "author", "content");
            Update(5, "title", "author", "content");
            Delete(4);

        }
        private void Read()
        {
            // AppDbContext db = new AppDbContext();
            var lst = db.Blogs.ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-------------");
            }

        }
        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("No Data Found ");
            }
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-------------");
        }

        private void Create(string Title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = Title,
                BlogAuthor = author,
                BlogContent = content,
            };
            db.Blogs.Add(item);
            var result = db.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
               Console.WriteLine("No Data Found ");
            }
            //item.BlogId = id,
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("No Data Found ");
            }
            db.Blogs.Remove(item);
            var result = db.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
        }
    }
}
