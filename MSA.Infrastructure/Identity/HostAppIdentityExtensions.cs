using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSA.Application.Interfaces;
using MSA.Domain;

namespace MSA.Infrastructure.Identity;

public static class HostAppIdentityExtensions
{
    public static void AddMicroserviceIdentityServices<T>(this IHostApplicationBuilder builder) where T : DbContext
    {
        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<T>()
            .AddApiEndpoints();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));
    }
}
