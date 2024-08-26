using Auth.Shared.Extensions;

namespace Auth.Shared.DependencyInjections
{
	public static class GlobalServicesInjections
	{
		public static IServiceCollection RegisterGlobalServices(this IServiceCollection services)
		{
			services.AddScoped<IHashService, HashService>();

			return services;
		}
	}
}