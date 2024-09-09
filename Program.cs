using Serilog;
using FastEndpoints;
using System.Reflection;
using FastEndpoints.Security;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
using Auth.Shared.RequestPipeline;
using Auth.Shared.DependencyInjections;

internal class Program
{
    private static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            builder.Services.RegisterOpenTelemetry();
            builder.AddSerilog();

            builder.Services.RegisterGlobalServices(builder.Configuration);
            builder.Services.AddServiceDiscovery(o => o.UseConsul());

            builder.Services.RegisterDBContext(builder.Configuration);
            builder.Services.RegisterRepositories();

            builder.Services.AddJWT(builder.Configuration);
            builder.Services.AddAuthorization();

            builder.Services.AddFastEndpoints(o =>
                 o.IncludeAbstractValidators = true);

            builder.Services.AddSwagger();

            builder.Services.AddMediatR(config =>
                 config.RegisterServicesFromAssembly(assembly));

            builder.Services.ApplyBehaviors();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            WebApplication app = builder.Build();
            {
                app.UseMiddleware<CorrelationIdLoggingMiddleware>();
                app.UseSerilogRequestLogging();

                app.UseExceptionHandler();

                await app.ApplyMigrations();

                app.UseJwtRevocation<TokenValidationMiddleware>();
                app.UseAuthentication();
                app.UseAuthorization();

                app.UseFastEndpoints();
                app.UseSwagger();

                await app.RunAsync();
            }
        }
    }
}