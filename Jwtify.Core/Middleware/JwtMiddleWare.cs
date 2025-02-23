using Jwtify.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Jwtify.Core.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtHelper jwtHelper)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null && jwtHelper.ValidateToken(token, out _))
        {
            context.Items["User"] = jwtHelper.ValidateToken(token, out var claims) ? claims : null;
        }

        await _next(context);
    }
}