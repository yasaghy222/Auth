using Auth.Shared.Extensions;
using Auth.Features.Users.Services;
using Auth.Shared.RequestPipeline;
using Microsoft.AspNetCore.Authorization;
using Auth.Features.Users.CommandQuery.Commands.Login;
using Auth.Contracts.Common;

namespace Auth.Shared.DependencyInjections
{
	public static class GlobalServicesInjections
	{
		public static IServiceCollection RegisterGlobalServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IHashService, HashService>();
			services.AddSingleton<ITokenService, TokenService>();

			services.RegisterRedis(configuration);
			services.AddSingleton<ISessionService, SessionService>();

			services.AddScoped<LoginByPasswordHandler>();

			services.AddHttpContextAccessor();

			services.AddSingleton<IUserClaimsInfo, UserClaimsInfo>();
			services.AddSingleton<IAuthorizationMiddlewareResultHandler, PermissionMiddleware>();
			return services;
		}
	}
}