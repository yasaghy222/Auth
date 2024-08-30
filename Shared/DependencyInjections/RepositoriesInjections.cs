using Auth.Domain.Entities;
using Auth.Data.Repositories;
using Auth.Contracts.Common;
using Auth.Features.Users.Repositories;
using Auth.Features.Organizations.Repositories;

namespace Auth.Shared.DependencyInjections
{
	public static class RepositoriesInjections
	{
		public static IServiceCollection RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

			services.AddScoped<IRepository<User, Ulid>, UserRepository>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddScoped<IRepository<Organization, Ulid>, OrganizationRepository>();
			services.AddScoped<IOrganizationRepository, OrganizationRepository>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}