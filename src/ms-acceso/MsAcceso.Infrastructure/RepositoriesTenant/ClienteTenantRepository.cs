using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class ClienteTenantRepository : RepositoryTenant<ClienteTenant,ClienteTenantId>, IClienteTenantRepository
{
    public ClienteTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

}