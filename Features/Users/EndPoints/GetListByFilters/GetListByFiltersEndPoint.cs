using MediatR;
using FastEndpoints;
using Auth.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.CommandQuery.Queries.GetListByFilters;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.EndPoints.GetListByFilters
{
    public class GetListByFiltersEndPoint(
        ISender sender,
        ILogger<GetListByFiltersEndPoint> logger)
        : Endpoint<GetListByFiltersDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<GetListByFiltersEndPoint> _logger = logger;

        public override void Configure()
        {
            Get("/user/list/filter");
            Description(b => b
                   .Accepts<GetListByFiltersDto>("application/json+custom")
                   .Produces<UsersResponse>(200, "application/json+custom")
                   .ProducesProblemFE(400)
                   .ProducesProblemFE<InternalErrorResponse>(500));
        }

        public override async Task<IResult> ExecuteAsync([FromQuery] GetListByFiltersDto dto, CancellationToken ct)
        {
            GetListByFiltersQuery query = dto.MapToQuery();
            _logger.LogInformation("Query: {query}", query.ToJson());

            UsersResponse response = await _sender.Send(query, ct);
            return Results.Ok(response);
        }
    }
}