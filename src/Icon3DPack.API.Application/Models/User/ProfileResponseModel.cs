namespace Icon3DPack.API.Application.Models.User
{
    public class ProfileResponseModel
    {
        
        public string FullName { get; set; } = string.Empty;
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool ReceiveEmailNotification { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
