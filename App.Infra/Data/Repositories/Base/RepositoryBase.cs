namespace App.Infra.Data.Repositories.Base
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks; 
    using Shared.Either;
    using System.Linq.Expressions;
    using Core.Domain.Entities.Base;
    using Core.Domain.Entities.Behavior;
    using Microsoft.EntityFrameworkCore;
    using Core.Domain.Repositories.Base;


    public abstract class RepositoryBase<TContext, TEntity> : IRepositoryBase<TEntity>, IDisposable
            where TEntity : Entity
            where TContext : DbContext
    {
        protected TContext _context;

        public RepositoryBase(TContext context) { _context = context; }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = (IQueryable<TEntity>)_context.Set<TEntity>();

            return await query.ToListAsync();
        }


        public virtual async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync()
        {
            var query = (IQueryable<TEntity>)_context.Set<TEntity>();

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = default!,
            bool excludeDeleted = false)
        {
            var query = (IQueryable<TEntity>)_context.Set<TEntity>();

            if (excludeDeleted)
                query = query.Where(x => x.DeletedAt == null);

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPaginated(int page, int pageSize)
        {
            return await _context
                .Set<TEntity>()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<TEntity?> GetByIdAsNoTrackingAsync(Guid id)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await InternalAdd(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            if (entity.Id.Equals(default(Guid)))
                await AddAsync(entity);
            else
                await UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddOrUpdateAsync(IEnumerable<TEntity> entities)
        {
            List<TEntity> persistedEntities = new();

            foreach (var item in entities.ToList())
                persistedEntities.Add(await AddOrUpdateAsync(item));

            return persistedEntities;
        }

        public async Task<Option<Exception>> Delete(Guid id)
        {
            TEntity? entity = await GetByIdAsync(id);

            if (entity is null)
                throw new IndexOutOfRangeException("Id não encontrado");

            await Delete(entity);

            return Option<Exception>.None;
        }

        public async Task<Option<Exception>> Delete(IEnumerable<Guid> ids)
        {
            List<TEntity> entities = new();

            foreach (var id in ids)
            {
                var entity = await GetByIdAsync(id);

                if (entity is null)
                    return Option<Exception>.Some(new IndexOutOfRangeException("Id não encontrado"));

                entities.Add(entity);
            }

            await Delete(entities);

            return Option<Exception>.None;
        }

        public Task<bool> HasAny(Guid id)
        {
            return _context
                .Set<TEntity>()
                .AnyAsync(x => x.Id.Equals(id));
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task<Result<IEnumerable<TEntity>, Exception>> AddAsync(IEnumerable<TEntity> entities)
        {
            var workers = entities.Select(entity => AddAsync(entity)).ToArray();
            if (workers is null)
                return new ArgumentNullException(nameof(workers));

            await Task.WhenAll(workers);

            return Result<IEnumerable<TEntity>, Exception>.Ok(entities);
        }

        protected virtual async Task UpdateAsync(TEntity entity)
        {
            InternalUpdate(entity);
            await _context.SaveChangesAsync();
        }

        protected virtual async Task<Option<Exception>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            var workers = entities.Select(x => UpdateAsync(x)).ToArray();
            if (workers is null)
                return Option<Exception>.Some(new ArgumentNullException(nameof(workers)));

            await Task.WhenAll(workers);

            return Option<Exception>.None;
        }

        private async Task InternalAdd(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        private void InternalUpdate(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        private async Task<Option<Exception>> Delete(IEnumerable<TEntity> entities)
        {
            var workers = entities.Select(entity => Delete(entity)).ToArray();
            if (workers is null)
                return Option<Exception>.Some(new ArgumentNullException(nameof(workers)));

            await Task.WhenAll(workers);

            return Option<Exception>.None;
        }

        private async Task Delete(TEntity entity)
        {
            if (entity is IPhysicallyDeletable)
                _context.Set<TEntity>().Remove(entity);
            else
                entity.Delete();

            await _context.SaveChangesAsync();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any
        }
    }
}
