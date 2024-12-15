using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PartidaTenantRepository : RepositoryTenant<PartidaTenant, PartidaTenantId>, IPartidaTenantRepository
{

    public PartidaTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<bool> PartidaExistsByName(string nombrePartidaTenant, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PartidaTenant>().AnyAsync(x => x.Nombre == nombrePartidaTenant && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<List<PartidaTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<PartidaTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }



    // public async Task<List<PartidaTenant>> GetAllPartidasBySubnivel(PartidaTenantId Id, CancellationToken cancellationToken)
    // {
    //     var dependentPartidas = await DbContext.Set<PartidaTenant>()
    //                                          .Where(x => x.Dependencia == Id)
    //                                         //  .Include(x => x.Opciones)
    //                                          .ToListAsync(cancellationToken);

    //     var sistemasToProcess = new List<PartidaTenant>(dependentPartidas);

    //     while (sistemasToProcess.Count > 0)
    //     {
    //         var sistema = sistemasToProcess[0];
    //         sistemasToProcess.RemoveAt(0);

    //         // var childPartidas = await LoadDependenciesToDeleteAsync(sistema, cancellationToken);

    //         // foreach (var childPartida in childPartidas)
    //         // {
    //         //     if (!dependentPartidas.Any(s => s.Id == childPartida.Id))
    //         //     {
    //         //         dependentPartidas.Add(childPartida);
    //         //         sistemasToProcess.Add(childPartida);
    //         //     }
    //         // }
    //     }

    //     return dependentPartidas!;
    // }
}
