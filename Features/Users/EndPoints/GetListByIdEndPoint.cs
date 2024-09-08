using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.CommandQuery.Queries.GetById;

namespace Auth.Features.Users.EndPoints
{

    public class GetListByIdEndPoint(
            ISender sender,
            ILogger<GetListByIdEndPoint> logger,
            IUserClaimsInfo userClaimsInfo)
            : EndpointWithoutRequest<IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<GetListByIdEndPoint> _logger = logger;
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;

        public override void Configure()
        {
            Get(UserConstes.Get_Id_Resource_Url);
            Permissions(UserConstes.Get_Id_Permission_Id);
            Description(b => b
                   .Accepts<Ulid>()
                   .Produces<UsersResponse>(200, "application/json")
                   .ProducesProblemFE(400)
                   .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync(CancellationToken ct)
        {
            Ulid? id = Route<Ulid>("id");
            if (id == null)
            {
                return UserErrors.NotFound().ToResult();
            }

            IEnumerable<Ulid> userOrganizationsIds =
                _userClaimsInfo.UserOrganizations?.Select(i => i.OrganizationId) ?? [];

            GetByIdQuery query = new((Ulid)id, userOrganizationsIds);
            _logger.LogInformation("Query: {query}", query.ToJson());

            ErrorOr<UserResponse> response = await _sender.Send(query, ct);
            return response.Match(
                value => Results.Ok(value),
                error => UserErrors.NotFound().ToResult()
            );
        }
    }
}