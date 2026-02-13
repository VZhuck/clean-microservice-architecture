using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MSA.Application.Interfaces;

namespace MSA.Infrastructure.Data;

public static class WebAppDataExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var initialiser = scope.ServiceProvider.GetRequiredService<IDbContextInitializer>();
        
        await initialiser.InitialiseAsync();
        
        await initialiser.SeedAsync();
        
        // TODO: Can be deleted, after connection string is configured correctly
        // await Task.CompletedTask;
    }
}
