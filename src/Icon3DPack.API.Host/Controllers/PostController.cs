using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Post;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Application.Services.Impl;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class PostController : BaseAPIController<Post, PostRequestModel, PostResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostController(IPostService postService, IMapper mapper) : base(postService, mapper)
        {
            _mapper = mapper;
            _postService = postService;
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            return Ok(ApiResult<PostResponseModel>.Success(_mapper.Map<PostResponseModel>(await _postService.GetBySlug(slug))));
        }

        [HttpPost("posts")]
        public async Task<IActionResult> GetAll(BaseFilterDto filter)
        {
            return Ok(ApiResult<PaginationResult<PostResponseModel>>.Success(await _postService.GetAll(filter)));
        }
    }
}
