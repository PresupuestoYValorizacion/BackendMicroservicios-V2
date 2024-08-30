using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Roles.DeleteRoles;

public sealed record DeleteRolesCommand(
    RolId RolId
) : ICommand<Guid>;