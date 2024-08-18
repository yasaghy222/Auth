using Auth.Data.Repositories;
using Auth.Contracts.Common;

namespace Auth.Shared.DependencyInjections
{
	public static class RepositoriesInjections
	{
		public static IServiceCollection RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));


			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}