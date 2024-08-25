using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Personas;

public interface IPersonaRepository
{

    Task<Persona?> GetByIdAsync(PersonaId id, CancellationToken cancellationToken = default);

    void Add(Persona persona);

    void Update(Persona persona);
    void Delete(Persona persona);

    Task<bool> IsEmpresaExists(
        PersonaId Id, 
        CancellationToken cancellationToken = default
    );

    Task<bool> NumeroDocumentoExists(
        string NumeroDocumento, 
        CancellationToken cancellationToken = default
    );

}