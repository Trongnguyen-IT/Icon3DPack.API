using AutoMapper;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestModel, Product>()
                .ForMember(p => p.ProductTags, pp => pp.MapFrom(r => r.Tags.Select(t => new ProductTag { TagId = t.Id })));

            CreateMap<Product, ProductResponseModel>()
                .ForMember(p => p.CategoryName, s => s.MapFrom(pp => pp.Category.Name))
                .ForMember(p => p.DownloadCount, s => s.MapFrom(pp => pp.FileEntities.Sum(p=>p.DownloadCount)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.ProductTags.Select(pt => pt.Tag)));
        }
    }
}
