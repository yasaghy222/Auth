using Serilog;
using FastEndpoints;
using System.Reflection;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
using Auth.Shared.RequestPipeline;
using Auth.Shared.DependencyInjections;
using FastEndpoints.Swagger;
using NJsonSchema.Generation.TypeMappers;
using NJsonSchema;

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
            builder.Services.RegisterValidators();


            builder.Services.ApplyJWT(builder.Configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddFastEndpoints(o => o.IncludeAbstractValidators = true);
            builder.Services.SwaggerDocument(
                o => o.DocumentSettings =
                    s => s.SchemaSettings.TypeMappers.Add(
                        new PrimitiveTypeMapper(
                            typeof(Ulid),
                            schema =>
                            {
                                schema.Type = JsonObjectType.String;
                                schema.MinLength = 32;
                                schema.UniqueItems = true;
                                schema.Example = Ulid.NewUlid();
                                schema.Format = "ulid";
                            }
                        )
                    )
                );

            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            builder.Services.ApplyBehaviors();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            WebApplication app = builder.Build();
            {
                app.UseMiddleware<CorrelationIdLoggingMiddleware>();
                app.UseSerilogRequestLogging();

                app.UseExceptionHandler();

                await app.ApplyMigrations();

                app.UseAuthentication();
                app.UseAuthorization();
                app.UseFastEndpoints();
                app.UseSwaggerGen();

                await app.RunAsync();
            }
        }
    }
}