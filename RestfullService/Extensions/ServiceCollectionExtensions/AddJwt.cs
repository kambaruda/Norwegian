using Core.JwtAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestfullService.Extensions.ConfigurationExtensions;
using System.Text;

namespace RestfullService.Extensions.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration config)
        {
            var jwtOptions = config.GetOptionsSection<JwtOptions>(JwtOptions.SectionName);
            services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));

            services.AddJwtAuthentication(jwtOptions);
            services.AddAuthHandlers();

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = GetTokenValidationParameters(jwtOptions);
                options.Events = GetJwtEvents();
            });

            return services;
        }

        private static TokenValidationParameters GetTokenValidationParameters(JwtOptions jwtOptions)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };
        }

        private static JwtBearerEvents GetJwtEvents()
        {
            return new JwtBearerEvents
            {
                OnAuthenticationFailed = async context =>
                {
                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var message = context.Exception switch
                        {
                            SecurityTokenExpiredException => context.Exception.Message,
                            SecurityTokenInvalidSignatureException => "Invalid signature",
                            SecurityTokenNoExpirationException => "Token missing expiration",
                            _ => "Token invalid"
                        };

                        await context.Response.WriteAsJsonAsync(new { message });
                    }
                },
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsJsonAsync(new { message = "Token missing or malformed" });
                    }
                }
            };
        }

    }
}
