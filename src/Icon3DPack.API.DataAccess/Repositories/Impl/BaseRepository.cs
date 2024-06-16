using System.Linq.Expressions;
using Icon3DPack.API.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Exceptions;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace Icon3DPack.API.DataAccess.Repositories.Impl;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public readonly DatabaseContext _dbContext;
    public readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DatabaseContext context)
    {
        _dbContext = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().AsQueryable();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.Id = Guid.NewGuid();
        var addedEntity = (await _dbSet.AddAsync(entity)).Entity;
        await _dbContext.SaveChangesAsync();

        return addedEntity;
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var removedEntity = _dbSet.Remove(entity).Entity;
        await _dbContext.SaveChangesAsync();

        return removedEntity;
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (predicate != null) query = query.Where(predicate);

        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) query = orderBy(query);

        return await query.ToListAsync();
    }

    public async Task<PaginationResult<TEntity>> GetPagedAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageNumber = 1,
        int pageSize = 200,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (predicate != null) query = query.Where(predicate);

        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        int totalCount = await query.CountAsync();

        if (orderBy != null) query = orderBy(query);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<TEntity>(items, pageNumber, pageSize, totalCount);
    }

    public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (predicate != null) query = query.Where(predicate);

        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        return query;
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet;

        query = query.Where(predicate);

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        var entity = await query.FirstOrDefaultAsync();

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
}
