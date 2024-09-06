using Auth.Features.Users.EndPoints.Create;
using Auth.Features.Users.EndPoints.Login;
using FluentValidation;

namespace Auth.Shared.DependencyInjections
{
	public static class ValidatorsInjections
	{
		public static IServiceCollection RegisterValidators(this IServiceCollection services)
		{

			return services;
		}
	}
}