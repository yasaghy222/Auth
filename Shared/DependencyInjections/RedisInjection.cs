using StackExchange.Redis;

namespace Auth.Shared.DependencyInjections
{
    public static class RedisInjection
    {
        public static IServiceCollection RegisterRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                string? defUrl = configuration["BaseUrl:Redis"];
                string? defPassword = configuration["Settings:RedisPassword"];


                string? url = Environment.GetEnvironmentVariable("REDIS_HOST") ?? defUrl;
                string? password = Environment.GetEnvironmentVariable("REDIS_PASSWORD") ?? defPassword;

                string connectionString = $"{url},password={password}";

                return ConnectionMultiplexer.Connect(connectionString);
            });

            return services;
        }
    }
}