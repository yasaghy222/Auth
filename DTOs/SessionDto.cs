using Authenticate.Enums;
using Microsoft.CodeAnalysis;

namespace Authenticate.DTOs
{
    public class SessionDto
    {
        public Guid UserId { get; set; }
        public required string Token { get; set; }
        public SessionPlatform Platform { get; set; }
        public required string UniqueId { get; set; }
        public string? IP { get; set; }
    }
}