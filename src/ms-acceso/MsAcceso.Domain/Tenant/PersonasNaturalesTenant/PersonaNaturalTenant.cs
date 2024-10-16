using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Domain.Tenant.PersonasNaturalesTenant;

public sealed class PersonaNaturalTenant
{
    private PersonaNaturalTenant(){}

    private PersonaNaturalTenant(
        PersonaTenantId personaId,
        string nombreCompleto
    )
    {
        PersonaId = personaId;
        NombreCompleto = nombreCompleto;
    }

    public PersonaTenantId? PersonaId { get; private set; }
    public string? NombreCompleto;

    public static PersonaNaturalTenant Create(
        PersonaTenantId personaId,
        string nombreCompleto
    )
    {
        var personaNatural = new PersonaNaturalTenant(personaId, nombreCompleto);

        return personaNatural;
    }

     public Result Update(
        string nombreCompleto)
    {
        NombreCompleto = nombreCompleto;
        return Result.Success();
    }
}