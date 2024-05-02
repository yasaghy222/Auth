using Authenticate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.Context
{

    public class AuthenticateContextDb(DbContextOptions<AuthenticateContextDb> option) : DbContext(option)
    {
        public DbSet<User> Users { get; set; }
    }

}