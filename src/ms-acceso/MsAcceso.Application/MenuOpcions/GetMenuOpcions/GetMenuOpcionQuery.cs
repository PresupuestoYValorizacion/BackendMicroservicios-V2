using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.GetMenuOpcions;

public sealed record GetMenuOpcionQuery : IQuery<List<MenuOpcionDto>?>
{
    public string? SistemaId {get; set;}  
}