
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasNaturales;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class PersonaNaturalRepository : RepositoryApplication<PersonaNatural>, IPersonaNaturalRepository
{
    public PersonaNaturalRepository(ApplicationDbContext dbContext) : base(dbContext)
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