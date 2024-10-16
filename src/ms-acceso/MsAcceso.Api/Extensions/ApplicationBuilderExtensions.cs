using MsAcceso.Middleware;
using MsAcceso.Api.Middleware;

namespace MsAcceso.Api.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
    public static void UseSessionValidationHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<SessionValidationMiddleware>();
    }

    public static IApplicationBuilder UseRequestContextLogging(
        this IApplicationBuilder app
    )
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}