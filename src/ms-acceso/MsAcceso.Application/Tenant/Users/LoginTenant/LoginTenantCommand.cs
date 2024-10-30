using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Tenant.Users.LoginTenant;

public record LoginTenantCommand(string Email, string Password, bool IsForcedSession, string UserTenantRolId, string UserTenantId) : ICommand<LoginTenantResponse?>;