using Microsoft.AspNetCore.Builder;

namespace MSA.Infrastructure.Web;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}
