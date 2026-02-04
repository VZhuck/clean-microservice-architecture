using Microsoft.Extensions.Hosting;
using System.Reflection;
using MSA.Application.Behaviours;
using MSA.Mediator;

namespace MSA.Application;

public class DependencyInjection
{
    //public static void AddApplicationServices(this IHostApplicationBuilder builder)
    //{
    //    //builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    //    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    //    builder.Services.AddMediatR(cfg => {
    //        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
    //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
    //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
    //    });
    //}
}