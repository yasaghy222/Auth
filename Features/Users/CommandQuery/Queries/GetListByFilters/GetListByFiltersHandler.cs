using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.Repositories;
using MediatR;

namespace Auth.Features.Users.CommandQuery.Queries.GetListByFilters
{
    public class GetListByFiltersHandler(
        IUserRepository userRepository)
        : IRequestHandler<GetListByFiltersQuery, UsersResponse>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UsersResponse> Handle(
            GetListByFiltersQuery query, CancellationToken ct)
        {
            UserFilterRequest request = query.MapToRequest();
            return await _userRepository.ToListByFilters(request, ct);
        }
    }
}