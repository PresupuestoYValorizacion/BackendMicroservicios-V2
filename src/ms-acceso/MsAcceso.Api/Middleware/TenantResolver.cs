
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;
        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        // Get Tenant Id from incoming requests 
        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            context.Request.Headers.TryGetValue("tenant", out var tenantId); 
            context.Request.Headers.TryGetValue("licenciaId", out var licenciaId); 
            if (string.IsNullOrEmpty(tenantId) == false && string.IsNullOrEmpty(licenciaId) == false)
            {
                await currentTenantService.SetTenant(new Guid(tenantId!), new Guid(licenciaId!));
            }

            await _next(context);
        }


    }
}
