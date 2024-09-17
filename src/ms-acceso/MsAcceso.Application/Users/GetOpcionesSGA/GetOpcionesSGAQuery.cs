using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Users.GetOpcionesSGA;

public sealed record GetOpcionesSGAQuery : IQuery<List<MenuOpcionDto>>
{
    public RolId? RolId { get; set; }
    public string? Url { get; set; }
}