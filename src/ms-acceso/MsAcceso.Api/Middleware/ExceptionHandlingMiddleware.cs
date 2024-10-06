
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;

namespace MsAcceso.Middleware;

public class ExceptionHandlingMiddleware
{

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
           await _next(context);
        }
        catch(Exception exception){
            
            _logger.LogError(exception, "Ocurrio una exception: {Message}", exception.Message);
            var exceptionDetails = GetExceptionDetails(exception);
            
            var error = new Error(exceptionDetails.Status, exceptionDetails.Detail);

            var result = Result.Failure(
                error
            );

            if(exceptionDetails.Errors is not null)
            {
                result.Messages = exceptionDetails.Errors.ToList().Select(e => e.GetType().GetProperty("ErrorMessage")!.GetValue(e)!.ToString()).ToList()!;
            }

            context.Response.StatusCode = exceptionDetails.Status;

            await context.Response.WriteAsJsonAsync(result);

        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
            return exception switch
            {
                ValidationException validationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validacion de Error",
                    "han ocurrido uno o mas errores de validacion",
                    validationException.Errors
                ),
                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Error de Servidor",
                    "Un inesperado error a ocurrido en la App",
                    null
                )
                
            };
    }


    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors
    );

}