using Jwtify.Core.Abstractions;
using System.Security.Cryptography;

namespace Jwtify.Core.Services;

public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly HashSet<string> _validRefreshTokens = new();

    public string GenerateRefreshToken()
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        _validRefreshTokens.Add(refreshToken);
        return refreshToken;
    }

    public bool ValidateRefreshToken(string refreshToken)
    {
        return _validRefreshTokens.Contains(refreshToken);
    }
}
