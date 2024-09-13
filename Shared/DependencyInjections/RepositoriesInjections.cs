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
			services.AddScoped<Features.Users.Repositories.IOrganizationRepository, UserRepository>();

			services.AddScoped<IRepository<Organization, Ulid>, OrganizationRepository>();
			services.AddScoped<Features.Organizations.Repositories.IOrganizationRepository, OrganizationRepository>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}