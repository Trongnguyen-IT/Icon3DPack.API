using AutoMapper;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagRequestModel, Tag>();
            CreateMap<Tag, TagResponseModel>();
        }
    }
}