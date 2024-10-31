namespace MsAcceso.Application.Tenant.Users.UpdateUsersTenant;

public sealed record UpdateUsersTenantRequest(
    Guid Id,
    string Email, 
    string Username,
    string RolId
);