using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MSA.Application.Behaviours;
using MSA.Mediator;
using MSA.Mediator.Abstractions;

namespace MSA.Application;

public static class MsaDependencyInjection
{
    public static void AddMsaApplicationServices(this IHostApplicationBuilder builder)
    {
        //    //builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Implement Mediator/Middleware 
        builder.Services.AddScoped<IMediator, MediatorStub>();
        
        //    builder.Services.AddMediatR(cfg => {
        //        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        //    });
    }
}