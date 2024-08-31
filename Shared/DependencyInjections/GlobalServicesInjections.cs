using Auth.Features.Users.CommandQuery.Commands.Login;
using Auth.Features.Users.Services;
using Auth.Shared.Extensions;

namespace Auth.Shared.DependencyInjections
{
	public static class GlobalServicesInjections
	{
		public static IServiceCollection RegisterGlobalServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IHashService, HashService>();
			services.AddScoped<ITokenService, TokenService>();

			services.RegisterRedis(configuration);
			services.AddScoped<ISessionService, SessionService>();

			services.AddScoped<LoginByPasswordHandler>();
			return services;
		}
	}
}