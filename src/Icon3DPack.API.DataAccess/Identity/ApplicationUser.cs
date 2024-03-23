using Microsoft.AspNetCore.Identity;

namespace Icon3DPack.API.DataAccess.Identity;

public class ApplicationUser : IdentityUser {
    public int FullName { get; set; }
}
