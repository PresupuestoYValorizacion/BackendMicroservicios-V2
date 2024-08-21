using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Root.PersonasNaturales;

public sealed class PersonaNatural
{
    private PersonaNatural(){}

    private PersonaNatural(
        PersonaId personaId,
        string apellidoPaterno,
        string apellidoMaterno,
        string nombres
    )
    {
        PersonaId = personaId;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Nombres = nombres;
    }

    public PersonaId? PersonaId { get; private set; }
    public string? ApellidoPaterno { get; private set; }
    public string? ApellidoMaterno { get; private set; }
    public string? Nombres { get; private set; }
}