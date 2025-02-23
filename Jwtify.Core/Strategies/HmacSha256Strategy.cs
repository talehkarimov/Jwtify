using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jwtify.Core.Strategies;

public sealed class HmacSha256Strategy : ISigningStrategy
{
    public SigningCredentials GetSigningCredentials(string secretKey)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}
