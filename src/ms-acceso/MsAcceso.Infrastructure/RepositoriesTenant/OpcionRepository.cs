using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.RepositoriesTenant;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.Repositories;

internal sealed class OpcionRepository : RepositoryTenant<Opcion, OpcionId>, IOpcionRepository, IPaginationOpcionRepository
{

    public OpcionRepository(TenantDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<bool> OpcionExist(string nombreOpcion, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Opcion>().AnyAsync(x => x.Nombre == nombreOpcion && x.Activo == new Activo(true), cancellationToken);
    }
}