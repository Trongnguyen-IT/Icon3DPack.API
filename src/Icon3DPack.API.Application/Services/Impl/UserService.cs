using AutoMapper;
using Icon3DPack.API.Application.Common.Email;
using Icon3DPack.API.Application.Exceptions;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Application.Models.User;
using Icon3DPack.API.Application.Templates;
using Icon3DPack.API.AwsS3.Models.AwsS3;
using Icon3DPack.API.AwsS3.Services;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Identity;
using Icon3DPack.API.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit.Encodings;
using Newtonsoft.Json.Linq;
using System.Text;
using static Amazon.S3.Util.S3EventNotification;

namespace Icon3DPack.API.Application.Services.Impl;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITemplateService _templateService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IClaimService _claimService;
    private readonly IRefreshTokenService _refreshTokenService;

    public UserService(IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ITemplateService templateService,
        IEmailService emailService,
        IClaimService claimService,
        IRefreshTokenService refreshTokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _templateService = templateService;
        _emailService = emailService;
        _claimService = claimService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<CreateUserResponseModel> RegisterAsync(CreateUserModel createUserModel)
    {
        var user = _mapper.Map<ApplicationUser>(createUserModel);
        //user.Id = Guid.NewGuid().ToString();
        user.UserName = createUserModel.Email;
        var result = await _userManager.CreateAsync(user, createUserModel.Password);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var code = EncodeHelper.Base64Encode(token);

        var emailTemplate = await _templateService.GetTemplateAsync(TemplateConstants.ConfirmationEmail);

        var emailBody = _templateService.ReplaceInTemplate(emailTemplate,
            new Dictionary<string, string> { { "{Endpoint}", "http://localhost:3000" }, { "{UserId}", user.Id }, { "{Token}", code } });

        await _emailService.SendEmailAsync(EmailMessage.Create(user.Email, emailBody, "[Icon3DPack.API]Confirm your email"));

        return new CreateUserResponseModel
        {
            Id = Guid.Parse((await _userManager.FindByEmailAsync(user.Email)).Id)
        };
    }

    public async Task<UserResponsetModel> GetAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        return _mapper.Map<UserResponsetModel>(user);

    }

    public async Task<BaseResponseModel> CreateAsync(UserRequestModel model)
    {
        var user = _mapper.Map<ApplicationUser>(model);
        user.UserName = model.Email;
        user.EmailConfirmed = true;
        user.CreatedBy = _claimService.GetUserId();
        var result = await _userManager.CreateAsync(user, "Admin@123");

        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        return new BaseResponseModel
        {
            Id = Guid.Parse((await _userManager.FindByEmailAsync(user.Email)).Id)
        };
    }

    public async Task<BaseResponseModel> UpdateAsync(Guid id, UserRequestModel model)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        var updateUser = _mapper.Map(model, user);

        await _userManager.UpdateAsync(updateUser);

        return new BaseResponseModel
        {
            Id = Guid.Parse(user.Id)
        };
    }

    public async Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userManager.FindByEmailAsync(loginUserModel.Email);

        if (user == null)
            throw new NotFoundException("Username or password is incorrect");

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginUserModel.Password, false, false);

        if (!signInResult.Succeeded)
            throw new BadRequestException("Username or password is incorrect");

        var roles = await _userManager.GetRolesAsync(user);

        var token = JwtHelper.GenerateToken(user, roles, _configuration);

        //var refreshToken = _refreshTokenService.GenerateRefreshToken(user);

        //await _refreshTokenService.SaveRefreshToken(refreshToken);

        return new LoginResponseModel
        {
            Username = user.UserName,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
    {
        var user = await _userManager.FindByIdAsync(confirmEmailModel.UserId);

        if (user == null)
            throw new UnprocessableRequestException("Your verification link is incorrect");

        var token = EncodeHelper.Base64Decode(confirmEmailModel.Token);
        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            throw new UnprocessableRequestException("Your verification link has expired");

        return new ConfirmEmailResponseModel
        {
            Confirmed = true
        };
    }

    public async Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword,
                changePasswordModel.NewPassword);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        return new BaseResponseModel
        {
            Id = Guid.Parse(user.Id)
        };
    }

    public async Task<ProfileResponseModel> GetProfileAsync()
    {
        var userId = _claimService.GetUserId();

        if (userId == null)
            throw new NotFoundException("User does not exist anymore");


        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        return _mapper.Map<ProfileResponseModel>(user); ;
    }

    public async Task<BaseResponseModel> UpdateProfileAsync(UpdateUserModel updateUserModel)
    {
        var user = await _userManager.FindByEmailAsync(updateUserModel.Email);
        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        var updateUser = _mapper.Map(updateUserModel, user);

        await _userManager.UpdateAsync(updateUser);

        return new BaseResponseModel
        {
            Id = Guid.Parse(user.Id)
        };
    }

    public async Task<BaseResponseModel> UpdateNotificationAsync(Guid userId, bool isNotification)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        user.ReceiveEmailNotification = isNotification;

        await _userManager.UpdateAsync(user);

        return new BaseResponseModel
        {
            Id = Guid.Parse(user.Id)
        };
    }

    public async Task<BaseResponseModel> DeleteAccount(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        await _userManager.DeleteAsync(user);

        return new BaseResponseModel
        {
            Id = Guid.Parse(user.Id)
        };
    }

    public async Task<PaginationResult<ApplicationUser>> GetAll(BaseFilterDto filter)
    {
        var query = _userManager.Users
               .WhereIf(filter.Keyword.IsNotNullOrEmpty(), p => p.Email!.Contains(filter.Keyword!));

        var totalCount = await query.CountAsync();

        var items = await query.OrderAndPaging(filter).ToListAsync();

        return new PaginationResult<ApplicationUser>(items, filter.PageNumber ?? 1, filter.PageSize ?? 10, totalCount);
    }
}
