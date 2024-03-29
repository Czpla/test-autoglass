namespace App.Core.Domain.Repositories.Base
{
    using Shared.Either;
    using System.Linq.Expressions;
    using Core.Domain.Entities.Base;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;

    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = default!, bool excludeDeleted = false);
        Task<IEnumerable<TEntity>> GetPaginated(int page, int pageSize);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdAsNoTrackingAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> AddOrUpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddOrUpdateAsync(IEnumerable<TEntity> entities);
        Task<Option<Exception>> Delete(int id);
        Task<Option<Exception>> Delete(IEnumerable<int> ids);
    }
}