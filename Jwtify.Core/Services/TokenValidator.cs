using Jwtify.Core.Abstractions;
using Jwtify.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Jwtify.Core.Services;

public sealed class TokenValidator(JwtOptions options) : ITokenValidator
{
    public bool ValidateToken(string token, out Dictionary<string, object> claims)
    {
        claims = new Dictionary<string, object>();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            IssuerSigningKey = key
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            claims = principal.Claims.ToDictionary(c => c.Type, c => (object)c.Value);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
