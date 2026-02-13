using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSA.Application.Interfaces;
using MSA.Domain;
using MSA.Infrastructure.Data;
using MSA.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using MSA.Infrastructure.Web;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace MSA.Infrastructure
{
    public static class MsaDependencyInjection
    {
        public static void AddMsaInfrastructureServices<TDbContext,TDbInitializer>(this IHostApplicationBuilder builder, string connStrKey )
            where TDbContext: DbContext, IMicroserviceDbContext
            where TDbInitializer: IDbContextInitializer
        {
            builder.AddConfiguredDbContext<TDbContext, TDbInitializer>(connStrKey);
            builder.AddAndConfigureIdentityServices<TDbContext>();
            
            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.AddTransient<IIdentityService, IdentityService>();

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator));
        }
        
        public static void AddMsaWebServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<IUser, CurrentUser>();
        
            builder.Services.AddHttpContextAccessor();
        
            // Provide unified exception handling and mapping to HTTP Status 
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
         
            // Customise default API behaviour
            builder.Services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddOpenApiDocument((configure, sp) =>
            {
                configure.Title = "Microservice API";

                // Add JWT
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }

        private  static void AddConfiguredDbContext<TDbContext, TDbInitializer>(this IHostApplicationBuilder builder,  string connStrKey) 
            where TDbContext: DbContext, IMicroserviceDbContext
            where TDbInitializer: IDbContextInitializer
        {
            var connectionString = builder.Configuration.GetConnectionString(connStrKey);
            Guard.Against.Null(connectionString, message: $"Connection string {connStrKey} not found.");

            builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            builder.Services.AddDbContext<TDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            });
            
            builder.Services.AddScoped<IMicroserviceDbContext>(provider => provider.GetRequiredService<TDbContext>());
            builder.Services.AddScoped<IDbContextInitializer>(provider =>
                provider.GetRequiredService<TDbInitializer>());
        } 
        
        private static void AddAndConfigureIdentityServices<T> (this IHostApplicationBuilder builder)
            where T: DbContext, IMicroserviceDbContext
        {
            builder.Services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

            builder.Services.AddAuthorizationBuilder();
            
            // TODO: Decide the same DB contexts or different 
            builder.Services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<T>()
                .AddApiEndpoints();
        }

        
        // public static void AddKeyVaultIfConfigured(this IHostApplicationBuilder builder)
        // {
        //     var keyVaultUri = builder.Configuration["AZURE_KEY_VAULT_ENDPOINT"];
        //     if (!string.IsNullOrWhiteSpace(keyVaultUri))
        //     {
        //         builder.Configuration.AddAzureKeyVault(
        //             new Uri(keyVaultUri),
        //             new DefaultAzureCredential());
        //     }
        // }
        
    }
}
