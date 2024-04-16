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
            return await _baseRepository.GetFirstAsync(p=>p.Slug == slug);
        }

        public override async Task<List<Post>> GetAllAsync()
        {
            return (await  base.GetAllAsync()).OrderBy(p=>p.Order).ThenBy(p=>p.CreatedTime).ToList();
        }
    }
}
