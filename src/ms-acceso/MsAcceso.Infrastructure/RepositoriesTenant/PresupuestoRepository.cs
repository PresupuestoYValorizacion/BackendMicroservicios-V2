
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.Presupuestos;
using MsAcceso.Domain.Tenant.Users;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PresupuestoTenantRepository : RepositoryTenant<Presupuesto,PresupuestoId>, IPresupuestoTenantRepository
{
    public PresupuestoTenantRepository(EnterpriseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Presupuesto>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Presupuesto>().ToListAsync(cancellationToken);
    }

}