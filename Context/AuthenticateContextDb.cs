using Authenticate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authenticate.Context
{

    public class AuthenticateContextDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public AuthenticateContextDb(DbContextOptions<AuthenticateContextDb> option) : base(option)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if(!dbCreator.CanConnect()) dbCreator.Create();
                    if(!dbCreator.HasTables()) dbCreator.CreateTables();
                } 
            }   
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

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