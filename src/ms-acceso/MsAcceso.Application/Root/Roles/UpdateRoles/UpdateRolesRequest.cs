namespace MsAcceso.Application.Root.Roles.UpdateRoles;

public record UpdateRolesRequest(
    string RolId,
    string Nombre,
    int TipoRolId,
    string? LicenciaId
);