using Serilog;
using FastEndpoints;
using System.Reflection;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
using Microsoft.OpenApi.Models;
using Auth.Shared.RequestPipeline;
using Auth.Shared.DependencyInjections;

internal class Program
{
    private static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            builder.AddSerilog();

            builder.Services.AddServiceDiscovery(o => o.UseConsul());

            builder.Services.RegisterGlobalServices();

            builder.Services.RegisterDBContext(builder.Configuration);
            builder.Services.RegisterRepositories();

            builder.Services.RegisterValidators();

            builder.Services.AddFastEndpoints(o => o.IncludeAbstractValidators = true);
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth", Version = "v1" });
             });

            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            builder.Services.ApplyBehaviors();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.RegisterOpenTelemetry();
        }

        WebApplication app = builder.Build();
        {
            app.UseMiddleware<CorrelationIdLoggingMiddleware>();
            app.UseSerilogRequestLogging();

            app.AddSwagger();

            await app.ApplyMigrations();

            app.UseHttpsRedirection();
            app.UseExceptionHandler();
            app.UseFastEndpoints();

            await app.RunAsync();
        }
    }
}