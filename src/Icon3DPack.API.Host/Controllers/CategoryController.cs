using AutoMapper;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class CategoryController : BaseAPIController<Category, CategoryRequestModel, CategoryResponseModel>
    {
        public CategoryController(IBaseService<Category> baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
