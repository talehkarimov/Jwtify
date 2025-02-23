namespace Jwtify.Core.Abstractions;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken);
}
