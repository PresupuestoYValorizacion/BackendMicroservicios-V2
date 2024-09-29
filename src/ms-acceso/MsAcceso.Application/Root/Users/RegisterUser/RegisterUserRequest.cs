namespace MsAcceso.Application.Root.Users.RegisterUser;

public record RegisterUserRequest(
    string Email, 
    string Username, 
    string Password, 
    int TipoId, 
    int TipoDocumentoId, 
    string NumeroDocumento, 
    string RazonSocial, 
    string NombreCompleto,
    bool IsAdmin,
    int PeriodoLicenciaId,
    string? LicenciaId,
    string? RolId
);