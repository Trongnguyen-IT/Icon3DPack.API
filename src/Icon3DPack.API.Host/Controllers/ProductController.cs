using AutoMapper;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Application.Services.Impl;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess;
using Icon3DPack.API.Host.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class ProductController : BaseAPIController<Product, ProductRequestModel, ProductResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;


        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger) : base(productService, mapper)
        {
            _mapper = mapper;
            _productService = productService;
            _logger = logger;
        }

        public override async Task<IActionResult> GetAll()
        {
            var paginationResult = await _productService.GetAllPagingAsync();

            var result = new PaginationResult<ProductResponseModel>(_mapper.Map<IReadOnlyList<ProductResponseModel>>(paginationResult.Items),
                paginationResult.PageNumber,
                paginationResult.PageSize,
                paginationResult.TotalCount);

            return Ok(ApiResult<PaginationResult<ProductResponseModel>>.Success(result));
        }

        [HttpPost("product-filter")]
        public async Task<IActionResult> ProductFilter([FromQuery] Dictionary<string, string> filter, int? pageNumber = 1, int? pageSize = 200)
        {
            string name = filter.GetValueOrDefault("name");
            string category = filter.GetValueOrDefault("categoryId");
            string sortOrder = filter.GetValueOrDefault("sortOrder");

            var paginationResult = await _productService.ProductFilter(filter, pageNumber: pageNumber ?? 1, pageSize: pageSize ?? 200);

            var result = new PaginationResult<ProductResponseModel>(_mapper.Map<IReadOnlyList<ProductResponseModel>>(paginationResult.Items),
                paginationResult.PageNumber,
                paginationResult.PageSize,
                paginationResult.TotalCount);

            return Ok(ApiResult<PaginationResult<ProductResponseModel>>.Success(result));
        }
    }
}
