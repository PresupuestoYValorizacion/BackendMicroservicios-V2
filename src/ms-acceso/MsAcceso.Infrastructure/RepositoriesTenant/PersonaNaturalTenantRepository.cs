
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaNaturalTenantRepository : RepositoryTenant<PersonaNaturalTenant>, IPersonaNaturalTenantRepository
{
    public PersonaNaturalTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async void DeleteById(PersonaTenantId Id)
    {
        var entity = await DbContext.Set<PersonaNaturalTenant>()
        .FirstOrDefaultAsync(x => x.PersonaId == Id);

        if (entity != null)
        {
            DbContext.Remove(entity);
        }
    }


    public async Task<PersonaNaturalTenant?> GetByIdAsync(PersonaTenantId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PersonaNaturalTenant>()
        .FirstOrDefaultAsync(x => x.PersonaId == id);
    }
}