namespace Authenticate.Entities
{
	public class User
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid? RoleId { get; set; }
		public required string Username { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public required string Password { get; set; }

		public Guid OrganizationId { get; set; }
		public Organization? Organization { get; set; }

		public ICollection<Session>? Sessions { get; set; }
	}
}