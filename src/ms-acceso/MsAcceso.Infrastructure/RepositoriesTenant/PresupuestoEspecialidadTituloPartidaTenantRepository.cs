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

    public async Task<string?> GetLastCorrelativoAsync(PresupuestoEspecialidadTituloTenantId especialidadTituloId, CancellationToken cancellationToken = default)
    {
       return await DbContext.Set<PresupuestoEspecialidadTituloPartidaTenant>()
                                                                .Where(x => x.Activo == new Activo(true) && x.PresupuestoEspecialidadTituloId == especialidadTituloId)
                                                                .OrderByDescending(x => x.Correlativo)
                                                                .Select(x => x.Correlativo)
                                                                .FirstOrDefaultAsync(cancellationToken);
    }
}