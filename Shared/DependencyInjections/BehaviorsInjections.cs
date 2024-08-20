using MediatR;
using Auth.Shared.RequestPipeline;

namespace Auth.Shared.DependencyInjections;

public static class BehaviorsInjections
{
    public static void ApplyBehaviors(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));



        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
    }
}
