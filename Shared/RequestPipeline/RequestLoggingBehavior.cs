using ErrorOr;
using MediatR;
using Serilog.Context;

namespace Auth.Shared.RequestPipeline
{
    internal sealed class RequestLoggingBehavior<TRequest, TResponse>
    (ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly ILogger<RequestLoggingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogInformation("Processing request {RequestName}", requestName);

            TResponse result = await next();

            if (result.IsError)
            {
                using (LogContext.PushProperty("Error", result.Errors, true))
                {
                    _logger.LogError("Completed request {RequestName} with error", requestName);
                }
            }
            else
            {
                _logger.LogInformation("Completed request {RequestName}", requestName);
            }

            return result;
        }
    }
}
