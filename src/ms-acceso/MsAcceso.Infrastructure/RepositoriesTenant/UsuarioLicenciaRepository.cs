
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class UsuarioLicenciaRepository : RepositoryTenant<UsuarioLicencia, UsuarioLicenciaId>, IUsuarioLicenciaRepository
{
    public UsuarioLicenciaRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

}