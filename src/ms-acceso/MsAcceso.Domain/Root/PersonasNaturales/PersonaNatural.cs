using MsAcceso.Domain.Abstractions;
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

    public static PersonaNatural Create(
        PersonaId personaId,
        string nombreCompleto
    )
    {
        var personaNatural = new PersonaNatural(personaId, nombreCompleto);

        return personaNatural;
    }

     public Result Update(
        string nombreCompleto)
    {
        NombreCompleto = nombreCompleto;
        return Result.Success();
    }
}