using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.Organizations.CommandQuery.Queries.GetByFilters;

namespace Auth.Features.Organizations.EndPoints.GetListByFilters
{

    public class GetListByFiltersEndPoint(
            ISender sender,
            ILogger<GetListByFiltersEndPoint> logger,
            IUserClaimsInfo userClaimsInfo)
            : Endpoint<OrganizationGetListByFiltersDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<GetListByFiltersEndPoint> _logger = logger;
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;

        public override void Configure()
        {
            Get(OrganizationConstes.Get_List_Filter_Resource_Url);
            Permissions(OrganizationConstes.Get_List_Filter_Permission_Id);
            Description(b => b
                   .Accepts<OrganizationGetListByFiltersDto>()
                   .Produces<OrganizationsResponse>(200, "application/json")
                   .ProducesProblemFE(400)
                   .ProducesProblemFE(401)
                   .ProducesProblemFE(403)
                   .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync([FromQuery] OrganizationGetListByFiltersDto dto, CancellationToken ct)
        {
            IEnumerable<Ulid> userOrganizationsIds =
                _userClaimsInfo.UserOrganizations?.Select(i => i.OrganizationId) ?? [];

            GetListByFiltersQuery query = dto.MapToQuery(userOrganizationsIds);
            _logger.LogInformation("Query: {query}", query.ToJson());

            OrganizationsResponse response = await _sender.Send(query, ct);
            return Results.Ok(response);
        }
    }
}