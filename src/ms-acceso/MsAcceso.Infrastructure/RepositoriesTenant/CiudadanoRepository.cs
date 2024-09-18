using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Ciudadanos;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class CiudadanoRepository : RepositoryTenant<Ciudadano, CiudadanoId>, ICiudadanoRepository
{
    public CiudadanoRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Ciudadano>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Ciudadano>().Include(x => x.Pasaporte).Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

}