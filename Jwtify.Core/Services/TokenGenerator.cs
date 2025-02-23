using Jwtify.Core.Abstractions;
using Jwtify.Core.Models;
using Jwtify.Core.Strategies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Jwtify.Core.Services;

public sealed class TokenGenerator(JwtOptions options, ISigningStrategy signingStrategy) : ITokenGenerator
{
    public TokenResult GenerateToken(Dictionary<string, object> claims)
    {
        var securityKey = signingStrategy.GetSigningCredentials(options.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.Select(c => new Claim(c.Key, c.Value.ToString()))),
            Expires = DateTime.UtcNow.AddMinutes(options.ExpirationMinutes),
            Issuer = options.Issuer,
            Audience = options.Audience,
            SigningCredentials = securityKey
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new TokenResult
        {
            AccessToken = accessToken,
            RefreshToken = options.EnableRefreshToken ? GenerateRefreshToken() : null
        };
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
