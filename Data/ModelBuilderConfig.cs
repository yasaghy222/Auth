using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using Auth.Features.Roles.Services;
using Auth.Features.Users.Services;
using Microsoft.EntityFrameworkCore;
using Auth.Features.Resources.Services;
using Auth.Features.Permissions.Services;
using Auth.Features.Organizations.Services;
using Auth.Features.UserOrganizations.Services;

namespace Auth.Data;

public static class ModelBuilderConfig
{
	public static void OnModelCreatingBuilder(this ModelBuilder modelBuilder, IHashService hashService)
	{
		modelBuilder.Entity<Organization>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.Title).HasMaxLength(200);
			entity.Property(e => e.ParentId).HasMaxLength(200);

			entity.HasOne(e => e.Parent)
				.WithMany(e => e.Chides)
				.HasForeignKey(e => e.ParentId)
				.OnDelete(DeleteBehavior.NoAction);

			entity.HasData(OrganizationDataSeeding.InitialItems);
		});

		modelBuilder.Entity<Role>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.Title).HasMaxLength(200);
			entity.Property(e => e.OrganizationId).HasMaxLength(200);

			entity.HasOne(e => e.Organization)
				.WithMany(e => e.Roles)
				.HasForeignKey(e => e.OrganizationId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasData(RolesDataSeeding.InitialItems);

			entity.Navigation(e => e.Permissions).AutoInclude();
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.Name).HasMaxLength(100);
			entity.Property(e => e.Family).HasMaxLength(200);
			entity.Property(e => e.Username).HasMaxLength(300);
			entity.Property(e => e.Phone).HasMaxLength(30);
			entity.Property(e => e.Email).HasMaxLength(300);
			entity.Property(e => e.Password).HasMaxLength(500);
			entity.Property(e => e.StatusDescription).HasMaxLength(500);

			entity.HasData(UsersDataSeeding.GetInitialItems(hashService));

			entity.Navigation(e => e.UserOrganizations).AutoInclude();
		});

		modelBuilder.Entity<UserOrganization>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.UserId).HasMaxLength(200);
			entity.Property(e => e.OrganizationId).HasMaxLength(200);
			entity.Property(e => e.RoleId).HasMaxLength(200);

			entity.HasOne(e => e.User)
				.WithMany(e => e.UserOrganizations)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.Organization)
				.WithMany(e => e.UserOrganizations)
				.HasForeignKey(e => e.OrganizationId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.Role)
				.WithMany(e => e.UserOrganizations)
				.HasForeignKey(e => e.RoleId)
				.OnDelete(DeleteBehavior.NoAction);

			entity.HasData(UserOrganizationsDataSeeding.InitialItems);

			entity.Navigation(e => e.Role).AutoInclude();
		});


		modelBuilder.Entity<ResourceGroup>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.Title).HasMaxLength(200);
			entity.Property(e => e.ParentId).HasMaxLength(200);

			entity.HasOne(e => e.Parent)
				.WithMany(e => e.Chields)
				.HasForeignKey(e => e.ParentId)
				.OnDelete(DeleteBehavior.NoAction);

			entity.HasData(ResourceGroupsDataSeeding.InitialItems);
		});

		modelBuilder.Entity<Resource>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.Title).HasMaxLength(200);
			entity.Property(e => e.OrganizationId).HasMaxLength(200);
			entity.Property(e => e.Url).HasMaxLength(400);
			entity.Property(e => e.GroupId).HasMaxLength(200);

			entity.HasOne(e => e.Organization).WithMany(e => e.Resources)
				.HasForeignKey(e => e.OrganizationId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.Group).WithMany(e => e.Resources)
				.HasForeignKey(e => e.GroupId)
				.OnDelete(DeleteBehavior.NoAction);

			entity.HasData(ResourcesDataSeeding.InitialItems);
		});


		modelBuilder.Entity<Permission>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.RoleId).HasMaxLength(200);
			entity.Property(e => e.ResourceId).HasMaxLength(200);

			entity.HasOne(e => e.Role).WithMany(e => e.Permissions)
				.HasForeignKey(e => e.RoleId)
				.OnDelete(DeleteBehavior.NoAction);


			entity.HasOne(e => e.Resource).WithMany(e => e.Permissions)
				.HasForeignKey(e => e.ResourceId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasData(PermissionsDataSeeding.InitialItems);
		});
	}
}
