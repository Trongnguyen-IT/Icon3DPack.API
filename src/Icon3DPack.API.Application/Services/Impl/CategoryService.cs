using AutoMapper;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Amazon.S3.Util.S3EventNotification;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public override async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync(orderBy: c => c.OrderBy(p => p.Id),
                include: c => c.Include(cc => cc.CategoryTags)
                .ThenInclude(ct => ct.Tag));
        }

        public async Task DecreaseProductAmountAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetFirstAsync(p => p.Id == categoryId);
            if (category != null)
            {
                category.ProductAmount--;
                await _categoryRepository.UpdateAsync(category);
            }
        }

        public async Task IncreateProductAmountAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetFirstAsync(p => p.Id == categoryId);
            if (category != null)
            {
                category.ProductAmount++;
                await _categoryRepository.UpdateAsync(category);
            }
        }

        public async Task<Category> UpdateAsync(CategoryRequestModel model)
        {
            var category = await _categoryRepository.GetFirstAsync(p => p.Id == model.Id, p => p.Include(p => p.CategoryTags));

            category?.CategoryTags.Clear();

            var updateCategory = _mapper.Map(model, category);

            return await _categoryRepository.UpdateAsync(updateCategory);
        }
    }
}
