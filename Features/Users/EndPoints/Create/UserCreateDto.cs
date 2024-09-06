using Auth.Domain.Aggregates;
using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Features.Users.EndPoints.Create
{
    public class UserCreateDto
    {
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string Username { get; set; }

        public required string Password { get; set; }
        public required string RepeatPassword { get; set; }

        public required string Phone { get; set; }
        public string? Email { get; set; }
    }
}