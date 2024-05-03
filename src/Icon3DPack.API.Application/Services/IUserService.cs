using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.User;

namespace Icon3DPack.API.Application.Services;

public interface IUserService
{
    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);

    Task<ProfileResponseModel> GetProfileAsync();

    Task<BaseResponseModel> UpdateProfileAsync(UpdateUserModel updateUserModel);

    Task<BaseResponseModel> UpdateNotificationAsync(Guid userId, bool isNotification);
    Task<BaseResponseModel> DeleteAccount(Guid userId);
}
