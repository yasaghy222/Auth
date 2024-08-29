using Auth.Contracts.Response;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Contracts.Responses
{
    public record UserResponse
    {
        public required Ulid Id { get; set; }
        public required string FullName { get; set; }
        public required string Username { get; set; }

        public string? Phone { get; set; }
        public bool IsPhoneValid = false;

        public string? Email { get; set; }
        public bool IsEmailValid = false;

        public UserStatus Status { get; set; }

        public DateTime CreateAt { get; set; }
    }

    public record UsersResponse : QueryResponse<UserResponse>;
}