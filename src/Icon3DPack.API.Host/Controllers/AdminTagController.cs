using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminTagController : AdminBaseController<Tag, TagRequestModel, TagResponseModel>
    {
        private readonly ITagService _tagService;
        public AdminTagController(ITagService tagService, IMapper mapper) : base(tagService, mapper)
        {
            _tagService = tagService;
        }

        [HttpPost("tags")]
        public async Task<IActionResult> GetAll(BaseFilterDto filter)
        {
            return Ok(ApiResult<PaginationResult<TagResponseModel>>.Success(await _tagService.GetAll(filter)));
        }
    }
}
