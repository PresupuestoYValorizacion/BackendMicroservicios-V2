using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.GetMenuOpcionById;

public sealed record GetMenuOpcionByIdQuery : IQuery<MenuOpcionDto?>
{
    public string? Id {get; set;}
}