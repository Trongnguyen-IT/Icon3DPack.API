using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.User
{
    public class UserRequestModel : BaseAuditRequestModel
    {
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool? ReceiveEmailNotification { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; } = true;
    }

    public class UserResponsetModel : BaseAuditResponseModel
    {
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool? ReceiveEmailNotification { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
