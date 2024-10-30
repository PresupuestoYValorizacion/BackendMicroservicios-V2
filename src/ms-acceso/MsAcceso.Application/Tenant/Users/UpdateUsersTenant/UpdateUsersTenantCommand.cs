using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.UpdateUsersTenant;

public sealed record UpdateUsersTenantCommand(
    UserTenantId Id, 
    string? Email, 
    string? Username,
    RolTenantId RolId
) : ICommand<Guid>;