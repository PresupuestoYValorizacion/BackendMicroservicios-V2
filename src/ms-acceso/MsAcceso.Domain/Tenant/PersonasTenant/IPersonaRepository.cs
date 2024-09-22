using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Tenant.PersonasTenant;

public interface IPersonaRepository
{

    Task<PersonaTenant?> GetByIdAsync(PersonaTenantId id, CancellationToken cancellationToken = default);

    void Add(PersonaTenant persona);

    void Update(PersonaTenant persona);
    void Delete(PersonaTenant persona);

    Task<bool> IsEmpresaExists(
        PersonaTenantId Id, 
        CancellationToken cancellationToken = default
    );

    Task<bool> NumeroDocumentoExists(
        string NumeroDocumento, 
        CancellationToken cancellationToken = default
    );

}