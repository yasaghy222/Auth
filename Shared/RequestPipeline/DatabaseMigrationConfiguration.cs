using Auth.Data;
using Auth.Features.Organizations.Services;
using Auth.Features.Roles.Services;
using Microsoft.EntityFrameworkCore;

namespace Auth.Shared.RequestPipeline;

public static class DatabaseMigrationConfiguration
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        AuthDBContext? dbContext = scope.ServiceProvider.GetRequiredService<AuthDBContext>();

        await dbContext.Database.MigrateAsync();

        if (await dbContext.Database.CanConnectAsync() &&
            await dbContext.Database.EnsureCreatedAsync())
        {
            await dbContext.Organizations.ApplyInitialDatas();
            await dbContext.Roles.ApplyInitialDatas();

            await dbContext.SaveChangesAsync();
        }
    }
}
