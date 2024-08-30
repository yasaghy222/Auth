using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
	public class AuthDBContext(DbContextOptions<AuthDBContext> options, IHashService hashService) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserOrganization> UserOrganizations { get; set; }

		public DbSet<Organization> Organizations { get; set; }

		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }

		public DbSet<Resource> Resources { get; set; }
		public DbSet<ResourceGroup> ResourceGroups { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.OnModelCreatingBuilder(hashService);
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
