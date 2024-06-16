using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface ITagService : IBaseService<Tag>
    {
        Task<PaginationResult<TagResponseModel>> GetAll(BaseFilterDto filter);
    }
}
