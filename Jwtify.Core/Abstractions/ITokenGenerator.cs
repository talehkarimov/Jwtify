using Jwtify.Core.Models;

namespace Jwtify.Core.Abstractions;

public interface ITokenGenerator
{
    TokenResult GenerateToken(Dictionary<string, object> claims);
}
