
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class UsuarioLicenciaRepository : RepositoryTenant<UsuarioLicencia, UsuarioLicenciaId>, IUsuarioLicenciaRepository
{
    public UsuarioLicenciaRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

     public async Task<UsuarioLicencia?> GetByUserAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<UsuarioLicencia>()
             .FirstOrDefaultAsync(x => x.UserId == id && x.Activo == new Activo(true), cancellationToken);
    }


}