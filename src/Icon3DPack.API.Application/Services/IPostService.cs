using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Post;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface IPostService : IBaseService<Post>
    {
        Task<PaginationResult<PostResponseModel>> GetAll(BaseFilterDto filter);
        Task<Post> GetBySlug(string slug);
    }
}
