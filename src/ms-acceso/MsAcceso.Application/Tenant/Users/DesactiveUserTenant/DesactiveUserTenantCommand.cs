using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.DesactiveUserTenant;


public sealed record DesactiveUserTenantCommand(
    UserTenantId Id
) : ICommand<Guid>;