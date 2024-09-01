using System.Text;
using Microsoft.IdentityModel.Tokens;
using Auth.Features.Organizations.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Auth.Shared.DependencyInjections;

public static class Authenticate
{
    public static void ApplyJWT(this IServiceCollection services, IConfiguration configuration)
    {
        string? defSecretKey = configuration["Settings:SecretKey"];

        string secretKey = defSecretKey ?? Environment
            .GetEnvironmentVariable("Settings_SecretKey") ?? "";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "Auth.Service",
                ValidAudiences = OrganizationDataSeeding.InitialItems.Select(i => i.Title),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        services.AddAuthorization();
    }
}
