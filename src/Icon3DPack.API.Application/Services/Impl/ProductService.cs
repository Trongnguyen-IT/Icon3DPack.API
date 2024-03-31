using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly ICategoryService _categoryService;

        public ProductService(IBaseRepository<Product> baseRepository, ICategoryService categoryService) : base(baseRepository)
        {
            _categoryService = categoryService;
        }

        public async override Task<Product> AddAsync(Product entity)
        {
            var product = await base.AddAsync(entity);
            var category = await _categoryService.GetFirstAsync(p => p.Id == product.CategoryId);

            if (category != null)
            {
                category.ProductAmount++;
                await _categoryService.UpdateAsync(category);
            }

            return product;
        }

        public async override Task<Product> DeleteAsync(Guid id)
        {
            var product = await base.DeleteAsync(id);

            var category = await _categoryService.GetFirstAsync(p => p.Id == product.CategoryId);

            if (category != null)
            {
                category.ProductAmount--;
                await _categoryService.UpdateAsync(category);
            }

            return product;
        }
    }
}
