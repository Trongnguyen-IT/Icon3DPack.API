using AutoMapper;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequestModel, Category>().ForMember(p => p.CategoryTags, pp => pp.MapFrom(r => r.TagIds.Select(t => new CategoryTag { TagId = t })));
            CreateMap<Category, CategoryResponseModel>().ForMember(p => p.Tags, pp => pp.MapFrom(c => c.CategoryTags.Select(ct => ct.Tag)));
        }
    }
}
