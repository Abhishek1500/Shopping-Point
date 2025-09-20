using Microsoft.EntityFrameworkCore;
using ProductMicroService.Models;

namespace ProductMicroService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
