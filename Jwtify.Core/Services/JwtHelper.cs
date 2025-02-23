using Jwtify.Core.Abstractions;
using Jwtify.Core.Models;

namespace Jwtify.Core.Services;

public sealed class JwtHelper(ITokenGenerator tokenGenerator, ITokenValidator tokenValidator, IRefreshTokenService refreshTokenService) : IJwtHelper
{
    public TokenResult GenerateToken(Dictionary<string, object> claims)
    => tokenGenerator.GenerateToken(claims);

    public bool ValidateToken(string token, out Dictionary<string, object> claims)
        => tokenValidator.ValidateToken(token, out claims);

    public string? GenerateRefreshToken()
        => refreshTokenService.GenerateRefreshToken();
}
