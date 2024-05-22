using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface IProductService : IBaseService<Product>
    {
        Task<PaginationResult<Product>> GetAllPagingAsync();
        Task<PaginationResult<ProductResponseModel>> ProductFilter(ProductFilter filter);
       // Task<Product> AddAsync(ProductRequestModel createTodoListModel);
        Task<Product> UpdateAsync(ProductRequestModel updateTodoListModel);
        Task<BaseResponseModel> DownloadFileAsync(Guid productId);
    }
}
