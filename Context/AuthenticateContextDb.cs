using Authenticate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.Context
{

    public class AuthenticateContextDb(DbContextOptions<AuthenticateContextDb> option) : DbContext(option)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(e => e.Organization)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired();
        }
    }

}