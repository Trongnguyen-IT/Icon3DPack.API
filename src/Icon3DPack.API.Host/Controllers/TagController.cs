using AutoMapper;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class TagController : BaseAPIController<Tag, TagRequestModel, TagResponseModel>
    {
        public TagController(ITagService baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
