namespace Auth.Features.Users.Contracts.Requests
{
    public class ChangePasswordRequest
    {
        public Ulid Id { get; set; }
        public required string OldPassword { get; set; }
        public required string Password { get; set; }
    }
}