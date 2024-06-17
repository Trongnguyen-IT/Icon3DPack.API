using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Application.Models.User;

public class CreateUserModel 
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedTime { get; set; } = DateTime.UtcNow;
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedTime { get; set; }
}

public class CreateUserResponseModel : BaseResponseModel { }
