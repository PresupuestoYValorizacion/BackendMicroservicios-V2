using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Root.Users.GetOpcionesSGO;

public sealed record GetOpcionesSGOQuery : IQuery<List<MenuOpcionDto>>
{
    public RolId? RolId { get; set; }
    public string? Url { get; set; }
}