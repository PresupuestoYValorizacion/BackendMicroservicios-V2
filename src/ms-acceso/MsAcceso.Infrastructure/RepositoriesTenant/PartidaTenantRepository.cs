using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PartidaTenantRepository : RepositoryTenant<PartidaTenant, PartidaTenantId>, IPartidaTenantRepository
{

    public PartidaTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<bool> PartidaExistsByName(string nombrePartidaTenant,PartidaTenantId dependencia, PresupuestoEspecialidadTituloTenantId especialidadTituloId, CancellationToken cancellationToken = default)
    {
        bool isEmptyGuid = especialidadTituloId.Value == Guid.Empty;

        // var depedenciaNueva = isEmptyGuid ? null : dependencia;
        if(isEmptyGuid)
        {
            return  await DbContext.Set<PartidaTenant>().AnyAsync(x => x.Nombre == nombrePartidaTenant && x.Activo == new Activo(true) && x.Dependencia == dependencia , cancellationToken);
        }

        return await DbContext.Set<PartidaTenant>().AnyAsync(x => x.Nombre == nombrePartidaTenant && x.Activo == new Activo(true) && x.PresupuestosEspecialidadesTitulosPartidas.Any(x => x.PresupuestoEspecialidadTituloId == especialidadTituloId), cancellationToken);
    }

    public async Task<List<PartidaTenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<PartidaTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<string?> GetLastCorrelativoAsync(PresupuestoEspecialidadTituloTenantId especialidadTituloId, CancellationToken cancellationToken = default)
    {

        return await DbContext.Set<PartidaTenant>()
                                                                 .Where(x => x.Activo == new Activo(true) && x.PresupuestosEspecialidadesTitulosPartidas.Any(x => x.PresupuestoEspecialidadTituloId == especialidadTituloId))
                                                                 .OrderByDescending(x => x.Correlativo)
                                                                 .Select(x => x.Correlativo)
                                                                 .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<string?> GetLastCorrelativoDependenciaAsync(int nivel, PartidaTenantId dependencia, CancellationToken cancellationToken = default)
    {
        bool isEmptyGuid = dependencia.Value == Guid.Empty;

        var depedenciaNueva = isEmptyGuid ? null : dependencia;

        return await DbContext.Set<PartidaTenant>()
                                                                 .Where(x => x.Activo == new Activo(true) && x.Nivel == nivel && x.Dependencia == depedenciaNueva)
                                                                 .OrderByDescending(x => x.Correlativo)
                                                                 .Select(x => x.Correlativo)
                                                                 .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PartidaTenant?> GetByIdWithIncludesAsync(PartidaTenantId partidaId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<PartidaTenant>()
                                                   .Where(x => x.Activo == new Activo(true) && x.Id == partidaId )
                                                   .Include(x => x.PresupuestosEspecialidadesTitulosPartidas)
                                                   .FirstOrDefaultAsync(cancellationToken);
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
