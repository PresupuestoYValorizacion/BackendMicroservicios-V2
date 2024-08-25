namespace MsAcceso.Application.Users.RegisterUser;

public record RegisterUserRequest(string Email, string Username, string Password, int TipoId, int TipoDocumentoId, string NumeroDocumento, string RazonSocial, string NombreCompleto);