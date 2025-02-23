using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Jwtify.Core.Strategies;

public sealed class RsaStrategy : ISigningStrategy
{
    public SigningCredentials GetSigningCredentials(string secretKey)
    {
        var rsa = RSA.Create();
        rsa.ImportFromPem(secretKey.ToCharArray());
        return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
    }
}
