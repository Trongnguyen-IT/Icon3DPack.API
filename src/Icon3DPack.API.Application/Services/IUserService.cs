using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.User;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.DataAccess.Identity;

namespace Icon3DPack.API.Application.Services;

public interface IUserService
{
    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<UserResponsetModel> GetAsync(Guid id);
    Task<CreateUserResponseModel> RegisterAsync(CreateUserModel model);
    Task<BaseResponseModel> CreateAsync(UserRequestModel model);
    Task<BaseResponseModel> UpdateAsync(Guid id, UserRequestModel model);

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);

    Task<ProfileResponseModel> GetProfileAsync();

    Task<BaseResponseModel> UpdateProfileAsync(UpdateUserModel updateUserModel);

    Task<BaseResponseModel> UpdateNotificationAsync(Guid userId, bool isNotification);
    Task<BaseResponseModel> DeleteAccount(Guid userId);
    Task<PaginationResult<ApplicationUser>> GetAll(BaseFilterDto filter);
}

