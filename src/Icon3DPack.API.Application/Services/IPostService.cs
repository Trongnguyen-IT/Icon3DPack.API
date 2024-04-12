using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface IPostService : IBaseService<Post>
    {
        Task<Post> GetBySlug(string slug);
    }
}
