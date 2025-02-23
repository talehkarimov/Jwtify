using Microsoft.IdentityModel.Tokens;

namespace Jwtify.Core.Strategies;

public interface ISigningStrategy
{
    SigningCredentials GetSigningCredentials(string secretKey);
}
