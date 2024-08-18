
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Infrastructure.RepositoriesTenant;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.Repositories;

internal sealed class PersonaRepository : RepositoryTenant<Persona,PersonaId>, IPersonaRepository
{
    public PersonaRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsEmpresaExists(PersonaId Id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Persona>()
                    .AnyAsync(x => x.Id == Id,cancellationToken);
    }
}