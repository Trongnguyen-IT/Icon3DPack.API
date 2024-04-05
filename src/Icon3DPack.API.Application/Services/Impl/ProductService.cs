using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;
using System.Runtime.CompilerServices;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService) : base(productRepository)
        {
            _categoryService = categoryService;
            _productRepository = productRepository;
        }

        public override async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync(p => true);
        }

        public async override Task<Product> AddAsync(Product entity)
        {
            return await _productRepository.AddAsync(entity); ;
        }

        public override async Task<Product> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetFirstAsync(p => p.Id == id);
            return await _productRepository.DeleteAsync(product);
        }
    }
}
