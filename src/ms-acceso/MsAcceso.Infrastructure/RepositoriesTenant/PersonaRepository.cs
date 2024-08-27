
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaRepository : RepositoryTenant<Persona,PersonaId>, IPersonaRepository
{
    public PersonaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsEmpresaExists(PersonaId Id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Persona>()
                    .AnyAsync(x => x.Id == Id,cancellationToken);
    }

    public async Task<bool> NumeroDocumentoExists(string NumeroDocumento, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Persona>()
                    .AnyAsync(x => x.NumeroDocumento == NumeroDocumento && x.Activo == new Activo(true) ,cancellationToken);
    }
}