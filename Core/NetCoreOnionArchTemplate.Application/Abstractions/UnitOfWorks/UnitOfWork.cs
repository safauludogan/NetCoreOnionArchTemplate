using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities.Common;

namespace NetCoreOnionArchTemplate.Application.Abstractions.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : BaseEntity, IEntityBase, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity, IEntityBase, new();
        Task<int> SaveAsync();
    }
}
