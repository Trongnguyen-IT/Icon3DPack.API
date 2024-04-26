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
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(p => p.CategoryTags, pp => pp.MapFrom(r => r.Tags.Select(t => new CategoryTag { TagId = t.Id })));

            CreateMap<Category, CategoryResponseModel>()
                .ForMember(c => c.ProductAmount, c => c.MapFrom(cc => cc.Products.Count));
        }
    }
}
