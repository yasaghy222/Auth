using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.CommandQuery.Commands.ChangePassword;

namespace Auth.Features.Users.EndPoints.ChangePassword
{
    public class ChangePasswordEndPoint(
        ISender sender,
        ILogger<ChangePasswordEndPoint> logger)
        : Endpoint<UserChangePasswordDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<ChangePasswordEndPoint> _logger = logger;

        public override void Configure()
        {
            Patch("/user/change-password");
            Description(b => b
              .Accepts<UserChangePasswordDto>()
              .Produces(200)
              .Produces(401)
              .ProducesProblemFE(400)
              .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync(
            UserChangePasswordDto dto, CancellationToken ct)
        {
            ChangePasswordCommand command = dto.MapToCommand();
            _logger.LogInformation("Command: {command}", command.ToJson());

            ErrorOr<Updated> result = await _sender.Send(command, ct);

            return result.Match(
                updated => Results.Ok(),
                errors =>
                {
                    _logger.LogWarning(UserErrors.ChangePasswordLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}