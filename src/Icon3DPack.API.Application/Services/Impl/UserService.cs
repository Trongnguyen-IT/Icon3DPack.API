using AutoMapper;
using Icon3DPack.API.Application.Common.Email;
using Icon3DPack.API.Application.Exceptions;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.User;
using Icon3DPack.API.Application.Templates;
using Icon3DPack.API.AwsS3.Models.AwsS3;
using Icon3DPack.API.AwsS3.Services;
using Icon3DPack.API.DataAccess.Identity;
using Icon3DPack.API.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit.Encodings;
using Newtonsoft.Json.Linq;
using System.Text;

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

    public UserService(IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ITemplateService templateService,
        IEmailService emailService,
        IClaimService claimService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _templateService = templateService;
        _emailService = emailService;
        _claimService = claimService;
    }

    public async Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel)
    {
        var user = _mapper.Map<ApplicationUser>(createUserModel);
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
}
