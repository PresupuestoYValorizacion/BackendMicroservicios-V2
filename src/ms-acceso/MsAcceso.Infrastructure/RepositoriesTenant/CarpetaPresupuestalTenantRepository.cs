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



    // public async Task<List<CarpetaPresupuestalTenant>> GetAllCarpetaPresupuestalsBySubnivel(CarpetaPresupuestalTenantId Id, CancellationToken cancellationToken)
    // {
    //     var dependentCarpetaPresupuestals = await DbContext.Set<CarpetaPresupuestalTenant>()
    //                                          .Where(x => x.Dependencia == Id)
    //                                         //  .Include(x => x.Opciones)
    //                                          .ToListAsync(cancellationToken);

    //     var sistemasToProcess = new List<CarpetaPresupuestalTenant>(dependentCarpetaPresupuestals);

    //     while (sistemasToProcess.Count > 0)
    //     {
    //         var sistema = sistemasToProcess[0];
    //         sistemasToProcess.RemoveAt(0);

    //         // var childCarpetaPresupuestals = await LoadDependenciesToDeleteAsync(sistema, cancellationToken);

    //         // foreach (var childCarpetaPresupuestal in childCarpetaPresupuestals)
    //         // {
    //         //     if (!dependentCarpetaPresupuestals.Any(s => s.Id == childCarpetaPresupuestal.Id))
    //         //     {
    //         //         dependentCarpetaPresupuestals.Add(childCarpetaPresupuestal);
    //         //         sistemasToProcess.Add(childCarpetaPresupuestal);
    //         //     }
    //         // }
    //     }

    //     return dependentCarpetaPresupuestals!;
    // }
}
