
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.DeleteUserTenant;

public sealed record DeleteUserTenantCommand(
    UserTenantId Id
) : ICommand<Guid>;