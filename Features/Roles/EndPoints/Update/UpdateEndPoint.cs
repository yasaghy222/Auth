using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Roles.Contracts.Mappings;
using Auth.Features.Roles.CommandQuery.Commands.Update;

namespace Auth.Features.Roles.EndPoints.Update
{
    public class RoleEndPoint(
        ISender sender,
        ILogger<RoleEndPoint> logger)
        : Endpoint<RoleUpdateDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<RoleEndPoint> _logger = logger;

        public override void Configure()
        {
            Put(RoleConstes.Update_Resource_Url);
            Permissions(RoleConstes.Update_Permission_Id);
            Description(b => b
                .Accepts<RoleUpdateDto>("application/json")
                .Produces(200)
                .Produces(401)
                .Produces(403)
                .ProducesProblemDetails(400)
                .ProducesProblemFE<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync(
            RoleUpdateDto dto, CancellationToken ct)
        {
            UpdateCommand command = dto.MapToCommand();
            _logger.LogInformation("Command: {command}", command.ToJson());

            ErrorOr<Updated> result = await _sender.Send(command, ct);

            return result.Match(
                created => Results.Ok(),
                errors =>
                {
                    _logger.LogWarning(RoleErrors.UpdateLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}