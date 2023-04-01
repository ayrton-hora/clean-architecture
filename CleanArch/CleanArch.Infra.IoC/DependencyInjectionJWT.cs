using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArch.Infra.IoC
{
    public static class DependencyInjectionJWT
    {
        public static IServiceCollection AddInfraestructureJWT(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Environment.GetEnvironmentVariable("CLEAN_ARCH_ISSUER") ??
                        throw new NullReferenceException("Issuer is not configured"),

                    ValidAudience = Environment.GetEnvironmentVariable("CLEAN_ARCH_AUDIENCE") ??
                        throw new NullReferenceException("Audience is not configured"),

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                        Environment.GetEnvironmentVariable("CLEAN_ARCH_KEY") ??
                                            throw new NullReferenceException("Private security key is not configured"))),

                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

            return services;
        }
    }
}
