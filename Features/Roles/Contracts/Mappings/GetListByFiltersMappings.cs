using Auth.Features.Roles.Contracts.Requests;
using Auth.Features.Roles.CommandQuery.Queries.GetByFilters;
using Auth.Features.Roles.EndPoints.GetListByFilters;

namespace Auth.Features.Roles.Contracts.Mappings
{
    public static class GetListByFiltersMappings
    {
        public static GetListByFiltersQuery MapToQuery(
            this RoleGetListByFiltersDto dto)
        {
            return new()
            {
                Ids = dto.Ids,
                IdOrderType = dto.IdOrderType,

                OrganizationId = dto.OrganizationId,

                Title = dto.Title,
                TitleOrderType = dto.TitleOrderType,
                TittleComparisonType = dto.TittleComparisonType,

                Status = dto.Status,
                StatusOrderType = dto.StatusOrderType,
            };
        }

        public static RoleFilterRequest MapToRequest(
            this GetListByFiltersQuery query)
        {
            return new()
            {
                Ids = query.Ids,
                IdOrderType = query.IdOrderType,

                OrganizationId = query.OrganizationId,

                Title = query.Title,
                TitleOrderType = query.TitleOrderType,
                TittleComparisonType = query.TittleComparisonType,

                Status = query.Status,
                StatusOrderType = query.StatusOrderType,
            };
        }
    }
}