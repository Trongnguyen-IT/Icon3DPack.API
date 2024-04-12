using Icon3DPack.API.Core.Common;
using Icon3DPack.API.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _baseRepository.AddAsync(entity);
        }

        public virtual async Task<TEntity> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetFirstAsync(x => x.Id == id);
            return await _baseRepository.DeleteAsync(entity);
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _baseRepository.GetAllAsync(predicate);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync(x => true);
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _baseRepository.GetFirstAsync(predicate);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _baseRepository.UpdateAsync(entity);
        }
    }
}
