using Icon3DPack.API.DataAccess.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Icon3DPack.API.Application.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(ApplicationUser user, IEnumerable<string> roles, IConfiguration configuration)
    {
        var secretKey = configuration.GetValue<string>("JwtConfiguration:SecretKey");
        var issuer = configuration.GetValue<string>("JwtConfiguration:Issuer");
        var audience = configuration.GetValue<string>("JwtConfiguration:Audience");

        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>(roles.Select(p => new Claim(ClaimTypes.Role, p)))
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
               issuer: issuer,
               audience: audience,
               signingCredentials: credentials,
               claims: claims,
               expires: DateTime.UtcNow.AddDays(10));

        var token = tokenHandler.WriteToken(tokenDescriptor);

        return token;
    }

    //public static string GenerateRefreshToken()
    //{
    //    var randomNumber = new byte[32];
    //    using var rng = RandomNumberGenerator.Create();
    //    rng.GetBytes(randomNumber);
    //    return Convert.ToBase64String(randomNumber);
    //}

    public static string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret)
    {
        // Implement logic to generate a new access token from the refresh token
        // Verify the refresh token and extract necessary information (e.g., user ID)
        // Then generate a new access token

        // For demonstration purposes, return a new token with an extended expiry
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(15), // Extend expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
