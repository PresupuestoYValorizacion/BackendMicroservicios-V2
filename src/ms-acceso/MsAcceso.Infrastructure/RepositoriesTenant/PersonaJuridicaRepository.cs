
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class PersonaJuridicaRepository : RepositoryTenant<PersonaJuridica>, IPersonaJuridicaRepository
{
    public PersonaJuridicaRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

}