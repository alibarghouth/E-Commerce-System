using E_Commerce_System.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace E_Commerce_System.Context
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }

        public DbSet<Customer>  Customers { get; set; }
        public DbSet<OrderItem>  OrderItems { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Order>  Orders { get; set; }
        public DbSet<Product>  Products { get; set; }

    }
}
