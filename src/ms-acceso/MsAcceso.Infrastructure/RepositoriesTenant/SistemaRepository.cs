using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class SistemaRepository : RepositoryTenant<Sistema, SistemaId>, ISistemaRepository
{
    public SistemaRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Sistema>> GetAllSistemasBySubnivel(SistemaId Id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().Where(x => x.Dependencia == Id && x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> SistemaExistsByName(string name, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Sistema>().AnyAsync(x => x.Nombre == name && x.Activo == new Activo(true), cancellationToken);
    }

    public async Task<List<Sistema>> GetAllSistemas(CancellationToken cancellationToken)
    {
        var rootSystems = await DbContext.Set<Sistema>().Where(x => x.Dependencia == null)
                                                         .ToListAsync(cancellationToken);
        
        foreach( var system in rootSystems){
            await LoadDependenciesAsync(system, cancellationToken);
        }
       
        return rootSystems;

    }

    private async Task LoadDependenciesAsync(Sistema system, CancellationToken cancellationToken)
    {
        var childSystems = await DbContext.Set<Sistema>()
            .Where(x => x.Dependencia == system.Id && x.Activo == new Activo(true))
            .ToListAsync(cancellationToken);

        foreach (var childSystem in childSystems)
        {
            await LoadDependenciesAsync(childSystem, cancellationToken);
        }
    }

}