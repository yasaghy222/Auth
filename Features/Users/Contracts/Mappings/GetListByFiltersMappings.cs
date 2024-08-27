using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Contracts.Response;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.EndPoints.GetListByFilters;
using Auth.Features.Users.CommandQuery.Queries.GetListByFilters;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class GetListByFiltersMappings
    {
        private static IEnumerable<Ulid> GetUserOrganizationIds(
            this IEnumerable<Ulid>? filterIds, IEnumerable<Ulid> userOrganizationIds)
        {
            if (filterIds == null || !filterIds.Any())
                return userOrganizationIds;

            return userOrganizationIds.Filter(i => filterIds.Contains(i));
        }

        public static GetListByFiltersQuery MapToQuery(
            this GetListByFiltersDto dto)
        {
            return new()
            {
                Organizations = dto.Organizations,

                Usernames = dto.Usernames,
                UsernamesComparisonType = dto.UsernamesComparisonType,

                FullName = dto.FullName,
                FullNameComparisonType = dto.FullNameComparisonType,

                Phones = dto.Phones,
                PhonesComparisonType = dto.PhonesComparisonType,
                IsPhoneValid = dto.IsPhoneValid,

                Emails = dto.Emails,
                EmailsComparisonType = dto.EmailsComparisonType,
                IsEmailValid = dto.IsEmailValid,

                Status = dto.Status,
            };
        }

        public static UserFilterRequest MapToRequest(
            this GetListByFiltersQuery query,
            IEnumerable<Ulid> userOrganizationIds)
        {
            return new()
            {
                OrganizationIds = query.Organizations.GetUserOrganizationIds(userOrganizationIds),

                Usernames = query.Usernames,
                UsernamesComparisonType = query.UsernamesComparisonType,

                FullName = query.FullName,
                FullNameComparisonType = query.FullNameComparisonType,

                Phones = query.Phones,
                PhonesComparisonType = query.PhonesComparisonType,
                IsPhoneValid = query.IsPhoneValid,

                Emails = query.Emails,
                EmailsComparisonType = query.EmailsComparisonType,
                IsEmailValid = query.IsEmailValid,

                Status = query.Status,
            };
        }

        public static UserResponse ToResponse(this User user)
        {
            return new()
            {
                Id = user.Id,
                FullName = $"{user.Name} + {user.Family}",
                Username = user.Username,
                Phone = user.Phone,
                IsPhoneValid = user.IsPhoneValid,
                Email = user.Email,
                IsEmailValid = user.IsEmailValid,
                Status = user.Status,
                CreateAt = user.CreateAt
            };
        }

        public static UsersResponse ToResponse(this QueryResponse<UserResponse> queryResponse)
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

        public static DateFilterDto ToDateFilterDto(this UserFilterRequest request)
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