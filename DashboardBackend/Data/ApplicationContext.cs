using DashboardBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardBackend.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { } 

        public DbSet<Customer> Customers {  get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
