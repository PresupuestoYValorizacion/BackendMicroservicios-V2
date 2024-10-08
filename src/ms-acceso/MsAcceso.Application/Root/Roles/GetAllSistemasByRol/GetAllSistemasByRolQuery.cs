using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Roles.GetAllSistemasByRol;

public sealed record GetAllSistemasByRolQuery : IQuery<List<SistemaByRolDto>>
{
    public string? RolId { get; set; }

}