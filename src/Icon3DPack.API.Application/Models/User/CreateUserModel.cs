using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.User;

public class CreateUserModel
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}

public class CreateUserResponseModel : BaseResponseModel { }
