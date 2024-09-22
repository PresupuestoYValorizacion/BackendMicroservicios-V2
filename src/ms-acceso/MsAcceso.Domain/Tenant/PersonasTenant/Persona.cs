using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Domain.Root.Personas;

public sealed class PersonaTenant : Entity<PersonaTenantId>
{
    private PersonaTenant() { }

    private PersonaTenant(
        PersonaTenantId id,
        string tipo,
        string tipoDocumento,
        string numeroDocumento
        ) : base(id)
    {
        TipoId = tipo;
        TipoDocumentoId = tipoDocumento;
        NumeroDocumento = numeroDocumento;
    }

    public string? TipoId { get; private set; }

    public string? TipoDocumentoId { get; private set; }

    // public Parametro? Tipo { get; private set; }

    // public Parametro? TipoDocumento { get; private set; }
    public string? NumeroDocumento { get; private set; }
    public PersonaNaturalTenant? PersonaNatural { get; private set; }
    public PersonaJuridicaTenant? PersonaJuridica { get; private set; }


    public static PersonaTenant Create(
        PersonaTenantId personaId,
        string tipoId,
        string tipoDocumentoId,
        string numeroDocumento
    )
    {
        var persona = new PersonaTenant(personaId, tipoId, tipoDocumentoId, numeroDocumento);

        return persona;
    }

    public Result Update(
        string tipoId,
        string tipoDocumentoId,
        string numeroDocumento)
    {
        TipoId = tipoId;
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumento = numeroDocumento;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}