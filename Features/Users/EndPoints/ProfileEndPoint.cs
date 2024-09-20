using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.CommandQuery.Queries.Profile;

namespace Auth.Features.Users.EndPoints
{

    public class ProfileEndPoint(
            ISender sender,
            ILogger<ProfileEndPoint> logger,
            IUserClaimsInfo userClaimsInfo)
            : EndpointWithoutRequest<IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<ProfileEndPoint> _logger = logger;
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;

        public override void Configure()
        {
            Get(UserConstes.Get_Profile_Resource_Url);
            Description(b => b
                .Produces<UserResponse>(200, "application/json")
                .Produces(401)
                .Produces<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync(CancellationToken ct)
        {
            IEnumerable<Ulid> userOrganizationsIds =
                _userClaimsInfo.UserOrganizations?.Select(i => i.OrganizationId) ?? [];

            Ulid id = _userClaimsInfo.UserInfo?.Id ?? Ulid.NewUlid();

            ProfileQuery query = new(id, userOrganizationsIds);
            _logger.LogInformation("Query: {query}", query.ToJson());

            ErrorOr<UserResponse> response = await _sender.Send(query, ct);
            return response.Match(
                value => Results.Ok(value),
                error => UserErrors.NotFound().ToResult()
            );
        }
    }
}