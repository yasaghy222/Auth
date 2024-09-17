using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Organizations.CommandQuery.Commands.Delete;

namespace Auth.Features.Organizations.EndPoints
{
    public class DeleteEndPoint(
        ISender sender,
        ILogger<DeleteEndPoint> logger)
        : EndpointWithoutRequest<IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<DeleteEndPoint> _logger = logger;

        public override void Configure()
        {
            Delete(OrganizationConstes.Delete_Resource_Url);
            Permissions(OrganizationConstes.Delete_Permission_Id);
            Description(b => b
                .Accepts<Ulid>()
                .Produces(200)
                .Produces(401)
                .Produces(403)
                .ProducesProblemFE(400)
                .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync(CancellationToken ct)
        {
            Ulid? id = Route<Ulid>("id");
            if (id == null)
            {
                return OrganizationErrors.NotFound().ToResult();
            }

            DeleteCommand command = new((Ulid)id);
            _logger.LogInformation("id: {id}", id.ToString());

            ErrorOr<Deleted> result = await _sender.Send(command, ct);

            return result.Match(
                deleted => Results.Ok(),
                errors =>
                {
                    _logger.LogWarning(OrganizationErrors.DeleteLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}