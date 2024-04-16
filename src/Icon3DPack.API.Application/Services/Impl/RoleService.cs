using Icon3DPack.API.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Amazon.S3.Util.S3EventNotification;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Assign(int userId, int roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            return await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task<IEnumerable<IdentityRole>> GetAll()
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}
