using Auth.Features.Users.EndPoints.Create;
using FluentValidation;

namespace Auth.Shared.DependencyInjections
{
	public static class ValidatorsInjections
	{
		public static IServiceCollection RegisterValidators(this IServiceCollection services)
		{
			services.AddScoped<IValidator<UserCreateDto>, UserCreateValidation>();

			return services;
		}
	}
}