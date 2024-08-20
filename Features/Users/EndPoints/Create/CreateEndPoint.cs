using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Features.Users.CommandQuery.Commands.Create;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Shared.CustomErrors;
using Auth.Shared.Extensions;

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