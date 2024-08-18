﻿
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
            context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader); // Tenant Id from incoming request header
            if (string.IsNullOrEmpty(tenantFromHeader) == false)
            {
                await currentTenantService.SetTenant(new Guid(tenantFromHeader!));
            }

            await _next(context);
        }


    }
}
