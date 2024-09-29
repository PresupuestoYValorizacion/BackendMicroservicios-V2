using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Root.Roles.RegisterRoles;

public sealed record RegisterRolesCommand(
    string Nombre,
    ParametroId TipoRolId,
    string? LicenciaId
) : ICommand<Guid>;