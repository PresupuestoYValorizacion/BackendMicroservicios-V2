using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class RolPermisoOpcionTenantRepository : RepositoryTenant<RolPermisoOpcionTenant, RolPermisoOpcionTenantId>, IRolPermisoOpcionTenantRepository
{
   public RolPermisoOpcionTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<RolPermisoOpcionTenant?> GetByOpcionAndRolPermiso(RolPermisoTenantId rolPermisoId, string opcionId, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<RolPermisoOpcionTenant>()
            .Where(x => x.RolPermisoId == rolPermisoId && x.OpcionId == opcionId && x.Activo == new Activo(true))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ValidarPermisoOpcion(RolPermisoTenantId rolPermisoId, string opcionId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<RolPermisoOpcionTenant>()
            .AnyAsync(x => x.RolPermisoId == rolPermisoId && x.OpcionId == opcionId && x.Activo == new Activo(true), cancellationToken);
    }
}