using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.GetMenuOpcionesBySistema;

public sealed record GetMenuOpcionesBySistemaQuery(
    SistemaId SistemaId
)
 : IQuery<List<MenuOpcionDto>>;