using System.ComponentModel.DataAnnotations.Schema;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;

namespace MsAcceso.Domain.Tenant.ClientesTenant;

public sealed class ClienteTenant : Entity<ClienteTenantId>
{
    private ClienteTenant() { }

    private ClienteTenant(
        ClienteTenantId id,
        int tipoPersonaId,
        int tipoDocumentoId,
        string numeroDocumento,
        string nombre
    ) : base(id)
    {
        TipoPersonaId = tipoPersonaId;
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumento = numeroDocumento;
        Nombre = nombre;
    }

    public int? TipoPersonaId { get; private set; }

    public int? TipoDocumentoId { get; private set; }

    [NotMapped]
    public Parametro? TipoPersona { get; set; }
    
    [NotMapped]
    public Parametro? TipoDocumento { get; set; }
    public string? NumeroDocumento { get; private set; }
    public string? Nombre { get; private set; }

    public static ClienteTenant Create(
        ClienteTenantId clienteTenantId,
        int tipoPersonaId,
        int tipoDocumentoId,
        string numeroDocumento,
        string nombre
    )
    {
        var clienteTenant = new ClienteTenant(clienteTenantId, tipoPersonaId, tipoDocumentoId, numeroDocumento, nombre);

        return clienteTenant;
    }

    public Result Update(
        int tipoPersonaId,
        int tipoDocumentoId,
        string numeroDocumento,
        string nombre)
    {
        TipoPersonaId = tipoPersonaId;
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumento = numeroDocumento;
        Nombre = nombre;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}