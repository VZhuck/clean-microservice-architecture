using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MSA.Infrastructure.Web;

public static class WebAppExtensions
{
    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName}")
            .WithGroupName(groupName)
            .WithTags(groupName);
    }
    
    public static WebApplication MapEndpoints(this WebApplication app, Assembly assembly)
    {
        var endpointGroupType = typeof(EndpointGroupBase);
        
        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                instance.Map(app);
            }
        }

        return app;
    }

}
