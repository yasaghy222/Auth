using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Microsoft.AspNetCore.Mvc;
using Auth.Features.Roles.Contracts.Mappings;
using Auth.Features.Roles.Contracts.Responses;
using Auth.Features.Roles.CommandQuery.Queries.GetByFilters;

namespace Auth.Features.Roles.EndPoints.GetListByFilters
{

    public class GetListByFiltersEndPoint(
        ISender sender,
        ILogger<GetListByFiltersEndPoint> logger)
        : Endpoint<RoleGetListByFiltersDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<GetListByFiltersEndPoint> _logger = logger;

        public override void Configure()
        {
            Get(RoleConstes.Get_List_Filter_Resource_Url);
            Permissions(RoleConstes.Get_List_Filter_Permission_Id);
            Description(b => b
                .Accepts<RoleGetListByFiltersDto>()
                .Produces<RolesResponse>(200, "application/json")
                .ProducesProblemFE(400)
                .ProducesProblemFE(401)
                .ProducesProblemFE(403)
                .Produces<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync(
            [FromQuery] RoleGetListByFiltersDto dto, CancellationToken ct)
        {
            GetListByFiltersQuery query = dto.MapToQuery();
            _logger.LogInformation("Query: {query}", query.ToJson());

            ErrorOr<RolesResponse> response = await _sender.Send(query, ct);

            return response.Match(
                list => Results.Ok(list),
                errors =>
                {
                    _logger.LogWarning(RoleErrors.GetByFilterLogMsg, errors);
                    return response.ToProblemDetails();
                });
        }
    }
}