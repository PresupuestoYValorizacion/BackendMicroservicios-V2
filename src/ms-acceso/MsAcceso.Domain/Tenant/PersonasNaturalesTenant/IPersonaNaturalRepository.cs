

using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Domain.Tenant.PersonasNaturalesTenant;

public interface IPersonaNaturalRepository
{


    void Add(PersonaNaturalTenant user);

    void Update(PersonaNaturalTenant user);

    void DeleteById(PersonaTenantId Id);

    Task<PersonaNaturalTenant?> GetByIdAsync(PersonaTenantId id, CancellationToken cancellationToken = default);


}