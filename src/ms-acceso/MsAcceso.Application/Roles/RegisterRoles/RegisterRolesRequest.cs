namespace MsAcceso.Application.Roles.RegisterRoles;

public record RegisterRolesRequest(
    string Nombre,
    int TipoRolId,
    string? LicenciaId
);