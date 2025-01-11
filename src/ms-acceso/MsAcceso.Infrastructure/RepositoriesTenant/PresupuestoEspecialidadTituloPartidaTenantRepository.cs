using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PresupuestoEspecialidadTituloPartidaTenantRepository : RepositoryTenant<PresupuestoEspecialidadTituloPartidaTenant,PresupuestoEspecialidadTituloPartidaTenantId>, IPresupuestoEspecialidadTituloPartidaTenantRepository
{
    public PresupuestoEspecialidadTituloPartidaTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

   
}