using Auth.Contracts.Common;
using Auth.Contracts.Response;
using Auth.Features.Organizations.Contracts.Requests;
using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.Organizations.EndPoints.GetListByFilters;
using Auth.Features.Organizations.CommandQuery.Queries.GetByFilters;

namespace Auth.Features.Organizations.Contracts.Mappings
{
    public static class GetListByFiltersMappings
    {
        private static IEnumerable<Ulid> GetUserOrganizationIds(
            this IEnumerable<Ulid>? filterIds,
            IEnumerable<Ulid> userOrganizationIds)
        {
            if (filterIds == null || !filterIds.Any())
                return userOrganizationIds;

            return userOrganizationIds.Filter(i => filterIds.Contains(i));
        }

        public static GetListByFiltersQuery MapToQuery(
            this OrganizationGetListByFiltersDto dto,
            IEnumerable<Ulid> userOrganizations)
        {
            return new()
            {
                Ids = dto.Ids.GetUserOrganizationIds(userOrganizations),
                IdOrderType = dto.IdOrderType,

                Title = dto.Title,
                TitleOrderType = dto.TitleOrderType,
                TittleComparisonType = dto.TittleComparisonType,

                Status = dto.Status,
                StatusOrderType = dto.StatusOrderType,

                FromCreateAt = dto.FromCreateAt,
                FromCreateAtComparisonType = dto.FromCreateAtComparisonType,

                ToCreateAt = dto.ToCreateAt,
                ToCreateAtComparisonType = dto.ToCreateAtComparisonType,
                CreateAtOrderType = dto.CreateAtOrderType,

                PageIndex = dto.PageIndex,
                PageSize = dto.PageSize,
            };
        }

        public static OrganizationFilterRequest MapToRequest(
            this GetListByFiltersQuery query)
        {
            return new()
            {
                Ids = query.Ids,
                IdOrderType = query.IdOrderType,

                Title = query.Title,
                TitleOrderType = query.TitleOrderType,
                TittleComparisonType = query.TittleComparisonType,

                Status = query.Status,
                StatusOrderType = query.StatusOrderType,

                FromCreateAt = query.FromCreateAt,
                FromCreateAtComparisonType = query.FromCreateAtComparisonType,

                ToCreateAt = query.ToCreateAt,
                ToCreateAtComparisonType = query.ToCreateAtComparisonType,
                CreateAtOrderType = query.CreateAtOrderType,

                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
            };
        }

        public static OrganizationsResponse MapToResponse(
            this QueryResponse<OrganizationResponse> queryResponse)
        {
            return new()
            {
                Items = queryResponse.Items,

                Count = queryResponse.Count,
                TotalCount = queryResponse.TotalCount,

                PageSize = queryResponse.PageSize,
                PageIndex = queryResponse.PageIndex,
                TotalPageIndex = queryResponse.TotalPageIndex,
            };
        }

        public static DateFilterDto MapToDateFilterDto(this OrganizationFilterRequest request)
        {
            return new()
            {
                FromCreateAt = request.FromCreateAt,
                FromCreateAtComparisonType = request.FromCreateAtComparisonType,

                ToCreateAt = request.ToCreateAt,
                ToCreateAtComparisonType = request.ToCreateAtComparisonType,

                CreateAtOrderType = request.CreateAtOrderType
            };
        }
    }
}