using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class CarpetaPresupuestalTenantRepository : RepositoryTenant<CarpetaPresupuestalTenant, CarpetaPresupuestalTenantId>, ICarpetaPresupuestalTenantRepository
{

    public CarpetaPresupuestalTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<bool> CarpetaPresupuestalExistsByName(string nombreCarpetaPresupuestalTenant, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<CarpetaPresupuestalTenant>().AnyAsync(x => x.Nombre == nombreCarpetaPresupuestalTenant && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<List<CarpetaPresupuestalTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<CarpetaPresupuestalTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }



    public async Task<List<CarpetaPresupuestalTenant>> GetAllCarpetaPresupuestales(CancellationToken cancellationToken)
    {

        var carpetaPresupuestalTenants = await DbContext.Set<CarpetaPresupuestalTenant>()
                                             .Where(x => x.Activo == new Activo(true))
                                             .Include(u => u.CarpetasPresupuestales)
                                             .ToListAsync(cancellationToken);

        return carpetaPresupuestalTenants!;
    }

    public async Task<CarpetaPresupuestalTenant?> GetByNombreAsync(string nombre, int nivel, string? dependencia, CancellationToken cancellationToken)
    {

        return await DbContext.Set<CarpetaPresupuestalTenant>().Where(x => x.Activo == new Activo(true) && x.Nombre == nombre && x.Nivel == nivel && (dependencia != null
                                                                                ? x.Dependencia == new CarpetaPresupuestalTenantId(Guid.Parse(dependencia))
                                                                                : x.Dependencia == null)).FirstOrDefaultAsync(cancellationToken);





    }
}
