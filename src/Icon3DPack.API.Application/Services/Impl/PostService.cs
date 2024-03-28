using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class PostService : BaseService<Post>, IPostService
    {
        public PostService(IBaseRepository<Post> baseRepository) : base(baseRepository)
        {
        }
    }
}
