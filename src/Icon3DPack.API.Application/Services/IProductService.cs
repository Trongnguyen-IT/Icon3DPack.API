using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess;

namespace Icon3DPack.API.Application.Services
{
    public interface IProductService : IBaseService<Product>
    {
        Task<PaginatedList<Product>> ProductFilter(string? name, string? categoryId, string? sortOrdere, int? pageNumber, int? pageSize);
    }
}
