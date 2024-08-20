using FluentValidation;

namespace Auth.Shared.DependencyInjections
{
	public static class ValidatorsInjections
	{
		public static IServiceCollection RegisterValidators(this IServiceCollection services)
		{
			// services.AddScoped<IValidator<>, >();

			return services;
		}
	}
}