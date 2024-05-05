namespace Authenticate
{
    public class JWTOptions
    {
        public string Issueer { get; init; } = "http://localhost:5280";
        public string Audience { get; init; } = "http://localhost:5227";
        public string SecretKey { get; init; } = "4f858f9c-ab7d-4db4-8c2d-d2aaa67d1b83";
    }
}