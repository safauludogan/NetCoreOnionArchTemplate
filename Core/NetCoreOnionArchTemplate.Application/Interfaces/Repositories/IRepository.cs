using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Domain.Entities.Common;

namespace NetCoreOnionArchTemplate.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity, IEntityBase, new()
    {
        DbSet<T> Table { get; }
    }
}
