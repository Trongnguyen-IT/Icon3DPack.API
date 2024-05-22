using Amazon.Auth.AccessControlPolicy;
using Azure.Core;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.Authorization;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.User;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Application.Services.Impl;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Icon3DPack.API.Host.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(IUserService userService,
        IRefreshTokenService refreshTokenService,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _userService = userService;
        _refreshTokenService = refreshTokenService;
        _userManager = userManager;
        _configuration = configuration;
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

    [HttpPut("{id:guid}/update-notification/{isNotification:bool}")]
    [Authorize]
    public async Task<IActionResult> UpdateNotificationAsync(Guid id, bool isNotification)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _userService.UpdateNotificationAsync(id, isNotification)));
    }

    [HttpDelete("{id:guid}/delete-account")]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _userService.DeleteAccount(id)));
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel model)
    {
        try
        {
            var principal = _refreshTokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            if (principal == null)
            {
                throw new Exception("Invalid access token!");
            }

            var validatedRefreshToken = _refreshTokenService.ValidateRefreshToken(model.RefreshToken);
            if (validatedRefreshToken == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(validatedRefreshToken.UserId);

            if (user == null || validatedRefreshToken.ExpiresAt <= DateTime.Now)
            {
                throw new Exception("Refresh token has expired!");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = JwtHelper.GenerateToken(user, roles, _configuration);

            return Ok(new { AccessToken = token });
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
