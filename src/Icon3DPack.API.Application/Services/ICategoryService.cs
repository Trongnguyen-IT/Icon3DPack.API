using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<PaginationResult<CategoryResponseModel>> GetAllAsync(BaseFilterDto filter);
        Task<Category> UpdateAsync(CategoryRequestModel model);
        Task<Category> GetBySlug(string slug);
        //Task IncreateProductAmountAsync(Guid categoryId);
        //Task DecreaseProductAmountAsync(Guid categoryId);
    }
}
