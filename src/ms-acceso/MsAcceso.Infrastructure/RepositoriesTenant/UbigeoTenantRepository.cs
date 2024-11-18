using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Shared;
//using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Tenant.UbigeosTenant;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class UbigeoTenantRepository : RepositoryApplication<UbigeoTenant, UbigeoTenantId>, IUbigeoTenantRepository//, IPaginationUbigeoTenantRepository
{

    public UbigeoTenantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    // public async Task<List<UbigeoTenant>> GetAllAsync(CancellationToken cancellationToken)
    // {
    //     return await DbContext.Set<UbigeoTenant>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    // }

    // public async Task<bool> UbigeoTenantExist(string nombreUbigeoTenant, CancellationToken cancellationToken = default)
    // {
    //     return await DbContext.Set<UbigeoTenant>().AnyAsync(x => x.Nombre == nombreUbigeoTenant && x.Activo == new Activo(true), cancellationToken);
    // }

    public async Task<List<UbigeoTenant>> GetAllUbigeosBySubnivel(UbigeoTenantId Id, CancellationToken cancellationToken)
    {
        var dependentUbigeos = await DbContext.Set<UbigeoTenant>()
                                             .Where(x => x.Dependencia == Id)
                                            //  .Include(x => x.Opciones)
                                             .ToListAsync(cancellationToken);

        var sistemasToProcess = new List<UbigeoTenant>(dependentUbigeos);

        while (sistemasToProcess.Count > 0)
        {
            var sistema = sistemasToProcess[0];
            sistemasToProcess.RemoveAt(0);

            // var childUbigeos = await LoadDependenciesToDeleteAsync(sistema, cancellationToken);

            // foreach (var childUbigeo in childUbigeos)
            // {
            //     if (!dependentUbigeos.Any(s => s.Id == childUbigeo.Id))
            //     {
            //         dependentUbigeos.Add(childUbigeo);
            //         sistemasToProcess.Add(childUbigeo);
            //     }
            // }
        }

        return dependentUbigeos!;
    }
}
