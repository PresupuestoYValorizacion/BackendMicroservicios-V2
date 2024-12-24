namespace MsAcceso.Application.Sgo.Clientes.CreateClienteTenant;

public record CreateClienteTenantRequest(
    int TipoPersonaId,
    int TipoDocumentoId,
    int TipoClienteId,
    string NumeroDocumento,
    string Nombre
);