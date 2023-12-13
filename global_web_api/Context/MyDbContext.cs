using global_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace global_web_api.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
