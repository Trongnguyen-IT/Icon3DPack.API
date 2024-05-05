using System.Linq.Expressions;
using Icon3DPack.API.Core.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace Icon3DPack.API.DataAccess.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true);

    IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>>? predicate = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
       bool disableTracking = true);

    Task<PaginationResult<TEntity>> GetPagedAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageNumber = 1,
        int pageSize = 200,
        bool disableTracking = true);

    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> DeleteAsync(TEntity entity);
}
