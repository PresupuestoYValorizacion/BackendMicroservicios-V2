
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Tenant.Paginations;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class RolTenantRepository : RepositoryTenant<RolTenant, RolTenantId>, IRolTenantRepository, IPaginationRolesTenantRepository
{
    public RolTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<bool> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
    {
         return await DbContext.Set<RolTenant>()
                    .AnyAsync(x => x.Nombre == nombre && x.Activo == new Activo(true), cancellationToken);

    }

}