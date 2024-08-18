using Microsoft.EntityFrameworkCore;
using Auth.Data;

namespace Auth.Shared.DependencyInjections
{
	public static class DBContextInjections
	{
		public static IServiceCollection RegisterDBContext(this IServiceCollection services, WebApplicationBuilder builder)
		{
			string? defName = builder.Configuration["Db:Name"];
			string? defHost = builder.Configuration["Db:Host"];
			string? defUser = builder.Configuration["Db:User"];
			string? defPass = builder.Configuration["Db:Pass"];

			string? dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
			string? dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
			string? dbUser = Environment.GetEnvironmentVariable("DB_User") ?? defUser;
			string? dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;

			string connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID={defUser};Password={dbPass};Initial Catalog={dbName};";

			builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(connectionString));

			return services;
		}
	}
}