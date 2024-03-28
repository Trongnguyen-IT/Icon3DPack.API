using AutoMapper;
using Icon3DPack.API.Application.Models.Post;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class PostController : BaseAPIController<Post, PostRequestModel, PostResponseModel>
    {
        public PostController(IPostService postService, IMapper mapper) : base(postService, mapper)
        {
        }
    }
}
