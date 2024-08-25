

using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Root.PersonasNaturales;

public interface IPersonaNaturalRepository
{


    void Add(PersonaNatural user);

    void Update(PersonaNatural user);

    void DeleteById(PersonaId Id);

    Task<PersonaNatural?> GetByIdAsync(PersonaId id, CancellationToken cancellationToken = default);


}