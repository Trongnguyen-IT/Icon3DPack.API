using AutoMapper;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class ProductController : BaseAPIController<Product, ProductRequestModel, ProductResponseModel>
    {
        public ProductController(IProductService productService, IMapper mapper) : base(productService, mapper)
        {
        }
    }
}
