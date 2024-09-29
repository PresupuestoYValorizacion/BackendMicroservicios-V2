namespace MsAcceso.Application.Root.Users.UpdatePersona;

public record UpdatePersonaRequest(
    Guid Id,
    int TipoId, 
    int TipoDocumentoId, 
    string NumeroDocumento, 
    string RazonSocial, 
    string NombreCompleto 
);