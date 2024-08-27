using MsAcceso.Middleware;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure;

namespace MsAcceso.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseRequestContextLogging(
        this IApplicationBuilder app
    )
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}