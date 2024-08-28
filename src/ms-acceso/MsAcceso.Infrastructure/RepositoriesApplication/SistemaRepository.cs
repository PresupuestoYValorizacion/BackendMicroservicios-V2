using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.MenuOpciones;
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
        var sistemasDependientes = await DbContext.Set<Sistema>().Where(x => x.Dependencia == Id)
                                             .Include(x => x.Opciones)
                                             .ToListAsync(cancellationToken);

        foreach (var system in sistemasDependientes)
        {
            await LoadDependenciesAsync(system, cancellationToken);
        }

        return sistemasDependientes;
    }

    public async Task<bool> SistemaExistsByName(string name, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().AnyAsync(x => x.Nombre == name && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<List<Sistema>> GetAllSistemas(CancellationToken cancellationToken)
    {
        var rootSystems = await DbContext.Set<Sistema>().Where(x => x.Dependencia == null)
                                                         .Include(x => x.Opciones)
                                                         .ToListAsync(cancellationToken);

        foreach (var system in rootSystems)
        {
            await LoadDependenciesAsync(system, cancellationToken);
        }

        return rootSystems;

    }

    private async Task LoadDependenciesAsync(Sistema system, CancellationToken cancellationToken)
    {

        var prueba = await DbContext.Set<MenuOpcion>().ToListAsync(cancellationToken);

        var childSystems = await DbContext.Set<Sistema>()
            .Where(x => x.Dependencia == system.Id)
            .Include(x => x.Opciones) // Cargar también las opciones relacionadas
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
}