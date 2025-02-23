using Jwtify.Core.Abstractions;
using Jwtify.Core.Builders;
using Jwtify.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jwtify.Core.Extensions
{
    public static class JwtServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtHelper(this IServiceCollection services, Action<JwtOptionsBuilder> configure)
        {
            var builder = new JwtOptionsBuilder();
            configure(builder);
            var options = builder.Build();

            services.AddSingleton(options);
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ITokenValidator, TokenValidator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            return services;
        }
    }
}