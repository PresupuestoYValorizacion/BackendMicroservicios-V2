// using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PresupuestoEspecialidadTituloTenantRepository : RepositoryTenant<PresupuestoEspecialidadTituloTenant, PresupuestoEspecialidadTituloTenantId>, IPresupuestoEspecialidadTituloTenantRepository
{
    public PresupuestoEspecialidadTituloTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<PresupuestoEspecialidadTituloTenant>> GetAllAsyncWithIncludes(EspecialidadTenantId especialidadId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<PresupuestoEspecialidadTituloTenant>().Where(x => x.Activo == new Activo(true) && x.EspecialidadId == especialidadId)
                    .Include(x => x.PresupuestosEspecialidadTituloPartidas)
                    .Include(x => x.Titulo)
                    // .Include(x => x.!)
                    // .ThenInclude(x => x.CarpetaPresupuestal!)
                    // .Include(x => x.Presupuesto!)
                    // .ThenInclude(x => x.Cliente!)
                    .ToListAsync(cancellationToken);
    }

    public async Task<string?> GetLastCorrelativoAsync(EspecialidadTenantId especialidadId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PresupuestoEspecialidadTituloTenant>()
                                                                .Where(x => x.Activo == new Activo(true) && x.EspecialidadId == especialidadId)
                                                                .OrderByDescending(x => x.Correlativo)
                                                                .Select(x => x.Correlativo)
                                                                .FirstOrDefaultAsync(cancellationToken);

    }
}