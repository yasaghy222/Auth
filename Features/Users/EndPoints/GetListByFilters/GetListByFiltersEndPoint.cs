using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.CommandQuery.Queries.GetListByFilters;

namespace Auth.Features.Users.EndPoints.GetListByFilters
{

    public class GetListByFiltersEndPoint(
            ISender sender,
            ILogger<GetListByFiltersEndPoint> logger,
            IUserClaimsInfo userClaimsInfo)
            : Endpoint<UserGetListByFiltersDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<GetListByFiltersEndPoint> _logger = logger;
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;

        public override void Configure()
        {
            Get(UserConstes.Get_List_Filter_Resource_Url);
            Permissions(UserConstes.Get_List_Filter_Permission_Id);
            Description(b => b
                .Accepts<UserGetListByFiltersDto>()
                .Produces<UsersResponse>(200, "application/json")
                .ProducesProblemFE(400)
                .ProducesProblemFE(401)
                .ProducesProblemFE(403)
                .Produces<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync([FromQuery] UserGetListByFiltersDto dto, CancellationToken ct)
        {
            IEnumerable<Ulid> userOrganizationsIds =
                _userClaimsInfo.UserOrganizations?.Select(i => i.OrganizationId) ?? [];

            GetListByFiltersQuery query = dto.MapToQuery(userOrganizationsIds);
            _logger.LogInformation("Query: {query}", query.ToJson());

            UsersResponse response = await _sender.Send(query, ct);
            return Results.Ok(response);
        }
    }
}