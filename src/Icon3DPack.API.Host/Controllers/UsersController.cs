using Amazon.Auth.AccessControlPolicy;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.User;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.DataAccess.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(CreateUserModel createUserModel)
    {
        return Ok(ApiResult<CreateUserResponseModel>.Success(await _userService.CreateAsync(createUserModel)));
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginUserModel loginUserModel)
    {
        var result = ApiResult<LoginResponseModel>.Success(await _userService.LoginAsync(loginUserModel));
       
        return Ok(result);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
    {
        return Ok(ApiResult<ConfirmEmailResponseModel>.Success(
            await _userService.ConfirmEmailAsync(confirmEmailModel)));
    }

    [HttpPut("{id:guid}/change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(Guid id, ChangePasswordModel changePasswordModel)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(
            await _userService.ChangePasswordAsync(id, changePasswordModel)));
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfileAsync()
    {
        return Ok(ApiResult<ProfileResponseModel>.Success(await _userService.GetProfileAsync()));
    }

    [HttpPut("update-profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfileAsync(UpdateUserModel updateUserModel)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _userService.UpdateProfileAsync(updateUserModel)));
    }
}
