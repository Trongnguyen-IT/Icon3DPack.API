using AutoMapper;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper  _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task DecreaseProductAmountAsync(Guid id)
        {
            var category = await _categoryRepository.GetFirstAsync(p => p.Id == id);
            if (category != null)
            {
                category.ProductAmount--;
                await _categoryRepository.UpdateAsync(category);
            }
        }

        public async Task IncreateProductAmountAsync(Guid id)
        {
            var category = await _categoryRepository.GetFirstAsync(p => p.Id == id);
            if (category != null)
            {
                category.ProductAmount++;
                await _categoryRepository.UpdateAsync(category);
            }
        }

        public async Task<Category> UpdateAsync(CategoryRequestModel model)
        {
            var category = await _categoryRepository.GetFirstAsync(p => p.Id == model.Id);
            if (category != null)
            {
                await _categoryRepository.UpdateCategoryTagsAsync(model.Id);
            }
            var updateCategory = _mapper.Map(model,category);

            return await _categoryRepository.UpdateAsync(updateCategory);
        }
    }
}
