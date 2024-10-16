
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaJuridicaTenantRepository : RepositoryTenant<PersonaJuridicaTenant>, IPersonaJuridicaTenantRepository
{
    public PersonaJuridicaTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async void DeleteById(PersonaTenantId Id)
    {
        var entity = await DbContext.Set<PersonaJuridicaTenant>()
        .FirstOrDefaultAsync(x => x.PersonaId == Id);

        if (entity != null)
        {
            DbContext.Remove(entity);
        }
    }

    public async Task<PersonaJuridicaTenant?> GetByIdAsync(PersonaTenantId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PersonaJuridicaTenant>()
        .FirstOrDefaultAsync(x => x.PersonaId == id);
    }
}