
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Tenant.Paginations;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class RolPermisoTenantRepository : RepositoryTenant<RolPermisoTenant, RolPermisoTenantId>, IRolPermisoTenantRepository
{
    public RolPermisoTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public Task<RolPermisoTenant?> GetByMenuAndRol(SistemaId menuId, RolId rolId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ValidarPermisoMenu(string menuId, RolTenantId rolId, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<RolPermisoTenant>()
                    .AnyAsync(x => x.MenuId == menuId && x.RolId == rolId && x.Activo == new Activo(true), cancellationToken);

    }
}