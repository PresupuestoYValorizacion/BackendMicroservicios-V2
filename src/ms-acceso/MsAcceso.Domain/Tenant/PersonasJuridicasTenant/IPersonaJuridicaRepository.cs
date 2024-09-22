

using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Domain.Tenant.PersonasJuridicasTenant;

public interface IPersonaJuridicaTenantRepository
{

    void Add(PersonaJuridicaTenant user);

    void Update(PersonaJuridicaTenant user);

    void DeleteById(PersonaTenantId Id);

    Task<PersonaJuridicaTenant?> GetByIdAsync(PersonaTenantId id, CancellationToken cancellationToken = default);

}