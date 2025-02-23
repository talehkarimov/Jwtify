using Jwtify.Core.Abstractions;
using Jwtify.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Jwtify.Core.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtOptions _jwtOptions;
    public JwtMiddleware(RequestDelegate next, JwtOptions jwtOptions)
    {
        _next = next;
        _jwtOptions = jwtOptions;
    }

    public async Task Invoke(HttpContext context, IJwtHelper jwtHelper, IRefreshTokenService refreshTokenService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token) || !jwtHelper.ValidateToken(token, out var claims))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid authorization token");
            return;
        }

        context.Items["User"] = claims;

        if (_jwtOptions.EnableRefreshToken)
        {
            var refreshToken = context.Request.Headers["Refresh-Token"].FirstOrDefault();

            if (string.IsNullOrEmpty(refreshToken))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Refresh token is missing.");
                return;
            }

            if (refreshTokenService.ValidateRefreshToken(refreshToken))
            {
                var tokenResult = jwtHelper.GenerateToken(claims);
                context.Response.Headers["New-Access-Token"] = tokenResult.AccessToken;

                if (!string.IsNullOrEmpty(tokenResult.RefreshToken))
                {
                    context.Response.Headers["New-Refresh-Token"] = tokenResult.RefreshToken;
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid refresh token");
                return;
            }
        }
        await _next(context);
    }

}