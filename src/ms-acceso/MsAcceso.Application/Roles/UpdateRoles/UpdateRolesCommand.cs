using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Roles.UpdateRoles;

public sealed record UpdateRolesCommand(
    RolId RolId,
    string Nombre,
    ParametroId TipoRolId,
    LicenciaId? LicenciaId
) : ICommand<Guid>;