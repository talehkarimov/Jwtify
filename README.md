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

## Prerequisites ##
Ensure that you have the required dependencies:
**Microsoft.IdentityModel.Tokens** (Version **8.6.0** or above)
```bash
dotnet add package Microsoft.IdentityModel.Tokens --version 8.6.0
```

## Usage
### Configure JWT Options ###
In your **Startup.cs** or **Program.cs** file, configure the JWT helper by specifying the secret key, issuer, audience, expiration time, and signing algorithm.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddJwtHelper(options =>
    {
        options.WithSecretKey("YourSecretKey")
               .WithAudience("YourAudience")
               .WithIssuer("YourIssuer")
               .WithExpiration(60) // expiration in minutes
               .WithAlgorithm(SigningAlgorithm.HS256);
    });
}
```

### Use the JWT Middleware for Authentication(Optional) ###
**This is the key feature** of **Jwtify.Core**: **The JWT Middleware automatically validates the JWT token** in the request headers for you.
You don't need to manually validate the token in each service or controller. Just add the JWT middleware to your pipeline, and it will ensure that the incoming requests have valid JWT tokens.

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseMiddleware<JwtMiddleware>();
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

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null) return null;

        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
        var claims = new Dictionary<string, object>
        {
            { "userId", user.Id.ToString() },
            { "email", user.Email }
        };
        var generatedToken = jwtService.GenerateToken(claims);
        return result.Succeeded ? generatedToken.AccessToken : null;
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
**Note: The JWT Middleware simplifies the process by automatically validating the token in each incoming HTTP request. However, if you choose not to use the middleware, you can still manually validate the token as shown above.**


### Usage of Refresh Tokens in Jwtify.Core ###
**Enable Refresh Token**
If you want to enable the use of refresh tokens in your application, you need to configure the AddJwtHelper method with the refresh token option.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddJwtHelper(options =>
    {
        options.WithSecretKey("YourSecretKey")
               .WithAudience("YourAudience")
               .WithIssuer("YourIssuer")
               .WithExpiration(60) // expiration in minutes
               .WithAlgorithm(SigningAlgorithm.HS256)
               .WithRefreshToken(true); // Enable refresh token generation
    });
}
```

**Using Refresh Token in Your Endpoints:**
You can send the refresh token as part of your HTTP headers in your requests. Here's an example of how to pass the refresh token when making API calls.

**Example: Sending the Refresh Token in the Request Header**
When calling an endpoint where you need to refresh the access token, you will include the refresh token in the Refresh-Token header:
```bash
POST /api/your-endpoint
Authorization: Bearer <access_token>
Refresh-Token: <your_refresh_token>
```

**Response Headers with New Tokens**
Once the access token and refresh token are regenerated, they will be sent back in the HTTP response headers:
```bash
HTTP/1.1 200 OK
Content-Type: application/json
New-Access-Token: <new_access_token>
New-Refresh-Token: <new_refresh_token>
```
**Note:** If you enable the refresh token feature by calling **WithRefreshToken(true)** and use the default middleware (**JwtMiddleware**), the refresh token functionality will be automatically handled. However, if you prefer not to use the middleware, you can skip the middleware setup and implement your own custom logic to validate the access token and refresh token manually.


