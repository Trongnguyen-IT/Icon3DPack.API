using Icon3DPack.API.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;

namespace Icon3DPack.API.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Ensure required roles are created
        string[] roleNames = { "Admin", "Customer" };

        foreach (var roleName in roleNames)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                // Create the role if it doesn't exist
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }


        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true, CreatedTime = DateTime.UtcNow };

            IdentityResult result = await userManager.CreateAsync(user, "Admin@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        await context.SaveChangesAsync();
    }
}
