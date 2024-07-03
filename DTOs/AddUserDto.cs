namespace Authenticate.DTOs
{
    public class AddUserDto
    {
        public required Guid OrganizationId { get; set; }
        public required string Username { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}