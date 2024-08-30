using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Roles.DesactiveRoles;

public sealed record DesactiveRolesCommand(
    RolId RolId
) : ICommand<Guid>;