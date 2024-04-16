using Icon3DPack.API.DataAccess.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Icon3DPack.API.Application.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(ApplicationUser user,IEnumerable<string> roles, IConfiguration configuration)
    {
        var secretKey = configuration.GetValue<string>("JwtConfiguration:SecretKey");
        var issuer = configuration.GetValue<string>("JwtConfiguration:Issuer");
        var audience = configuration.GetValue<string>("JwtConfiguration:Audience");

        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenHandler = new JwtSecurityTokenHandler();


        //var tokenDescriptor = new SecurityTokenDescriptor
        //{
        //    Issuer = issuer,
        //    Audience = audience,
        //    Subject = new ClaimsIdentity(new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim(ClaimTypes.Email, user.Email)
        //    }),
        //    Expires = DateTime.UtcNow.AddDays(7),
        //    SigningCredentials =
        //        new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //    Claims=roles.Select(p=>new Dictionary<string,string> { { ClaimTypes.Role, p  } } )
        //};

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
               expires: DateTime.UtcNow.AddHours(3));

        var token = tokenHandler.WriteToken(tokenDescriptor);

        return token;
    }
}
