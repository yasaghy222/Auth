using Serilog;
using FastEndpoints;
using System.Reflection;
using OpenTelemetry.Trace;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
using OpenTelemetry.Resources;
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

            builder.Services.RegisterValidators();
            builder.Services.AddServiceDiscovery(o => o.UseConsul());

            builder.Services.RegisterDBContext(builder);
            builder.Services.RegisterRepositories();


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

            builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("Auth.Api"))
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation()
                     .AddHttpClientInstrumentation()
                     .AddSqlClientInstrumentation(o => o.SetDbStatementForText = true)
                     .AddGrpcClientInstrumentation();

                tracing.AddOtlpExporter();
            });
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

            app.Run();
        }
    }
}