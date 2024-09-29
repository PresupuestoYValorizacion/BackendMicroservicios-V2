using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.UpdateRoleTenant;

public sealed record UpdateRoleTenantCommand(
    RolTenantId RolId,
    string Nombre
) : ICommand<Guid>;