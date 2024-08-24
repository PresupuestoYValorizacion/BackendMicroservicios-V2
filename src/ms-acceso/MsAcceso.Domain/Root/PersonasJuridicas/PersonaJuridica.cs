using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Domain.Root.PersonasJuridicas;

public sealed class PersonaJuridica
{
    private PersonaJuridica(){}

    private PersonaJuridica(
        PersonaId personaId,
        string razonSocial
    )
    {
        PersonaId = personaId;
        RazonSocial = razonSocial;
    }

    public PersonaId? PersonaId {get; private set;}
    public string? RazonSocial {get; private set;}

    public static PersonaJuridica Create(
        PersonaId personaId,
        string razonSocial
    )
    {
        var personaJuridica = new PersonaJuridica(personaId, razonSocial);

        return personaJuridica;
    }
}