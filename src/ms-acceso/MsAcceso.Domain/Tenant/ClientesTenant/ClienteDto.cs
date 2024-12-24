namespace MsAcceso.Domain.Tenant.ClientesTenant;

public class ClienteDto
{
    public string? Id {get; set;}
    public int TipoPersonaId {get; set;}
    public string? TipoPersona {get; set;}
    public int TipoDocumentoId {get; set;}
    public string? TipoDocumento {get; set;}
    public int TipoClienteId {get; set;}
    public string? TipoCliente {get; set;}
    public string? NumeroDocumento {get; set;}
    public string? Nombre {get; set;}
}