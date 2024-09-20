using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Roles.CommandQuery.Commands.Delete;

namespace Auth.Features.Roles.EndPoints
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
            Delete(RoleConstes.Delete_Resource_Url);
            Permissions(RoleConstes.Delete_Permission_Id);
            Description(b => b
                .Accepts<Ulid>()
                .Produces(200)
                .Produces(401)
                .Produces(403)
                .ProducesProblemDetails(400)
                .Produces<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync(CancellationToken ct)
        {
            Ulid? id = Route<Ulid>("id");
            if (id == null)
            {
                return RoleErrors.NotFound().ToResult();
            }

            DeleteCommand command = new((Ulid)id);
            _logger.LogInformation("id: {id}", id.ToString());

            ErrorOr<Deleted> result = await _sender.Send(command, ct);

            return result.Match(
                deleted => Results.Ok(),
                errors =>
                {
                    _logger.LogWarning(RoleErrors.DeleteLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}