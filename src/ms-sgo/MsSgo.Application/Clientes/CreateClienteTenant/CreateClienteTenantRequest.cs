namespace MsAcceso.Application.Tenant.Roles.RegisterRoleTenant;

public record CreateClienteTenantRequest(
    int TipoPersonaId,
    int TipoDocumentoId,
    string NumeroDocumento,
    string Nombre
);