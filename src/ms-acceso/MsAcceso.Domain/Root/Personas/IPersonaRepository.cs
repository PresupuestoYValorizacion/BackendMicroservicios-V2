using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Personas;

public interface IPersonaRepository
{

    Task<Persona?> GetByIdAsync(PersonaId id, CancellationToken cancellationToken = default);

    void Add(Persona user);

    void Update(Persona user);

    Task<bool> IsEmpresaExists(
        PersonaId Id, 
        CancellationToken cancellationToken = default
    );

}