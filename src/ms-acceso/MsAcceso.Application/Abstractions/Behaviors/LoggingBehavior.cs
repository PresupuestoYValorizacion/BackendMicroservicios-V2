
using MediatR;
using Microsoft.Extensions.Logging;
using MsAcceso.Domain.Abstractions;
using Serilog.Context;

namespace MsAcceso.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseRequest
where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken
        )
    {
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Ejecutando el request: {name}", name);
            var result = await next();

            if(result.Status == 200)
            {
                _logger.LogInformation($"El request: {name} fue exitoso",name);
            }
            else
            {
                using(LogContext.PushProperty("Error", result.Message, true))
                {
                    _logger.LogError($"El Request {name} tiene errores", name);
                }
            }

            return result;
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, $"El request {name} tuvo errores",name);
            throw;
        }

    }
}
