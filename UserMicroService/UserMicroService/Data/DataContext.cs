using Microsoft.EntityFrameworkCore;
using UserMicroService.Models;

namespace UserMicroService.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
