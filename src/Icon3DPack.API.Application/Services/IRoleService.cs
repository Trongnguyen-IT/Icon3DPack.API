using Microsoft.AspNetCore.Identity;

namespace Icon3DPack.API.Application.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<IdentityRole>> GetAll();
        Task<IdentityResult> Assign(int userId, int roleId);
    }
}
