using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface ICategoryService : IBaseService<Category>
    {
        //Task<Category> AddAsync(CategoryRequestModel model);
        Task<Category> UpdateAsync(CategoryRequestModel model);
        //Task IncreateProductAmountAsync(Guid categoryId);
        //Task DecreaseProductAmountAsync(Guid categoryId);
    }
}
