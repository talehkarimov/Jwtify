using Jwtify.Core.Enums;

namespace Jwtify.Core.Models;

public sealed class JwtOptions
{
    public string SecretKey { get; set; } = "default_secret";
    public string Issuer { get; set; } = "default_issuer";
    public string Audience { get; set; } = "default_audience";
    public int ExpirationMinutes { get; set; } = 60;
    public SigningAlgorithm Algorithm { get; set; } = SigningAlgorithm.HS256;
    public bool EnableRefreshToken { get; set; } = false;
}
