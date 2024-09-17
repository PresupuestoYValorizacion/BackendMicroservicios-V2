using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Users.GetMenusByUser;

public sealed record GetMenusByUserQuery : IQuery<List<SistemaByRolDto>>
{
    public RolId? RolId { get; set; }
    public string? Dependencia { get; set; }
}