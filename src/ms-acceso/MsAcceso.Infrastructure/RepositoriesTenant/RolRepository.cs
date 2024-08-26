
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class RolRepository : RepositoryTenant<Rol, RolId>, IRolRepository
{
    public RolRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Rol?> GetByLicenciaAsync(LicenciaId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Rol>()
             .FirstOrDefaultAsync(x => x.LicenciaId == id && x.Activo == new Activo(true), cancellationToken);
    }
}