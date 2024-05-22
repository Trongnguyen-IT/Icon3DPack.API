using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Identity;
using System.Security.Claims;

namespace Icon3DPack.API.Application.Services
{
    public interface IRefreshTokenService
    {
        Task SaveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenById(Guid id);
        RefreshToken GenerateRefreshToken(ApplicationUser user);
        RefreshToken ValidateRefreshToken(string token);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
