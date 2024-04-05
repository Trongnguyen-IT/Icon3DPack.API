using AutoMapper;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestModel, Product>();
            CreateMap<Product, ProductResponseModel>().ForMember(p => p.CategoryName, s => s.MapFrom(pp => pp.Category.Name));
        }
    }
}
