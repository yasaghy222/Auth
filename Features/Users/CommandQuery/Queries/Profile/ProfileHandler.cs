using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.CommandQuery.Queries.Profile
{
    public class ProfileHandler(IUserRepository userRepository)
        : IRequestHandler<ProfileQuery, ErrorOr<UserResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<UserResponse>> Handle(
            ProfileQuery query, CancellationToken ct)
        {
            Option<User> user = await _userRepository
                .FindAsync(
                    expression: i => i.Id == query.Id,
                    organizationChidesIds: query.OrganizationIds, ct);

            return user.Match<ErrorOr<UserResponse>>(
                value => value.MapToResponse(),
                () => UserErrors.NotFound());
        }
    }
}