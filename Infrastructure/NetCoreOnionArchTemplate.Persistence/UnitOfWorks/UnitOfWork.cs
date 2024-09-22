using NetCoreOnionArchTemplate.Application.Interfaces.UnitOfWorks;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities.Common;
using NetCoreOnionArchTemplate.Persistence.Context;
using NetCoreOnionArchTemplate.Persistence.Repositories;

namespace NetCoreOnionArchTemplate.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : BaseEntity, IEntityBase, new()
        => new ReadRepository<T>(_context);

        public IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity, IEntityBase, new()
         => new WriteRepository<T>(_context);

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}
