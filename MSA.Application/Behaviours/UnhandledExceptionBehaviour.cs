using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSA.Mediator;

namespace MSA.Application.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly string _appName;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger, IHostEnvironment hostingEnvironment)
    {
        _logger = logger;
        _appName = hostingEnvironment.ApplicationName;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex,
                "{AppName} Request: Unhandled Exception for Request {Name} {@Request}",
                _appName, requestName, request);
            throw;
        }
    }
}
