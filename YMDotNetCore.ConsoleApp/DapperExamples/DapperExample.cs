using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YMDotNetCore.ConsoleApp.DBSetting;
using YMDotNetCore.ConsoleApp.Dtos;

namespace YMDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(2);
            //Create("title", "author", "content");
            Delete(5);
        }
        private void Read()
        {
            IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from Tbl_blog").ToList();
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
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("select * from Tbl_blog where blogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return ;
            }
            Console.WriteLine(item);

        }
        private void Create(string Title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = Title,
                BlogAuthor = author,
                BlogContent = content,
            };
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
            ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES
             (@BlogTitle,@BlogAuthor,@BlogContent)";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };
            string query = @"UPDATE [dbo].[Tbl_Blog]
                             SET [BlogTitle] = @BlogTitle
                             ,[BlogAuthor] = @BlogAuthor
                             ,[BlogContent] = @BlogContent
                             WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Updating Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id
            };
            string query = @"DELETE FROM  [dbo].[Tbl_Blog]
                                WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Deleting  Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
    }
}
