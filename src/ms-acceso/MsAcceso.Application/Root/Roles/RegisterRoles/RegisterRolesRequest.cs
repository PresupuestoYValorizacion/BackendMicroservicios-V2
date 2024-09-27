namespace MsAcceso.Application.Root.Roles.RegisterRoles;

public record RegisterRolesRequest(
    string Nombre,
    int TipoRolId,
    string? LicenciaId
);