using AutoMapper;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Core.Exceptions;
using Icon3DPack.API.DataAccess;
using Icon3DPack.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService, IMapper mapper) : base(productRepository)
        {
            _categoryService = categoryService;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public override async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync(p => true);
        }

        public async override Task<Product> AddAsync(Product entity)
        {
            return await _productRepository.AddAsync(entity);
        }

        public override async Task<Product> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetFirstAsync(p => p.Id == id);
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<PaginatedList<Product>> ProductFilter(string? name, string? categoryId, string? sortOrder, int? pageNumber, int? pageSize)
        {
            return await _productRepository.ProductFilter(name, categoryId, sortOrder, pageNumber, pageSize);
        }

        public async Task<Product> UpdateAsync(ProductRequestModel model)
        {
            var product = await _productRepository.GetFirstAsync(p => p.Id == model.Id);

            if ((product == null)) throw new ResourceNotFoundException(typeof(Product));

            var productUpdate = _mapper.Map(model, product);
            await _productRepository.UpdateAsync(productUpdate);

            await _categoryService.DecreaseProductAmountAsync(product.CategoryId);
            await _categoryService.IncreateProductAmountAsync(model.CategoryId);

            return productUpdate;
        }
    }
}
