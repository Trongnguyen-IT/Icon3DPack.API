using AutoMapper;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Core.Exceptions;
using Icon3DPack.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
            ICategoryService categoryService,
            IMapper mapper) : base(productRepository)
        {
            _categoryService = categoryService;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public override async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync(orderBy: p => p.OrderByDescending(pp => pp.CreatedTime),
                include: p => p.Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag));
        }

        public async Task<PaginationResult<Product>> GetAllPagingAsync()
        {
            return await _productRepository.GetPagedAsync(orderBy: p => p.OrderByDescending(pp => pp.CreatedTime),
                include: p => p.Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag));
        }

        public override async Task<Product> GetFirstAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.GetFirstAsync(
                predicate: predicate,
                include: p => p.Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag));
        }

        public async override Task<Product> AddAsync(Product entity)
        {
            var newProduct = await _productRepository.AddAsync(entity);
            await _categoryService.IncreateProductAmountAsync(entity.CategoryId);

            return newProduct;
        }

        public override async Task<Product> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetFirstAsync(p => p.Id == id);
            await _categoryService.DecreaseProductAmountAsync(product.CategoryId);

            return await _productRepository.DeleteAsync(product);
        }

        public async Task<PaginationResult<Product>> ProductFilter(Dictionary<string, string> filter, int? pageNumber = 1, int? pageSize = 200)
        {
            var keyword = filter.GetValueOrDefault("keyword");
            var categoryId = filter.GetValueOrDefault("categoryId");
            var sortOrder = filter.GetValueOrDefault("sortOrder");

            var result = await _productRepository.GetPagedAsync(conditionPredicate(keyword, categoryId),
               orderBy: p => OrderedParallelQuery(p, sortOrder),
               pageNumber: pageNumber ?? 1,
               pageSize: pageSize ?? 200);

            return result;
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

        private Expression<Func<Product, bool>> conditionPredicate(string? keyword, string? categoryId)
            => (product => product.IsPublish && (string.IsNullOrEmpty(keyword) || product.Name.ToLower() == keyword.ToLower())
            && (string.IsNullOrEmpty(categoryId) || product.CategoryId.ToString() == categoryId));

        private IOrderedQueryable<Product> OrderedParallelQuery(IQueryable<Product> query, string? sortOrder)
        {
            IOrderedQueryable<Product> result;

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "name_asc":
                        result = query.OrderBy(p => p.Name);
                        break;
                    case "name_desc":
                        result = query.OrderByDescending(p => p.Name);
                        break;
                    case "date_asc":
                        result = query.OrderBy(p => p.CreatedTime);
                        break;
                    case "date_desc":
                        result = query.OrderByDescending(p => p.CreatedTime);
                        break;
                    default:
                        result = query.OrderByDescending(p => p.ModifiedTime);
                        break;
                }
            }
            else
            {
                result = query.OrderByDescending(p => p.ModifiedTime);
            }

            return result;
        }
    }
}
