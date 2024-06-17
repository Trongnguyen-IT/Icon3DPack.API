using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.User;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.DataAccess.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers;

public class AdminUsersController : AdminController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AdminUsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("users")]
    public async Task<IActionResult> GetAll(BaseFilterDto filter)
    {
        return Ok(ApiResult<PaginationResult<ApplicationUser>>.Success(await _userService.GetAll(filter)));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(ApiResult<UserResponsetModel>.Success(await _userService.GetAsync(id)));
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserRequestModel model)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _userService.CreateAsync(model)));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UserRequestModel model)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _userService.UpdateAsync(id, model)));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _userService.DeleteAccount(id)));
    }
}