namespace Auth.Features.Users.Contracts.Responses
{
    public record TokenResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }

        public DateTime AccessTokenExpiry { get; init; }
        public DateTime RefreshTokenExpiry { get; init; }
    }
}