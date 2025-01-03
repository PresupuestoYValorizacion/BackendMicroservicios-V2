
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaNaturalRepository : RepositoryTenant<PersonaNatural>, IPersonaNaturalRepository
{
    public PersonaNaturalRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async void DeleteById(PersonaId Id)
    {
        var entity = await DbContext.Set<PersonaNatural>()
        .FirstOrDefaultAsync(x => x.PersonaId == Id);

        if (entity != null)
        {
            DbContext.Remove(entity);
        }
    }

    public async Task<PersonaNatural?> GetByIdAsync(PersonaId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PersonaNatural>()
        .FirstOrDefaultAsync(x => x.PersonaId == id);
    }
}