﻿
namespace YMDotNetCore.RestApiWithNLayer.Db
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        } 
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
