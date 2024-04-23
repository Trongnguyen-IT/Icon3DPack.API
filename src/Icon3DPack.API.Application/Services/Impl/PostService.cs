using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class PostService : BaseService<Post>, IPostService
    {
        private readonly IBaseRepository<Post> _baseRepository;

        public PostService(IBaseRepository<Post> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<Post> GetBySlug(string slug)
        {
            return await _baseRepository.GetFirstAsync(p => p.Slug == slug);
        }

        public override async Task<IReadOnlyList<Post>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync(orderBy: p => p.OrderByDescending(p => p.Order).ThenByDescending(p => p.CreatedTime));
        }
    }
}
