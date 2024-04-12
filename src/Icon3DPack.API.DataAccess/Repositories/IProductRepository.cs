using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.DataAccess.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PaginatedList<Product>> ProductFilter(string? name, string? categoryId, string? sortOrder, int? pageSize, int? pageNumber);
    }
}
