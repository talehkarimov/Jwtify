# Jwtify.Core
Jwtify.Core is a lightweight, easy-to-use library for working with JSON Web Tokens (JWT) in .NET applications. It provides a flexible and customizable JWT helper with simple configuration and integration.

## Features
- JWT token generation and validation
- Customizable token options (e.g., secret key, issuer, expiration, algorithm)
- Support for refresh tokens
- Simple middleware for integrating JWT validation into your application
- Dependency injection support for easy integration into your services

- ## Installation

You can install the Jwtify.Core package via NuGet:

```bash
dotnet add package Jwtify.Core
```

## Usage
### Configure JWT Options ###
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddJwtHelper(options =>
    {
        options.WithSecretKey("YourSecretKey")
               .WithIssuer("YourIssuer")
               .WithExpiration(60) // expiration in minutes
               .WithAlgorithm(SigningAlgorithm.HS256);
    });
}
```

### Inject JWT Helper and Use in Your Application ###
### Example: Generating a JWT Token ###
```csharp
public class MyService
{
    private readonly IJwtHelper _jwtHelper;

    public MyService(IJwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }

    public string GenerateToken()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "UserName")
        };

        return _jwtHelper.GenerateToken(claims);
    }
}

```

### Example: Validating a JWT Token ###
```csharp
public class MyService
{
    private readonly IJwtHelper _jwtHelper;

    public MyService(IJwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }

    public bool ValidateToken(string token)
    {
        return _jwtHelper.ValidateToken(token, out var claims); // Validate the token and get the claims
    }
}
```

### Example: Generating a Refresh Token ###
```csharp
public class MyService
{
    private readonly IJwtHelper _jwtHelper;
    private readonly IRefreshTokenService _refreshTokenService;

    public MyService(IJwtHelper jwtHelper, IRefreshTokenService refreshTokenService)
    {
        _jwtHelper = jwtHelper;
        _refreshTokenService = refreshTokenService;
    }

    public string GenerateRefreshToken(string userId)
    {
        return _refreshTokenService.GenerateRefreshToken(userId); // Generate a refresh token for the user
    }
}
```

### Use the JWT Middleware for Authentication ###
```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseMiddleware<JwtMiddleware>();
}
```

