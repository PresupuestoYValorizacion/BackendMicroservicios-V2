
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaNaturalRepository : RepositoryTenant<PersonaNatural>, IPersonaNaturalRepository
{
    public PersonaNaturalRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

}