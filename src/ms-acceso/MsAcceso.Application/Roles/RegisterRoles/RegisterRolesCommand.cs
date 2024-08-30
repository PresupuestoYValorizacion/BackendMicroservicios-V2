using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Roles.RegisterRoles;

public sealed record RegisterRolesCommand(
    string Nombre,
    ParametroId TipoRolId,
    LicenciaId? LicenciaId
) : ICommand<Guid>;