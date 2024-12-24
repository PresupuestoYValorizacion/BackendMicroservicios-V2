namespace MsAcceso.Application.Sgo.Clientes.UpdateClienteTenant;

public record UpdateClienteTenantRequest(
    string Id,
    int TipoPersonaId,
    int TipoDocumentoId,
    int TipoClienteId,
    string NumeroDocumento,
    string Nombre
);