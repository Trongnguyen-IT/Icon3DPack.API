using Amazon.Runtime.Internal.Transform;
using AutoMapper;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Post;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class PostService : BaseService<Post>, IPostService
    {
        private readonly IBaseRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public PostService(IBaseRepository<Post> baseRepository, IMapper mapper) : base(baseRepository)
        {
            _postRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<Post> GetBySlug(string slug)
        {
            return await _postRepository.GetFirstAsync(p => p.Slug == slug);
        }

        public override async Task<IReadOnlyList<Post>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync(orderBy: p => p.OrderByDescending(p => p.Order).ThenByDescending(p => p.CreatedTime));
        }

        public async Task<PaginationResult<PostResponseModel>> GetAll(BaseFilterDto filter)
        {
            var query = _postRepository
                .GetAll()
                .WhereIf(filter.Keyword.IsNotNullOrEmpty(), p => p.Name.ToLower().Contains(filter.Keyword!.ToLower()));

            var totalCount = await query.CountAsync();

            var items = _mapper.Map<IReadOnlyList<PostResponseModel>>(await query.OrderAndPaging(filter).ToListAsync());

            return new PaginationResult<PostResponseModel>(items, filter.PageNumber ?? 1, filter.PageSize ?? 10, totalCount);
        }
    }
}
