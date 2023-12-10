using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
