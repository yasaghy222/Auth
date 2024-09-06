using FastEndpoints.Swagger;

namespace Auth.Shared.DependencyInjections
{
    public static class SwaggerDocInjections
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.SwaggerDocument();
            return services;
        }
    }
}