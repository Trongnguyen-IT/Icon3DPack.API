using AutoMapper;
using Icon3DPack.API.Application.Models.Post;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminPostController : AdminBaseController<Post, PostRequestModel, PostResponseModel>
    {
        public AdminPostController(IBaseService<Post>  baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
