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
        public bool IsPhoneValid { get; set; } = false;

        public string? Email { get; set; }
        public bool IsEmailValid { get; set; } = false;

        public ICollection<UserOrganization> UserOrganizations { get; set; } = [];

        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? AccountLockedUntil { get; set; }
    }
}