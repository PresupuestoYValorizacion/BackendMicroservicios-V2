namespace MsAcceso.Application.Tenant.Users.RegisterUsersTenant;

public sealed record RegisterUsersTenantRequest(
    string Email, 
    string Username, 
    string Password,
    int TipoId,
    int TipoDocumentoId,
    string NumeroDocumento,
    string RazonSocial,
    string NombreCompleto,
    string RolId
);