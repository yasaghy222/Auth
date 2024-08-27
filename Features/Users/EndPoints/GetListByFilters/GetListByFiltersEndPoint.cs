using MediatR;
using FastEndpoints;
using Auth.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.CommandQuery.Queries.GetListByFilters;

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
            AllowAnonymous();
        }

        public override async Task<IResult> ExecuteAsync([FromQuery] GetListByFiltersDto dto, CancellationToken ct)
        {
            GetListByFiltersQuery query = dto.MapToQuery();
            _logger.LogInformation("Query: {query}", query.ToJson());

            return Results.Ok(await _sender.Send(query, ct));
        }
    }
}