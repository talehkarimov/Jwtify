using Jwtify.Core.Enums;
using Jwtify.Core.Models;

namespace Jwtify.Core.Builders;

public sealed class JwtOptionsBuilder
{
    private readonly JwtOptions _options = new();
    public JwtOptionsBuilder WithSecretKey(string secretKey)
    {
        _options.SecretKey = secretKey;
        return this;
    }

    public JwtOptionsBuilder WithIssuer(string issuer)
    {
        _options.Issuer = issuer;
        return this;
    }
    public JwtOptionsBuilder WithExpiration(int minutes)
    {
        _options.ExpirationMinutes = minutes;
        return this;
    }

    public JwtOptionsBuilder WithAlgorithm(SigningAlgorithm algorithm)
    {
        _options.Algorithm = algorithm;
        return this;
    }

    public JwtOptions Build() => _options;
}
