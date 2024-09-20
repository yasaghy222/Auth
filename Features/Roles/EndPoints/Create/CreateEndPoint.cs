using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Roles.Contracts.Mappings;
using Auth.Features.Roles.CommandQuery.Commands.Create;

namespace Auth.Features.Roles.EndPoints.Create
{
    public class CreateEndPoint(
        ISender sender,
        ILogger<CreateEndPoint> logger)
        : Endpoint<RoleCreateDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<CreateEndPoint> _logger = logger;

        public override void Configure()
        {
            Post(RoleConstes.Create_Resource_Url);
            Permissions(RoleConstes.Create_Permission_Id);
            Description(b => b
                .Accepts<RoleCreateDto>("application/json")
                .Produces(200, typeof(string))
                .Produces(401)
                .Produces(403)
                .ProducesProblemFE(400)
                .Produces<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync(
            RoleCreateDto dto, CancellationToken ct)
        {
            CreateCommand command = dto.MapToCommand();
            _logger.LogInformation("Command: {command}", command.ToJson());

            ErrorOr<Ulid> result = await _sender.Send(command, ct);

            return result.Match(
                id => Results.Ok(id),
                errors =>
                {
                    _logger.LogWarning(OrganizationErrors.CreateLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}