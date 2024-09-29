using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.DeleteRoleTenant;


public sealed record DeleteRoleTenantCommand(
    RolTenantId RolId
) : ICommand<Guid>;