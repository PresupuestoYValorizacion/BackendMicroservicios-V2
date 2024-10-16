
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sesiones;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Api.Middleware
{
    public class SessionValidationMiddleware
    {
        private readonly RequestDelegate _next;
        public SessionValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISesionRepository sesionRepository, IUnitOfWorkApplication uow)
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var session = await sesionRepository.GetSessionByTokenAsync(token);


                if (session == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(Result.Failure(Error.SessionNotFound));
                    return;
                }

                // Si hay inactividad de más de 10 minutos, cerrar sesión
                if (DateTime.UtcNow.Subtract(session.LastActivity ?? new DateTime()).TotalMinutes > 10)
                {
                    session.Desactive();
                    sesionRepository.Update(session);
                    await uow.SaveChangesAsync();

                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(Result.Failure(Error.SessionExpired));
                    return;
                }

                session.UpdateLastActivity(DateTime.UtcNow);
                sesionRepository.Update(session);

                await uow.SaveChangesAsync();

            }

            await _next(context);
        }


    }
}
