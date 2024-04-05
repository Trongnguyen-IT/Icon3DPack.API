using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Persistence;

namespace Icon3DPack.API.DataAccess.Repositories.Impl
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context) : base(context)
        {
            _context= context;
        }
    }
}
