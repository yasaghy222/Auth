using Auth.Domain.Aggregates;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Domain.Entities
{
    public class User : StatusAggregate<Ulid, UserStatus>
    {
        public required string Name { get; set; }
        public required string Family { get; set; }

        public required string Username { get; set; }
        public required string Password { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }

        public ICollection<Session>? Sessions { get; set; }
        public ICollection<UserOrganization>? UserOrganizations { get; set; }
    }
}