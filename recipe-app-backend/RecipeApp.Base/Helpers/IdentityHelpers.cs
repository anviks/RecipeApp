using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RecipeApp.Base.Helpers;

public static class IdentityHelpers
{
    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience, int expiresInSeconds)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        DateTime expires = DateTime.Now.AddSeconds(expiresInSeconds);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public static bool IsJwtValid(string token, string key, string issuer, string audience)
    {
        var validationParams = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidateIssuer = true,

            ValidAudience = audience,
            ValidateAudience = true,

            ValidateLifetime = false
        };

        try
        {
            new JwtSecurityTokenHandler().ValidateToken(token, validationParams, out _);
        }
        catch
        {
            return false;
        }

        return true;

    }
}