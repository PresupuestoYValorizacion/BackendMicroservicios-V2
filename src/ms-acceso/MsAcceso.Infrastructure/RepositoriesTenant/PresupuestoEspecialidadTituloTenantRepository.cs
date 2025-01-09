// using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PresupuestoEspecialidadTituloTenantRepository : RepositoryTenant<PresupuestoEspecialidadTituloTenant,PresupuestoEspecialidadTituloTenantId>, IPresupuestoEspecialidadTituloTenantRepository
{
    public PresupuestoEspecialidadTituloTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

}