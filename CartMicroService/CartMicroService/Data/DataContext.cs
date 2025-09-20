using CartMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartMicroService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions opt) : base(opt) { }

        public DbSet<CartRequest> CartRequests { get; set; } 

    }
}
