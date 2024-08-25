namespace MsAcceso.Application.Users.UpdatePersona;

public record UpdatePersonaRequest(Guid Id, int TipoId, int TipoDocumentoId, string NumeroDocumento, string RazonSocial, string NombreCompleto);