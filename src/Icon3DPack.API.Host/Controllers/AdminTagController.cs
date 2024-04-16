using AutoMapper;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminTagController : AdminBaseController<Tag, TagRequestModel, TagResponseModel>
    {
        public AdminTagController(ITagService tagService, IMapper mapper) : base(tagService, mapper)
        {
        }
    }
}
