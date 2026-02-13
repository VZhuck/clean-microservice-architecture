using System.Reflection;
using Mapster;
using Microsoft.Extensions.Hosting;
using MSA.Application;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddMsaApplicationServices();
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        
        // TODO Generic Mediatr Middleware Approach
        
    }
}