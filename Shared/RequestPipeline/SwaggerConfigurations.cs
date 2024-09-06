using FastEndpoints.Swagger;

namespace Auth.Shared.RequestPipeline;

public static class SwaggerConfigurations
{
    public static void UseSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerGen();
        }
    }
}
