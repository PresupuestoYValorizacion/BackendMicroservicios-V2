using MsAcceso.Application.Abstractions.Messaging;


namespace MsAcceso.Application.Tenant.Roles.RegisterRoleTenant;

public sealed record RegisterRoleTenantCommand(
    string Nombre
) : ICommand<Guid>;