using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Auth.Shared.RequestPipeline;

public static class DatabaseMigrationConfiguration
{
    public static async void ApplyMigrations<TDBContext>(this WebApplication app) where TDBContext : DbContext
    {
        using IServiceScope scope = app.Services.CreateScope();

        TDBContext? dbContext = scope.ServiceProvider.GetRequiredService<TDBContext>();

        if (dbContext.Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator dbCreator)
        {
            if (!dbCreator.CanConnect())
                await dbCreator.EnsureCreatedAsync();
        }
    }
}
