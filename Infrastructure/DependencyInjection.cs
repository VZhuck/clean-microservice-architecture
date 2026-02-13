
using Infrastructure.Data;
using Microsoft.Extensions.Hosting;
using MSA.Infrastructure;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.AddMsaInfrastructureServices<ApplicationDbContext, AppDbContextInitializer>(connStrKey: "AppConnStr");

        builder.AddMsaWebServices();
    }
}