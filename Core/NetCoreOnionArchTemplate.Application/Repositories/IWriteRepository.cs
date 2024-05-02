﻿using NetCoreOnionArchTemplate.Domain.Entities.Common;

namespace NetCoreOnionArchTemplate.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Update(T model);
        bool UpdateRange(List<T> datas);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(Guid Id);
        Task<int> SaveAsync();
    }
}
