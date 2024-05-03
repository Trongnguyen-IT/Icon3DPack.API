namespace Icon3DPack.API.Application.Models.User
{
    public class ProfileResponseModel
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool? ReceiveEmailNotification { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
