using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Roles.AddPermisos;

public sealed record AddPermisosCommand(
    RolId RolId,
    List<SistemaRequest> SistemasRequest

) : ICommand<Guid>;