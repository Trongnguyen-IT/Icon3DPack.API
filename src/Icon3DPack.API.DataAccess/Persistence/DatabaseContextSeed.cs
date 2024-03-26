using Icon3DPack.API.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;

namespace Icon3DPack.API.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };

            await userManager.CreateAsync(user, "Admin@123");
        }

        await context.SaveChangesAsync();
    }
}
