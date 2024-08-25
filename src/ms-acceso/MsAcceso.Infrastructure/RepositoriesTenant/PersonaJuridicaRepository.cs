
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaJuridicaRepository : RepositoryTenant<PersonaJuridica>, IPersonaJuridicaRepository
{
    public PersonaJuridicaRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async void DeleteById(PersonaId Id)
    {
        var entity = await DbContext.Set<PersonaJuridica>()
        .FirstOrDefaultAsync(x => x.PersonaId == Id);

        if (entity != null)
        {
            DbContext.Remove(entity);
        }
    }

    public async Task<PersonaJuridica?> GetByIdAsync(PersonaId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<PersonaJuridica>()
        .FirstOrDefaultAsync(x => x.PersonaId == id);
    }
}