using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Data
{
    public class DessertContext : DbContext
    {
        public DessertContext(DbContextOptions<DessertContext> options) : base(options) 
        {
        
        }

        public DbSet<Dessert> Desserts { get; set; }
    }
}
