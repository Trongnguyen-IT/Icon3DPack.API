using AutoMapper;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequestModel, Category>()
                .ForMember(p => p.CategoryTags, pp => pp.MapFrom(r => r.Tags.Select(t => new CategoryTag { TagId = t.Id })));

            CreateMap<Category, CategoryResponseModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.CategoryTags.Select(pt => pt.Tag)))
                .ForMember(p => p.ProductAmount, opt => opt.MapFrom(p => p.Products.Count));
        }
    }
}
