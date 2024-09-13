using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.CommandQuery.Queries.GetById
{
    public class GetByIdHandler(IOrganizationRepository userRepository)
        : IRequestHandler<GetByIdQuery, ErrorOr<UserResponse>>
    {
        private readonly IOrganizationRepository _userRepository = userRepository;

        public async Task<ErrorOr<UserResponse>> Handle(
            GetByIdQuery query, CancellationToken ct)
        {
            Option<User> user = await _userRepository
                .FindAsync(
                    expression: i => i.Id == query.Id
                        && query.OrganizationIds.Any(j =>
                            i.UserOrganizations.Select(i => i.OrganizationId).Contains(j)),
                    organizationChidesIds: query.OrganizationIds, ct);

            return user.Match<ErrorOr<UserResponse>>(
                value => value.MapToResponse(),
                () => UserErrors.NotFound());
        }
    }
}