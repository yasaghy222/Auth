namespace Authenticate
{
    public class JWTOptions
    {
        public string Issueer { get; init; } = "issueer";
        public string Audience { get; init; } = "audience";
        public string SecretKey { get; init; } = "admin-service-api-key-jwt-private-secret-authenticate";
    }
}