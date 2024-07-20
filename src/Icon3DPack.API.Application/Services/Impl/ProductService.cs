using Amazon.S3.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Core.Exceptions;
using Icon3DPack.API.DataAccess.Persistence;
using Icon3DPack.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductRepository _productRepository;
        private readonly IBaseRepository<FileEntity> _fileRepository;
        private readonly IMapper _mapper;
        public readonly DatabaseContext _dbContext;

        public ProductService(IProductRepository productRepository,
            ICategoryService categoryService,
            IBaseRepository<FileEntity> fileRepository,
            DatabaseContext dbContext,
            IMapper mapper) : base(productRepository)
        {
            _categoryService = categoryService;
            _productRepository = productRepository;
            _fileRepository = fileRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PaginationResult<ProductResponseModel>> GetAllAsync(BaseFilterDto filter)
        {
            var query = _productRepository
                .GetAll()
                .Include(p => p.Category)
                .Include(p => p.FileEntities)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag)
                .WhereIf(filter.Keyword.IsNotNullOrEmpty(), p => p.Name.ToLower().Contains(filter.Keyword!.ToLower()));

            var totalCount = await query.CountAsync();

            var items = totalCount > 0 ?
               _mapper.Map<IReadOnlyList<ProductResponseModel>>(await query.OrderAndPaging(filter).ToListAsync())
                : new List<ProductResponseModel>();

            return new PaginationResult<ProductResponseModel>(items, filter.PageNumber ?? 1, filter.PageSize ?? 200, totalCount);
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

        public async Task<PaginationResult<ProductResponseModel>> ProductFilter(ProductFilter filter)
        {
            var query = _productRepository.GetAll()
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag)
                .Where(p=>p.IsPublish)
                .WhereIf(filter.Keyword.IsNotNullOrEmpty(),
                    p => p.Name!.Contains(filter.Keyword!)
                    || p.ProductTags.Any(p => p.Tag.Name.Contains(filter.Keyword!)))
                .WhereIf(filter.CategoryId != null && filter.CategoryId != Guid.Empty, p => p.CategoryId == filter.CategoryId);

            var totalCount = await query.CountAsync();

            var items = totalCount > 0 ?
                           _mapper.Map<IReadOnlyList<ProductResponseModel>>(await query.OrderAndPaging(filter).ToListAsync())
                            : new List<ProductResponseModel>();

            return new PaginationResult<ProductResponseModel>(items, filter.PageNumber ?? 1, filter.PageSize ?? 200, totalCount);
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

        public async Task<Product> GetBySlug(string slug)
        {
            var product = await _productRepository.GetFirstAsync(
                predicate: p => p.Slug == slug,
                include: p => p.Include(p => p.Category)
                .Include(p => p.FileEntities)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag));

            if ((product == null)) throw new ResourceNotFoundException(typeof(Product));

            return product;
        }

        public async Task<BaseResponseModel> UpdateCountDownloadFileAsync(Guid fileId)
        {
            var fileEntity = await _fileRepository.GetFirstAsync(p => p.Id == fileId);

            if ((fileEntity == null)) throw new ResourceNotFoundException(typeof(FileEntity));
            fileEntity.DownloadCount++;
            await _fileRepository.UpdateAsync(fileEntity);

            return new BaseResponseModel { Id = fileId };
        }
    }
}
