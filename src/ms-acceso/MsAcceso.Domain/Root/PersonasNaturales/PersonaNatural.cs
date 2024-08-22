using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Root.PersonasNaturales;

public sealed class PersonaNatural
{
    private PersonaNatural(){}

    private PersonaNatural(
        PersonaId personaId,
        string nombreCompleto
    )
    {
        PersonaId = personaId;
        NombreCompleto = nombreCompleto;
    }

    public PersonaId? PersonaId { get; private set; }
    public string? NombreCompleto;
}