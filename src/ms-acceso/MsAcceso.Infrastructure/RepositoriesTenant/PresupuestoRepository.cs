
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.Presupuestos;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PresupuestoTenantRepository : RepositoryTenant<Presupuesto,PresupuestoId>, IPresupuestoTenantRepository
{
    public PresupuestoTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<Presupuesto>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Presupuesto>().ToListAsync(cancellationToken);
    }

}