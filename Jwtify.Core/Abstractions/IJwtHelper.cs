using Jwtify.Core.Models;

namespace Jwtify.Core.Abstractions;

public interface IJwtHelper
{
    TokenResult GenerateToken(Dictionary<string, object> claims);
    bool ValidateToken(string token, out Dictionary<string, object> claims);
    string? GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken);
}
