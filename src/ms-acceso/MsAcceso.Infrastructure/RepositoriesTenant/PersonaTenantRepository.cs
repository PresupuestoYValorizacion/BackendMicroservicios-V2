
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaTenantRepository : RepositoryTenant<PersonaTenant,PersonaTenantId>, IPersonaTenantRepository
{
    public PersonaTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<bool> IsEmpresaExists(PersonaTenantId Id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PersonaTenant>()
                    .AnyAsync(x => x.Id == Id,cancellationToken);
    }

    public async Task<bool> NumeroDocumentoExists(string NumeroDocumento, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PersonaTenant>()
                    .AnyAsync(x => x.NumeroDocumento == NumeroDocumento && x.Activo == new Activo(true) ,cancellationToken);
    }
}