using Auth.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth.Shared.RequestPipeline;

public static class DatabaseMigrationConfiguration
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        AuthDBContext? dbContext = scope.ServiceProvider.GetRequiredService<AuthDBContext>();

        await dbContext.Database.MigrateAsync();
    }
}
