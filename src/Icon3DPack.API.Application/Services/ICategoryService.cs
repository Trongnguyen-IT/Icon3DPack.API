using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<Category> UpdateAsync(CategoryRequestModel updateTodoListModel);
        Task IncreateProductAmountAsync(Guid id);
        Task DecreaseProductAmountAsync(Guid id);
    }
}
