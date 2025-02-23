namespace Jwtify.Core.Abstractions;

public interface ITokenValidator
{
    bool ValidateToken(string token, out Dictionary<string, object> claims);
}
