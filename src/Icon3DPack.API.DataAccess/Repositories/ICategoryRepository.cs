using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.DataAccess.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task UpdateCategoryTagsAsync(Guid id);
    }
}
