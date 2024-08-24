using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.DesactiveMenuOpcions;

public sealed record DesactiveMenuOpcionCommand(
    MenuOpcionId MenuOpcionId   
): ICommand<Guid>;