namespace MsAcceso.Application.Root.Users.UpdatePersonasTenant;

public record UpdatePersonasTenantRequest(
    Guid Id,
    int TipoId, 
    int TipoDocumentoId, 
    string NumeroDocumento, 
    string RazonSocial, 
    string NombreCompleto 
);