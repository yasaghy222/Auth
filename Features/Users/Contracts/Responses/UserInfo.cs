namespace Auth.Features.Users.Contracts.Responses
{
    public record UserInfo
    {
        public required Ulid Id { get; set; }

        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string Username { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}