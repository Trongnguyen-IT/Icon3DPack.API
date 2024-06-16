using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Post;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminPostController : AdminBaseController<Post, PostRequestModel, PostResponseModel>
    {
        private readonly IPostService _postService;
        public AdminPostController(IPostService postService, IMapper mapper) : base(postService, mapper)
        {
            _postService = postService;
        }

        [HttpPost("posts")]
        public async Task<IActionResult> GetAll(BaseFilterDto filter)
        {
            return Ok(ApiResult<PaginationResult<PostResponseModel>>.Success(await _postService.GetAll(filter)));
        }
    }
}
