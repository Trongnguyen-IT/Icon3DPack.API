using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface IProductService : IBaseService<Product>
    {
        Task<PaginationResult<ProductResponseModel>> GetAllAsync(BaseFilterDto filter);
        Task<PaginationResult<Product>> GetAllPagingAsync();
        Task<Product> GetBySlug(string slug);
        Task<PaginationResult<ProductResponseModel>> ProductFilter(ProductFilter filter);
        Task<Product> UpdateAsync(ProductRequestModel updateTodoListModel);
        Task<BaseResponseModel> UpdateCountDownloadFileAsync(Guid productId);
    }
}
