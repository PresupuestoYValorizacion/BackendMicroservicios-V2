using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class SistemaRepository : RepositoryApplication<Sistema, SistemaId>, ISistemaRepository
{
    public SistemaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Sistema>> GetAllSistemasBySubnivel(SistemaId Id, CancellationToken cancellationToken)
    {
        var dependentSistemas = await DbContext.Set<Sistema>()
                                             .Where(x => x.Dependencia == Id)
                                             .Include(x => x.Opciones)
                                             .ToListAsync(cancellationToken);

        var sistemasToProcess = new List<Sistema>(dependentSistemas);

        while (sistemasToProcess.Count > 0)
        {
            var sistema = sistemasToProcess[0];
            sistemasToProcess.RemoveAt(0);

            var childSistemas = await LoadDependenciesToDeleteAsync(sistema, cancellationToken);

            foreach (var childSistema in childSistemas)
            {
                if (!dependentSistemas.Any(s => s.Id == childSistema.Id))
                {
                    dependentSistemas.Add(childSistema);
                    sistemasToProcess.Add(childSistema);
                }
            }
        }

        return dependentSistemas!;
    }

    private async Task<List<Sistema>> LoadDependenciesToDeleteAsync(Sistema sistema, CancellationToken cancellationToken)
    {

        var childSistemas = await DbContext.Set<Sistema>()
                                       .Where(x => x.Dependencia == sistema.Id && x.Activo == new Activo(true))
                                       .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true)))
                                       .ThenInclude(x => x.Opcion)
                                       .ToListAsync(cancellationToken);

        return childSistemas;

    }




    public async Task<bool> SistemaExistsByName(string name, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().AnyAsync(x => x.Nombre == name && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<List<Sistema>> GetAllSistemas(CancellationToken cancellationToken)
    {
        var rootSystems = await DbContext.Set<Sistema>().Where(x => x.Dependencia == null && x.Activo == new Activo(true))
                                                         .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true) && mo.Opcion!.Activo == new Activo(true)))
                                                         .ThenInclude(x => x.Opcion)
                                                         .OrderBy(x => x.Orden)
                                                         .ToListAsync(cancellationToken);

        foreach (var system in rootSystems)
        {
            await LoadDependenciesAsync(system, cancellationToken);
        }

        return rootSystems;

    }

    public async Task<List<Sistema>> GetSistemasByDependencia(SistemaId dependencia, CancellationToken cancellationToken)
    {
        bool isEmptyGuid = dependencia.Value == Guid.Empty;

        var depedenciaNueva = isEmptyGuid ? null : dependencia;
        return await DbContext.Set<Sistema>().Where(x => x.Dependencia == depedenciaNueva && x.Activo == new Activo(true))
                                                         //  .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true) && mo.Opcion!.Activo == new Activo(true)))
                                                         //  .ThenInclude(x => x.Opcion)
                                                         .OrderBy(x => x.Orden)
                                                         .ToListAsync(cancellationToken);

    }

    private async Task LoadDependenciesAsync(Sistema system, CancellationToken cancellationToken)
    {

        var childSystems = await DbContext.Set<Sistema>()
            .Where(x => x.Dependencia == system.Id && x.Activo == new Activo(true))
            .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true) && mo.Opcion!.Activo == new Activo(true)))
            .ThenInclude(x => x.Opcion)
            .OrderBy(x => x.Orden)
            .ToListAsync(cancellationToken);

        foreach (var childSystem in childSystems)
        {
            await LoadDependenciesAsync(childSystem, cancellationToken);
        }

    }

    public async Task<Sistema?> SistemaGetByIdAsync(SistemaId Id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>()
            .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
    }

    public async Task<List<Sistema>> GetAllSistemasByRol(RolId rolId, CancellationToken cancellationToken)
    {
        var rootSystems = await DbContext.Set<Sistema>()
            .Where(x => x.Dependencia == null && x.Activo == new Activo(true))
            .Include(x => x.RolPermisos!.Where(rp => rp.RolId == rolId && rp.Activo == new Activo(true)))
            .ThenInclude(x => x.RolPermisoOpcions!.Where(rpo => rpo.Activo == new Activo(true)))
            .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true) && mo.Opcion!.Activo == new Activo(true)))
            .ThenInclude(mo => mo.Opcion)
            .OrderBy(x => x.Orden)

            .ToListAsync(cancellationToken);

        foreach (var system in rootSystems)
        {
            await LoadDependenciesByRolAsync(system, rolId, cancellationToken);
        }

        return rootSystems!;
    }

    private async Task LoadDependenciesByRolAsync(Sistema system, RolId rolId, CancellationToken cancellationToken)
    {
        var childSystems = await DbContext.Set<Sistema>()
            .Where(x => x.Dependencia == system.Id && x.Activo == new Activo(true))
            .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true) && mo.Opcion!.Activo == new Activo(true)))
            .ThenInclude(mo => mo.Opcion)
            .Include(x => x.RolPermisos!.Where(rp => rp.RolId == rolId && rp.Activo == new Activo(true)))
            .ThenInclude(rp => rp.RolPermisoOpcions!.Where(rpo => rpo.Activo == new Activo(true)))
            .OrderBy(x => x.Orden)
            .ToListAsync(cancellationToken);

        foreach (var childSystem in childSystems)
        {
            await LoadDependenciesByRolAsync(childSystem, rolId, cancellationToken);
        }
    }

    public async Task<Sistema?> GetSistemaByIdAndRol(RolId rolId, SistemaId sistemaId, CancellationToken cancellationToken)
    {
        var sistema = await DbContext.Set<Sistema>()
            .Where(x => x.Id == sistemaId && x.Activo == new Activo(true))
            .Include(x => x.RolPermisos!.Where(rp => rp.RolId == rolId && rp.Activo == new Activo(true)))
            .ThenInclude(x => x.RolPermisoOpcions!.Where(rpo => rpo.Activo == new Activo(true)))
            .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true) && mo.Opcion!.Activo == new Activo(true)))
            .ThenInclude(mo => mo.Opcion)
            .FirstOrDefaultAsync(cancellationToken);

        return sistema!;
    }

    public async Task<bool> SistemaExistsByUrl(string url, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().AnyAsync(x => x.Url == url && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<Sistema?> GetByUrlAsync(string url, RolId rolId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().Where(x => x.Url == url && x.Activo == new Activo(true))
                                                         .Include(x => x.MenuOpcions!.Where(mo => mo.Activo == new Activo(true) && mo.Opcion!.Activo == new Activo(true)))
                                                         .ThenInclude(x => x.Opcion)
                                                         .Include(x => x.RolPermisos!.Where(rp => rp.RolId == rolId && rp.Activo == new Activo(true)))
                                                         .ThenInclude(x => x.RolPermisoOpcions!.Where(rpo => rpo.Activo == new Activo(true)))
                                                         .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetSistemasWithoutDependencies(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().CountAsync(x => x.Dependencia == null && x.Activo == new Activo(true));
    }

    public async Task<int> GetSistemasWithDependencies(SistemaId Id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().CountAsync(x => x.Dependencia == Id && x.Activo == new Activo(true));
    }

    public async Task<Sistema?> GetByOrdenAsync(int orden, SistemaId dependencia, CancellationToken cancellationToken)
    {
         return await DbContext.Set<Sistema>().Where(x => x.Orden == orden && x.Dependencia == dependencia && x.Activo == new Activo(true))
                                                         .FirstOrDefaultAsync(cancellationToken);
    }
}