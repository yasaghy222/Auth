using Microsoft.EntityFrameworkCore;
using Auth.Data;
using Auth.Shared.Extensions;
using Steeltoe.Extensions.Configuration;

namespace Auth.Shared.DependencyInjections
{
	public static class DBContextInjections
	{
		public static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
		{
			string? defName = configuration["Db:Name"];
			string? defHost = configuration["Db:Host"];
			string? defUser = configuration["Db:User"];
			string? defPass = configuration["Db:Pass"];

			string? dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
			string? dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
			string? dbUser = Environment.GetEnvironmentVariable("DB_User") ?? defUser;
			string? dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;

			string connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID={defUser};Password={dbPass};Initial Catalog={dbName};";

			services.AddDbContext<AuthDBContext>(options => options.UseSqlServer(connectionString));

			return services;
		}
	}
}