using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities.Common;
using NetCoreOnionArchTemplate.Persistence.Context;
using System.Linq.Expressions;

namespace NetCoreOnionArchTemplate.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, IEntityBase, new()
    {
        private readonly DataContext _context;

        public ReadRepository(DataContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetAllByPaging(bool tracking = true, int currentPage = 1, int pageSize = 3)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        public async Task<T?> GetByIdAsync(Guid Id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }


        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? method = null)
        {
            var query = Table.AsQueryable().AsNoTracking();
            if (method != null) query = query.Where(method);
            return await query.CountAsync();
        }
    }
}
