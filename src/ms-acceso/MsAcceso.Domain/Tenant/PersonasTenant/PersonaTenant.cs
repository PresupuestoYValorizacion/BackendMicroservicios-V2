using System.ComponentModel.DataAnnotations.Schema;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;

namespace MsAcceso.Domain.Tenant.PersonasTenant;

public sealed class PersonaTenant : Entity<PersonaTenantId>
{
    private PersonaTenant() { }

    private PersonaTenant(
        PersonaTenantId id,
        int tipo,
        int tipoDocumento,
        string numeroDocumento
        ) : base(id)
    {
        TipoId = tipo;
        TipoDocumentoId = tipoDocumento;
        NumeroDocumento = numeroDocumento;
    }

    public int? TipoId { get; private set; }

    public int? TipoDocumentoId { get; private set; }

    [NotMapped]
    public Parametro? Tipo { get; set; }
    
    [NotMapped]
    public Parametro? TipoDocumento { get; set; }
    public string? NumeroDocumento { get; private set; }
    public PersonaNaturalTenant? PersonaNatural { get; private set; }
    public PersonaJuridicaTenant? PersonaJuridica { get; private set; }


    public static PersonaTenant Create(
        PersonaTenantId personaId,
        int tipoId,
        int tipoDocumentoId,
        string numeroDocumento
    )
    {
        var persona = new PersonaTenant(personaId, tipoId, tipoDocumentoId, numeroDocumento);

        return persona;
    }

    public Result Update(
        int tipoId,
        int tipoDocumentoId,
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