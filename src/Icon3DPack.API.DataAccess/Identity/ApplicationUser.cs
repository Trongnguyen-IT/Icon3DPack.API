using Microsoft.AspNetCore.Identity;

namespace Icon3DPack.API.DataAccess.Identity;

public class ApplicationUser : IdentityUser {
    public string? AvatarUrl { get; set; }
    public bool ReceiveEmailNotification { get; set; }
}
