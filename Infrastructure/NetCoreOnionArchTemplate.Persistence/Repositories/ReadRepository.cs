using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities.Common;
using NetCoreOnionArchTemplate.Persistence.Context;
using System.Linq.Expressions;

namespace NetCoreOnionArchTemplate.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;

        public ReadRepository(DataContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
          => Table;

        public async Task<T> GetByIdAsync(int Id)
         => await Table.Where(x => x.Id == Id).FirstOrDefaultAsync();


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
         => await Table.FirstOrDefaultAsync(method);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        => Table.Where(method);

    }
}
