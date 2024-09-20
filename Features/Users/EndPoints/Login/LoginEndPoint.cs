using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.CommandQuery.Commands.Login;

namespace Auth.Features.Users.EndPoints.Login
{
    public class LoginEndPoint(
        ISender sender,
        ILogger<LoginEndPoint> logger)
        : Endpoint<LoginDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<LoginEndPoint> _logger = logger;

        public override void Configure()
        {
            Post(UserConstes.Login_Resource_Url);
            Description(b => b
                .Accepts<LoginDto>("application/json")
                .Produces(200)
                .ProducesProblemFE(400)
                .Produces<InternalErrorResponse>(500));
            AllowAnonymous();
        }

        public override async Task<IResult> ExecuteAsync(LoginDto dto, CancellationToken ct)
        {
            string ip = HttpContext.Connection.
                RemoteIpAddress?.ToString() ?? string.Empty;

            LoginCommand command = dto.MapToCommand(ip);
            _logger.LogInformation("Command: {command}", command.ToJson());

            ErrorOr<TokenResponse> result = await _sender.Send(command, ct);

            return result.Match(
                tokens => Results.Ok(tokens),
                errors =>
                {
                    _logger.LogWarning(UserErrors.CreateLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}