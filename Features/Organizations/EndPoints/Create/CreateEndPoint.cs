using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.CommandQuery.Commands.Create;

namespace Auth.Features.Organizations.EndPoints.Create
{
    public class CreateEndPoint(
        ISender sender,
        ILogger<CreateEndPoint> logger)
        : Endpoint<OrganizationCreateDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<CreateEndPoint> _logger = logger;

        public override void Configure()
        {
            Post(OrganizationConstes.Create_Resource_Url);
            Permissions(OrganizationConstes.Create_Permission_Id);
            Description(b => b
                .Accepts<OrganizationCreateDto>("application/json")
                .Produces(200, typeof(string))
                .Produces(401)
                .Produces(403)
                .ProducesProblemFE(400)
                .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync(
            OrganizationCreateDto dto, CancellationToken ct)
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