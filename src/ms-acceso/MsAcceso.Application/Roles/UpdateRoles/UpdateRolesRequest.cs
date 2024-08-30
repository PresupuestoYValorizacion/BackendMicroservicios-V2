namespace MsAcceso.Application.Roles.UpdateRoles;

public record UpdateRolesRequest(
    string RolId,
    string Nombre,
    int TipoRolId,
    string? LicenciaId
);