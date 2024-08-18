using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
	public class AuthContext(DbContextOptions<AuthContext> options) : DbContext(options)
	{
		public DbSet<Device> Devices { get; set; }
		public DbSet<Sensor> Sensors { get; set; }
		public DbSet<Log_IR> Log_IRs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.OnModelCreatingBuilder();
			base.OnModelCreating(modelBuilder);
		}

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder
			    .Properties<Ulid>()
			    .HaveConversion<UlidToStringConverter>()
			    .HaveConversion<UlidToBytesConverter>();
		}
	}
}
