using MediatR;
using FastEndpoints;
using System.Security.Claims;
using Auth.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.CommandQuery.Queries.GetListByFilters;
using Auth.Shared.RequestPipeline;

namespace Auth.Features.Users.EndPoints.GetListByFilters
{

    public class GetListByFiltersEndPoint(
            ISender sender,
            ILogger<GetListByFiltersEndPoint> logger,
            IHttpContextAccessor httpContextAccessor)
            : Endpoint<GetListByFiltersDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<GetListByFiltersEndPoint> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public override void Configure()
        {
            Get("/user/list/filter");
            PreProcessor<PermissionValidation<GetListByFiltersDto>>();
            Description(b => b
                   .Accepts<GetListByFiltersDto>()
                   .Produces<UsersResponse>(200, "application/json")
                   .ProducesProblemFE(400)
                   .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync([FromQuery] GetListByFiltersDto dto, CancellationToken ct)
        {
            Claim? userOrganizationsClaim = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(i => i.Type == UserClaimNames.UserOrganizations);

            IEnumerable<UserOrganizationInfo>? userOrganizations =
                userOrganizationsClaim?.Value.FromJson<IEnumerable<UserOrganizationInfo>>();

            IEnumerable<Ulid> userOrganizationsIds =
                userOrganizations?.Select(i => i.OrganizationId) ?? [];

            GetListByFiltersQuery query = dto.MapToQuery(userOrganizationsIds);
            _logger.LogInformation("Query: {query}", query.ToJson());

            UsersResponse response = await _sender.Send(query, ct);
            return Results.Ok(response);
        }
    }
}