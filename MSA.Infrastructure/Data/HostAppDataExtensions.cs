using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSA.Application.Interfaces;

namespace MSA.Infrastructure.Data;

public static class HostAppDataExtensions
{
    public static IHostApplicationBuilder AddSqlEfInfrastructure<I, T>(
        this IHostApplicationBuilder builder, string dbConStringKey)
        where I : class, IMicroserviceDbContext
        where T : DbContext, I
    {
        var connectionString = builder.Configuration.GetConnectionString(dbConStringKey);
        Guard.Against.Null(connectionString, message: $"Connection string '{dbConStringKey}' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddDbContext<T>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(T).Assembly.FullName);
                // sqlOptions.MigrationsAssembly("FSyncService.Infrastructure");
                // sqlOptions.EnableRetryOnFailure();
            });
        });

        // Add Aspire
        // builder.EnrichSqlServerDbContext<ApplicationDbContext>();

        // TODD: Re-Think
        builder.Services.AddScoped<IMicroserviceDbContext>(provider => provider.GetRequiredService<T>());
        builder.Services.AddScoped<I>(provider => provider.GetRequiredService<T>());

        return builder;
    }

    public static IHostApplicationBuilder WithDbContextInitializer<TInit>(this IHostApplicationBuilder builder)
        where TInit : class, IDbContextInitialiser
    {
        // Add DB Initializer
        builder.Services.AddScoped<TInit>();
        builder.Services.AddScoped<IDbContextInitialiser>(provider => provider.GetRequiredService<TInit>());
        return builder;
    }
}
