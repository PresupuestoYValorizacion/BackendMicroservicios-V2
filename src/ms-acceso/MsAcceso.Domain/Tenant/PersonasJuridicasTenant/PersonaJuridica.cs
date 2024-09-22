using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Domain.Tenant.PersonasJuridicasTenant;

public sealed class PersonaJuridicaTenant
{
    private PersonaJuridicaTenant(){}

    private PersonaJuridicaTenant(
        PersonaTenantId personaId,
        string razonSocial
    )
    {
        PersonaId = personaId;
        RazonSocial = razonSocial;
    }

    public PersonaTenantId? PersonaId {get; private set;}
    public string? RazonSocial {get; private set;}

    public static PersonaJuridicaTenant Create(
        PersonaTenantId personaId,
        string razonSocial
    )
    {
        var personaJuridica = new PersonaJuridicaTenant(personaId, razonSocial);

        return personaJuridica;
    }

    public Result Update(
        string razonSocial)
    {
        RazonSocial = razonSocial;
        return Result.Success();
    }
}