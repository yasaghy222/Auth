using Auth.Features.Users.Services;
using Auth.Shared.Extensions;

namespace Auth.Shared.DependencyInjections
{
	public static class GlobalServicesInjections
	{
		public static IServiceCollection RegisterGlobalServices(this IServiceCollection services)
		{
			services.AddScoped<IHashService, HashService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<ISessionService, SessionService>();

			return services;
		}
	}
}