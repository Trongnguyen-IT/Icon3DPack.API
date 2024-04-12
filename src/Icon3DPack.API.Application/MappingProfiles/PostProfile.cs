using AutoMapper;
using Icon3DPack.API.Application.Models.Post;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostRequestModel, Post>();
            CreateMap<Post, PostResponseModel>();
        }
    }
}