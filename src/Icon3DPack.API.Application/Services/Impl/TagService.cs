using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class TagService : BaseService<Tag>, ITagService
    {
        public TagService(IBaseRepository<Tag> baseRepository) : base(baseRepository)
        {
        }
    }
}
