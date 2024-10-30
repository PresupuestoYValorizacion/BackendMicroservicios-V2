using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Tenant.Users.LoginTenant;

namespace MsAcceso.Application.Tenant.Users.SingInByTokenTenant;

public record SingInByTokenTenantCommand(string Email, string Token, string RolId, string TenantId) : ICommand<LoginTenantResponse?>;