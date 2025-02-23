using Jwtify.Core.Abstractions;
using Jwtify.Core.Builders;
using Jwtify.Core.Enums;
using Jwtify.Core.Services;
using Jwtify.Core.Strategies;
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

            if (options.Algorithm == SigningAlgorithm.HS256)
            {
                services.AddSingleton<ISigningStrategy>(provider =>
                    new HmacSha256Strategy());
            }
            else if (options.Algorithm == SigningAlgorithm.RS256)
            {
                services.AddSingleton<ISigningStrategy>(provider =>
                    new RsaStrategy()); 
            }

            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ITokenValidator, TokenValidator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            return services;
        }
    }
}