
using MsAcceso.Domain.Tenant.PresupuestosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PresupuestoTenantRepository : RepositoryTenant<PresupuestoTenant, PresupuestoTenantId>, IPresupuestoTenantRepository
{

    public PresupuestoTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    
}
