using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Identity;
using Icon3DPack.API.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class RefreshTokenService: IRefreshTokenService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IConfiguration  _configuration;

        public RefreshTokenService(DatabaseContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenById(Guid id)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(p => p.Id == id);
        }

        public RefreshToken ValidateRefreshToken(string token)
        {
            var refreshToken = _dbContext.RefreshTokens.FirstOrDefault(rt => rt.Token == token && rt.ExpiresAt > DateTime.UtcNow);
            return refreshToken;
        }

        public RefreshToken GenerateRefreshToken(ApplicationUser user)
        {
            var token = new RefreshToken
            {
                UserId = user.Id,
                Token = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddDays(_configuration.GetValue<double>("JwtConfiguration:RefreshTokenTTL"))
            };

            return token;
        }

        private string GenerateRefreshToken()
        {
            var refreshToken = getUniqueToken();

            return refreshToken;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                // ensure token is unique by checking against db
                var tokenIsUnique = !_dbContext.RefreshTokens.Any(u => u.Token == token);

                if (!tokenIsUnique)
                    return getUniqueToken();

                return token;
            }
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _configuration.GetValue<string>("JwtConfiguration:Issuer"),
                ValidAudience = _configuration.GetValue<string>("JwtConfiguration:Audience"),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtConfiguration:SecretKey"))),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
