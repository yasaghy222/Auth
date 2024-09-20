using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.CommandQuery.Commands.Update;

namespace Auth.Features.Organizations.EndPoints.Update
{
    public class UpdateEndPoint(
        ISender sender,
        ILogger<UpdateEndPoint> logger)
        : Endpoint<OrganizationUpdateDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<UpdateEndPoint> _logger = logger;

        public override void Configure()
        {
            Put(OrganizationConstes.Update_Resource_Url);
            Permissions(OrganizationConstes.Update_Permission_Id);
            Description(b => b
                .Accepts<OrganizationUpdateDto>("application/json")
                .Produces(200)
                .Produces(401)
                .Produces(403)
                .ProducesProblemFE(400)
                .Produces<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync(
            OrganizationUpdateDto dto, CancellationToken ct)
        {
            UpdateCommand command = dto.MapToCommand();
            _logger.LogInformation("Command: {command}", command.ToJson());

            ErrorOr<Updated> result = await _sender.Send(command, ct);

            return result.Match(
                created => Results.Ok(),
                errors =>
                {
                    _logger.LogWarning(OrganizationErrors.UpdateLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}