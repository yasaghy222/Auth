using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Auth.Shared.DependencyInjections
{
	public static class OpenTelemetryInjection
	{
		public static IServiceCollection RegisterOpenTelemetry(this IServiceCollection services)
		{
			services.AddOpenTelemetry()
         				.ConfigureResource(resource => resource.AddService("Auth.Api"))
         				.WithTracing(tracing =>
         				{
					         tracing.AddAspNetCoreInstrumentation()
						         .AddHttpClientInstrumentation()
						         .AddSqlClientInstrumentation(o => o.SetDbStatementForText = true)
						         .AddGrpcClientInstrumentation();

					         tracing.AddOtlpExporter();
	         			});

			return services;
		}
	}
}