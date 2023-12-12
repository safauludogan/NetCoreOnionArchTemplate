using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Domain.Entities.Common;

namespace NetCoreOnionArchTemplate.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            //ChangeTracker : Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan property. Update operasyonlarında Track edilen verileri yakalar.

            var datas = ChangeTracker
                .Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
