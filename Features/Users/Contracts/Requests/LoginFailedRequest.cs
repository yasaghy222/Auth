using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Contracts.Requests
{
    public record LoginFailedRequest
    {

        public Ulid Id { get; set; }

        public int FailedLoginAttempts { get; set; }

        public DateTime? AccountLockedUntil { get; set; }

        public UserStatus Status { get; set; }
    }
}