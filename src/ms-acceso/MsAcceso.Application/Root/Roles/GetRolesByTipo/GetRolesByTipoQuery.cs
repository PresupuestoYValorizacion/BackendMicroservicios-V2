using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Root.Roles.GetRolesByTipo;

public sealed record GetRolesByTipoQuery : IQuery<List<RolDto>>
{
    public ParametroId? TipoRolId { get; set; }
}