namespace Authenticate.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? RoleId { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}