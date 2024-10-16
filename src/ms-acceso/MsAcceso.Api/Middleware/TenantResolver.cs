
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Api.Middleware
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
            context.Request.Headers.TryGetValue("Tenant", out var tenantId); 
            context.Request.Headers.TryGetValue("Rol", out var rolId); 
            
            if (string.IsNullOrEmpty(tenantId) == false && string.IsNullOrEmpty(rolId) == false)
            {
                await currentTenantService.SetTenant(new Guid(tenantId!), new Guid(rolId!));
            }

            await _next(context);
        }


    }
}
