using AutoMapper;
using AutoMapper.QueryableExtensions;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Core.Exceptions;
using Icon3DPack.API.DataAccess.Persistence;
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
        public readonly DatabaseContext _dbContext;

        public ProductService(IProductRepository productRepository,
            ICategoryService categoryService,
            DatabaseContext dbContext,
            IMapper mapper) : base(productRepository)
        {
            _categoryService = categoryService;
            _productRepository = productRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync(orderBy: p => p.OrderByDescending(pp => pp.CreatedTime),
                include: p => p.Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag));
        }

        public async Task<PaginationResult<Product>> GetAllPagingAsync()
        {
            return await _productRepository.GetPagedAsync(orderBy: p => p.OrderByDescending(pp => pp.CreatedTime),
                include: p => p.Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag));
        }

        public override async Task<Product> GetFirstAsync(Expression<Func<Product, bool>> predicate)
        {
            var ressult = await _productRepository.GetFirstAsync(
                predicate: predicate,
                include: p => p.Include(p => p.Category)
                .Include(p => p.FileEntities)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag));

            return ressult;
        }

        public async override Task<Product> AddAsync(Product entity)
        {
            var newProduct = await _productRepository.AddAsync(entity);

            return newProduct;
        }

        public override async Task<Product> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetFirstAsync(p => p.Id == id, p => p.Include(pp => pp.ProductTags).Include(p => p.FileEntities));
            if (product.ProductTags.Any()) _dbContext.ProductTags.RemoveRange(product.ProductTags);
            if (product.FileEntities.Any()) _dbContext.FileEntities.RemoveRange(product.FileEntities);

            return await _productRepository.DeleteAsync(product);
        }

        public async Task<PaginationResult<ProductResponseModel>> ProductFilter(ProductFilter filter, int? pageNumber = 1, int? pageSize = 200)
        {
            var query = _productRepository.GetAllQueryable(conditionPredicate(filter.Keyword, filter.CategoryId));

            var totalCount = await query.CountAsync();

            var items = totalCount > 0 ?
                           _mapper.Map<IReadOnlyList<ProductResponseModel>>(await query.OrderAndPaging(filter).ToListAsync())
                            : new List<ProductResponseModel>();

            return new PaginationResult<ProductResponseModel>(items, pageNumber ?? 1, pageSize ?? 200, totalCount);
        }

        public async Task<Product> UpdateAsync(ProductRequestModel model)
        {
            var product = await _productRepository.GetFirstAsync(p => p.Id == model.Id, p => p.Include(pp => pp.ProductTags).Include(p => p.FileEntities));

            if ((product == null)) throw new ResourceNotFoundException(typeof(Product));

            //if (product.ProductTags.Any()) _dbContext.ProductTags.RemoveRange(product.ProductTags);
            //if (product.FileEntities.Any()) _dbContext.FileEntities.RemoveRange(product.FileEntities);

            product.ProductTags.Clear();
            product.FileEntities?.Clear();

            var productUpdate = _mapper.Map(model, product);
            await _productRepository.UpdateAsync(productUpdate);

            return productUpdate;
        }

        private Expression<Func<Product, bool>> conditionPredicate(string? keyword, Guid? categoryId)
            => (product => product.IsPublish && (string.IsNullOrEmpty(keyword) || product.Name.ToLower().Contains(keyword.ToLower()))
            && (categoryId == null || categoryId == Guid.Empty || product.CategoryId == categoryId));

        private IOrderedQueryable<Product> OrderedlQuery(IQueryable<Product> query, string? sortOrder, string? sortDirection = "desc")
        {
            //IOrderedQueryable<Product> result;
            //return  query.OrderByDynamic(sortOrder);
            return sortDirection == "desc" ? query.OrderByDescending(p => sortOrder) : query.OrderBy(p => sortOrder);
            //if (!string.IsNullOrEmpty(sortOrder))
            //{
            //    switch (sortOrder)
            //    {
            //        case "name_asc":
            //            result = query.OrderBy(p => sortOrder);
            //            break;
            //        case "name_desc":
            //            result = query.OrderByDescending(p => p.Name);
            //            break;
            //        case "date_asc":
            //            result = query.OrderBy(p => p.CreatedTime);
            //            break;
            //        case "date_desc":
            //            result = query.OrderByDescending(p => p.CreatedTime);
            //            break;
            //        default:
            //            result = query.OrderByDescending(p => p.ModifiedTime);
            //            break;
            //    }
            //}
            //else
            //{
            //    result = query.OrderByDescending(p => p.ModifiedTime);
            //}

            //return result;
        }

        public async Task<BaseResponseModel> DownloadFileAsync(Guid productId)
        {
            var product = await _productRepository.GetFirstAsync(p => p.Id == productId);

            if ((product == null)) throw new ResourceNotFoundException(typeof(Product));
            product.DownloadCount++;
            await _productRepository.UpdateAsync(product);

            return new BaseResponseModel { Id = productId };
        }
    }
}
