using AutoMapper;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminCategoryController : AdminBaseController<Category, CategoryRequestModel, CategoryResponseModel>
    {
        public AdminCategoryController(ICategoryService categoryService, IMapper mapper) : base(categoryService, mapper)
        {
        }
    }
}
