using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.CommandQuery.Commands.Create;

namespace Auth.Features.Users.EndPoints.Create
{
    public class CreateEndPoint(
        ISender sender,
        ILogger<CreateEndPoint> logger)
        : Endpoint<UserCreateDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<CreateEndPoint> _logger = logger;

        public override void Configure()
        {
            Post("/user");
            AllowAnonymous();
        }

        public override async Task<IResult> ExecuteAsync(UserCreateDto dto, CancellationToken ct)
        {
            CreateCommand command = dto.MapToCommand();
            _logger.LogInformation("Command: {command}", command.ToJson());

            ErrorOr<Created> result = await _sender.Send(command, ct);

            return result.Match(
                created => Results.Created(),
                errors =>
                {
                    _logger.LogWarning(UserErrors.CreateLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}