using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminProductController : AdminBaseController<Product, ProductRequestModel, ProductResponseModel>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public AdminProductController(IProductService productService, IMapper mapper) : base(productService, mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        //public override async Task<IActionResult> CreateAsync(ProductRequestModel model)
        //{
        //    return Ok(ApiResult<ProductResponseModel>.Success(_mapper.Map<ProductResponseModel>((await _productService.AddAsync(model)))));
        //}

        public override async Task<IActionResult> UpdateAsync(Guid id, ProductRequestModel model)
        {
            return Ok(ApiResult<ProductResponseModel>.Success(_mapper.Map<ProductResponseModel>((await _productService.UpdateAsync(model)))));
        }
    }
}
