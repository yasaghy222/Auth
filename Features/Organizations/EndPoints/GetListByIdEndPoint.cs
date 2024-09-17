using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.Organizations.CommandQuery.Queries.GetById;

namespace Auth.Features.Organizations.EndPoints
{

    public class GetListByIdEndPoint(
            ISender sender,
            ILogger<GetListByIdEndPoint> logger)
            : EndpointWithoutRequest<IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<GetListByIdEndPoint> _logger = logger;

        public override void Configure()
        {
            Get(OrganizationConstes.Get_Id_Resource_Url);
            Permissions(OrganizationConstes.Get_Id_Permission_Id);
            Description(b => b
                   .Accepts<Ulid>()
                   .Produces<OrganizationResponse>(200, "application/json")
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

            GetByIdQuery query = new((Ulid)id);
            _logger.LogInformation("Query: {query}", query.ToJson());

            ErrorOr<OrganizationResponse> response = await _sender.Send(query, ct);
            return response.Match(
                value =>
                {
                    return Results.Ok(value);
                },
                error => OrganizationErrors.NotFound().ToResult()
            );
        }
    }
}