using AutoMapper;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Core.Exceptions;
using Icon3DPack.API.DataAccess.Persistence;
using Icon3DPack.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public readonly DatabaseContext _dbContext;

        public CategoryService(ICategoryRepository categoryRepository,
            IMapper mapper,
            DatabaseContext dbContext) : base(categoryRepository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _categoryRepository = categoryRepository;
        }

        public override async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync(orderBy: c => c.OrderBy(p => p.Id),
                include: c => c.Include(cc => cc.Products)
                .Include(p => p.CategoryTags)
                .ThenInclude(p => p.Tag));
        }

        public override async Task<Category> GetFirstAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _categoryRepository.GetFirstAsync(predicate,
               include: c => c.Include(cc => cc.Products)
                .Include(p => p.CategoryTags)
                .ThenInclude(p => p.Tag));
        }

        public async Task<Category> UpdateAsync(CategoryRequestModel model)
        {
            var category = await _categoryRepository.GetFirstAsync(p => p.Id == model.Id, p => p.Include(p => p.CategoryTags));

            if ((category == null)) throw new ResourceNotFoundException(typeof(Category));

            category?.CategoryTags.Clear();

            var updateCategory = _mapper.Map(model, category);

            return await _categoryRepository.UpdateAsync(updateCategory);
        }
    }
}
