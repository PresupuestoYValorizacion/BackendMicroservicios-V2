using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.DesactiveRoleTenant;

public sealed record DesactiveRoleTenantCommand(
    RolTenantId RolId
) : ICommand<Guid>;