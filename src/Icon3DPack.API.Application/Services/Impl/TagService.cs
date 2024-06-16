using AutoMapper;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class TagService : BaseService<Tag>, ITagService
    {
        private readonly IBaseRepository<Tag> _repository;
        private readonly IMapper _mapper;
        public TagService(IBaseRepository<Tag> repository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<TagResponseModel>> GetAll(BaseFilterDto filter)
        {
            var query = _repository
                .GetAll()
                .WhereIf(filter.Keyword.IsNotNullOrEmpty(), p => p.Name!.Contains(filter.Keyword!));

            var totalCount = await query.CountAsync();

            var items = _mapper.Map<IReadOnlyList<TagResponseModel>>(await query.OrderAndPaging(filter).ToListAsync());

            return new PaginationResult<TagResponseModel>(items, filter.PageNumber ?? 1, filter.PageSize ?? 10, totalCount);
        }
    }
}
