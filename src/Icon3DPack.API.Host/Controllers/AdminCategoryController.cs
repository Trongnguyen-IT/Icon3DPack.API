using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Category;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminCategoryController : AdminBaseController<Category, CategoryRequestModel, CategoryResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public AdminCategoryController(ICategoryService categoryService, IMapper mapper) : base(categoryService, mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public override async Task<IActionResult> UpdateAsync(Guid id, CategoryRequestModel updateTodoListModel)
        {
            if (id != updateTodoListModel.Id)
            {
                return BadRequest();
            }

            return Ok(ApiResult<CategoryResponseModel>.Success(
            _mapper.Map<CategoryResponseModel>(await _categoryService.UpdateAsync(updateTodoListModel))));
        }
    }
}
