using Authenticate.Enums;

namespace Authenticate.Entities
{
	public class Session
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid UserId { get; set; }

		public required string Token { get; set; }

		public SessionPlatform Platform { get; set; }
		public required string UniqueId { get; set; }
		public string? IP { get; set; }

		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime? ExpireDate { get; set; } = DateTime.UtcNow.AddMonths(1);

		public required User User { get; set; }
	}
}